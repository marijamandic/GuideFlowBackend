using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class AppRatingDatabaseRepository : IAppRatingRepository
    {
        private readonly StakeholdersContext _dbContext;

        public AppRatingDatabaseRepository(StakeholdersContext dbContext)
        {
            _dbContext = dbContext;
        }

        public AppRating Create(AppRating appRating)
        {
            _dbContext.Ratings.Add(appRating);
            _dbContext.SaveChanges();
            return appRating;
        }
    }
}
