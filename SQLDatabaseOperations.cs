using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Data.SqlTypes;

namespace BeverageServiceConsoleApp
{
    public class SQLDatabaseOperations
    {
        private static readonly string databaseConnectionString = "Data Source=DESKTOP-4OBEQSQ;Initial Catalog=BevBuddyDB;Integrated Security=True";
        
        private static readonly SqlConnection sqlConnection = new SqlConnection(databaseConnectionString);

        public static void AddNewBetToDatabase(Bet bet)
        {        
            sqlConnection.Open();

            var insertQuery = "INSERT INTO Wagers (bettor_name, wager_value, bet_description, wager_date) VALUES (@BettorName, @Wager, @BetDescription, @WagerDate)";

            using SqlCommand command = new(insertQuery, sqlConnection);

            command.Parameters.AddWithValue("@BettorName", bet.BettorName);
            command.Parameters.AddWithValue("@Wager", bet.Wager);
            command.Parameters.AddWithValue("@BetDescription", bet.WagerDescription);
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

            Console.WriteLine("What is the name and wager of the bet you would like to change?\nEx: Jon = 2\n");
            var condition = Console.ReadLine();

            Console.WriteLine("What should the new name be?");
            var newNameValue = Console.ReadLine();

            var updateQuery = $"UPDATE Wagers\nSET bettor_name = {newNameValue}\nWHERE {condition}";
            using SqlCommand command = new(updateQuery, sqlConnection);

            command.ExecuteNonQuery();
            Console.WriteLine("Bet updated. Press enter to return to Main Menu...");
            Console.ReadLine();

            sqlConnection.Close();
        }

        public static void UpdateBetByWagerInDatabase()
        {
            sqlConnection.Open();

            Console.WriteLine("What is the name and wager of the bet you would like to change?\nEx: Jon = 2\n");
            var condition = Console.ReadLine();

            Console.WriteLine("What should the new wager be?");
            var newWagerValue = Console.ReadLine();

            var updateQuery = $"UPDATE Wagers\nSET wager_value = {newWagerValue}\nWHERE {condition}";
            using SqlCommand command = new(updateQuery, sqlConnection);

            command.ExecuteNonQuery();
            Console.WriteLine("Bet updated. Press enter to return to Main Menu...");
            Console.ReadLine();

            sqlConnection.Close();
        }

        public static void UpdateBetByDescriptionInDatabase()
        {
            sqlConnection.Open();

            Console.WriteLine("What is the name and wager of the bet you would like to change?\nEx: Jon = 2\n");
            var condition = Console.ReadLine();

            Console.WriteLine("What should the new description be?");
            var newDescription = Console.ReadLine();

            var updateQuery = $"UPDATE Wagers\nSET bettor_name = {newDescription}\nWHERE {condition}";
            using SqlCommand command = new(updateQuery, sqlConnection);

            command.ExecuteNonQuery();
            Console.WriteLine("Bet updated. Press enter to return to Main Menu...");
            Console.ReadLine();

            sqlConnection.Close();
        }

        public static void UpdateBetByDeclaringWinner(Bet bet)
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

            Console.WriteLine("Type the name of the bet you would like to delete\n");
            var condition = Console.ReadLine();
            var deleteQuery = $"DELETE FROM Wagers WHERE bettor_name = '{condition}'";

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
