using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Internal;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.API.Internal;
using FluentResults;

namespace Explorer.Payments.Core.UseCases;

public class TourBundleService : BaseService<TourBundleDto, TourBundle>, ITourBundleService, IInternalTourBundleService
{
    private readonly ITourBundleRepository _tourBundleRepository;

    private readonly IInternalTourHelperService _internalTourService;

    public TourBundleService(
        IMapper mapper,
        ITourBundleRepository tourBundleRepository,
        IInternalTourHelperService internalTourService) : base(mapper)
    {
        _tourBundleRepository = tourBundleRepository;
        _internalTourService = internalTourService;
    }

    public Result<PagedResult<TourBundleDto>> GetAll()
    {
        return MapToDto(_tourBundleRepository.GetAll());
    }

    public Result<TourBundleDto> Get(int id)
    {
        try
        {
            var result = _tourBundleRepository.Get(id);
            return MapToDto(result);
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
    }

    public Result<TourBundleDto> Create(TourBundleDto tourBundleDto)
    {
        var result = _internalTourService.Get(tourBundleDto.TourIds[0]);
        if (result.IsFailed) return Result.Fail(FailureCode.NotFound).WithError("Tour ID mismatch");

        var tour = result.Value;
        tourBundleDto.ImageUrl = tour.Checkpoints[0].ImageUrl!;

        _tourBundleRepository.Create(MapToDomain(tourBundleDto));
        return tourBundleDto;
    }

    public Result<TourBundleDto> Delete(long tourBundleId)
    {
        try
        {
            var tourBundle = _tourBundleRepository.GetById(tourBundleId);
            if (tourBundle.Status == Domain.BundleStatus.Published)
                return Result.Fail(FailureCode.InvalidArgument).WithError("Cannot Delete Published Tour Bundle");
            _tourBundleRepository.Delete(tourBundle);
            return MapToDto(tourBundle);
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
    }

    public Result<TourBundleDto> Modify(TourBundleDto tourBundleDto)
    {
        try
        {
            tourBundleDto.TourIds = tourBundleDto.TourIds.Distinct().ToList();
            var tourBundle = MapToDomain(tourBundleDto);    
            _tourBundleRepository.Save(tourBundle);
            return MapToDto(tourBundle);
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
    }

    public Result<TourBundleDto> Publish(long tourBundleId)
    {
        try
        {
            var tourBundle = _tourBundleRepository.GetById(tourBundleId);
            tourBundle.Publish();
            _tourBundleRepository.Save(tourBundle);
            return MapToDto(tourBundle);
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
    }

    public Result<TourBundleDto> Archive(long tourBundleId)
    {
        try
        {
            var tourBundle = _tourBundleRepository.GetById(tourBundleId);
            tourBundle.Archive();
            _tourBundleRepository.Save(tourBundle);
            return MapToDto(tourBundle);
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
    }

    public Result<List<int>> GetToursById(int bundleId)
    {
        try
        {
            var result = Get(bundleId);
            return result.Value.TourIds;
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
    }

	public Result<PagedResult<TourBundleDto>> GetAllPublished()
	{
        try
        {
            var result = _tourBundleRepository.GetAllPublished();
            return MapToDto(result);
        }
        catch (Exception e) {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
	}
}