using Catalog.Entities;
using Catalog.Repositories.Contracts;

namespace Catalog.Repositories
{
   

    public class MongoDbItemsRepository : IMongoDbItemsRepository
    {
        public MongoDbItemsRepository(IMangoClient mangoClient)
        {
            
        }
        public void CreateItem()
        {
            throw new NotImplementedException();
        }

        public void DeleteItem()
        {
            throw new NotImplementedException();
        }

        public Item GetItem()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Item> GetItems()
        {
            throw new NotImplementedException();
        }

        public void UpdateItem(Item item)
        {
            throw new NotImplementedException();
        }
    }
}