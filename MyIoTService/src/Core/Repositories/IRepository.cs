using MyIoTService.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyIoTService.Core.Repositories
{
    public interface IRepository<Entity, T> where Entity : class, IEntity<T>
    {
        Task<IEnumerable<Entity>> GetAll();
        Task<Entity> Get(T id);
        Task Create(Entity entity);
        Task Update(Entity entity);
        Task Delete(T id);
    }
}
