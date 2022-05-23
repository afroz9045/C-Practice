//// See https://aka.ms/new-console-template for more information
using ArithmeticFunctions;

Console.WriteLine("Enter first number :");
int num1 = Convert.ToInt32(Console.ReadLine());



Console.WriteLine("Enter operator :");
string opr = Console.ReadLine();

Console.WriteLine("Enter second number :");
int num2 = Convert.ToInt32(Console.ReadLine());



switch (opr)
{
    case "+":
        var result = Class1.Sum(num1,num2);
        Console.WriteLine(result);
        break;
    case "-":
        result = Class1.Difference(num1, num2);
        Console.WriteLine(result);
        break;
    case "*":
        result = Class1.Product(num1, num2);
        Console.WriteLine(result);
        break;
    case "/":
        result = Class1.Division(num1, num2);
        Console.WriteLine(result);
        break;
    case "%":
        result = Class1.Modulus(num1, num2);
        Console.WriteLine(result);
        break;
    default:
        Console.WriteLine("Invalid Operator");
        break;
}


