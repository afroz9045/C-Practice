using Catalog.Entities;

namespace Catalog.Repositories.Contracts
{

    public interface IMongoDbItemsRepository
    {
        void CreateItem();
        void DeleteItem();
        Item GetItem();
        IEnumerable<Item> GetItems();
        void UpdateItem(Item item);
    }

}