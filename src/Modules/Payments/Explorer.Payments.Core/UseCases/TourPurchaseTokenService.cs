using AutoMapper;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain.PurchaseTokens;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.UseCases
{
    public class TourPurchaseTokenService :BaseService<TourPurchaseTokenDto,TourPurchaseToken>, ITourPurchaseTokenService
    {
        private readonly ITourPurchaseTokenRepository tourPurchaseTokenRepository;

        public TourPurchaseTokenService(IMapper mapper,ITourPurchaseTokenRepository tourPurchaseTokenRepository) : base(mapper)
        {
            this.tourPurchaseTokenRepository = tourPurchaseTokenRepository; 
        }
        public Result<TourPurchaseTokenDto> Create(TourPurchaseTokenDto tourPurchaseToken)
        {
            throw new NotImplementedException();
        }

        public Result<PagedResult<TourPurchaseTokenDto>> GetAllByTouristId(int touristId)
        {
            throw new NotImplementedException();
        }

        public Result<TourPurchaseTokenDto> GetByTouristAndTourId(int touristId, int tourId)
        {
            throw new NotImplementedException();
        }
    }
}
