using FortisInternational_data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortisInternational_bus
{
    [Serializable]
    public class Checking : Account, ICalculations
    {
        const int WITHDRAWAL_LIMIT = 4;

        public Checking() : base() { }

        public Checking(int customerID, Date accountOpenDate, double accountBalance) : base(customerID, accountOpenDate, accountBalance)
        {
            AccountNumber = 1;
            AccountType = AccountTypes.CHECKINGS;
        }

        public void UpdateBalance(TransTypes transactionType, double amount)
        {
            switch (transactionType)
            {
                case TransTypes.DEPOSIT:
                    AccountBalance += amount;

                    break;
                case TransTypes.WITHDRAW:
                    CalculateExtraFees();
                    AccountBalance = AccountBalance - amount - ExtraFees;

                    break;
            }
        }

        public void CalculateExtraFees()
        {
            int count = DetermineDailyTransactions();

            if (count >= WITHDRAWAL_LIMIT)
            {
                ExtraFees = 1.0d;
            }
            else
            {
                ExtraFees = 0.0d;
            }
        }

        public int DetermineDailyTransactions()
        {
            int count = 0;
            Checking checkingAccount = (Checking)LoginInformation.customerSignedIn.AccountsList[0];

            foreach (Transaction transaction in checkingAccount.TransList)
            {
                if ((transaction.TransDate.DateDD == DateTime.Today.Day) && (transaction.TransDate.DateMM == DateTime.Today.Month) && (transaction.TransDate.DateYYYY == DateTime.Today.Year))
                {
                    if (transaction.TransType == TransTypes.WITHDRAW)
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        public override string ToString()
        {
            string state = "Account #: " + AccountNumber + "\r\nCustomer ID: " + CustomerID + "\r\nType: " + AccountType + "\r\nOpen Date: " + AccountOpenDate + "\r\nBalance: " + AccountBalance + "\r\nCurrent Transactions Count: " + DetermineDailyTransactions();

            return state;
        }

    }
}
