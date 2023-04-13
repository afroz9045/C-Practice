using IGse.Core.Entities;

namespace IGse.Core.Contracts.Services
{
    public interface ISetPriceService
    {
        Task<SetPrice> SetPriceAsync(SetPrice setPrice,int userId);
        Task<SetPrice> SetElectricityDayPrice(decimal electricityDayPrice, int userId);
        Task<SetPrice> SetElectricityNightPrice(decimal electricityNightPrice, int userId);
        Task<SetPrice> SetGasPrice(decimal gasPrice, int userId);
        Task<SetPrice> SetStandingCharge(decimal standingCharge, int userId);
    }
}