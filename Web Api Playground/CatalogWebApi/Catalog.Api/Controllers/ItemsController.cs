using Catalog.Dtos;
using Catalog.Entities;
using Catalog.Repositories;
using Catalog.Repositories.Contracts;
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
            var itemsResult = _inMemoryItemsRepository.GetItems().Select(item=>item.AsDto());
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

    }
}