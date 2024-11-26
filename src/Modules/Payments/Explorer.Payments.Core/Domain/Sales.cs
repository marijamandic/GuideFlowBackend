using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Payments.Core.Domain;

public class Sales : Entity
{
    public DateTime CreatedAt { get; private set; }
    public DateTime EndsAt { get; private set; }
    public int Discount { get; private set; }
	public List<long> TourIds { get; private set; } = new();

    public Sales(long id, DateTime createdAt, DateTime endsAt, int discount)
	{
		Id = id;
		CreatedAt = createdAt;
		EndsAt = endsAt;
		Discount = discount;
		Validate();
	}

	private void Validate()
	{
		bool endsAtIsInRange = EndsAt < CreatedAt || EndsAt > CreatedAt.AddDays(14);
		if (CreatedAt != default && endsAtIsInRange)
			throw new ArgumentException("Invalid EndsAt");
	}
}
