using System;
using VendingMachineConsole.Entity;
using VendingMachineConsole.Helper;

namespace VendingMachineConsole.MainMenu
{
    public class InsertCoinMenu
    {
        private readonly IWallet wallet;

        public InsertCoinMenu(IWallet _wallet)
        {
            wallet = _wallet;
        }

        public int Execute()
        {
            CollectInsertCoinInput(out double coin);

            while (coin >= 0)
            {
                if (coin > 0) wallet.AddBalance(coin);

                wallet.DisplayBalance();

                CollectInsertCoinInput(out coin);
            }

            return 1;
        }

        private void CollectInsertCoinInput(out double coin)
        {
            DisplayInsertCoinMenuOptions();

            if (!double.TryParse(Console.ReadLine(), out double c))
            {
                Console.WriteLine("Invalid input. Must be number.");
                c = 0;
            }

            coin = c;
        }

        private void DisplayInsertCoinMenuOptions()
        {
            Console.WriteLine("===================== INSERT COIN ======================");
            Console.WriteLine(MessageHelper.INSERT_COIN);
            Console.WriteLine(MessageHelper.INSERT_COIN_EXIT_MESSAGE);
            Console.WriteLine("======================================================");
        }
    }
}
