public class Expense
{
    public string Name { get; set; }
    public DateTime CreatedDate { get; set; }
    public double Amount { get; set; }
    public Expense(string name, DateTime createdDate, double amount)
    {
        Name = name;
        CreatedDate = createdDate;
        Amount = amount;
    }





}