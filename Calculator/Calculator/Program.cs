using Calculator.Core;

Console.WriteLine("Enter first number: ");
int num1 = Convert.ToInt32(Console.ReadLine());

Console.WriteLine("Enter second number: ");
int num2 = Convert.ToInt32(Console.ReadLine());

Console.WriteLine("Enter operator: (+,_-,*,/)");
string op = Console.ReadLine();

if (op == "+")
{
    Console.WriteLine(CalculatorLogic.Adition(num1, num2));
}
else if(op == "-")
{
    Console.WriteLine(CalculatorLogic.Subtraction(num1,num2));
}
else if (op=="*")
{
    Console.WriteLine(CalculatorLogic.Multiplication(num1, num2));
}
else if(op == "/")
{
    Console.WriteLine(CalculatorLogic.Division(num1,num2));
}
else
{
    Console.WriteLine("Invalid operator!");
}
