using System.Net.WebSockets;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Bev Service Buddy\nMake a bet!");
        Console.WriteLine("Bettor 1: ");
        var bettorOne = Console.ReadLine();
        Console.WriteLine("Bettor 2: ");
        var bettorTwo = Console.ReadLine();
        Console.WriteLine("How many beverages are at stake?");
        int numberOfBeverages = int.Parse(Console.ReadLine());
        Console.WriteLine("{0} and {1} have bet {2} beverages!", bettorOne, bettorTwo, numberOfBeverages);

        Console.WriteLine("Who won?");
        var winner = Console.ReadLine();
        if (winner == bettorOne)
        {
            Console.WriteLine("{0} won! They won {1} beverages from {2}.", bettorOne, numberOfBeverages, bettorTwo);
        }
        else
        {
            Console.WriteLine("{0} won! They won {1} beverages from {2}.", bettorTwo, numberOfBeverages, bettorOne);
        }

        Console.WriteLine("Thanks for playing!\nBottoms Up!");
        Console.Read();
    }
}