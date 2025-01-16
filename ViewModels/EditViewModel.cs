using System.Windows;
using expense.Database;
using expense.Models;

namespace expense.ViewModels;

public class EditViewModel
{
    public event EventHandler OnRequestClose;
    public string Name { get; set; }
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }

    public double Amount {get; set;}
    public Type Type { get; set; }
    
    public List<Type> Types => new List<Type> { Type.Expense, Type.Income }; // ComboBox Data
    public RelayCommand EditExpense => new RelayCommand(execute => { SaveExpense();} , canExecute => { return true; });
    
    private void SaveExpense()
    {
        if (ValidateExpense())
        {
            Expense expense = new Expense(Id,Name, CreatedDate, Amount, Type);
            DatabaseService.EditExpense(expense);
            OnRequestClose?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            // Display the alert box
            MessageBox.Show("Please make sure all fields are filled correctly.", 
                "Validation Error", 
                MessageBoxButton.OK, 
                MessageBoxImage.Error);
        }
    }

    public EditViewModel(Expense selectedExpense)
    { 
        Id = selectedExpense.Id;
        Name = selectedExpense.Name;
        CreatedDate = selectedExpense.CreatedDate;
        Amount = selectedExpense.Amount;
        Type = selectedExpense.Type;

        Console.WriteLine("ID: " + Id);
    }
  

    private Boolean ValidateExpense()
    {
        if (Amount < 0) return false;
        return true;
    }
}