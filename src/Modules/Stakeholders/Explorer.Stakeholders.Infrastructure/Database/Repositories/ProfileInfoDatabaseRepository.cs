using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class ProfileInfoDatabaseRepository : CrudDatabaseRepository<ProfileInfo, StakeholdersContext>, IProfileInfoRepository
    {
        public ProfileInfoDatabaseRepository(StakeholdersContext dbContext) : base(dbContext) { }


        public override PagedResult<ProfileInfo> GetPaged(int page, int pageSize)
        {
            var task = DbContext.Profiles.GetPagedById(page, pageSize);
            task.Wait();
            return task.Result;
        }

        public override ProfileInfo Get(long id)
        {
            var entity = DbContext.Profiles
                .FirstOrDefault(u => u.Id == id);

            if (entity == null) throw new KeyNotFoundException("Not found: " + id);
            return entity;
        }

        public override ProfileInfo Update(ProfileInfo profileInfo)
        {
            // Ažuriranje ProfileInfo zapisa
            DbContext.Entry(profileInfo).State = EntityState.Modified;
            DbContext.SaveChanges();

            // Ažuriranje svih followera gde je FollowerId jednak ID-ju korisnika koji je ažuriran
            var followersToUpdate = DbContext.Followers
                .Where(follower => follower.FollowerId == profileInfo.Id) // Pronađi sve relevantne followere
                .ToList();

            if (profileInfo.ImageUrl != null)
            {
                foreach (var follower in followersToUpdate)
                {
                    follower.ImageUrl = profileInfo.ImageUrl; // Ažuriranje imageUrl
                }
            }

            DbContext.SaveChanges(); // Sačuvaj promene za followere

            return profileInfo;
        }


        // Implementacija dodatne metode Exists
        public bool Exists(string username)
        {
            return DbContext.Profiles
                .Any(p => p.FirstName == username || p.LastName == username);  // Pretpostavljamo da se username odnosi na FirstName ili LastName
        }
        public ProfileInfo GetByUserId(long id)
        {
            try
            {
                var profileInfo = DbContext.Profiles.Include(p => p.Followers)
                    .FirstOrDefault(u => u.UserId == id);

                if (profileInfo == null)
                    throw new KeyNotFoundException("Profile not found: " + id);

                return profileInfo;
            }
            catch (KeyNotFoundException ex)
            {
                // Logovanje greške (ako koristiš logger)
                Console.WriteLine(ex.Message); // Ovde možeš koristiti logger umesto Console.WriteLine
                                               // Možeš vratiti null ili neki podrazumevani objekat, zavisno od potreba
                return null;
            }
        }


        public List<ProfileInfo> GetAll()
        {
            return DbContext.Profiles.ToList();
        }

        public long GetPersonId(long userId)
        {
            var profileInfo = DbContext.Users
                .FirstOrDefault(u => u.Id == userId);

            if (profileInfo == null) throw new KeyNotFoundException("User not found: " + userId);

            // Assuming there's a property "PersonId" that you intend to return
            return profileInfo.Id; // Adjust this if there's a specific PersonId to return
        }

        public List<int> GetFollowerIdsByUserId(int userId)
        {
            return DbContext.Profiles
        .Where(profile => profile.UserId == userId) 
        .SelectMany(profile => profile.Followers)  
        .Select(follower => follower.FollowerId)    
        .ToList();
        }

        public List<int> GetUserIdsByFollowerId(int followerId)
        {
            return DbContext.Followers
                .Where(follower => follower.FollowerId == followerId) 
                .Select(follower => (int)follower.UserId)             
                .ToList();                                            
        }

    }
}
