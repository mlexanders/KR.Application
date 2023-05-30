﻿using Common;
using Microsoft.EntityFrameworkCore;

namespace MusalovKR.Repositories
{
    public class PostsRepository
    {
        private readonly AppDbContext dbContext;

        public PostsRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Posts> Read(int skip)
        {
            return await dbContext.Posts.AsNoTrackingWithIdentityResolution().Skip(skip).FirstOrDefaultAsync();
        }

        public async Task<List<Posts>> Read(int skip, int take)
        {
            return await dbContext.Posts.AsNoTrackingWithIdentityResolution().Skip(skip).Take(take).ToListAsync();
        }

        public void Delete(int id)
        {
            dbContext.Posts.Remove(new Posts() { ID_Posts = id });
            dbContext.SaveChanges();
        }

        public async Task Create(Posts procedureType)
        {
            await dbContext.Posts.AddAsync(procedureType);
            await dbContext.SaveChangesAsync();
        }

        public async Task Update(int id, Posts procedureType)
        {
            var entity = await dbContext.Posts.AsNoTrackingWithIdentityResolution().FirstOrDefaultAsync(pt => pt.ID_Posts == id);
            entity.Tittle = procedureType.Tittle;
            entity.Responsibility = procedureType.Responsibility;
            dbContext.Update(entity);
            await dbContext.SaveChangesAsync();
        }

        public List<Posts> ReadWithScript(string script)
        {
            return dbContext.Posts.FromSqlRaw(script).ToList();
        }
    }
}
