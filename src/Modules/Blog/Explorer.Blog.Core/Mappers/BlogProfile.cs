using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.Core.Domain.Posts;

namespace Explorer.Blog.Core.Mappers;

public class BlogProfile : Profile
{
    public BlogProfile()
    {
        CreateMap<PostDto, Post>()
            .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments))
            .ForMember(dest => dest.Ratings, opt => opt.MapFrom(src => src.Ratings))
            .ReverseMap();

        CreateMap<CommentDto, Comment>().ReverseMap();
        CreateMap<BlogRatingDto, BlogRating>().ReverseMap();
    }
}
