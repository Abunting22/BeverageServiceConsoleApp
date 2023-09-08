using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BeverageServiceConsoleApp
{
    public class Login
    {
        private static readonly string databaseConnectionString = "Data Source=DESKTOP-4OBEQSQ;Initial Catalog=BevBuddyDB; Integrated Security=True";
        private static readonly SqlConnection sqlConnection = new(databaseConnectionString);
        private static readonly AccountCreator newAccount = new();

        public static AccountCreator CreateNewAccount()
        {
            sqlConnection.Open();

            Console.WriteLine("----- Create Account -----");
            Console.WriteLine("First name: ");
            newAccount.FirstName = Console.ReadLine();
            Console.WriteLine("Last name: ");
            newAccount.LastName = Console.ReadLine();
            Console.WriteLine("Username: ");
            newAccount.Username = Console.ReadLine();
            Console.WriteLine("Password: ");
            newAccount.Password = Console.ReadLine();

            var insertQuery = $"INSERT INTO UserAccounts (first_name, last_name, username, password) VALUES ('{newAccount.FirstName}', '{newAccount.LastName}', '{newAccount.Username}', '{newAccount.Password}')";

            SqlCommand command = new(insertQuery, sqlConnection);

            var executeQuery = command.ExecuteNonQuery();

            sqlConnection.Close();

            Console.WriteLine($"{executeQuery} account created.\nPress enter to continue...");
            Console.ReadLine();

            return newAccount;
        }

        public void AccountLogin()
        {
            Console.WriteLine("----- Account Login -----");
            Console.WriteLine("Username: ");
            var username = Console.ReadLine();
            Console.WriteLine("Password: ");
            var password = Console.ReadLine();

            bool isAuthenticated = AccountAuthenticator(username, password);

            if (isAuthenticated == true)
            {
                Program.HomePage();
            }
            else
            {
                Console.WriteLine("Invalid Username or Password. Would you like to try again(1) or create an account(2)?");
                var userConsoleInput = int.Parse(Console.ReadLine());
                switch (userConsoleInput)
                {
                    case 1:
                        AccountLogin();
                        break;
                    case 2:
                        CreateNewAccount();
                        break;
                    default:
                        Console.WriteLine("Bottoms Up!");
                        break;
                }
            }
        }
        public static bool AccountAuthenticator(string username, string password)
        {
            sqlConnection.Open();

            var searchQuery = $"SELECT COUNT(*) FROM UserAccounts WHERE username = '{username}' AND password = '{password}'";

            SqlCommand command = new(searchQuery, sqlConnection);

            var executeQuery = (int)command.ExecuteScalar();

            sqlConnection.Close();

            return executeQuery > 0;
        }
    }
}
