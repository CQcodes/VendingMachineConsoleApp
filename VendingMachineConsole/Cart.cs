using System;
using System.Collections.Generic;
using System.Linq;

namespace VendingMachineConsole
{
    public interface ICart
    {
        void DisplayCartItems();
        bool DispenseCartItem(Item item);
        bool TryGetItem(int id, out Item item);
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
                new Item{ Id = 1, Count = 1, Name = "Cola", Price = 100 },
                new Item{ Id = 2, Count = 1, Name = "Chips", Price = 50  },
                new Item{ Id = 3, Count = 1, Name = "Candy", Price = 65  }
            };
        }

        public void DisplayCartItems()
        {
            foreach(var item in cartItems)
            {
                Console.WriteLine($"id : {item.Id}, name: {item.Name}, Price: {item.Price}, Count: {item.Count}");
            }
        }

        public bool DispenseCartItem(Item item)
        {
            if(cartItems.Any(a=>a.Id==item.Id && a.Count > 0))
            {
                int i = cartItems.IndexOf(cartItems.First(f => f.Id == item.Id));
                item.Count--;
                cartItems[i] = item;

                return true;
            }

            return false;
        }

        public bool TryGetItem(int id, out Item item)
        {
            item = cartItems.FirstOrDefault(a => a.Id == id);

            if (item == null)
            {
                return false;
            }
            return true;
        }
    }
}
