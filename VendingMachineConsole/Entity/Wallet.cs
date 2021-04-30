using System;
using VendingMachineConsole.Helper;

namespace VendingMachineConsole.Entity
{
    public interface IWallet
    {
        void DisplayBalance();
        double AddBalance(double coin);
        double GetBalance();
        bool DeductBalance(Item item);
    }

    public class Wallet : IWallet
    {
        private double balance;

        public double GetBalance()
        {
            return balance;
        }

        public void DisplayBalance()
        {
            Console.WriteLine($"Your wallet balance is : {balance.ToString("C2")}");
        }

        public double AddBalance(double coin)
        {
            if (CoinsHelper.IsAcceptableDenomination(coin))
            {
                balance = balance + coin;
            }
            else
            {
                Console.WriteLine("Invalid denomination.");
            }
            
            return balance;
        }

        public bool DeductBalance(Item item)
        {
            if (item.Price > balance)
                return false;

            balance = balance - item.Price;

            return true;
        }
    }
}
