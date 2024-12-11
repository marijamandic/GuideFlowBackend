﻿using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Explorer.Stakeholders.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Explorer.Stakeholders.API.Dtos;
using System.Linq;

public class UserDatabaseRepository : CrudDatabaseRepository<User, StakeholdersContext>, IUserRepository
{
    public UserDatabaseRepository(StakeholdersContext dbContext) : base(dbContext) { }

    public override PagedResult<User> GetPaged(int page, int pageSize)
    {
        var task = DbContext.Users.GetPagedById(page, pageSize);
        task.Wait();
        return task.Result;
    }

    public override User Get(long id)
    {
        var entity = DbContext.Users
            .FirstOrDefault(u => u.Id == id);

        if (entity == null) throw new KeyNotFoundException("Not found: " + id);
        return entity;
    }

    public override User Update(User user)
    {
        DbContext.Entry(user).State = EntityState.Modified;
        DbContext.SaveChanges();
        return user;
    }
    public Tourist UpdateTourist(Tourist tourist)
    {
        DbContext.Entry(tourist).State = EntityState.Modified;
        DbContext.SaveChanges();
        return tourist;
    }

    public bool Exists(string username)
    {
        return DbContext.Users.Any(u => u.Username == username);
    }

    public User GetById(long id)
    {
        var user = DbContext.Users
            .FirstOrDefault(u => u.Id == id);

        //if (user == null) throw new KeyNotFoundException("User not found: " + id);
        return user;
    }
    public Tourist GetTouristById(long id)
    {
        var user = DbContext.Users.OfType<Tourist>()
            .FirstOrDefault(u => u.Id == id);

       // if (user == null) throw new KeyNotFoundException("Tourist not found: " + id);
        return user;
    }

    public User? GetActiveByName(string username)
    {
        return DbContext.Users
            .FirstOrDefault(u => u.Username == username && u.IsActive);
    }

    public List<User> GetAll()
    {
        return DbContext.Users.ToList();
    }

    public long GetPersonId(long userId)
    {
        var user = DbContext.Users
            .FirstOrDefault(u => u.Id == userId);

        if (user == null) throw new KeyNotFoundException("User not found: " + userId);

        // Assuming there's a property "PersonId" that you intend to return
        return user.Id; // Adjust this if there's a specific PersonId to return
    }

    public Tourist CreateTourist(Tourist tourist)
    {
        DbContext.Tourists.Add(tourist);
        DbContext.SaveChanges();
        return tourist;
    }
    public PagedResult<Tourist> GetTouristsPaged(int page, int pageSize)
    {
        var task = DbContext.Users.OfType<Tourist>().GetPagedById(page, pageSize);
        task.Wait();
        return task.Result;

    }
    public List<User> GetAllByIds(List<long> ids)
    {
        if (ids == null || !ids.Any())
            return new List<User>();

        return DbContext.Users
            .Where(u => ids.Contains(u.Id))
            .ToList();
    }
}
