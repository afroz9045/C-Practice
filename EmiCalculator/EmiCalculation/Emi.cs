namespace EmiCalculation
{
    public class Emi
    {
        public static string EmiCalculate(long productPrice,long downPayment,int tenure,int interestPercentage)
        {
            long loanAmount = productPrice - downPayment;
            long interestAmount = (loanAmount / 100) * interestPercentage;
            long emiAmount = loanAmount + interestAmount;
            long monthlyEmi = emiAmount / tenure;
            return $"Total EMI amount is {emiAmount}, monthly instalment is {monthlyEmi}";

        }
    }
}