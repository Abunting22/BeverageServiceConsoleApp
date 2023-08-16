using BeverageServiceConsoleApp;
using System.Net.WebSockets;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Bev Service Buddy\nMake a bet!");
        Bet bet = new Bet();
        bet.CreateNewBet();
        bet.BetWinner();
        Console.WriteLine("Bottoms up!");

        Console.Read();
    }
}