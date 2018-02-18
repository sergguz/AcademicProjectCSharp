using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortisInternational_bus
{
    [Serializable]
    public class Saving : Account, ICalculations
    {
        const double INTEREST_RATE = 0.02d;
        double totalSavings;
        DateTime lastUpdated;

        public Saving() : base()
        {
            totalSavings = 0.0d;
            lastUpdated = new DateTime();
        }

        public Saving(int customerID, Date accountOpenDate, double accountBalance) : base(customerID, accountOpenDate, accountBalance)
        {
            AccountNumber = 2;
            AccountType = AccountTypes.SAVINGS;
            AccountOpenDate = accountOpenDate;
            totalSavings = 0.0d;
            lastUpdated = new DateTime(AccountOpenDate.DateYYYY, AccountOpenDate.DateMM, AccountOpenDate.DateDD);
        }

        public void UpdateBalance(TransTypes transactionType, double amount)
        {
            CalculateExtraFees();

            switch (transactionType)
            {
                case TransTypes.DEPOSIT:
                    AccountBalance += DetermineSavings() - ExtraFees + amount;

                    break;
                case TransTypes.WITHDRAW:
                    AccountBalance += DetermineSavings() - ExtraFees - amount;

                    break;
            }

            lastUpdated = DateTime.Today;
        }

        double DetermineSavings()
        {
            int yearsOfSavings = 0;
            int monthsOfSavings = 0;
            double recentSavings = 0.0d;

            if (DateTime.Today.Year == lastUpdated.Year)
            {
                yearsOfSavings = 0;
                monthsOfSavings = DateTime.Today.Month - lastUpdated.Month;
            }
            else
            {
                yearsOfSavings = DateTime.Today.Year - lastUpdated.Year;

                if (DateTime.Today.Month > lastUpdated.Month)
                {
                    monthsOfSavings = DateTime.Today.Month - lastUpdated.Month;
                }
                else if (DateTime.Today.Month == lastUpdated.Month)
                {
                    monthsOfSavings = 0;
                }
                else if (DateTime.Today.Month < lastUpdated.Month)
                {
                    yearsOfSavings--;
                    monthsOfSavings = (12 - lastUpdated.Month) + DateTime.Today.Month;
                }
            }

            recentSavings = (AccountBalance * ((yearsOfSavings * (INTEREST_RATE * 12)) + (monthsOfSavings * INTEREST_RATE)));
            totalSavings += recentSavings;

            return recentSavings;
        }

        public void CalculateExtraFees()
        {
            int monthsSinceLastUpdate = 0;
            TimeSpan timeSpanSinceLastUpdated = DateTime.Today - lastUpdated;

            if ((timeSpanSinceLastUpdated.Days > 30) && (AccountBalance < 1000))
            {
                monthsSinceLastUpdate = timeSpanSinceLastUpdated.Days / 30;
                ExtraFees = 5 * monthsSinceLastUpdate;
            }
            else
            {
                ExtraFees = 0;
            }

        }

        public override string ToString()
        {
            string state = "Account #: " + AccountNumber + "\r\nCustomer ID: " + CustomerID + "\r\nType: " + AccountType + "\r\nOpen Date: " + AccountOpenDate + "\r\nBalance: " + AccountBalance + "\r\nExtra Fees: " + ExtraFees + "\r\nInterest Rate: " + INTEREST_RATE + "\r\nAnnual Gain: " + totalSavings;

            return state;
        }


        /*--------------PROPERTIES--------------*/
        public double TotalSavings
        {
            get
            {
                return totalSavings;
            }

            set
            {
                totalSavings = value;
            }
        }
    }
}
