using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Internal;
using Explorer.Stakeholders.API.Internal;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Internal;
using Explorer.Tours.API.Public.Author;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using Explorer.Tours.Core.Domain.WeatherForecast;
using Explorer.Tours.Core.UseCases.Weather;
using FluentResults;
using System.Collections;
using TourStatus = Explorer.Tours.Core.Domain.Tours.TourStatus;

namespace Explorer.Tours.Core.UseCases.Authoring
{
	public class TourService : BaseService<TourDto, Tour>, ITourService, IInternalTourService
    {
        private readonly ITourRepository tourRepository;
        private readonly IMapper mapper;
        private readonly IInternalPurchaseTokenService _purchaseTokenService;
        private readonly IInternalTourBundleService _tourBundleService;
        private readonly IInternalUserService _userService;
        private readonly IWeatherConnection _weatherConnection;
        private readonly IInternalAuthorService _authorService;
        public TourService(ITourRepository tourRepository, IMapper mapper, IInternalPurchaseTokenService purchaseTokenService, IInternalTourBundleService tourBundleService, IInternalUserService userService, IWeatherConnection weatherConnection, IInternalAuthorService authorService) : base(mapper) 
        { 
            this.tourRepository=tourRepository;
            this.mapper=mapper;
            _purchaseTokenService = purchaseTokenService;
            _tourBundleService = tourBundleService;
            _userService = userService;
            _weatherConnection = weatherConnection;
            _authorService = authorService;
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

                var results = pagedResult.Results.Where(tour => tour.Status == TourStatus.Published);
                allTours.AddRange(results.Select(tour => tour.ToString()));

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

        public async Task<Result<IEnumerable<TourDto>>> GetPurchasedAndArchivedByUser(int userId, DateTime? date = null)
        {
            DateTime effectiveDate = date ?? DateTime.MinValue;

            var tokenResult = _purchaseTokenService.GetTokensByTouristId(userId);
            if (!tokenResult.IsSuccess)
            {
                return Result.Fail(tokenResult.Errors);
            }

            var tokens = tokenResult.Value.Results;
            var purchased = new List<TourDto>();
            foreach (var token in tokens)
            {
                var tourResult = Get(token.TourId);
                if (tourResult.IsSuccess)
                {
                    var tour = tourResult.Value;
                    if (tour.Status == API.Dtos.TourStatus.Published || tour.Status == API.Dtos.TourStatus.Archived) {
                        //LOGIKA CE VEROVATNO BITI IZDVOJENA U DOMENSKU KLASU KAD SE PROSIRI Tour.cs (Radi se o poslovnoj logici)
                        if (effectiveDate.Equals(DateTime.MinValue))
                        {
                            await MapWeatherConditionsToTour(tour);
                        }
                        else
                        {
                            await MapWeatherConditionsForDateToTour(tour,effectiveDate);
                        }
                        purchased.Add(tour);
                    }
                }
            }
            return purchased;
        }

        public async Task<Result<IEnumerable<TourDto>>> RecommendToursForDate(int touristId, DateTime date)
        {
            var tokenResult = _purchaseTokenService.GetTokensByTouristId(touristId);
            if (!tokenResult.IsSuccess)
            {
                return Result.Fail(tokenResult.Errors);
            }

            var purchasedToursRecommendationCount = new Dictionary<TourDto, int>();

            var tourTasks = tokenResult.Value.Results
                .Select(async token =>
                {
                    var tourResult = Get(token.TourId);
                    if (tourResult.IsSuccess)
                    {
                        var tour = tourResult.Value;
                        if (tour.Status == API.Dtos.TourStatus.Published || tour.Status == API.Dtos.TourStatus.Archived)
                        {
                            var recommendationCount = await MapWeatherConditionsForDateToTour(tour, date);
                            purchasedToursRecommendationCount[tour] = recommendationCount;
                        }
                    }
                });

            await Task.WhenAll(tourTasks);

            if (purchasedToursRecommendationCount.Any())
            {
                var maxRecommendationCount = purchasedToursRecommendationCount.Values.Max();
                var recommendedTours = purchasedToursRecommendationCount
                    .Where(x => x.Value == maxRecommendationCount)
                    .Select(x => x.Key)
                    .ToList();

                return Result.Ok(recommendedTours.AsEnumerable());
            }

            return Result.Ok(Enumerable.Empty<TourDto>());
        }


        public Result<List<long>> GetTourIdsByAuthorId(int authorId)
        {
            Console.WriteLine($"Author ID: {authorId}");
            try
            {
                // Dohvatamo listu ID-jeva direktno iz repozitorijuma
                var tourIds = tourRepository.GetListByAuthorId(authorId);

                // Proveravamo da li lista nije prazna
                if (tourIds == null || !tourIds.Any())
                {
                    return Result.Fail<List<long>>("No tours found for the given author.");
                }

                // Vraćamo uspešan rezultat sa listom ID-jeva
                return Result.Ok(tourIds);
            }
            catch (Exception ex)
            {
                // Vraćamo neuspešan rezultat u slučaju greške
                return Result.Fail<List<long>>($"Error while retrieving tour IDs: {ex.Message}");
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

        public async Task<bool> GetweatherByCoords(double latitude, double longitude)
        {
            var weatherResponse = await _weatherConnection.GetWeatherAsync(latitude, longitude);
            return weatherResponse == null;
        }

        public Result<TourDto> UpdatePremium(long tourId)
        {
            try
            {
                var tour = tourRepository.Get(tourId);
                if (!tour.IsPremium)
                {
                    tour.UpdatePremium(true);
                    _authorService.RemoveAuthorMoney(tour.AuthorId, 159.99);    // izmeni 10 na cenu placanja premium ture
                    var result = tourRepository.Update(tour);
                    return MapToDto(result);
                }
                return Result.Fail("tura je vec premium");
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }
            
    #region HelpperMethods

        private async Task MapWeatherConditionsToTour(TourDto tour) {
            var recommendedCounter = 0;
            var weatherTags = tour.WeatherRequirements.SuitableConditions.Select(condition => condition.ToString()).ToList();
            var weather = await _weatherConnection.GetWeatherAsync(tour.Checkpoints[0].Latitude, tour.Checkpoints[0].Longitude);
            tour.WeatherDescription = weather.Weather[0].Description;
            tour.WeatherIcon = weather.Weather[0].Icon;
            tour.Temperature = weather.Main.Temp;
            if (weather.Main.Temp > tour.WeatherRequirements.MinTemperature && weather.Main.Temp < tour.WeatherRequirements.MaxTemperature)
                recommendedCounter++;
            if (weather.Weather[0].Main == "Thunderstorm" || weather.Weather[0].Main == "Tornado" || weather.Weather[0].Main == "Fog")
                recommendedCounter -= 1000;
            if (weather.Wind.Speed > 5)
            {
                recommendedCounter -= 1;
                if (weather.Wind.Speed > 10)
                    recommendedCounter -= 2;
            }
            if (weather.Visibility < 5000 || weather.Weather[0].Main == "Mist")
                recommendedCounter--;
            if (weatherTags.Contains(weather.Weather[0].Main, StringComparer.OrdinalIgnoreCase))
                recommendedCounter++;
            MapRecommendedWeather(tour, recommendedCounter);
        }

        private async Task<int> MapWeatherConditionsForDateToTour(TourDto tour, DateTime date)
        {
            int recommendedCounter = 0;

            var weatherTags = tour.WeatherRequirements.SuitableConditions.Select(condition => condition.ToString()).ToList();
            var weather = await _weatherConnection.GetFiveDayForecast(tour.Checkpoints[0].Latitude, tour.Checkpoints[0].Longitude);

            GetTourWeather(weather, tour, date);
            var wind = GetAverageWindSpeed(weather, date);

            recommendedCounter += EvaluateTemperature(tour);
            recommendedCounter += EvaluateWeatherDescription(tour);
            recommendedCounter += EvaluateWindConditions(wind);
            recommendedCounter += EvaluateWeatherTags(weatherTags, tour);

            MapRecommendedWeather(tour, recommendedCounter);

            return recommendedCounter;
        }

        private Wind GetAverageWindSpeed(ForecastWeatherResponse weather, DateTime date)
        {
            var forecastItems = weather.List.Where(f => f.Dt.Date.Equals(date.Date)).ToList();
            double sumWindSpeed = forecastItems.Sum(f => f.Wind.Speed);

            return new Wind { Speed = sumWindSpeed / forecastItems.Count };
        }

        private void GetTourWeather(ForecastWeatherResponse weather, TourDto tour, DateTime date)
        {
            var forecastItems = weather.List.Where(f => f.Dt.Date.Equals(date.Date)).ToList();

            tour.Temperature = forecastItems.Average(f => f.Main.Temp);
            tour.WeatherDescription = GetMostFrequentWeatherCondition(forecastItems);
            tour.WeatherIcon = GetWeatherIconFromCondition(tour.WeatherDescription);
        }

        private string GetMostFrequentWeatherCondition(IEnumerable<ForecastItem> forecastItems)
        {
            return forecastItems
                .GroupBy(f => f.Weather[0].Main)
                .OrderByDescending(g => g.Count())
                .FirstOrDefault()?.Key ?? "Unknown";
        }

        private string GetWeatherIconFromCondition(string weatherCondition)
        {
            switch (weatherCondition.ToUpper())
            {
                case "CLEAR":
                    return "01d"; // Ikona za jasno vreme
                case "CLOUDS":
                    return "02d"; // Ikona za oblačno vreme
                case "RAIN":
                    return "09d"; // Ikona za kišu
                case "SNOW":
                    return "13d"; // Ikona za sneg
                case "MIST":
                    return "50d"; // Ikona za maglu
                default:
                    return "unknown";
            }
        }

        private int EvaluateTemperature(TourDto tour)
        {
            if (tour.Temperature > tour.WeatherRequirements.MinTemperature && tour.Temperature < tour.WeatherRequirements.MaxTemperature)
                return 1;

            return 0;
        }

        private int EvaluateWeatherDescription(TourDto tour)
        {
            if (tour.WeatherDescription == "Thunderstorm" || tour.WeatherDescription == "Tornado" || tour.WeatherDescription == "Fog")
                return -1000;

            return 0;
        }

        private int EvaluateWindConditions(Wind wind)
        {
            int windPenalty = 0;

            if (wind.Speed > 5)
                windPenalty = -1;
            if (wind.Speed > 10)
                windPenalty -= 2;

            return windPenalty;
        }

        private int EvaluateWeatherTags(List<string> weatherTags, TourDto tour)
        {
            return weatherTags.Contains(tour.WeatherDescription, StringComparer.OrdinalIgnoreCase) ? 1 : 0;
        }

        private void MapRecommendedWeather(TourDto tour, int recommendedCounter) {
            if (recommendedCounter == 2)
            {
                tour.WeatherRecommend = WeatherRecommend.HighyRecommend;
            }
            else if (recommendedCounter == 1)
            {
                tour.WeatherRecommend = WeatherRecommend.Recommend;
            }
            else if (recommendedCounter == 0)
            {
                tour.WeatherRecommend = WeatherRecommend.Neutral;
            }
            else if (recommendedCounter == -1)
            {
                tour.WeatherRecommend = WeatherRecommend.DontRecommend;
            }
            else
            {
                tour.WeatherRecommend = WeatherRecommend.HighlyDontRecommend;
            }
        }

    #endregion
    }

}
