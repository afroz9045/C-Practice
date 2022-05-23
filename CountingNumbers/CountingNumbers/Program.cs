// See https://aka.ms/new-console-template for more information

Console.WriteLine("Enter a number to get counting till it");
int number = Convert.ToInt32(Console.ReadLine());
Console.WriteLine("\n");

for(int i = 1; i <= number; i++)
{
    
    if (i % 2 == 0)
    {
        
        Console.WriteLine($"{i} is Even Number");
    }
    else
    {
        Console.WriteLine(i);
    }
}