namespace TemperatureUnitConversion
{
    public class TemperatureConversion
    {

        static string result;
        public static string TemperatureConvert(float temperature, int choice)
        {
            switch (choice)
            {
                case 1:
                    result = Convert.ToString((temperature * 9) / 5 + 32);
                    result += "F";
                    break;
                case 2:
                    result = Convert.ToString((temperature - 32) * 5 / 9);
                    result += "C";
                    break;
                default:
                    Console.WriteLine("Invalid choice!");
                    break;
            }
            return TemperatureConversion.result;
        }
    }
}