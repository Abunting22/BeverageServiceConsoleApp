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
        public string BettorName { get; set; }
        public int Wager { get; set; }
        public string Winner { get; set; }
        public string WagerDescription { get; set; }
        public DateOnly WagerDate { get; set; }
    }
}
