namespace Explorer.Blog.API.Dtos
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public DateTime PublisDate { get; set; }
        public string ImageUrl { get; set; }
        public Status Status { get; set; }     
    }
    public enum Status
    {
        Draft,
        Published,
        Closed
    }
}
