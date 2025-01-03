using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
            Expenses = new ObservableCollection<Expense>();
        }

        private void AddItem()
        {
            AddWindow addWindow = new AddWindow();
            addWindow.Show();
            Expenses.Add(new Expense("Food" , DateTime.Now , 500000 , Type.Expense));
            Expenses.Add(new Expense("Salary" , DateTime.Now , 1100000 , Type.Income));

        }
    }
}
