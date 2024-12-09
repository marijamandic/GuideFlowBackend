using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class AccountService: IAccountService
    {
        private readonly ICrudRepository<Person> _personRepository;
        private readonly IUserRepository _userRepository;

        public AccountService(IUserRepository userRepository, ICrudRepository<Person> personRepository)
        {
            _userRepository = userRepository;
            _personRepository = personRepository;
        }

        public Result<AccountOverviewDto> ToggleAccountActivity(AccountOverviewDto account) 
        {
            try
            {
                var user = _userRepository.GetById(account.UserId);
                account.IsActive = account.IsActive ? false : true;
                user.IsActive = account.IsActive;
                _userRepository.Update(user);
                return account;
            }
            catch(KeyNotFoundException e) 
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }



        public Result<AccountOverviewDto> GetAccount(long id)
        {
            try
            {
                var user = _userRepository.GetById(id);
                var personId = _userRepository.GetPersonId(user.Id);
                var person = _personRepository.Get(personId);
                var account = new AccountOverviewDto
                {
                    UserId = user.Id,
                    Username = user.Username,
                    Password =  user.Password,
                    Email = person.Email,
                    Role = (Explorer.Stakeholders.API.Dtos.UserRole)user.Role,
                    IsActive = user.IsActive,
                    LastLogin = user.LastLogin,
                    LastLogout = user.LastLogout
                    
                };
                return account;
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }

        }

        public Result<List<AccountOverviewDto>> GetAllAccounts()
        {
           
            var accountDtos = new List<AccountOverviewDto>();
            var users = _userRepository.GetAll();
            Person person;

            foreach (var user in users)
            {
                try 
                {  var personId = _userRepository.GetPersonId(user.Id);
                   person = _personRepository.Get(personId);
                }
                catch(KeyNotFoundException ) { continue; }
                var account = new AccountOverviewDto
                {
                    UserId = user.Id,
                    Username = user.Username,
                    Password = user.Password,
                    Email = person.Email,
                    Role = (Explorer.Stakeholders.API.Dtos.UserRole)user.Role,
                    IsActive = user.IsActive,
                    LastLogout = user.LastLogout,
                    LastLogin = user.LastLogin
                };

                accountDtos.Add(account);
            }


            return Result.Ok(accountDtos);
        }

    }
}
