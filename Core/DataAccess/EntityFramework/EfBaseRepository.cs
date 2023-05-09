﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Core.DataAccess.EntityFramework
{
    public class EfBaseRepository<TEntity, TContext> : IBaseRepository<TEntity>
    where TEntity : class, IEntity, new()
    where TContext : DbContext, new()
    {
        public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            await using var context = new TContext();
            return filter == null
                ? await context.Set<TEntity>().ToListAsync()
                : await context.Set<TEntity>().Where(filter).ToListAsync();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            await using var context = new TContext();
            return await context.Set<TEntity>().SingleOrDefaultAsync(filter);
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await using var context = new TContext();
            await context.Set<TEntity>().AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            await using var context = new TContext();
            context.Set<TEntity>().Update(entity);
            await context.SaveChangesAsync();
            return entity;

        }

        public async Task<bool> DeleteAsync(int id)
        {
            await using var context = new TContext();
            var deleteEntity = await context.Set<TEntity>().FindAsync(id);
            context.Set<TEntity>().Remove(deleteEntity);
            var data = await context.SaveChangesAsync();
            if (data > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}