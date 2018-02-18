using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortisInternational_bus
{
    [Serializable]
    public class Date
    {
        int day, month, year;

        public Date()
        {
            day = 0;
            month = 0;
            year = 0;
        }

        public Date(int day, int month, int year)
        {
            this.day = day;
            this.month = month;
            this.year = year;
        }

        public override string ToString()
        {
            string state = day + "/" + month + "/" + year;

            return state;
        }


        /*--------------PROPERTIES--------------*/
        public int DateDD
        {
            get
            {
                return day;
            }

            set
            {
                day = value;
            }
        }
        public int DateMM
        {
            get
            {
                return month;
            }

            set
            {
                month = value;
            }
        }
        public int DateYYYY
        {
            get
            {
                return year;
            }

            set
            {
                year = value;
            }
        }
    }
}
