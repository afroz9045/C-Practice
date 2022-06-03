namespace FactorialLogic
{
    public class Factorial
    {
        public static double FactorialCalculation(int number)
        {
            double factorialResult = 1;
            while(number>0)
            {
                factorialResult *= number;
                number--;
            }
            return factorialResult;
        }
    }
}