using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Shopping;
using Explorer.BuildingBlocks.Infrastructure.Database;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class PurchaseTokenRepository : CrudDatabaseRepository<PurchaseToken, ToursContext>, IPurchaseTokenRepository
    {
        public PurchaseTokenRepository(ToursContext dbContext) : base(dbContext) { }

        public new PurchaseToken Create(PurchaseToken entity)
        {
            DbContext.PurchaseTokens.Add(entity);
            DbContext.SaveChanges();
            return entity;
        }

        public new PurchaseToken Update(PurchaseToken entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
            DbContext.SaveChanges();
            return entity;
        }

        public new void Delete(long id)
        {
            var token = DbContext.PurchaseTokens.Find(id);
            if (token == null) throw new KeyNotFoundException("Purchase token not found: " + id);

            DbContext.PurchaseTokens.Remove(token);
            DbContext.SaveChanges();
        }

        public new PurchaseToken Get(long id)
        {
            var token = DbContext.PurchaseTokens
                .Where(pt => pt.Id == id)
                .FirstOrDefault();

            if (token == null) throw new KeyNotFoundException("Purchase token not found: " + id);
            return token;
        }

        public PagedResult<PurchaseToken> GetByUserId(int userId)
        {
            var tokens = DbContext.PurchaseTokens
                .Where(pt => pt.UserId == userId)
                .ToList();

            if (!tokens.Any()) throw new KeyNotFoundException("No purchase tokens found for user: " + userId);

            return new PagedResult<PurchaseToken>(tokens, tokens.Count);
        }

        public PagedResult<PurchaseToken> GetPaged(int page, int pageSize)
        {
            var tokens = DbContext.PurchaseTokens
                .OrderBy(pt => pt.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PagedResult<PurchaseToken>(tokens, DbContext.PurchaseTokens.Count());
        }
    }
}
