using System;

namespace VendingMachineConsole
{
    public interface ITransaction
    {
        void DisplayBalance();
        double AddBalance(double coin);
        double GetBalance();
        bool DeductBalance(Item item);
    }

    public class Transaction : ITransaction
    {
        private double balance = 100;

        public double GetBalance()
        {
            return balance;
        }

        public void DisplayBalance()
        {
            Console.WriteLine($"your balance is : {balance}");
        }

        public double AddBalance(double coin)
        {
            if (coin == 5 || coin == 10 || coin == 25)
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
