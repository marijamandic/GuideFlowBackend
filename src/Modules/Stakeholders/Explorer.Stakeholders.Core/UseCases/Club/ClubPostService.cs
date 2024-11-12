using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Dtos.Club;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.API.Public.Club;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.Club;
using FluentResults;
using System.Collections.Generic;
using System.Linq;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class ClubPostService : CrudService<ClubPostDto, ClubPost>, IClubPostService
    {
        public ClubPostService(ICrudRepository<ClubPost> repository, IMapper mapper) : base(repository, mapper) { }

        public Result<List<ClubPostDto>> GetAll()
        {
            var pagedResult = GetPaged(1, int.MaxValue);

            if (pagedResult.IsFailed)
            {
                return Result.Fail<List<ClubPostDto>>(pagedResult.Errors);
            }

            return Result.Ok(pagedResult.Value.Results);
        }

        public Result<PagedResult<ClubPostDto>> GetPaged(int pageIndex, int pageSize)
        {
            return base.GetPaged(pageIndex, pageSize);
        }

        public Result<ClubPostDto> GetByClubId(long clubId)
        {
            var allClubPosts = GetAll();

            if (allClubPosts.IsFailed)
            {
                return Result.Fail<ClubPostDto>("Error retrieving all posts.");
            }

            var clubPost = allClubPosts.Value.FirstOrDefault(p => p.ClubId == clubId);

            return clubPost != null
                ? Result.Ok(clubPost)
                : Result.Fail<ClubPostDto>($"No post found for Club ID {clubId}.");
        }
    }
}
