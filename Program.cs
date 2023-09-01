using BeverageServiceConsoleApp;
using System.Runtime.CompilerServices;

public class Program
{
    private static readonly BetService betService = new();
    private static readonly Bet bet = new();
    public static void Main(string[] args)
    {
        HomePage();
    }

    public static void HomePage()
    {
        Console.WriteLine("----- Welcome to Bev Buddy -----");
        Console.WriteLine("Main Menu:\n1. Create New Bet\n2. View Bets\n3. Update a Bet\n4. Delete a Bet\n5. Exit");

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
                betService.UpdateBet(bet);
                HomePage();
                break;
            case 4:
                betService.DeleteBet();
                HomePage();
                break;
            default:
                Console.WriteLine("Bottoms Up!");
                break;        
        }
               
    }
}
