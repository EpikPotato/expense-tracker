
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using expense.Models;

namespace expense.ViewModels
{
    public class StatisticsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        
       
      

        private void OnSelectionChanged()
        {
            
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public SeriesCollection IncomePieSeriesCollection { get; set; } 
        public SeriesCollection ExpensePieSeriesCollection { get; set; } 
        public SeriesCollection TotalPieSeriesCollection { get; set; } 
        
        public StatisticsViewModel(List<Expense> expenses)
        {
            IncomePieSeriesCollection = new SeriesCollection();
            ExpensePieSeriesCollection  = new SeriesCollection();
            TotalPieSeriesCollection  = new SeriesCollection();
            double TotalIncome = 0;
            double TotalExpense = 0;
            foreach (var item in expenses)
            {
                if (item.Type == Type.Income)
                {
                    Console.WriteLine("hello3");
                    Console.WriteLine(item);
                    IncomePieSeriesCollection.Add(new PieSeries
                    {
                        Title = item.Name,
                        Values = new ChartValues<double>{item.Amount},
                        DataLabels =  true,
                        LabelPoint = point => $"{point.SeriesView.Title}: {point.Y} ({point.Participation:P})"
                    }
                    );
                    TotalIncome += item.Amount;
                    Console.WriteLine("done adding");
                }
                else
                {
                    ExpensePieSeriesCollection.Add(new PieSeries
                        {
                            Title = item.Name,
                            Values = new ChartValues<double> { item.Amount },
                            DataLabels = true,
                            LabelPoint = point => $"{point.SeriesView.Title}: {point.Y} ({point.Participation:P})"
                        }
                    );
                    TotalExpense += item.Amount;
                }
            }
            TotalPieSeriesCollection.Add(new PieSeries
            {
                Title = "Income",
                Values = new ChartValues<double>{TotalIncome},
                DataLabels =  true,
                LabelPoint = point => $"{point.SeriesView.Title}: {point.Y} ({point.Participation:P})"
            });
            TotalPieSeriesCollection.Add(new PieSeries
            {
                Title = "Expense",
                Values = new ChartValues<double>{TotalExpense},
                DataLabels =  true,
                LabelPoint = point => $"{point.SeriesView.Title}: {point.Y} ({point.Participation:P})"
            });
            
            

        }

        // Chart Data
       
    }
}
