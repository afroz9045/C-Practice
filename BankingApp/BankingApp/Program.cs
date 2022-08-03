using Banking;
String Acc_num, ch;
bool programExecution = true;

Accounts Acc = new Accounts();

while(programExecution)
{
    Console.WriteLine("----------------------------------------------------------------------");
    Console.WriteLine("1.New Account\t2.Enquiry\t3.Deposit\t4.Withdraw\t5.Exit");
    Console.WriteLine("----------------------------------------------------------------------");
    ch = Console.ReadLine();

    switch (ch)
    {
        case "1":
            if (Acc.Create_Acc() == 1)
            {
                Console.WriteLine($"Account number \"{Acc.Acc_no}\" Created Successfuly");
            }
            else
            {
                Console.WriteLine("Account could not be created! Try again.");
            }

            break;

        case "2":

            Console.Write("Enter the Account Number:\t");
            Acc_num = Console.ReadLine();
            Acc.Acc_Availability(Acc_num);
            break;

        case "3":

            Console.Write("Enter the Account Number:\t");
            Acc_num = Console.ReadLine();
            Acc.Deposite(Acc_num);
            break;

        case "4":

            Console.Write("Enter The Customer Account Number:  ");
            Acc_num = Console.ReadLine();
            Acc.Withdraw(Acc_num);
            break;

        case "5":

            programExecution = false;
            break;

        default:

            Console.WriteLine("invalid choice!");
            break;
    }
}