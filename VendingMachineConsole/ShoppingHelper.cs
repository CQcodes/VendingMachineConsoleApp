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
            Console.WriteLine("Welcome to vending machine.");
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
            DisplayMainMenuInputOptions();

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
                return ExitStep();
            }
            else
            {
                Console.WriteLine("Invalid selection.");
                return 0;
            }
        }


        #region Shopping-Steps

        private int ExitStep()
        {
            DisplayExitMessage();
            return -1;
        }

        private int InsertCoinStep()
        {
            CollectInsertCoinInput(out int coin);

            while (coin >= 0)
            {
                if (coin > 0) transaction.AddBalance(coin);

                transaction.DisplayBalance();

                CollectInsertCoinInput(out coin);
            }

            return 1;
        }

        private int ShoppingStep()
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
                        if (transaction.DeductBalance(item))
                        {
                            // dispense item from the cart.
                            if (cart.DispenseCartItem(item))
                            {
                                Console.WriteLine("Item dispensed successfully. Please collect it.");
                            }
                            else
                            {
                                Console.WriteLine("Item out of stock.");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Insufficient balance.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid Item Id entered. Not Found.");
                    }
                }

                CollectShoppingInput(out selectedItemId);
            }

            return 1;
        }

        #endregion

        #region InputCollectors

        private void CollectInsertCoinInput(out int coin)
        {
            DisplayInsertCoinMenuOptions();

            if (!int.TryParse(Console.ReadLine(), out int c))
            {
                Console.WriteLine("Invalid input. Must be an integer.");
                c = 0;
            }

            coin = c;
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

        #endregion

        #region display-menu-options

        private void DisplayMainMenuInputOptions()
        {
            Console.WriteLine("===================== MAIN MENU ======================");
            Console.WriteLine(Message.INSERT_COIN_CODE_MESSAGE);
            Console.WriteLine(Message.PURCHASE_CODE_MESSAGE);
            Console.WriteLine(Message.EXIT_CODE_MESSAGE);
            Console.WriteLine("======================================================");
        }

        private void DisplayShoppingMenuOption()
        {
            // display balance before shopping
            transaction.DisplayBalance();

            Console.WriteLine("===================== SHOPPING ======================");
            // display input options
            Console.WriteLine("Enter '-1' to return to Main Menu.");
            Console.WriteLine("Enter any item id from the following list to dispense the item.");
            Console.WriteLine("=================== AVAILABLE ITEMS ====================");

            // display cart items
            cart.DisplayCartItems();

            Console.WriteLine("======================================================");
        }

        private void DisplayInsertCoinMenuOptions()
        {
            Console.WriteLine("===================== INSERT COIN ======================");
            Console.WriteLine(Message.INSERT_COIN);
            Console.WriteLine(Message.INSERT_COIN_EXIT_MESSAGE);
            Console.WriteLine("======================================================");
        }

        private void DisplayExitMessage()
        {
            Console.WriteLine("=================== THANK YOU FOR SHOPPING =======================");
            Console.WriteLine($"Please collect your remaining balance amaount.");
            transaction.DisplayBalance();
            Console.WriteLine("================================================================");
        }

        #endregion
    }
}
