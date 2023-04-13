using IGse.Core.Contracts.Repositories;
using IGse.Core.Contracts.Services;
using IGse.Core.Entities;

namespace IGse.Core.Services
{
    public class SetPriceService : ISetPriceService
    {
        private readonly ISetPriceRepository _setPriceRepository;
        private readonly ISetPriceHistoryRepository _setPriceHistoryRepository;

        public SetPriceService(ISetPriceRepository setPriceRepository,ISetPriceHistoryRepository setPriceHistoryRepository)
        {
            _setPriceRepository = setPriceRepository;
            _setPriceHistoryRepository = setPriceHistoryRepository;
        }


        public async Task<SetPrice> SetPriceAsync(SetPrice setPrice,int userId)
        {
            setPrice.SetDate = DateTime.UtcNow;
            var setPriceHistory = new SetPriceHistory
            {
                SetBy = userId,
                SetDate = DateTime.UtcNow,
                SetType = "All"
            };
            var existingPrice = await _setPriceRepository.GetPriceData();
            if (existingPrice is null)
            {
                var initialSetPrice = await _setPriceRepository.SetPrice(setPrice);
                await _setPriceHistoryRepository.SetPriceHistoryAsync(setPriceHistory);
                return initialSetPrice;
            }
            var updatedSetPrice = await _setPriceRepository.UpdatePrice(setPrice);
            await _setPriceHistoryRepository.SetPriceHistoryAsync(setPriceHistory);
            return updatedSetPrice;
        }

        public async Task<SetPrice> SetElectricityDayPrice(decimal electricityDayPrice, int userId)
        {
            var existingPrice = await _setPriceRepository.GetPriceData();
            var setPriceHistory = new SetPriceHistory
            {
                SetBy = userId,
                SetDate = DateTime.UtcNow,
                SetType = "Electricity Day"
            };
            if (existingPrice is null)
            { 
                var setPrice = new SetPrice();
                setPrice.ElectricityPriceDay = electricityDayPrice;
                setPrice.SetDate = DateTime.UtcNow;
                var initialSetPrice = await _setPriceRepository.SetPrice(setPrice);
                await _setPriceHistoryRepository.SetPriceHistoryAsync (setPriceHistory);
                return initialSetPrice;
            }
            existingPrice.ElectricityPriceDay = electricityDayPrice;
            var updatedSetPrice = await _setPriceRepository.UpdatePrice(existingPrice);
            await _setPriceHistoryRepository.SetPriceHistoryAsync(setPriceHistory);
            return updatedSetPrice;
        }

        public async Task<SetPrice> SetElectricityNightPrice(decimal electricityNightPrice,int userId)
        {
            var existingPrice = await _setPriceRepository.GetPriceData();
            var setPriceHistory = new SetPriceHistory
            {
                SetBy = userId,
                SetDate = DateTime.UtcNow,
                SetType = "Electricity Night"
            };
            if (existingPrice is null)
            {
                var setPrice = new SetPrice();
                setPrice.ElectricityPriceNight= electricityNightPrice;
                setPrice.SetDate = DateTime.UtcNow;
                var initialSetPrice = await _setPriceRepository.SetPrice(setPrice);
                await _setPriceHistoryRepository.SetPriceHistoryAsync(setPriceHistory);
                return initialSetPrice;
            }
            existingPrice.ElectricityPriceNight = electricityNightPrice;
            var updatedSetPrice = await _setPriceRepository.UpdatePrice(existingPrice);
            await _setPriceHistoryRepository.SetPriceHistoryAsync(setPriceHistory);
            return updatedSetPrice;
        }

        public async Task<SetPrice> SetGasPrice(decimal gasPrice,int userId)
        {
            var existingPrice = await _setPriceRepository.GetPriceData();
            var setPriceHistory = new SetPriceHistory
            {
                SetBy = userId,
                SetDate = DateTime.UtcNow,
                SetType = "Gas"
            };
            if (existingPrice is null)
            {
                var setPrice = new SetPrice();
                setPrice.GasPrice = gasPrice;
                setPrice.SetDate = DateTime.UtcNow;
                var initialSetPrice = await _setPriceRepository.SetPrice(setPrice);
                await _setPriceHistoryRepository.SetPriceHistoryAsync (setPriceHistory);    
                return initialSetPrice;
            }
            existingPrice.GasPrice = gasPrice;
            var updatedSetPrice = await _setPriceRepository.UpdatePrice(existingPrice);
            await _setPriceHistoryRepository.SetPriceHistoryAsync(setPriceHistory);
            return updatedSetPrice;
        }

        public async Task<SetPrice> SetStandingCharge(decimal standingCharge,int userId)
        {
            var existingPrice = await _setPriceRepository.GetPriceData();
            var setPriceHistory = new SetPriceHistory
            {
                SetBy = userId,
                SetDate = DateTime.UtcNow,
                SetType = "Standing"
            };
            if (existingPrice is null)
            {
                var setPrice = new SetPrice();
                setPrice.StandingCharge = standingCharge;
                setPrice.SetDate = DateTime.UtcNow;
                var initialSetPrice = await _setPriceRepository.SetPrice(setPrice);
                await _setPriceHistoryRepository.SetPriceHistoryAsync(setPriceHistory);
                return initialSetPrice;
            }
            existingPrice.StandingCharge = standingCharge;
            var updatedSetPrice = await _setPriceRepository.UpdatePrice(existingPrice);
            await _setPriceHistoryRepository.SetPriceHistoryAsync(setPriceHistory);
            return updatedSetPrice;
        }
    }
}
