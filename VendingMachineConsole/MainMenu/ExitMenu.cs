using System;
using VendingMachineConsole.Entity;

namespace VendingMachineConsole.MainMenu
{
    public class ExitMenu
    {
        private readonly IWallet wallet;

        public ExitMenu(IWallet _wallet)
        {
            wallet = _wallet;
        }

        public int Execute()
        {
            DisplayExitMessage();
            return -1;
        }

        private void DisplayExitMessage()
        {
            Console.WriteLine("=================== THANK YOU FOR SHOPPING =======================");
            Console.WriteLine($"Please collect your remaining balance amaount.");
            wallet.DisplayBalance();
            Console.WriteLine("================================================================");
        }
    }
}
