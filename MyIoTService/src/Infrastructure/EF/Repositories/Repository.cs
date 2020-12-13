using Microsoft.EntityFrameworkCore;
using MyIoTService.Core.Repositories;
using MyIoTService.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyIoTService.Infrastructure.EF.Repositories
{
    public abstract class Repository<Entity, T>: IRepository<Entity, T> where Entity : class, IEntity<T>
    {
        protected readonly MyIoTDbContext _db;
        protected DbSet<Entity> _dbSet;

        public Repository(MyIoTDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<Entity>();
        }

        public async Task Create(Entity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task Delete(T id)
        {
            var entity = await _dbSet.FindAsync(id);
            _dbSet.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<Entity> Get(T id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<Entity>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task Update(Entity entity)
        {
            await _db.SaveChangesAsync();
        }
    }
}
