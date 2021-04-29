using System;

namespace VendingMachineConsole
{
    public interface ITransaction
    {
        void DisplayBalance();
        double AddBalance(double coin);

        bool DeductBalance(double itemPrice);
    }

    public class Transaction : ITransaction
    {
        private double balance;

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

        public bool DeductBalance(double itemPrice)
        {
            if (itemPrice > balance)
                return false;

            balance = balance - itemPrice;

            return true;
        }
    }
}
