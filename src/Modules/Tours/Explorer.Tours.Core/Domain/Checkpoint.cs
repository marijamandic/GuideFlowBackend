using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain;

public class Checkpoint : Entity
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public double Latitude { get; private set; }
    public double Longitude { get; private set; }
    public string? ImageUrl { get; private set; }

    public int TourId { get; private set; } 

    public Checkpoint(string name, string description, double latitude, double longitude, string? imageUrl, int tourId)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Invalid Name.");
        if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Invalid Description.");
        if (latitude < -90 || latitude > 90) throw new ArgumentException("Invalid Latitude value.");
        if (longitude < -180 || longitude > 180) throw new ArgumentException("Invalid Longitude value.");

        Name = name;
        Description = description;
        Latitude = latitude;
        Longitude = longitude;
        ImageUrl = imageUrl;
        TourId = tourId;
    }
}
