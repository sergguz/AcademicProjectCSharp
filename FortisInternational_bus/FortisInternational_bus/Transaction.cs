using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortisInternational_bus
{
    [Serializable]
    public enum TransTypes { DEPOSIT, WITHDRAW, UNDEFINED }

    [Serializable]
    public class Transaction
    {
        int transNumber;
        string transDescription;
        Date transDate;
        Double transAmount;
        TransTypes transType;

        public Transaction()
        {
            transNumber = 0;
            transDescription = "";
            transDate = new Date();
            transAmount = 0.0d;
            transType = TransTypes.UNDEFINED;
        }

        public Transaction(int transNumber, Date transDate, Double transAmount, TransTypes transType)
        {
            this.transNumber = transNumber;
            this.transDate = transDate;
            this.transAmount = transAmount;
            this.transType = transType;
            transDescription = "Transaction #: " + transNumber + "\r\nTransaction Type: " + transType + "\r\nTransaction Date: " + transDate + "\r\nTransaction Amount: " + transAmount;
        }

        public override string ToString()
        {
            return transDescription;
        }


        /*--------------PROPERTIES--------------*/
        public int TransNumber
        {
            get
            {
                return transNumber;
            }

            set
            {
                transNumber = value;
            }
        }
        public string TransDescription
        {
            get
            {
                return transDescription;
            }

            set
            {
                transDescription = value;
            }
        }
        public Date TransDate
        {
            get
            {
                return transDate;
            }

            set
            {
                transDate = value;
            }
        }
        public double Amount
        {
            get
            {
                return transAmount;
            }

            set
            {
                transAmount = value;
            }
        }
        public TransTypes TransType
        {
            get
            {
                return transType;
            }

            set
            {
                transType = value;
            }
        }
    }
}
