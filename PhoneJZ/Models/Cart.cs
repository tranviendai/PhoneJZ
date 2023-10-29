using PhoneJZ.App_Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhoneJZ.Models
{
    public class CartItem
    {
        public Product product { get; set; }
        [Required]
        public int quantity { get; set; }
    }
    public class Cart
    {
        List<CartItem> items = new List<CartItem>();
        public IEnumerable<CartItem> Items
        {
            get { return items; }
        }
        public void Add(Product _product, int quantity = 1)
        {
            var item = items.FirstOrDefault(s => s.product.Id == _product.Id);
            if (item == null)
            {
                items.Add(new CartItem
                {
                    product = _product,
                    quantity = quantity
                });
            }
            else
            {
                item.quantity += quantity;
            }
        }

        public void Update(int id, int quantity)
        {
            var item = items.Find(s => s.product.Id == id);
            if (item != null)
            {
                item.quantity = quantity;
            }
        }
        public void Remove(int id)
        {
            items.RemoveAll(s => s.product.Id == id);
        }
        public decimal Total()
        {
            var total = items.Sum(s => s.product.totalPrice * s.quantity);
            return total;
        }
        public void ClearCart()
        {
            items.Clear();
        }
    }
}