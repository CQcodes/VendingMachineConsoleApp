using System;

namespace VendingMachineConsole
{
    public interface IShoppingHelper
    {
        void StartShopping();
    }

    public class ShoppingHelper : IShoppingHelper
    {
        private readonly ITransaction transaction;
        private readonly ICart cart;
        public ShoppingHelper(ITransaction _transaction, ICart _cart)
        {
            transaction = _transaction;
            cart = _cart;
        }

        public void StartShopping()
        {
            transaction.DisplayBalance();

            int i = 1;
            while (i >= 0)
            {
                i = Shopping();
            }
        }

        private int Shopping()
        {
            // display input options (Main Menu)
            DisplayInputOptions();

            // take input
            string entry = Console.ReadLine().ToUpper();

            if (entry == "C") // insert coin
            {
                return InsertCoinStep();
            }
            else if (entry == "S") // Shopping
            {
                return ShoppingStep();
            }
            else if (entry == "E") // exit
            {
                // Exit
                Console.WriteLine("Thank you for shopping.");
                Console.WriteLine($"Please collect your remaining balance amaount.");
                transaction.DisplayBalance();

                return -1;
            }

            Console.WriteLine("Invalid selection.");
            return 0;
        }


        private int InsertCoinStep()
        {
            int coin = 0;
            DisplayMessage(Message.INSERT_COIN);
            coin = Convert.ToInt32(Console.ReadLine());
            while (coin >= 0)
            {
                transaction.AddBalance(coin);
                transaction.DisplayBalance();

                DisplayMessage(Message.INSERT_COIN);
                coin = Convert.ToInt32(Console.ReadLine());
            }

            return 1;
        }

        private int ShoppingStep()
        {
            // display balance before shopping
            transaction.DisplayBalance();

            // display input options
            Console.WriteLine("Following are the available items. Please enter the item id to dispense. Enter '-1' to exit.");

            // display cart items
            cart.DisplayCartItems();

            // take input
            var selectedItemId = Convert.ToInt32(Console.ReadLine());

            while (selectedItemId > 0)
            {
                // get item price
                if (cart.GetItemPrice(selectedItemId, out var price))
                {
                    // deduct price from balance
                    if (transaction.DeductBalance(price))
                    {
                        // displense item from the cart.
                        if (cart.DispenseCartItem(selectedItemId))
                        {
                            Console.WriteLine("Item dispensed successfully. Please collect it.");
                        }
                        else
                        {
                            Console.WriteLine("Item unavailable.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Not Sufficient balance.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Item Id entered.");
                }


                // display remaining balance
                transaction.DisplayBalance();

                // display input options
                Console.WriteLine("Following are the available items. Please enter the item id to dispense. Enter '-1' to exit.");

                // display cart items
                cart.DisplayCartItems();

                // Take input
                selectedItemId = Convert.ToInt32(Console.ReadLine());
            }

            return 1;
        }


        #region helper-methods

        private static void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }

        private static void DisplayInputOptions()
        {
            Console.WriteLine(Message.EXIT_CODE_MESSAGE);
            Console.WriteLine(Message.INSERT_COIN_CODE_MESSAGE);
            Console.WriteLine(Message.PURCHASE_CODE_MESSAGE);
        }

        #endregion
    }
}
