namespace Explorer.Blog.API.Dtos
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public DateTime PublishDate { get; set; }
        public string? ImageUrl { get; set; }
        public string? ImageBase64 { get; set; }
        public PostStatus Status { get; set; }
        public List<CommentDto> Comments { get; set; } = new();
        public List<BlogRatingDto> Ratings { get; set; } = new();
    }
    public enum PostStatus
    {
        Draft,
        Published,
        Closed
    }
}
