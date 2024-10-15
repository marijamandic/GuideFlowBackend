namespace Explorer.Tours.API.Dtos
{
    public class CheckpointDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string ImageUrl { get; set; }
    }
}
