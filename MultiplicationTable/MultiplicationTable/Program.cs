// See https://aka.ms/new-console-template for more information

Console.WriteLine("Enter a Number to get the multiplication table of it.");
int num = Convert.ToInt32(Console.ReadLine());

for(int i = 1; i <= 10; i++)
{
    Console.WriteLine($"{num}*{i}={num * i}");
}
