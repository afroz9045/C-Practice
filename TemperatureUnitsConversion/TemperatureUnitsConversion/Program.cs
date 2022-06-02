// See https://aka.ms/new-console-template for more information
using TemperatureUnitConversion;
Console.WriteLine("Enter 1 to convert from celsius to fahrenheit");
Console.WriteLine("Enter 2 to convert from fahrenheit to celsius");

Console.WriteLine("Enter your choice:");
int choice = Convert.ToInt32(Console.ReadLine());

Console.WriteLine("Enter the temperature either in celsius or fahrenheit:");
float userTemperature = Convert.ToInt32(Console.ReadLine());





Console.WriteLine(TemperatureConversion.TemperatureConvert(userTemperature, choice));