using System;
using VendingMachineConsole.Entity;
using VendingMachineConsole.Helper;
using VendingMachineConsole.MainMenu;

namespace VendingMachineConsole
{
    public interface IVendingMachine
    {
        void Start();
    }

    public class VendingMachine : IVendingMachine
    {
        private readonly IWallet wallet;
        private readonly ICart cart;
        private readonly InsertCoinMenu insertCoin;
        private readonly PurchaseItemMenu purchaseItem;
        private readonly ExitMenu exit;
        public VendingMachine(IWallet _wallet, ICart _cart)
        {
            wallet = _wallet;
            cart = _cart;
            insertCoin = new InsertCoinMenu(wallet);
            purchaseItem = new PurchaseItemMenu(wallet, cart);
            exit = new ExitMenu(wallet);
        }

        public void Start()
        {
            Console.WriteLine(MessageHelper.WELCOME);
            wallet.DisplayBalance();

            int i = 1;
            while (i >= 0)
            {
                i = MainMenuNavigation();
            }
        }

        private int MainMenuNavigation()
        {
            DisplayMainMenuOptions();

            string selectedMenu = Console.ReadLine().ToUpper();

            if (selectedMenu == "C")
            {
                return insertCoin.Execute();
            }
            else if (selectedMenu == "S")
            {
                return purchaseItem.Execute();
            }
            else if (selectedMenu == "E")
            {
                return exit.Execute();
            }
            else
            {
                Console.WriteLine("Invalid selection.");
                return 0;
            }
        }

        private void DisplayMainMenuOptions()
        {
            Console.WriteLine("===================== MAIN MENU ======================");
            Console.WriteLine(MessageHelper.INSERT_COIN_CODE_MESSAGE);
            Console.WriteLine(MessageHelper.PURCHASE_CODE_MESSAGE);
            Console.WriteLine(MessageHelper.EXIT_CODE_MESSAGE);
            Console.WriteLine("======================================================");
        }
    }
}
