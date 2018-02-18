using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortisInternational_bus
{
    [Serializable]
    public class Phone
    {
        int countryCode, areaCode, firstThree, lastFour;

        public Phone()
        {
            countryCode = 0;
            areaCode = 0;
            firstThree = 0;
            lastFour = 0;
        }

        public Phone(int countryCode, int areaCode, int firstThree, int lastFour)
        {
            this.countryCode = countryCode;
            this.areaCode = areaCode;
            this.firstThree = firstThree;
            this.lastFour = lastFour;
        }

        public override string ToString()
        {
            string state = countryCode + " (" + areaCode + ") " + firstThree + "-" + lastFour;

            return state;
        }


        /*--------------PROPERTIES--------------*/
        public int AreaCode
        {
            get
            {
                return areaCode;
            }

            set
            {
                areaCode = value;
            }
        }
        public int CountryCode
        {
            get
            {
                return countryCode;
            }

            set
            {
                countryCode = value;
            }
        }
        public int FirstThree
        {
            get
            {
                return firstThree;
            }

            set
            {
                firstThree = value;
            }
        }
        public int LastFour
        {
            get
            {
                return lastFour;
            }

            set
            {
                lastFour = value;
            }
        }
    }
}
