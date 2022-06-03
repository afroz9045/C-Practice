// See https://aka.ms/new-console-template for more information
using FactorialLogic;

Console.WriteLine("Enter a number to get factorial of it: ");
int factorialNumber = Convert.ToInt32(Console.ReadLine());


Console.WriteLine($"Factorial of {factorialNumber} is: {Factorial.FactorialCalculation(factorialNumber)}");