using System;
using System.Data.SQLite;

namespace expense.Database
{
    public class DbConnection
    {
        
        public static SQLiteConnection CreateConnection()
        {
           
            var connection = new SQLiteConnection("Data Source=expense.db");

            
            connection.Open();

            
            string commandString = @"
                CREATE TABLE IF NOT EXISTS Expenses (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    CreatedDate TEXT NOT NULL,
                    Amount REAL NOT NULL,
                    Type TEXT NOT NULL
                );";

            Console.WriteLine("Database connection established.");

            // Execute 
            using (var command = new SQLiteCommand(commandString, connection))
            {
                command.ExecuteNonQuery();
            }

            
            return connection;
        }
    }
}