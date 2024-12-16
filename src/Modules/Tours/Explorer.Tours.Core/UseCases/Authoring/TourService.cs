using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Internal;
using Explorer.Stakeholders.API.Internal;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Internal;
using Explorer.Tours.API.Public.Author;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;

namespace Explorer.Tours.Core.UseCases.Authoring
{
	public class TourService : BaseService<TourDto, Tour>, ITourService, IInternalTourService
    {
        private readonly ITourRepository tourRepository;
        private readonly IMapper mapper;
        private readonly IInternalPurchaseTokenService _purchaseTokenService;
        private readonly IInternalTourBundleService _tourBundleService;
        private readonly IInternalUserService _userService;
        public TourService(ITourRepository tourRepository, IMapper mapper, IInternalPurchaseTokenService purchaseTokenService, IInternalTourBundleService tourBundleService, IInternalUserService userService) : base(mapper) 
        { 
            this.tourRepository=tourRepository;
            this.mapper=mapper;
            _purchaseTokenService = purchaseTokenService;
            _tourBundleService = tourBundleService;
            _userService = userService;
        }

		public TourService(
			ITourRepository tourRepository,
			IMapper mapper,
			IInternalPurchaseTokenService purchaseTokenService,
			IInternalTourBundleService tourBundleService) : base(mapper)
		{
			this.tourRepository = tourRepository;
			this.mapper = mapper;
			_purchaseTokenService = purchaseTokenService;
			_tourBundleService = tourBundleService;
		}

		public Result<PagedResult<TourDto>> GetPaged(int page, int pageSize)
        {
            var result = tourRepository.GetPaged(page, pageSize);
            return MapToDto(result);
        }

        public string GetDatabaseSummary()
        {
            const int pageSize = 100; // Process tours in batches of 100
            int currentPage = 1;
            var allTours = new List<string>();

            while (true)
            {
                var pagedResult = tourRepository.GetPaged(currentPage, pageSize);

                if (pagedResult.Results == null || !pagedResult.Results.Any())
                    break;

                allTours.AddRange(pagedResult.Results.Select(tour => tour.ToString()));

                // If fewer results were returned than pageSize, it means we reached the last page
                if (pagedResult.Results.Count < pageSize)
                    break;

                currentPage++;
            }

            return string.Join(Environment.NewLine, allTours);
        }


        public Result<TourDto> Get(int id)
        {
            try
            {
                var tour = tourRepository.Get(id);
                var tourDto = MapToDto(tour);
                tourDto.AuthorName = _userService.GetUsername(tourDto.AuthorId).Value;
                GetReviewsUsernames(tourDto.Reviews);
                return tourDto;
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        private void GetReviewsUsernames(List<TourReviewDto> reviews)
        {
            if (reviews == null || !reviews.Any())
                return;

            var touristIds = reviews.Select(r => (long)r.TouristId).Distinct().ToList();
            var usernames = _userService.GetUsernamesByIds(touristIds).Value;

            foreach (var review in reviews)
            {
                if (usernames.ContainsKey(review.TouristId))
                {
                    review.Username = usernames[review.TouristId];
                }
            }
        }

        public Result<TourDto> Create(TourDto entity)
        {
            try
            {
                var result = tourRepository.Create(MapToDomain(entity));
                return MapToDto(result);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result<TourDto> Update(TourDto entity)
        {
            try
            {
                var result = tourRepository.Update(MapToDomain(entity));
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
                tourRepository.Delete(id);
                return Result.Ok();
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<TourDto> AddCheckpoint(int id, CheckpointDto checkpoint)
        {
            try
            {
                Tour tour = tourRepository.Get(id);
                tour.AddCheckpoint(mapper.Map<Checkpoint>(checkpoint));
                var result = tourRepository.Update(tour);
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

        public Result<TourDto> UpdateCheckpoint(int id, CheckpointDto checkpoint)
        {
            try
            {
                Tour tour = tourRepository.Get(id);
                tour.UpdateCheckpoint(mapper.Map<Checkpoint>(checkpoint));
                var result = tourRepository.Update(tour);
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

        public Result DeleteCheckpoint(int id, CheckpointDto checkpoint)
        {
            try
            {
                Tour tour = tourRepository.Get(id);
                tour.DeleteCheckpoint(mapper.Map<Checkpoint>(checkpoint));
                var result = tourRepository.Update(tour);
                return Result.Ok();
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }

        }

        public Result<TourDto> AddTransportDurations(int id, List<TransportDurationDto> transportDurations)
        {
            try
            {
                Tour tour = tourRepository.Get(id);
                tour.AddTransportDuratios(transportDurations.Select(dto => mapper.Map<TransportDuration>(dto)).ToList());
                var result = tourRepository.Update(tour);
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

        public Result<TourDto> Archive(int id)
        {
            try
            {
                Tour tour = tourRepository.Get(id);
                tour.Archive();
                var result = tourRepository.Update(tour);
                return MapToDto(result);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        

        public Result<TourDto> Publish(int id)
        {
            try
            {
                var tour = tourRepository.Get(id);

                if (tour.CheckPublishConditions())
                {
                    tour.ChangeStatusToPublish();
                    var updatedTour = tourRepository.Update(tour);
                    return MapToDto(updatedTour);
                }

                return Result.Fail(FailureCode.InvalidArgument);
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

        public Result<TourDto> UpdateLength(int id, double length)
        {
            try
            {
                Tour tour = tourRepository.Get(id);
                tour.UpdateLength(length);
                var result = tourRepository.Update(tour);
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

        public Result<IEnumerable<TourDto>> GetPurchasedAndArchivedByUser(int userId) 
        {
            var tokens = _purchaseTokenService.GetTokensByTouristId(userId).Value.Results;
            var purchased = new List<TourDto>();

            foreach (var token in tokens)
            {
                var tour = Get(token.TourId).Value;
                if (tour.Status == API.Dtos.TourStatus.Published || tour.Status == API.Dtos.TourStatus.Archived)
                    purchased.Add(tour);
            }

            return purchased;
        }
        public Result<List<long>> GetTourIdsByAuthorId(int authorId)
        {
            try
            {
                var result = tourRepository.GetByAuthorId(authorId);
                return result.Results.Select(t => t.Id).ToList();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public Result<List<TourDto>> SearchTours(double lat, double lon, double distance, int page, int pageSize)
        {
            try
            {
                var publishedTours = tourRepository.GetPaged(page, pageSize).Results.Where(t => t.Status == Domain.Tours.TourStatus.Published).ToList();
                var matchingTours = new List<TourDto>();
                foreach (var tour in publishedTours)
                {
                    foreach (var checkpoint in tour.Checkpoints)
                    {
                        double distanceToCheckpoint = CalculateDistance(lat, lon, checkpoint.Latitude, checkpoint.Longitude);
                        if (distanceToCheckpoint <= distance)
                            matchingTours.Add(MapToDto(tour));
                        break;
                    }
                }
                return Result.Ok(matchingTours);
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

        private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            const double R = 6371; // Radius of the Earth in kilometers
            double dLat = DegreesToRadians(lat2 - lat1);
            double dLon = DegreesToRadians(lon2 - lon1);
            double a =
                Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(DegreesToRadians(lat1)) * Math.Cos(DegreesToRadians(lat2)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return R * c; // Distance in kilometers
        }

        private double DegreesToRadians(double degrees)
        {
            return degrees * (Math.PI / 180);
        }

        public Result<TourDto> CheckIfPurchased(int userId, int tourId)
        {
            try
            {   
                var tourTokens = _purchaseTokenService.GetTokensByTouristId(userId).Value.Results;
                if(tourTokens.Count == 0)
                {
                    return null;
                }

                var tourToken = tourTokens.Find(tt => tt.TourId == tourId);
                if(tourToken != null)
                {
                    var tour = Get(tourToken.TourId).Value;
                    if (tour.Status == API.Dtos.TourStatus.Published)
                        return tour;
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Result<PagedResult<TourDto>> GetToursByBundleId(int id)
        {
            try
            {
                var result = _tourBundleService.GetToursById(id);

                if (!result.IsSuccess || result.Value == null || !result.Value.Any())
                {
                    return Result.Fail(FailureCode.NotFound).WithError("No tours found for the given bundle ID.");
                }

                var tours = result.Value
                                  .Select(tourId => tourRepository.Get(tourId))
                                  .ToList();

                var pagedResult = new PagedResult<Tour>(tours, tours.Count);
                return MapToDto(pagedResult);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<List<TourDto>> GetSuggestedTours(double longitude, double latitude)
        {
            HashSet<long> nearTours = new HashSet<long>();
            List<TourDto> tours = new List<TourDto>();
            var publishedTours = tourRepository.GetPaged(0, 0).Results.Where(t => t.Status == Domain.Tours.TourStatus.Published).ToList();
            foreach(var tour in publishedTours)
            {
                var checkpoints = tour.Checkpoints;
                foreach(var checkpoint in checkpoints)
                {
                    bool isNearLatitude = Math.Abs((double)(checkpoint.Latitude - latitude)) <= 500.0 / 111320.0;
                    bool isNearLongitude = Math.Abs((double)(checkpoint.Longitude - longitude)) <= 500.0 / (111320.0 * Math.Cos(latitude));
                    if (isNearLatitude && isNearLongitude)
                    {
                        nearTours.Add(tour.Id);
                    }
                }
            }
            foreach(var tour in nearTours)
            {
                tours.Add(MapToDto(tourRepository.Get(tour)));
            }

            return tours;
        }

		public Result<string> GetImageUrlByProductId(int productType, int productId)
		{
			throw new NotImplementedException();
		}

        //public Result<string> GetDatabaseSummary()
        //{
        //    var tours = tourRepository.Get();
        //}
	}
}
