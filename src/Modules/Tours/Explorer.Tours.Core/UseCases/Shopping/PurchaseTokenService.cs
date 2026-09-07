using System;
using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.Shopping;
using Explorer.Tours.API.Public.Author;
using Explorer.Tours.API.Public.Shopping;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Shopping;
using FluentResults;

namespace Explorer.Tours.Core.UseCases.Shopping
{
    public class PurchaseTokenService : BaseService<PurchaseTokenDto, PurchaseToken>, IPurchaseTokensService
    {
        private readonly IPurchaseTokenRepository _purchaseTokenRepository;
        private readonly IMapper _mapper;

        public PurchaseTokenService(IPurchaseTokenRepository purchaseTokenRepository, IMapper mapper) : base(mapper)
        {
            _purchaseTokenRepository = purchaseTokenRepository;
            _mapper = mapper;
        }

        public Result<PagedResult<PurchaseTokenDto>> GetPaged(int page, int pageSize)
        {
            if (page < 0 || pageSize <= 0)
            {
                throw new ArgumentException("Page must be non-negative, and pageSize must be greater than zero.");
            }
            var result = _purchaseTokenRepository.GetPaged(page, pageSize);
            return MapToDto(result);
        }

        public Result<PurchaseTokenDto> Get(int id)
        {
            try
            {
                var result = _purchaseTokenRepository.Get(id);
                return MapToDto(result);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<PurchaseTokenDto> Create(PurchaseTokenDto entity)
        {
            try
            {
                var result = _purchaseTokenRepository.Create(MapToDomain(entity));
                return MapToDto(result);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result<PurchaseTokenDto> Update(PurchaseTokenDto entity)
        {
            try
            {
                var result = _purchaseTokenRepository.Update(MapToDomain(entity));
                return MapToDto(result);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result Delete(int id)
        {
            try
            {
                _purchaseTokenRepository.Delete(id);
                return Result.Ok();
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<PagedResult<PurchaseTokenDto>> GetTokensByUserId(int userId)
        {
            var result = _purchaseTokenRepository.GetByUserId(userId);
            return MapToDto(result);
        }

    }
}