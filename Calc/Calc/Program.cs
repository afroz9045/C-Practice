//// See https://aka.ms/new-console-template for more information
using ArithmeticFunctions;

Console.WriteLine("Enter first number :");
int num1 = Convert.ToInt32(Console.ReadLine());



Console.WriteLine("Enter operator :");
string mathematicalOperator = Console.ReadLine();

Console.WriteLine("Enter second number :");
int num2 = Convert.ToInt32(Console.ReadLine());

Console.WriteLine($"result is: {ArithmeticCalculations.Target(num1, mathematicalOperator, num2)}");



