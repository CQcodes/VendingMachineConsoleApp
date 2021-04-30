using VendingMachineConsole.Entity;

namespace VendingMachineConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            IVendingMachine vendingMachine = new VendingMachine(new Wallet(), new Cart()); // It should be resolved by Dependency Resolver
            vendingMachine.Start();
        }
    }
}
