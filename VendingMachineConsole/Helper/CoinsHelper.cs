using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachineConsole.Helper
{
    public static class CoinsHelper
    {
        public static bool IsAcceptableDenomination(double c)
        {
            return AcceptedDenominations().Contains(c);
        }

        private static List<double> AcceptedDenominations()
        {
            return new List<double> { 0.05, 0.1, 0.25 };
        }
    }
}
