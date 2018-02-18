using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortisInternational_bus
{
    [Serializable]
    public class Account
    {
        int accountNumber, customerID;
        AccountTypes accountType;
        Date accountOpenDate;
        double accountBalance, extraFees;
        List<Transaction> transList;

        public enum AccountTypes { CHECKINGS, SAVINGS, CREDIT, UNDEFINED }

        public Account()
        {
            accountNumber = 0;
            customerID = 0;
            accountType = AccountTypes.UNDEFINED;
            accountOpenDate = new Date();
            accountBalance = 0.0d;
            extraFees = 0.0d;
            transList = new List<Transaction>();
        }

        public Account(int customerID, Date accountOpenDate, double accountBalance)
        {
            this.customerID = customerID;
            this.accountOpenDate = accountOpenDate;
            this.accountBalance = accountBalance;
            transList = new List<Transaction>();
            extraFees = 0.0d;
        }

        /*--------------PROPERTIES--------------*/
        public int AccountNumber
        {
            get
            {
                return accountNumber;
            }

            set
            {
                accountNumber = value;
            }
        }
        public int CustomerID
        {
            get
            {
                return customerID;
            }

            set
            {
                customerID = value;
            }
        }
        public AccountTypes AccountType
        {
            get
            {
                return accountType;
            }

            set
            {
                accountType = value;
            }
        }
        public Date AccountOpenDate
        {
            get
            {
                return accountOpenDate;
            }

            set
            {
                accountOpenDate = value;
            }
        }
        public double AccountBalance
        {
            get
            {
                return accountBalance;
            }

            set
            {
                accountBalance = value;
            }
        }
        public List<Transaction> TransList
        {
            get
            {
                return transList;
            }

            set
            {
                transList = value;
            }
        }
        public double ExtraFees
        {
            get
            {
                return extraFees;
            }

            set
            {
                extraFees = value;
            }
        }
    }
}
