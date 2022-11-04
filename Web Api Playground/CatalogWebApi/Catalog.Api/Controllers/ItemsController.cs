using Catalog.Dtos;
using Catalog.Entities;
using Catalog.Repositories;
using Catalog.Repositories.Contracts;
using Catalog.Vms;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private readonly IInMemoryItemsRepository _inMemoryItemsRepository;

        public ItemsController(IInMemoryItemsRepository inMemoryItemsRepository)
        {
            _inMemoryItemsRepository = inMemoryItemsRepository;
        }

        public IInMemoryItemsRepository InMemoryItemsRepository { get; }

        [HttpGet]
        public ActionResult GetItems()
        {
            var itemsResult = _inMemoryItemsRepository.GetItems().Select(item => item.AsDto());
            if (itemsResult.Count() > 0)
            {
                return Ok(itemsResult);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public ActionResult GetItemById(Guid id)
        {
            var item = _inMemoryItemsRepository.GetItem(id);
            if (item is null)
                return NotFound();
            return Ok(item.AsDto());
        }


        [HttpPost]
        public ActionResult AddItem(AddItemVm itemVm)
        {
            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = itemVm.Name,
                CreatedDate = DateTime.UtcNow,
                Price = itemVm.Price
            };

            var addedItem = _inMemoryItemsRepository.CreateItem(item).AsDto();

            if (addedItem is not null)
                return Ok(addedItem);
            return BadRequest();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateItem(Guid id, UpdateItemVm updateItemVm)
        {
            var existingItem = _inMemoryItemsRepository.GetItem(id);

            if (existingItem is null)
                return NotFound();
            Item updatedItem = existingItem with
            {
                Name = updateItemVm.Name,
                Price = updateItemVm.Price
            };

            var updatedItemResult = _inMemoryItemsRepository.UpdateItem(updatedItem);
            return Ok(updatedItemResult);

        }


        [HttpDelete("{id}")]
        public ActionResult DeleteItem(Guid id)
        {
            var existingItem = _inMemoryItemsRepository.GetItem(id);
            if(existingItem is null)
                return NotFound();
            var deletedResult = _inMemoryItemsRepository.DeleteItem(id);
            return NoContent();
        }
    }
}