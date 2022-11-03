using Catalog.Entities;

namespace Catalog.Repositories.Contracts
{
     public interface IInMemoryItemsRepository
    {
        Item? GetItem(Guid id);
        IEnumerable<Item> GetItems();
    }

}