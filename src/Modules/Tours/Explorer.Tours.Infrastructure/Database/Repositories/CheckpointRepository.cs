using Explorer.BuildingBlocks.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;



namespace Explorer.Tours.Infrastructure.Database;

public class CheckpointRepository : ICheckpointRepository
{
    private readonly ToursContext _context;

    public CheckpointRepository(ToursContext context)
    {
        _context = context;
    }

    List<Checkpoint> ICheckpointRepository.GetByTour(int id)
    {
        return _context.Checkpoint
            .Where(te => te.TourId == id)
            .ToList(); // No need to include related entities since there's no navigation property
    }
}
