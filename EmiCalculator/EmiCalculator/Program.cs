// See https://aka.ms/new-console-template for more information
using EmiCalculation;
Console.WriteLine("Enter product price:");
long ProductPrice = Convert.ToInt32(Console.ReadLine());

Console.WriteLine("Enter down payment:");
long DownPayment = Convert.ToInt32(Console.ReadLine());

Console.WriteLine("Enter loan tenure:");
int Tenure = Convert.ToInt32(Console.ReadLine());

int InterestPercentage = 8;

Console.WriteLine(Emi.EmiCalculate(ProductPrice,DownPayment,Tenure,InterestPercentage));

