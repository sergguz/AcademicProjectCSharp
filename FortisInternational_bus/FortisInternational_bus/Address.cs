using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortisInternational_bus
{
    [Serializable]
    public class Address
    {
        int aptNumber, buildingNumber;
        string streetName, city, postalCode, province, country;

        public Address()
        {
            aptNumber = 0;
            buildingNumber = 0;
            streetName = "";
            city = "";
            province = "";
            postalCode = "";
            country = "";
        }

        public Address(int aptNumber, int buildingNumber, string streetName, string city, string province, string postalCode, string country)
        {
            this.aptNumber = aptNumber;
            this.buildingNumber = buildingNumber;
            this.streetName = streetName;
            this.city = city;
            this.province = province;
            this.postalCode = postalCode;
            this.country = country;
        }

        public override string ToString()
        {
            string state = aptNumber + "-" + buildingNumber + " " + streetName + "; " + city + ", " + province + "; " + postalCode + "; " + country;

            return state;
        }


        /*--------------PROPERTIES--------------*/
        public int AptNumber
        {
            get
            {
                return aptNumber;
            }

            set
            {
                aptNumber = value;
            }
        }
        public int BuildingNumber
        {
            get
            {
                return buildingNumber;
            }

            set
            {
                buildingNumber = value;
            }
        }
        public string City
        {
            get
            {
                return city;
            }

            set
            {
                city = value;
            }
        }
        public string Country
        {
            get
            {
                return country;
            }

            set
            {
                country = value;
            }
        }
        public string PostalCode
        {
            get
            {
                return postalCode;
            }

            set
            {
                postalCode = value;
            }
        }
        public string Province
        {
            get
            {
                return province;
            }

            set
            {
                province = value;
            }
        }
        public string StreetName
        {
            get
            {
                return streetName;
            }

            set
            {
                streetName = value;
            }
        }
    }
}
