namespace ProjectManagementSystem.Infrastructure.Validations
{
    public static class Validations
    {
        public static bool isContinueResult;
        public static bool IsContinue()
        {
            Console.WriteLine("Do you want to continue...? enter 'Y' for yes/ any key for no");
            var selectedChar = Convert.ToChar(Console.ReadLine());
            if (selectedChar == 'Y' || selectedChar == 'y')
            {
                isContinueResult = true;
            }
            else
            {
                isContinueResult = false;
            }
            return isContinueResult;
        }
        public static bool validate<T>(IEnumerable<T> collection)
        {
            if (collection.Any())
            {
                return true;
            }
            return false;
        }
    }
}
