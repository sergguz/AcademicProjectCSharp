using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortisInternational_bus
{
    interface ICalculations
    {
        void UpdateBalance(TransTypes transactionType, double amount);

        void CalculateExtraFees();

    }
}
