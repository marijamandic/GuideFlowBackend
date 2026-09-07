using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Core.Domain.Posts;
using Explorer.BuildingBlocks.Core.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Core.UseCases
{
    public class PostService : CrudService<PostDto, Post>, IPostService
    {
    
        public PostService(ICrudRepository<Post> repository, IMapper mapper) : base(repository, mapper) { }

        public string GetDatabaseSummary()
        {
            const int pageSize = 100; 
            int currentPage = 1;
            var allPosts = new List<string>();

            while (true)
            {
                var pagedResult = CrudRepository.GetPaged(currentPage, pageSize);

                if (pagedResult.Results == null || !pagedResult.Results.Any())
                    break;

                allPosts.AddRange(pagedResult.Results.Select(post => post.ToString()));

                // If fewer results were returned than pageSize, exit the loop
                if (pagedResult.Results.Count < pageSize)
                    break;

                currentPage++;
            }

            return string.Join(Environment.NewLine, allPosts);
        }
    }

}
