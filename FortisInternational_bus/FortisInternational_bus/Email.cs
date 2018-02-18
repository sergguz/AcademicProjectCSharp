using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortisInternational_bus
{
    [Serializable]
    public class Email
    {
        string address, domain;

        public Email()
        {
            address = "";
            domain = "";
        }

        public Email(string emailAddress)
        {
            string[] splitArray = emailAddress.Split('@');

            address = splitArray[0];
            domain = splitArray[1];
        }

        public override string ToString()
        {
            string state = address + "@" + domain;

            return state;
        }


        /*--------------PROPERTIES--------------*/
        public string Address
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
        public string Domain
        {
            get
            {
                return domain;
            }

            set
            {
                domain = value;
            }
        }
    }
}
