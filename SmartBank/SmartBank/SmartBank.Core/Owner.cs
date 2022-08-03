namespace SmartBank.Core
{
    public struct Owner(string FirstName,string LastName)
    {
        public string FullName => $"{FirstName} {LastName}";
    }
}
