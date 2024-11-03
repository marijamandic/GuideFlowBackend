using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Stakeholders.Core.Domain.Problems;

public class Problem : Entity
{
    private readonly List<Message> _messages = new();

    public long UserId { get; init; }
    public long TourId { get; init; }
    public Details Details { get; private set; }
    public Resolution Resolution { get; private set; }
    public IReadOnlyList<Message> Messages => new List<Message>(_messages);

    public Problem(long userId, long tourId)
    {
        UserId = userId;
        TourId = tourId;
        //Validate();
    }

    private void Validate()
    {
        
    }
}