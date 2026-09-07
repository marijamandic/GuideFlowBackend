using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos.Club;
using Explorer.Stakeholders.API.Public.Club;
using Explorer.Stakeholders.Core.Domain.Club;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases.Club
{
    public class ClubService : CrudService<ClubDto, Explorer.Stakeholders.Core.Domain.Club.Club>, IClubService
    {
        private readonly IClubMemberService _clubMemberService;

        public ClubService(ICrudRepository<Explorer.Stakeholders.Core.Domain.Club.Club> crudRepository, IMapper mapper, IClubMemberService clubMemberService) : base(crudRepository, mapper)
        {
            _clubMemberService = clubMemberService;
        }

        public Result<List<ClubDto>> GetTopClubsByMembers(int topCount = 5)
        {
            try
            {
                var clubsResult = GetPaged(1, int.MaxValue); // Fetch all clubs using GetPaged

                if (!clubsResult.IsSuccess)
                {
                    return Result.Fail<List<ClubDto>>("An error occurred while fetching clubs.");
                }

                var clubsWithMemberCounts = clubsResult.Value.Results // Access Results from PagedResult
                    .Select(club => new ClubDto
                    {
                        Id = club.Id,
                        OwnerId = club.OwnerId,
                        Name = club.Name,
                        Description = club.Description,
                        ImageUrl = club.ImageUrl,
                        MemberCount = _clubMemberService.GetMembersByClub(club.Id).Value.Count() // Count members using ClubMemberService
                    })
                    .OrderByDescending(c => c.MemberCount)
                    .Take(topCount)
                    .ToList();

                return Result.Ok(clubsWithMemberCounts);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging
                return Result.Fail<List<ClubDto>>("An error occurred while fetching top clubs.");
            }
        }

        public string GetDatabaseSummary()
        {
            const int pageSize = 100; // Process clubs in batches of 100
            int currentPage = 1;
            var allClubs = new List<string>();

            while (true)
            {
                var pagedResult = GetPaged(currentPage, pageSize);

                if (!pagedResult.IsSuccess || pagedResult.Value.Results == null || !pagedResult.Value.Results.Any())
                    break;

                allClubs.AddRange(pagedResult.Value.Results.Select(club =>
                    $"ID: {club.Id}, Name: {club.Name}, OwnerID: {club.OwnerId}, Description: {club.Description}, ImageURL: {club.ImageUrl}"
                ));

                // If fewer results were returned than pageSize, it means we reached the last page
                if (pagedResult.Value.Results.Count < pageSize)
                    break;

                currentPage++;
            }

            return string.Join(Environment.NewLine, allClubs);
        }

    }
}
