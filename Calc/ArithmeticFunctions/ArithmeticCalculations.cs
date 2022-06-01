namespace ArithmeticComputation
{
    public class ArithmeticCalculations
    {
        
        public static int Target(int num1, string mathematicalOperator,int num2) { 
switch (mathematicalOperator)
{
    case "+":
         return Sum(num1, num2);
    case "-":
         return Difference(num1, num2);
    case "*":
        return Product(num1, num2);
    case "/":
        return Division(num1, num2);
    case "%":
        return Modulus(num1, num2);
                default:
                    throw new Exception("Enter arithmetic operator only!");
            }
        }
        public static int Sum(int num1,int  num2)
        {
            return num1 + num2;
        }
        public static int Difference(int num1, int num2)
        {
            return num1 - num2;
        }
        public static int Product(int num1,int num2)
        {
            return num1 * num2;
        }
        public static int Division(int num1,int num2)
        {
            return num1 / num2;
        }
        public static int Modulus(int num1,int num2)
        {
            return num1 % num2;
        }
    }
}