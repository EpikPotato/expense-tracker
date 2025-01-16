namespace expense.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public DateTime CreatedDate { get; set; }

        public double Amount
        {
            get;
            set
            {
                if (field < 0)
                {
                    throw new ArgumentException("Value must be positive");
                }

                field = value;
            }
        }

        public Type Type { get; set; }

        public Expense(int id ,string name, DateTime createdDate, double amount, Type type)
        {
            Id = id;
            Name = name;
            CreatedDate = createdDate;
            Amount = amount;
            Type = type;
        }
        public Expense(string name, DateTime createdDate, double amount, Type type)
        {
            Name = name;
            CreatedDate = createdDate;
            Amount = amount;
            Type = type;
        }
       
        
        public override string ToString()
        {
            return $"Name: {Name}, Amount: {Amount:C}, Date: {CreatedDate:yyyy-MM-dd} , Type: {Type} , Id : {Id}" ;
        }
    }
}
