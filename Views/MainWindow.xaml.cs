using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace expense;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        DbConnection.CreateConnection();
        InitializeComponent();
        var expenses = new List<Expense>();
        expenses.Add(new Expense("Food ", DateTime.Now, 500000, Type.Expense)); //temp data
        expenseTable.ItemsSource = expenses;


    }
}