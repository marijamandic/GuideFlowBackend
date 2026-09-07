using Explorer.BuildingBlocks.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;



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
        return _context.Checkpoint.ToList(); //funkcija nema veze sa vezom samo sam je ispravio da nema greske
    }
}
