using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Diagnostics;

namespace BeverageServiceConsoleApp
{
    public class SQLDatabaseOperations
    {
        private static readonly string databaseConnectionString = "Data Source=DESKTOP-4OBEQSQ;Initial Catalog=BevBuddyDB;Integrated Security=True";
        
        private static readonly SqlConnection sqlConnection = new SqlConnection(databaseConnectionString);

        public static void GetBet(Bet bet)
        {
            var bettorname = bet.BettorName;
            var wager = bet.Wager;
        }

        public static void AddNewBetToDatabase(string bettorName, int wager)
        {        
            sqlConnection.Open();

            string insertQuery = "INSERT INTO Wagers (bettor_name, wager_value) VALUES (@BettorName, @Wager)";

            using SqlCommand command = new(insertQuery, sqlConnection);

            command.Parameters.AddWithValue("@BettorName", bettorName);
            command.Parameters.AddWithValue("@Wager", wager);

            int executeQuery = command.ExecuteNonQuery();
            Console.WriteLine($"{executeQuery} bet added\nPress enter to return to Main Menu...\n");
            Console.ReadLine();

            sqlConnection.Close();
        }

        public static void ViewAllBetsInDatabase()
        {
            sqlConnection.Open();

            string viewQuery = "SELECT * FROM Wagers";

            using SqlCommand command = new(viewQuery, sqlConnection);

            Console.WriteLine($"Viewing all bets\n");
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read()) 
            {
                string bettorName = reader["bettor_name"].ToString();
                string wagerValue = reader["wager_value"].ToString();

                Console.WriteLine($"Bettor: {bettorName}, Wager: {wagerValue}");
            }

            Console.WriteLine("Press enter to return to Main Menu...");
            Console.ReadLine();

            sqlConnection.Close();
        }

        public static void UpdateBetInDatabase()
        {
            sqlConnection.Open();

            Console.WriteLine("Would you like to change a:\n Name or Wager?");
            var userColumnChoice = Console.ReadLine().ToLower();

            if (userColumnChoice == "name")
            {
                Console.WriteLine("What is the current name that you would like to change?");
                var currentValue = Console.ReadLine();
                Console.WriteLine("What should the name be?");
                var newUserValue = Console.ReadLine();

                var condition = $"bettor_name = {currentValue}";
                string updateQuery = $"UPDATE Wagers\nSET bettor_name = @NewValue Where {condition}";

                using SqlCommand command = new(updateQuery, sqlConnection);

                command.Parameters.AddWithValue("@NewValue", newUserValue);

                var executeQuery = command.ExecuteNonQuery();
                Console.WriteLine($"{executeQuery} bets updated.\nPress enter to return to Main Menu...");
                Console.ReadLine();
            }
            else if (userColumnChoice == "wager")
            {
                Console.WriteLine("What is the current wager that you would like to change?");
                var currentValue = Console.ReadLine();
                Console.WriteLine("What should the wager be?");
                var newUserValue = Console.ReadLine();

                var condition = $"wager_value = {currentValue}";
                string updateQuery = $"UPDATE Wagers\nSET wager_value = @NewValue Where {condition}";

                using SqlCommand command = new(updateQuery, sqlConnection);

                command.Parameters.AddWithValue("@NewValue", newUserValue);

                var executeQuery = command.ExecuteNonQuery();
                Console.WriteLine($"{executeQuery} bets updated.\nPress enter to return to Main Menu...");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Press enter to return to Main Menu...");
                Console.ReadLine();
            }

            sqlConnection.Close();
        }

        public static void DeleteBetInDatabase() 
        {
            sqlConnection.Open();

            Console.WriteLine("Type the name of the bet you would like to delete\n");
            string condition = Console.ReadLine();
            string deleteQuery = $"DELETE FROM Wagers WHERE bettor_name = '{condition}'";

            using SqlCommand command = new(deleteQuery, sqlConnection);

            int executeQuery = command.ExecuteNonQuery();
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
                string deleteAllQuery = "DELETE FROM Wagers";
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
