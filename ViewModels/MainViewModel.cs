using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

        public MainViewModel()
        {
            Expenses = new ObservableCollection<Expense>
            {
                new("Food", DateTime.Now, 5231231, Type.Expense),
                new("Salary", DateTime.Now, 600000, Type.Income)
            };
        }
    }
}
