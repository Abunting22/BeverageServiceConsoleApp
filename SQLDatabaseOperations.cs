using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Data.SqlTypes;
using System.Windows.Markup;
using System.Data.Common;
using System.Runtime.CompilerServices;

namespace BeverageServiceConsoleApp
{
    public class SQLDatabaseOperations
    {
        private static readonly string databaseConnectionString = "Data Source=DESKTOP-4OBEQSQ;Initial Catalog=BevBuddyDB;Integrated Security=True";
        
        private static readonly SqlConnection sqlConnection = new(databaseConnectionString);

        private static string NewBettorName { get; set; }
        private static string CurrentBettorName { get; set; }
        private static int NewWagerValue { get; set; }
        private static int CurrentWagerValue { get; set; }
        private static string NewBetDescription { get; set; }

        public static void SearchDatabase()
        {
            sqlConnection.Open();

            Console.WriteLine("Search by name: ");
            CurrentBettorName = Console.ReadLine();

            var searchQuery = $"SELECT * FROM Wagers WHERE bettor_name = '{CurrentBettorName}'";

            SqlCommand command = new(searchQuery, sqlConnection);

            SqlDataReader reader = command.ExecuteReader();
            while(reader.Read()) 
            {
                string bettorName = reader.GetString(0);
                int wagerValue = reader.GetInt32(1);

                Console.WriteLine($"{bettorName}| {wagerValue}");
            }

            Console.WriteLine("Press enter to return to Main Menu...");
            Console.ReadLine();

            sqlConnection.Close();
        }

        public static void AddNewBetToDatabase(Bet bet)
        {
            sqlConnection.Open();

            var insertQuery = $"INSERT INTO Wagers (bettor_name, wager_value, bet_description, wager_date) VALUES ('{bet.BettorName}', {bet.Wager}, '{bet.WagerDescription}', @WagerDate)";

            using SqlCommand command = new(insertQuery, sqlConnection);

            command.Parameters.AddWithValue("@WagerDate", bet.WagerDate.ToString());

            var executeQuery = command.ExecuteNonQuery();
            Console.WriteLine($"{executeQuery} bet added\nPress enter to return to Main Menu...\n");
            Console.ReadLine();

            sqlConnection.Close();
        }

        public static void ViewAllBetsInDatabase()
        {
            sqlConnection.Open();

            var viewQuery = "SELECT * FROM Wagers";

            using SqlCommand command = new(viewQuery, sqlConnection);

            Console.WriteLine($"Viewing all bets\n");
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var bettorName = reader["bettor_name"].ToString();
                var wagerValue = reader["wager_value"].ToString();
                var wagerDescription = reader["bet_description"].ToString();
                var wagerDate = reader["wager_date"].ToString();
                var betWinner = reader["bet_winner"].ToString();

                Console.WriteLine($"Bettor: {bettorName}| Wager: {wagerValue}| Description: {wagerDescription}| Winner: {betWinner}| Date: {wagerDate}");
            }

            Console.WriteLine("Press enter to return to Main Menu...");
            Console.ReadLine();

            sqlConnection.Close();
        }

        public static void UpdateBetByNameInDatabase()
        {
            sqlConnection.Open();

            Console.WriteLine("What is the current name of the bettor you would like to change?");
            CurrentBettorName = Console.ReadLine();
            Console.WriteLine("What is the new name of the bettor?");
            NewBettorName = Console.ReadLine();

            var updateQuery = $"UPDATE Wagers\nSET bettor_name = '{NewBettorName}'\nWHERE bettor_name = '{CurrentBettorName}'";

            using SqlCommand command = new(updateQuery, sqlConnection);

            command.ExecuteNonQuery();

            Console.WriteLine("Bet Updated\nPress enter to return to Main Menu...");
            Console.ReadLine();

            sqlConnection.Close();
        }

        public static void UpdateBetByWagerInDatabase() 
        {
            sqlConnection.Open();

            Console.WriteLine("What is the name of the bettor you would like to change the wager with?");
            CurrentBettorName = Console.ReadLine();
            Console.WriteLine("What is the current wager you would like to change?");
            CurrentWagerValue = int.Parse(Console.ReadLine());
            Console.WriteLine("What is the new wager?");
            NewWagerValue = int.Parse(Console.ReadLine());
            try
            {
                var updateQuery = $"UPDATE Wagers\nSET wager_value = {NewWagerValue}\nWHERE bettor_name = '{CurrentBettorName}' AND wager_value = {CurrentWagerValue}";

                using SqlCommand command = new(updateQuery, sqlConnection);

                command.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("Whoops! Party foul!\nEnter to continue...");
                UpdateBetByWagerInDatabase();
            }

            Console.WriteLine("Bet Updated\nPress enter to return to Main Menu...");
            Console.ReadLine();

            sqlConnection.Close();
        }

        public static void UpdateBetByDescriptionInDatabase()
        {
            sqlConnection.Open();

            Console.WriteLine("What is the current name of the bettor you would like to change the description of?");
            CurrentBettorName = Console.ReadLine();
            Console.WriteLine("What is current wager of the bet you would like to change the description of?");
            CurrentWagerValue = int.Parse(Console.ReadLine());
            Console.WriteLine("What is the new description");
            NewBetDescription = Console.ReadLine();

            var updateQuery = $"UPDATE Wagers\nSET bet_description = '{NewBetDescription}'\nWHERE bettor_name = '{CurrentBettorName}' AND wager_value = {CurrentWagerValue}";

            using SqlCommand command = new(updateQuery, sqlConnection);

            command.ExecuteNonQuery();

            Console.WriteLine("Bet Updated\nPress enter to return to Main Menu...");
            Console.ReadLine();

            sqlConnection.Close();
        }


        public static void UpdateBetByDeclaringWinner(Bet bet) //Re-work needed
        {
            sqlConnection.Open();

            Console.WriteLine("Who won?");
            bet.Winner = Console.ReadLine();

            var addQuery = $"UPDATE Wagers SET bet_winner = @BetWinner WHERE bettor_name = '{bet.Winner}'";
            SqlCommand command = new(addQuery, sqlConnection);

            command.Parameters.AddWithValue("@BetWinner", bet.Winner);

            command.ExecuteNonQuery();

            Console.WriteLine("Winner added.\nPress enter to return to Main Menu...");
            Console.ReadLine();

            sqlConnection.Close();
        }

        public static void DeleteBetInDatabase()
        {
            sqlConnection.Open();

            Console.WriteLine("What is the name of the bet you would like to delete\n");
            CurrentBettorName = Console.ReadLine();
            Console.WriteLine("What is the wager of the bet you would like to delete?");
            CurrentWagerValue = int.Parse(Console.ReadLine());

            var deleteQuery = $"DELETE FROM Wagers WHERE bettor_name = '{CurrentBettorName}' AND wager_value = {CurrentWagerValue}";

            using SqlCommand command = new(deleteQuery, sqlConnection);

            var executeQuery = command.ExecuteNonQuery();
            Console.WriteLine("Bet deleted\nPress enter to return to Main Menu...\n");
            Console.ReadLine();

            sqlConnection.Close();
        }

        public static void DeleteAllBetsInDatabase()
        {
            sqlConnection.Open();

            Console.WriteLine("Would you like to delete all bets?");
            var condition = Console.ReadLine().ToLower();
            if (condition == "yes")
            {
                var deleteAllQuery = "DELETE FROM Wagers";
                using SqlCommand command = new(deleteAllQuery, sqlConnection);
                command.ExecuteNonQuery();
                Console.WriteLine("All bets have been deleted.\nPress enter to return to Main Menu...");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Press enter to return to Main Menu...");
                Console.ReadLine();
            }

            sqlConnection.Close();
        }
    }
}
