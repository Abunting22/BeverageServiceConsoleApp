using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BeverageServiceConsoleApp
{
    public class Bet
    {
        private string BettorOne { get; set; }
        private string BettorTwo { get; set; }
        private int Wager { get; set; }
        private string Winner { get; set; }

        public void CreateNewBet()
        {
            Console.WriteLine("Who is making the bet?\nBettor 1. ");
            BettorOne = Console.ReadLine();
            Console.WriteLine("Bettor 2. ");
            BettorTwo = Console.ReadLine();
            Console.WriteLine("What is the wager?");
            Wager = int.Parse(Console.ReadLine());
            Console.WriteLine("{0} has bet {1} {2} drinks!", BettorOne, BettorTwo, Wager);
        }

        public void BetWinner()
        {
            Console.WriteLine("Who won?");
            Winner = Console.ReadLine();
            if (Winner == BettorOne)
            {
                Console.WriteLine("{0} won! They won {1} drinks from {2}!", BettorOne, Wager, BettorTwo);
            }
            else 
            {
                Console.WriteLine("{0} won! They won {1} drinks from {2}!", BettorTwo, Wager, BettorTwo);
            }
        }
    }
}
