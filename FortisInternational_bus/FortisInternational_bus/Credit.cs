using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FortisInternational_bus
{
    [Serializable]
    public class Credit : Account, ICalculations
    {
        const double INTEREST_RATE = 0.18;  // Interest Rate: 18% annually
        const double PENALTY_RATE = 0.25;   // Interest Rate when AccountBalance > limitAmount

        double limitAmount;
        DateTime lastPayment;

        public Credit() : base()
        {
            limitAmount = 0.0d;
            lastPayment = new DateTime();
        }

        public Credit(int customerID, Date accountOpenDate, double limitAmount, double accountBalance) : base(customerID, accountOpenDate, accountBalance)
        {
            AccountNumber = 3;
            AccountType = AccountTypes.CREDIT;
            this.limitAmount = limitAmount;
            lastPayment = new DateTime(AccountOpenDate.DateYYYY, AccountOpenDate.DateMM, AccountOpenDate.DateDD);
        }

        public void UpdateBalance(TransTypes transactionType, double amount)
        {
            CalculateExtraFees();

            switch (transactionType)
            {
                case TransTypes.DEPOSIT:
                    AccountBalance += (AccountBalance * ExtraFees) - amount;
                    lastPayment = DateTime.Today;

                    break;
                case TransTypes.WITHDRAW:
                    if (AccountBalance + amount > limitAmount)
                    {
                        MessageBox.Show("Withdraw/Purchase denied. Credit limit reached.", "Insufficient Credit", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        AccountBalance += (AccountBalance * ExtraFees) + amount;
                    }

                    break;
            }

        }

        public void CalculateExtraFees()
        {
            int yearsOfInterest = 0;
            int monthsOfInterest = 0;

            if (DateTime.Today.Year == lastPayment.Year)
            {
                yearsOfInterest = 0;
                monthsOfInterest = DateTime.Today.Month - lastPayment.Month;
            }
            else
            {
                yearsOfInterest = DateTime.Today.Year - lastPayment.Year;

                if (DateTime.Today.Month > lastPayment.Month)
                {
                    monthsOfInterest = DateTime.Today.Month - lastPayment.Month;
                }
                else if (DateTime.Today.Month == lastPayment.Month)
                {
                    monthsOfInterest = 0;
                }
                else if (DateTime.Today.Month < lastPayment.Month)
                {
                    yearsOfInterest--;
                    monthsOfInterest = (12 - lastPayment.Month) + DateTime.Today.Month;
                }
            }

            ExtraFees = yearsOfInterest * INTEREST_RATE + (monthsOfInterest * (INTEREST_RATE / 12));
        }

        public override string ToString()
        {
            string state = "Account #: " + AccountNumber + "\r\nCustomer ID: " + CustomerID + "\r\nType: " + AccountType + "\r\nOpen Date: " + AccountOpenDate + "\r\nBalance: " + AccountBalance + "\r\nExtra Fees: " + ExtraFees + "\r\nInterest Rate: " + INTEREST_RATE + "\r\nLimit Amount: " + limitAmount;

            return state;
        }


        /*--------------PROPERTIES--------------*/
        public double LimitAmount
        {
            get
            {
                return limitAmount;
            }

            set
            {
                limitAmount = value;
            }
        }

        public DateTime LastPayment
        {
            get
            {
                return lastPayment;
            }

            set
            {
                lastPayment = value;
            }
        }
    }
}
