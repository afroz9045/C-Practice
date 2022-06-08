// See https://aka.ms/new-console-template for more information
using GstCalculationLogic;

Console.WriteLine("Enter product price:");
double productPrice = Convert.ToDouble(Console.ReadLine());

Console.WriteLine("Press '1' for state level business");
Console.WriteLine("Press '2' for outside state business");

int selection = Convert.ToInt32(Console.ReadLine());
Console.WriteLine();

var totalPrice = productPrice + GstCalculation.TaxCalculation((float)productPrice, selection);
Console.WriteLine($"Total price including GST is: {totalPrice}");