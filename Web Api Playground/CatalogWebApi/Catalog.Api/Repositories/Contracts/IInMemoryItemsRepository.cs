using Catalog.Entities;

namespace Catalog.Repositories.Contracts
{
     public interface IInMemoryItemsRepository
    {
        Item? GetItem(Guid id);
        IEnumerable<Item> GetItems();

        Item CreateItem(Item item);
        Item UpdateItem(Item item);
        Item? DeleteItem(Guid id);

    }
}