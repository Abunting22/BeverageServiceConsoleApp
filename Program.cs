using BeverageServiceConsoleApp;
using System.Runtime.CompilerServices;

public class Program
{
    private static readonly BetOperations betService = new();
    private static readonly Bet bet = new();
    private static readonly Login login = new();
    public static void Main(string[] args)
    {
        Login();
    }

    public static void Login()
    {
        Console.WriteLine("----- Welcome to Bev Buddy -----");
        Console.WriteLine("1. Login\n2. Create Account\n");
        var userConsoleInput = int.Parse(Console.ReadLine());
        switch (userConsoleInput)
        {
            case 1:
                login.AccountLogin();
                break;
            case 2:
                BeverageServiceConsoleApp.Login.CreateNewAccount();
                login.AccountLogin();
                break;
            default:
                Console.WriteLine("Bottoms Up!");
                break;
        }
    }

    public static void HomePage()
    {
        Console.WriteLine("----- Welcome to Bev Buddy -----");
        Console.WriteLine("Main Menu:\n1. Create New Bet\n2. View Bets\n3. Search Bet\n4. Update a Bet\n5. Delete a Bet\n6. Exit");

        var userConsoleInput = int.Parse(Console.ReadLine()!);
        switch (userConsoleInput)
        {
            case 1:
                betService.CreateNewBet(bet);
                HomePage();
                break;
            case 2:
                betService.ViewAllBets();
                HomePage();
                break;
            case 3:
                betService.SearchBet();
                HomePage();
                break;
            case 4:
                betService.UpdateBet(bet);
                HomePage();
                break;
            case 5:
                betService.DeleteBet();
                HomePage();
                break;
            default:
                Console.WriteLine("Bottoms Up!\n");
                Login();
                break;        
        }
    }
}
