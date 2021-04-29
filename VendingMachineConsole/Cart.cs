using System;
using System.Collections.Generic;
using System.Linq;

namespace VendingMachineConsole
{
    public interface ICart
    {
        void DisplayCartItems();
        bool DispenseCartItem(int itemId);
        bool GetItemPrice(int id, out double price);
    }

    public class Cart : ICart
    {
        private List<Item> cartItems;

        public Cart()
        {
            LoadCartItems();
        }

        private void LoadCartItems()
        {
            cartItems = new List<Item>()
            {
                new Item{ id = 1, Count = 1, Name = "Cola", Price = 100 },
                new Item{ id = 2, Count = 1, Name = "Chips", Price = 50  },
                new Item{ id = 3, Count = 1, Name = "Candy", Price = 65  }
            };
        }

        public void DisplayCartItems()
        {
            foreach(var item in cartItems)
            {
                Console.WriteLine($"{item.id}. {item.Name}, Price - {item.Price}, Count - {item.Count}");
            }
        }

        public bool DispenseCartItem(int itemId)
        {
            if(cartItems.Any(a=>a.id==itemId && a.Count > 0))
            {
                int i = cartItems.IndexOf(cartItems.First(f => f.id == itemId));
                var item = cartItems[i];
                item.Count--;
                cartItems[i] = item;

                return true;
            }

            return false;
        }

        public bool GetItemPrice(int id, out double price)
        {
            var item = cartItems.FirstOrDefault(a => a.id == id);

            if (item == null)
            {
                price = 0;
                return false;
            }

            price = item.Price;
            return true;
        }
    }
}
