using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using expense.Database;
using expense.Models;
using expense.Views;
using System.Collections.Generic;
using System.Linq;

namespace expense.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _selectedMonthYear;

        public event PropertyChangedEventHandler? PropertyChanged;
        public Expense? SelectedExpense{ get; set; }
        public List<string> MonthYearList { get; set; } 
        
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ObservableCollection<Expense> Expenses { get; set; }
        public RelayCommand AddExpense => new RelayCommand(execute => { AddItem();} , canExecute => { return true; });
        public RelayCommand EditExpense => new RelayCommand(execute => {EditItem();} , canExecute => { return true; });
        public RelayCommand ShowStatistics => new RelayCommand(execute => {OpenStatisticsWindow();} , canExecute => { return true; });

        public string SelectedMonthYear
        {
            get => _selectedMonthYear;

            set
            {
                if (_selectedMonthYear != value)
                {
                    _selectedMonthYear = value;
                    OnPropertyChanged(nameof(SelectedMonthYear));
                    OnSelectionChanged(); // Handle the selection change
                }
            }
        }
        
        private void OnSelectionChanged()
        
        {
            Console.WriteLine("OnSelectionChanged");

            var filteredExpenses = FilterExpenses();
          
            Expenses.Clear();
            foreach (var expense in filteredExpenses)
            {
                Expenses.Add(expense);
            }
        }

        private List<Expense> FilterExpenses()
        {
            List<Expense> result = new List<Expense>();
            result = DatabaseService.GetExpenses().Where(e => e.CreatedDate.ToString("MM/yyyy") == SelectedMonthYear)
                .ToList();
            return result;
        }
        public MainViewModel()
        {
            Expenses = DatabaseService.GetExpenses();
    
           
            MonthYearList = Expenses
                .Select(e => e.CreatedDate.ToString("MM/yyyy"))  
                .Distinct()  
                .OrderBy(date => date)  
                .ToList();  
            
        }

        private void OpenStatisticsWindow()
        {
            var vm = new StatisticsViewModel(FilterExpenses());
            StatisticsWindow statisticsWindow = new StatisticsWindow();
            statisticsWindow.DataContext = vm;
            
            
            statisticsWindow.ShowDialog();
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

        private void EditItem()
        {
            if (SelectedExpense == null) return;

            
            var vm = new EditViewModel(SelectedExpense);
            
            
            EditWindow editWindow = new EditWindow
            {
                DataContext = vm, 
                
            };
            vm.OnRequestClose += (s, e) =>
            {
                ReloadExpense();
                editWindow.Close();
            };
            editWindow.ShowDialog();

        }
        private void ReloadExpense()
        {
            Expenses.Clear();  
            var updatedExpenses = DatabaseService.GetExpenses();    // Reassign will not update the UI
            foreach (var expense in updatedExpenses)
            {
                Expenses.Add(expense);
            }
            MonthYearList = Expenses
                .Select(e => e.CreatedDate.ToString("MM/yyyy"))  
                .Distinct()  
                .OrderBy(date => date)  
                .ToList();  
            OnPropertyChanged(nameof(MonthYearList));
        }
    }
}
