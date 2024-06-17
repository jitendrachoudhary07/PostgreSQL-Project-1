using System;
using ShoppingWebApi.EfCore;

namespace ShoppingWebApi.Model
{
	public class OrderModel
	{
        public int id { get; set; }
        public int product_id { get; set; }
        public virtual Product Product { get; set; }
        public string name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string phone { get; set; } = string.Empty;
    }
}

