using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace BeverageServiceConsoleApp
{
    public class BetOperations
    {
        public Bet CreateNewBet(Bet bet)
        {
            var newBet = new Bet();
            Console.WriteLine("Who are you making the bet with?");
            bet.BettorName = Console.ReadLine();
            Console.WriteLine("How many drinks are being wagered?");
            bet.Wager = int.Parse(Console.ReadLine());
            Console.WriteLine("What is the reason for the bet");
            bet.WagerDescription = Console.ReadLine();
            Console.WriteLine($"I bet {bet.BettorName} {bet.Wager} drinks!");
            bet.WagerDate = DateOnly.FromDateTime(DateTime.Now);

            SQLDatabaseOperations.AddNewBetToDatabase(bet);

            return newBet;
        }

        public void ViewAllBets()
        {
            SQLDatabaseOperations.ViewAllBetsInDatabase();
        }

        public void SearchBet()
        {
            SQLDatabaseOperations.SearchDatabase();
        }

        public void UpdateBet(Bet bet)
        {
            Console.WriteLine("What would you like to update?\n1. Name\n2. Wager\n3. Description\n4. Declare Winner\n5. Return to Main Menu");
            var userConsoleInput = int.Parse(Console.ReadLine());

            switch (userConsoleInput)
            {
                case 1:
                    {
                        SQLDatabaseOperations.UpdateBetByNameInDatabase();
                        break;
                    }
                case 2: 
                    {
                        SQLDatabaseOperations.UpdateBetByWagerInDatabase();
                        break;
                    }
                case 3: 
                    {
                        SQLDatabaseOperations.UpdateBetByDescriptionInDatabase();
                        break;
                    }
                case 4:
                    {
                        SQLDatabaseOperations.UpdateBetByDeclaringWinner(bet);
                        break;
                    }
            }
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
