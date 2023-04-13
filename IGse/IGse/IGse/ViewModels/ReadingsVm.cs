namespace IGse.ViewModels;
public class ReadingsVm
{
    public int CustomerId { get; set; }
    public int DayElectricityReading { get; set; }
    public int NightElectricityReading { get; set; }
    public int GasReading { get; set; }
    public DateTime BillMonthYear { get; set; }
}
