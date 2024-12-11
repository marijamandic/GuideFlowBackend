using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Internal;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases;

public class AuthenticationService : IAuthenticationService
{
    private readonly ITokenGenerator _tokenGenerator;
    private readonly IUserRepository _userRepository;
    private readonly ICrudRepository<Person> _personRepository;
    private readonly IProfileInfoRepository _profileInfoRepository;
    private readonly IInternalShoppingCartService _shoppingCartService;
    private readonly IMapper _mapper;

    public AuthenticationService(IUserRepository userRepository, ICrudRepository<Person> personRepository, ITokenGenerator tokenGenerator, IInternalShoppingCartService shoppingCartService, IProfileInfoRepository profileInfoRepository, IMapper mapper)
    {
        _tokenGenerator = tokenGenerator;
        _userRepository = userRepository;
        _personRepository = personRepository;
        _shoppingCartService = shoppingCartService;
        _profileInfoRepository = profileInfoRepository;
        _mapper = mapper;
    }

    public Result<AuthenticationTokensDto> Login(CredentialsDto credentials)
    {
        var user = _userRepository.GetActiveByName(credentials.Username);
        if (user == null || credentials.Password != user.Password) return Result.Fail(FailureCode.NotFound);

        long personId;
        try
        {
            personId = _userRepository.GetPersonId(user.Id);
        }
        catch (KeyNotFoundException)
        {
            personId = 0;
        }
        user.SetLastLoginTime();
        _userRepository.Update(user);
        return _tokenGenerator.GenerateAccessToken(user, personId);
    }

    public Result<AuthenticationTokensDto> RegisterTourist(UserDto account)
    {
        if(_userRepository.Exists(account.Username)) return Result.Fail(FailureCode.NonUniqueUsername);

        try
        {
            var user = _userRepository.Create(_mapper.Map<User>(account));
            user.SetLastLoginTime();
            _userRepository.Update(user);
            var person = _personRepository.Create(new Person(user.Id, account.Name, account.Surname, account.Email));
            var profileInfo = _profileInfoRepository.Create(new ProfileInfo(user.Id, account.Name, account.Surname, "images/profileInfo/1c0f2ce0-b565-49d1-8455-02efdaae83a2.png", "bio", "moto"));
            if(account.Role == API.Dtos.UserRole.Tourist)
                _shoppingCartService.Create((int)user.Id);

            return _tokenGenerator.GenerateAccessToken(user, person.Id);
        }
        catch (ArgumentException e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            // There is a subtle issue here. Can you find it?
        }
    }

    public Result HandleLogout(long id)
    {
        try
        {
            var user = _userRepository.GetById(id);
            user.SetLastLogoutTime();
            _userRepository.Update(user);
            return Result.Ok();
        }
        catch (KeyNotFoundException e) { return Result.Fail(FailureCode.NotFound).WithError(e.Message); }

    }
}