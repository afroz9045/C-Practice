using Catalog.Entities;
using Catalog.Repositories.Contracts;

namespace Catalog.Repositories
{

    public class InMemoryItemsRepository : IInMemoryItemsRepository
    {
        private static readonly List<Item> items = new(){
            new Item{Id= Guid.NewGuid(),Name = "Potion",Price = 9, CreatedDate = DateTimeOffset.UtcNow },
            new Item{Id= Guid.NewGuid(),Name = "Iron Sword",Price = 19, CreatedDate = DateTimeOffset.UtcNow },
            new Item{Id= Guid.NewGuid(),Name = "Bronze Shield",Price = 25, CreatedDate = DateTimeOffset.UtcNow }
        };

        public IEnumerable<Item> GetItems()
        {
            return items;
        }

        public Item? GetItem(Guid id)
        {
            var result = items.Where(item => item.Id == id).SingleOrDefault();

            if (result is not null)
                return result;
            return null;
        }
        public Item CreateItem(Item item)
        {
            items.Add(item);
            return item;
        }

        public Item UpdateItem(Item item)
        {
            var index = items.FindIndex(existingItem=>existingItem.Id ==item.Id);
            items[index] = item;
            return item;
        }

        public Item? DeleteItem(Guid id)
        {
            var index = items.FindIndex(existingItem=>existingItem.Id==id);
            var itemToDelete = GetItem(id);
            items.RemoveAt(index);
            return itemToDelete;
        }
    }
}