namespace VendingMachineConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            IShoppingHelper shoppingHelper = new ShoppingHelper(new Transaction(), new Cart()); // It should be resolved by Dependency Resolver
            shoppingHelper.StartShopping();
        }
    }
}
