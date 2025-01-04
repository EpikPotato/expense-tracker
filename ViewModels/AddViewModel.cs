using System.ComponentModel;
using expense.Database;
using expense.Models;

namespace expense.ViewModels;

public class AddViewModel 
{
    public string Name { get; set; }
    public DateTime CreatedDate { get; set; }

    public double Amount {get; set;}
    public Type Type { get; set; }
    
    public List<Type> Types => new List<Type> { Type.Expense, Type.Income }; // ComboBox Data
    public RelayCommand SaveExpense => new RelayCommand(execute => { saveExpense();} , canExecute => { return true; });
    
    private void saveExpense()
    {
        Expense expense = new Expense(Name, CreatedDate, Amount, Type);
        DatabaseService.InsertExpense(expense);
    }
}