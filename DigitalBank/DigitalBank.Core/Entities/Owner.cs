namespace DigitalBank.Core.Entities
{
   public record struct Owner(string FirstName, string LastName)
   {
      public string FullName => $"{FirstName} {LastName}";
   }
}
