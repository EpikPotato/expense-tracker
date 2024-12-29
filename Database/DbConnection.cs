using System;
using System.Data.SqlClient;
using System.Data.SQLite;
using Xceed.Wpf.Toolkit.Primitives;


class DbConnection
{
    public static void CreateConnection()
    {

        using (var connection = new SQLiteConnection("Data Source=expense.db"))
        {

            connection.Open();
            string commandString = @"
                CREATE TABLE IF NOT EXISTS Expenses (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    CreatedDate TEXT NOT NULL,
                    Amount REAL NOT NULL
                );";

            Console.WriteLine("Database connection established.");
            using (var command = new SQLiteCommand(commandString, connection))
            {
                command.ExecuteNonQuery();
            }


        }
    }
}