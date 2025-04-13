﻿using Domain.Contract;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
    {
        private readonly StoreDbContext _context;
        public GenericRepository(StoreDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(TEntity entity)
       => await _context.Set<TEntity>().AddAsync(entity);

        public void Delete(TEntity entity)
        => _context.Set<TEntity>().Remove(entity);
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool IsTrackable = false)
        {
            if (IsTrackable)
                return await _context.Set<TEntity>().ToListAsync();
            return await _context.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<TEntity?> GetAsync(TKey Id)
        => await _context.Set<TEntity>().FindAsync(Id);

        public void UpDate(TEntity entity)
        => _context.Set<TEntity>().Update(entity);
    }
}
