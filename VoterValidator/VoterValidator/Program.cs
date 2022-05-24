// See https://aka.ms/new-console-template for more information

Console.WriteLine("Enter your age: ");
int age = Convert.ToInt32(Console.ReadLine());

Console.WriteLine("Enter your nationality");
string nationality = Console.ReadLine();

if(age>=18 && (nationality == "india" || nationality == "indian"))
{
    Console.WriteLine("You Are eligible to vote");
}
else
{
    Console.WriteLine("Not eligible to vote");
}