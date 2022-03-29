using Play.Catalog.Service.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace Play.Catalog.Service.Repositories
{
    public interface IItemsRepository
    {
        Task createAsync(Item entity);
        Task<IReadOnlyCollection<Item>> GetAllAsync();
        Task<Item> getAsync(Guid id);
        Task removeAsync(Guid id);
        Task updateAsync(Item entity);
    }
}