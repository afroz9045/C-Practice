namespace FactorialLogic
{
    public class Factorial
    {
        public static int FactorialCalculation(int number)
        {
            int factorialResult = 1;
            while(number>0)
            {
                factorialResult *= number;
                number--;
            }
            return factorialResult;
        }
    }
}