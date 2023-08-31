using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace BeverageServiceConsoleApp
{
    public class BetService
    {
        public Bet CreateNewBet()
        {
            var bet = new Bet();
            Console.WriteLine("Who are you making the bet with?");
            bet.BettorName = Console.ReadLine();
            Console.WriteLine("What is the wager?");
            bet.Wager = int.Parse(Console.ReadLine());
            Console.WriteLine($"I bet {bet.BettorName} {bet.Wager} drinks!");

            SQLDatabaseOperations.AddNewBetToDatabase(bet.BettorName, bet.Wager);

            return bet;
        }

        //public void DeclareBetWinner(Bet bet)
        //{
        //    Console.WriteLine("Who won?");
        //    var winner = Console.ReadLine();

        //    if (winner == bet.BettorName)
        //    {
        //        Console.WriteLine($"{bet.BettorName} won!\nI owe {bet.BettorName} {bet.Wager} drinks!\nBottoms Up!");
        //    }
        //    else
        //    {
        //        Console.WriteLine($"I won!\nI won {bet.Wager} drinks from {bet.BettorName}!\nBottoms Up!");
        //    }
        //}

        public void ViewAllBets()
        {
            SQLDatabaseOperations.ViewAllBetsInDatabase();
        }

        public void UpdateBet()
        {
            SQLDatabaseOperations.UpdateBetInDatabase();
        }

        public void DeleteBet()
        {
            Console.WriteLine("1. Delete A Bet\n2. Delete All Bets");
            var userConsoleInput = int.Parse(Console.ReadLine());

            switch (userConsoleInput) 
            {
                case 1:
                    {
                        SQLDatabaseOperations.DeleteBetInDatabase();
                        break;
                    }
                case 2: 
                    {
                        SQLDatabaseOperations.DeleteAllBetsInDatabase();
                        break;
                    }
            }
        }
    }
}
