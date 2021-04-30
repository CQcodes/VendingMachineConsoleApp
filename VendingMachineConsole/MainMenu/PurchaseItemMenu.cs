using System;
using VendingMachineConsole.Entity;

namespace VendingMachineConsole.MainMenu
{
    public class PurchaseItemMenu
    {
        private readonly IWallet wallet;
        private readonly ICart cart;

        public PurchaseItemMenu(IWallet _wallet, ICart _cart)
        {
            wallet = _wallet;
            cart = _cart;
        }

        public int Execute()
        {
            CollectShoppingInput(out int selectedItemId);

            while (selectedItemId >= 0)
            {
                if (selectedItemId > 0)
                {
                    // get item
                    if (cart.TryGetItem(selectedItemId, out var item))
                    {
                        // deduct price from balance
                        if (wallet.DeductBalance(item))
                        {
                            cart.DispenseCartItem(item);
                            Console.WriteLine("Item dispensed successfully. Please collect it.");
                        }
                        else
                        {
                            Console.WriteLine("Insufficient balance.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid ItemId entered or Item out of stock.");
                    }
                }

                CollectShoppingInput(out selectedItemId);
            }

            return 1;
        }

        private void CollectShoppingInput(out int selectedItemId)
        {
            DisplayShoppingMenuOption();

            // take input
            if (!int.TryParse(Console.ReadLine(), out int itemId))
            {
                Console.WriteLine("Invalid input. Must be an integer.");
                itemId = 0;
            }

            selectedItemId = itemId;
        }

        private void DisplayShoppingMenuOption()
        {
            Console.WriteLine("===================== SHOPPING ======================");

            // display balance before shopping
            wallet.DisplayBalance();

            // display input options
            Console.WriteLine("Enter '-1' to return to Main Menu.");
            Console.WriteLine("Enter any item id from the following list to dispense the item.");

            Console.WriteLine("=================== AVAILABLE ITEMS ====================");

            // display cart items
            cart.DisplayCartItems();

            Console.WriteLine("======================================================");
        }
    }
}
