namespace Banking { 
public class Accounts
{
    public int[] index;
    public string Acc_no = "", C_name = "", C_add = "";
    public double Balance = 0;
    int Age;
    public Accounts() { }
    public Accounts(String Acc_no, string C_name, int Age, String C_add, double Balance)
    {
        this.Acc_no = Acc_no;
        this.C_name = C_name;
        this.C_add = C_add;
        this.Balance = Balance;
    }

    public int Create_Acc()
    {
        try
        {
            Console.Write("Enter The Account Number:\t");
            Acc_no = Console.ReadLine();
            if (Acc_no == null)
                throw new("You must enter the Account number!");

            Console.Write("Enter The Customer Name:\t");
            C_name = Console.ReadLine();
            if (C_name == null)
                throw new("You must enter the name!");

            Console.Write("Age:\t\t\t\t");
            Age = int.Parse(Console.ReadLine());
            if (Age <= 0)
                throw new("You must enter the age!");

            Console.Write("Enter The Address:\t\t");
            C_add = Console.ReadLine();
            if (C_add == null)
                throw new("You must enter the address");

            Console.Write("Deposite amount:\t\t");
            Balance = double.Parse(Console.ReadLine());
            if (Balance <= 0)
                throw new("You must enter the Deposit amount");
        }
        catch (Exception e)
        {
            Console.Write(e.Message);
        }

        return 1;

    }

    public void Acc_Availability(string Acc_num)
    {
        if (Acc_no.Equals(Acc_num))
        {
            Console.WriteLine("-----------------------------****************-----------------------------");
            Console.WriteLine("Account Number:\t" + Acc_no);
            Console.WriteLine("Name:\t\t" + C_name);
            Console.WriteLine("Age:\t\t" + Age);
            Console.WriteLine("Address:\t" + C_add);
            Console.WriteLine("Balance: \t$" + Balance);
            Console.WriteLine("-----------------------------****************-----------------------------");
        }
        else
        {
            Console.WriteLine("Account does not exist!");
        }
    }
    public void Deposite(string Acc_num)
    {
        try
        {


            if (Acc_no.Equals(Acc_num))
            {
                Console.Write("Enter the amount:\t\t");
                int Amount = int.Parse(Console.ReadLine());
                if (Amount <= 0)
                    throw new("Amount must be larger than $0");
                else
                    this.Balance = Balance + Amount;

                Console.WriteLine("-----------------------------****************-----------------------------");
                Console.WriteLine("Balance is:  $" + Balance);
                Console.WriteLine("-----------------------------****************-----------------------------");
            }
            else
            {
                Console.WriteLine("Account does not exist!");
            }
        }
        catch (Exception e)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(e.Message);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
    public void Withdraw(string Acc_num)
    {
        if (Acc_no.Equals(Acc_num))
        {
            Console.Write("Enter the amount:\t\t");
            int Amount = int.Parse(Console.ReadLine());
            if (Balance == 0)
            {
                Console.WriteLine("Insufficient balance");

            }
            else if (Amount > Balance)
            {
                Console.WriteLine("Insufficient balance");
            }
            else
            {
                Balance = Balance - Amount;
                Console.WriteLine("Balance: $" + Balance);
            }
        }
        else
        {
            Console.WriteLine("Account does not exist!");
        }
    }
    public void Balenquiry()
    {
        Console.WriteLine("Your balance is: " + Balance);
    }

}
}