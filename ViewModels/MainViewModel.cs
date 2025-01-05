using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using expense.Database;
using expense.Models;
using expense.Views;

namespace expense.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ObservableCollection<Expense> Expenses { get; set; }
        public RelayCommand AddExpense => new RelayCommand(execute => { AddItem();} , canExecute => { return true; });
        
        public MainViewModel()
        {
            Expenses = DatabaseService.GetExpenses();
        }

        private void AddItem()
        {
            var vm = new AddViewModel();
            AddWindow addWindow = new AddWindow
            {
                DataContext = vm
            };
            
            vm.OnRequestClose += (s, e) =>
            {
                ReloadExpense(); 
                addWindow.Close();
                
            };
            addWindow.ShowDialog();
        }
        private void ReloadExpense()
        {
            Expenses.Clear();  
            var updatedExpenses = DatabaseService.GetExpenses();    // Reassign will not update the UI
            foreach (var expense in updatedExpenses)
            {
                Expenses.Add(expense);
            }
        }
    }
}
