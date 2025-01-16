using System.Collections.ObjectModel;
using System.Data.SQLite;
using expense.Models;

namespace expense.Database
{
    public class DatabaseService
    {
        public static ObservableCollection<Expense> GetExpenses()
        {

            ObservableCollection<Expense> expenses = new ObservableCollection<Expense>();

            using (var connection = DbConnection.CreateConnection())
            {
                string query = "SELECT Id , Name, CreatedDate, Amount, Type FROM Expenses";

                using (var command = new SQLiteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            DateTime createdDate = reader.GetDateTime(2);
                            Type type = reader.GetString(4) == "Income" ? Type.Income : Type.Expense;
                            double amount = reader.GetDouble(3);


                            var expense = new Expense(id, name, createdDate, amount, type);
                            expenses.Add(expense);
                        }
                    }
                }
            }

            return expenses;
        }

        public static void InsertExpense(Expense expense)
        {
            using (var connection = DbConnection.CreateConnection())
            {
                string query =
                    "INSERT INTO Expenses (Name, CreatedDate, Amount, Type) VALUES (@Name, @CreatedDate, @Amount, @Type)";

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", expense.Name);
                    command.Parameters.AddWithValue("@CreatedDate", expense.CreatedDate);
                    command.Parameters.AddWithValue("@Amount", expense.Amount);
                    command.Parameters.AddWithValue("@Type", expense.Type.ToString());

                    command.ExecuteNonQuery();
                }
            }
        }
        public static void EditExpense(Expense expense)
        {
            using (var connection = DbConnection.CreateConnection())
            {
                string query =
                    "UPDATE Expenses Set Name = @Name, CreatedDate = @CreatedDate, Amount = @Amount , Type = @Type WHERE Id = @Id";

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", expense.Name);
                    command.Parameters.AddWithValue("@CreatedDate", expense.CreatedDate);
                    command.Parameters.AddWithValue("@Amount", expense.Amount);
                    command.Parameters.AddWithValue("@Type", expense.Type.ToString());
                    command.Parameters.AddWithValue("@Id", expense.Id);
                    command.ExecuteNonQuery();
                }
            }
        }
        public static Expense GetExpenseById(int id)
        {
            Expense expense = null;
            using (var connection = DbConnection.CreateConnection())
            {
                string query = "SELECT  Name, CreatedDate, Amount, Type FROM Expenses WHERE Id = @Id";

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string name = reader.GetString(0);
                            DateTime createdDate = reader.GetDateTime(1);
                            Type type = reader.GetString(3) == "Income" ? Type.Income : Type.Expense;
                            double amount = reader.GetDouble(2);


                            expense = new Expense(id, name, createdDate, amount, type);
                            
                        }
                    }
                }

            }
            Console.WriteLine("Done ");
            return expense;
        }
    }
}