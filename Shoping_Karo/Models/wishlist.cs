using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Principal;

namespace Shoping_Karo.Models
{
    public class wishlist
    {
        public int Id { get; set; }
        public int user_id { get; set; }
        public int product_id { get; set; }
        public string title { get; set; }
        public decimal price { get; set; }
        public string image { get; set; }
    }
}

