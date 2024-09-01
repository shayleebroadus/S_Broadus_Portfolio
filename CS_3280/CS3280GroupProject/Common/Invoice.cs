using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupPrject.Common
{
    public class Invoice
    {
        /// <summary>
        /// Invoice Identification number 
        /// </summary>
        public int InvNum { get; set; }

        /// <summary>
        /// Date of Invoice Submission
        /// </summary>
        public string InvDate { get; set; }

        /// <summary>
        /// Array of Total charges (prices for individual items) for Invoice 
        /// </summary>
        public int InvCharge { get; set; }

        /// <summary>
        /// Invoice constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="date"></param>
        /// <param name="charges"></param>
        public Invoice( int id, string date, int charges)
        {
            this.InvNum = id;
            this.InvDate = date;
            this.InvCharge = charges;
        }
       
    }
}
