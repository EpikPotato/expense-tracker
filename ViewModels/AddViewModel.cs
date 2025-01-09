using System.ComponentModel;
using System.Windows;
using expense.Database;
using expense.Models;
using MessageBox = Xceed.Wpf.Toolkit.MessageBox;

namespace expense.ViewModels;

public class AddViewModel 
{
    public event EventHandler OnRequestClose;
    public string Name { get; set; }
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

  

    private Boolean ValidateExpense()
    {
        if (Amount < 0) return false;
        return true;
    }
}