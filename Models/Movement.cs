namespace CaixaDespesas.Models
{
    public class Movement
    {
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public decimal Ammount { get; set; }
        public MovementType Type { get; set; }
    }

    public enum MovementType{
        Debit,
        Credit,
    }
}

