using AutoMapper;
using IGse.Core.Contracts.Repositories;
using IGse.Core.Contracts.Services;
using IGse.Core.Entities;
using IGse.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace IGse.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class SetPriceController : ControllerBase
    {
        private readonly ISetPriceService _setPriceService;
        private readonly ISetPriceRepository _setPriceRepository;
        private readonly IMapper _mapper;
        private readonly ISetPriceHistoryRepository _setPriceHistoryRepository;

        public SetPriceController(ISetPriceService setPriceService,ISetPriceRepository setPriceRepository,IMapper mapper,ISetPriceHistoryRepository setPriceHistoryRepository)
        {
            _setPriceService = setPriceService;
            _setPriceRepository = setPriceRepository;
            _mapper = mapper;
            _setPriceHistoryRepository = setPriceHistoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetPrices()
        {
            var prices = await _setPriceRepository.GetPriceData();
            if(prices is not null)
                return Ok (prices);
            return NotFound("Prices not found!");
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> SetOrUpdatePrice([Required]SetPriceVm setPriceVm,[FromQuery,Required]int userId)
        {
            var priceToUpdateOrAdd = _mapper.Map<SetPriceVm, SetPrice>(setPriceVm);
            var updatedPrice = await _setPriceService.SetPriceAsync(priceToUpdateOrAdd,userId);
            if (updatedPrice is not null)
                return Ok(updatedPrice);
            return BadRequest();
        }

        [HttpPost("electric-day")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> SetElectricityDayPrice([Required]decimal electricityDayPrice, [FromQuery,Required] int userId)
        {
            var updatedPrices = await _setPriceService.SetElectricityDayPrice(electricityDayPrice,userId);
            if(updatedPrices is not null)
                return Ok(updatedPrices);
            return BadRequest();
        }

        [HttpPost("electric-night")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> SetElectricityNightPrice([Required] decimal electricityNightPrice, [FromQuery,Required] int userId)
        {
            var updatedPrices = await _setPriceService.SetElectricityNightPrice(electricityNightPrice, userId);
            if (updatedPrices is not null)
                return Ok(updatedPrices);
            return BadRequest();
        }
        
        [HttpPost("gas")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> SetGasPrice([Required] decimal gasPrice, [FromQuery, Required] int userId)
        {
            var updatedPrices = await _setPriceService.SetGasPrice(gasPrice, userId);
            if (updatedPrices is not null)
                return Ok(updatedPrices);
            return BadRequest();
        }
        
        [HttpPost("standing-charge")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> SetStandingPrice([Required] decimal standingPrice, [FromQuery, Required] int userId)
        {
            var updatedPrices = await _setPriceService.SetStandingCharge(standingPrice, userId);
            if (updatedPrices is not null)
                return Ok(updatedPrices);
            return BadRequest();
        }

        [HttpGet("/set-price-history")]
        public async Task<ActionResult> GetSetPriceHistory()
        {
            var setPriceHistory = await _setPriceHistoryRepository.GetSetPriceHistory();
            if(setPriceHistory.Any())
                return Ok(setPriceHistory);
            return NotFound("Set Price history not found!");
        }
    }
}
