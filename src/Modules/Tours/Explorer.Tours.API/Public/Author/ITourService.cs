﻿using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.Author
{
    public interface ITourService
    {
        Result<PagedResult<TourDto>> GetPaged(int page, int pageSize);
        Result<TourDto> Get(int id);
        Result<TourDto> Create(TourDto tour);
        Result<TourDto> Update(TourDto tour);
        Result Delete(int id);
        Result<TourDto> AddCheckpoint(int tourId, CheckpointDto checkpoint);
        Result<TourDto> AddTransportDurations(int id, List<TransportDurationDto> transportDurations);
        Result<TourDto> Archive(int id);   
        Result<TourDto> Publish(int id);
        Task<Result<IEnumerable<TourDto>>> GetPurchasedAndArchivedByUser(int userId, DateTime? date = null);
        Result<TourDto> UpdateLength(int id, double length);
        Result<TourDto> UpdateCheckpoint(int id, CheckpointDto checkpoint);
        Result DeleteCheckpoint(int id, CheckpointDto checkpoint);
        Result<List<long>> GetTourIdsByAuthorId(int authorId);
        Result<List<TourDto>> SearchTours(double lat, double lon, double distance, int page, int pageSize);
        Result<TourDto> CheckIfPurchased(int userId, int tourId);
        Result<PagedResult<TourDto>> GetToursByBundleId(int id);
        Result<List<TourDto>> GetSuggestedTours(double longitude, double latitude);
        Task<bool> GetweatherByCoords(double latitude, double longitude);
        Result<TourDto> UpdatePremium(long tourId);
        Task<Result<IEnumerable<TourDto>>> RecommendToursForDate(int touristId, DateTime date);
        public string GetDatabaseSummary();

        Result<TourDto> GetBestSellingTourByAuthorId(int id);
        Result<TourDto> GetWorstSellingTourByAuthorId(int id);
        Result<TourDto> GetBestRatedTourByAuthorId(int id);


    }
}
