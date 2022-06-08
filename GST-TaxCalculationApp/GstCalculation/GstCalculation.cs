namespace GstCalculationLogic
{
    public class GstCalculation
    {
        static float result;
        public static float TaxCalculation(float productPrice, int selection)
        {
            int cgst = 9;
            int sgst = 9;
            int igst = 18;
            try
            {
                switch (selection)
                {

                    case 1:
                        result = (productPrice / 100)*(cgst + sgst);
                        break;
                    case 2:
                        result = (productPrice / 100)* (igst);
                        break;
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }
            return result;
        }
    }
}