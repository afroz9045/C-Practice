using IGse.Core.Contracts.Repositories;
using IGse.Core.Contracts.Services;
using IGse.Core.Entities;

namespace IGse.Core.Services;
public class BillService : IBillService
{
    private readonly IBillRepository _billRepository;
    private readonly ISetPriceRepository _setPriceRepository;

    public BillService(IBillRepository billRepository, ISetPriceRepository setPriceRepository)
    {
        _billRepository = billRepository;
        _setPriceRepository = setPriceRepository;
    }

    public async Task<IEnumerable<Bill>> GetBillsAsync()
    {
        var bills = await _billRepository.GetBillsAsync();
        return bills;
    }

    public async Task<Bill?> GetBillAsync(Bill bill)
    {
        var previousBills = await _billRepository.GetBillsForCustomerId(bill.CustomerId);
        var prices = await _setPriceRepository.GetPriceData();
        var isPreviousBillsUnPaid = previousBills.Where(x => x.CustomerId == bill.CustomerId && !x.IsPaid).ToList();
        var previousElectricityDayReading = 0;
        var previousElectricityNightReading = 0;
        var previousGasReading = 0;
        if (isPreviousBillsUnPaid.Any(x=> !x.IsPaid))
        {
            if (previousBills.Count() >= 3)
            {
                return null;
            }
            foreach (var prevBill in isPreviousBillsUnPaid)
            {
                previousElectricityDayReading += prevBill.DayElectricityReading;
                previousElectricityNightReading += prevBill.NightElectricityReading;
                previousGasReading += prevBill.GasReading;
            }
        }
        bill.NumberOfDays = Convert.ToInt32(DateTime.UtcNow.Date.Subtract(bill.BillMonthYear).TotalDays);
        bill.Amount = (int)Math.Abs((int)Math.Round((bill.DayElectricityReading - previousElectricityDayReading) * prices.ElectricityPriceDay
                       +
                       (bill.NightElectricityReading - previousElectricityNightReading) * prices.ElectricityPriceNight
                       +
                       (bill.GasReading - previousGasReading) * prices.GasPrice)
                        + prices.StandingCharge * bill.NumberOfDays
                        );
        bill.IsPaid = false;
        bill.IsVoucherUsed = false;
        bill.DueDate = bill.BillMonthYear.AddDays(20);
        var addedBill = await _billRepository.AddBillAsync(bill);
        return addedBill;
    }
}
