using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.Core.Domain.Posts;

namespace Explorer.Blog.Core.Mappers;

public class BlogProfile : Profile
{
    public BlogProfile()
    {
        CreateMap < PostDto,Post>().ReverseMap();
        CreateMap<CommentDto, Comment>().ReverseMap();
    }
}