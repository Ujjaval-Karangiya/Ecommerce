namespace Shoping_Karo.Models
{
    public class orders
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public int product_id { get; set; }
        public string title { get; set; }
        public decimal price { get; set; }
        public int qty { get; set; }
        public string image { get; set; }
    }
}
