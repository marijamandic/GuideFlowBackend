namespace Explorer.Tours.API.Dtos
{
    public class CheckpointDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string? ImageUrl { get; set; }
        public string? ImageBase64 { get; set; }
        public string Secret {  get; set; }
    }
}
