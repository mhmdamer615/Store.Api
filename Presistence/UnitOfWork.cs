﻿using Domain.Contract;
using Domain.Entities;
using Presistence.Data;
using Presistence.Repositories;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _context;
        private ConcurrentDictionary<string, object> _repositories;
        public UnitOfWork(StoreDbContext context)
        {
            _context = context;

        }

        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        => (IGenericRepository<TEntity, TKey>)_repositories.GetOrAdd(typeof(TEntity).Name, _ => new GenericRepository <TEntity, TKey>(_context));

        public async Task<int> SaveChangesAsync()
        => await _context.SaveChangesAsync();
    }
}
