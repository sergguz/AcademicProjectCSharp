using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortisInternational_bus
{
    [Serializable]
    public class Customer
    {
        int customerID, pinCode;
        string firstName, lastName;
        Email emailAddress;
        Phone phoneNumber;
        Address address;
        ArrayList accountsList = new ArrayList();
        //List<Account> accountsList = new List<Account>();

        public Customer()
        {
            customerID = 0;
            pinCode = 0;
            firstName = "";
            lastName = "";
            emailAddress = new Email();
            phoneNumber = new Phone();
            address = new Address();
        }

        public Customer(int customerID, int pinCode, string firstName, string lastName, Email emailAddress, Phone phoneNumber, Address address)
        {
            this.customerID = customerID;
            this.pinCode = pinCode;
            this.firstName = firstName;
            this.lastName = lastName;
            this.emailAddress = emailAddress;
            this.phoneNumber = phoneNumber;
            this.address = address;
        }

        public override string ToString()
        {
            string state = "CustomerID: " + customerID + "\r\nPIN Code: " + pinCode + "Name: " + firstName + " " + lastName + "\r\nEmail: " + emailAddress + "\r\nPhone #: " + phoneNumber + "\r\nAddress: " + address;

            return state;
        }


        /*--------------PROPERTIES--------------*/
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
        public int PinCode
        {
            get
            {
                return pinCode;
            }

            set
            {
                pinCode = value;
            }
        }
        public string FirstName
        {
            get
            {
                return firstName;
            }

            set
            {
                firstName = value;
            }
        }
        public string LastName
        {
            get
            {
                return lastName;
            }

            set
            {
                lastName = value;
            }
        }
        public Email EmailAddress
        {
            get
            {
                return emailAddress;
            }

            set
            {
                emailAddress = value;
            }
        }
        public Phone PhoneNumber
        {
            get
            {
                return phoneNumber;
            }

            set
            {
                phoneNumber = value;
            }
        }
        public Address Address
        {
            get
            {
                return address;
            }

            set
            {
                address = value;
            }
        }
        public ArrayList AccountsList
        {
            get
            {
                return accountsList;
            }

            set
            {
                accountsList = value;
            }
        }
    }
}
