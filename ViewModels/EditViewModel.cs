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
    public RelayCommand SaveExpense => new RelayCommand(execute => { saveExpense();} , canExecute => { return true; });
    
    private void saveExpense()
    {
        if (ValidateExpense())
        {
            Expense expense = new Expense(Name, CreatedDate, Amount, Type);
            DatabaseService.InsertExpense(expense);
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

    /*public EditViewModel(int id)
    {
        Id = id;
      //  Expense temp = DatabaseService.GetExpenseById(Id);
        Name = temp.Name;
        CreatedDate = temp.CreatedDate;
        Amount = temp.Amount;
        Type = temp.Type;
    } */
  

    private Boolean ValidateExpense()
    {
        if (Amount < 0) return false;
        return true;
    }
}