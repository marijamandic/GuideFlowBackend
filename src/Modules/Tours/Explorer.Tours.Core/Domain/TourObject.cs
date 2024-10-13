using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain;

public enum ObjectCategory
{
    Parking,
    Restaurant,
    Toilet,
    Other
}

public class TourObject : Entity
{
    public string Name { get; init; }
    public string? Description { get; init; }
    public string ImageUrl { get; init; }
    public ObjectCategory Category { get; init; }

    public TourObject(string name, string? description, string imageUrl, ObjectCategory category)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Invalid Name.");
        if (string.IsNullOrWhiteSpace(imageUrl)) throw new ArgumentException("Invalid Image path.");
        Name = name;
        Description = description;
        ImageUrl = imageUrl;
        Category = category;
    }
}