using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupPrject.Common
{
    public class Item
    {
        /// <summary>
        /// Item's code
        /// </summary>
        public string ItemCode { get; set; }
        /// <summary>
        /// Item's Description
        /// </summary>
        public string ItemDesc { get; set; }
        /// <summary>
        /// Items's Cost
        /// </summary>
        public string Cost { get; set; }

        /// <summary>
        /// Item constructor
        /// </summary>
        /// <param name="code"></param>
        /// <param name="desc"></param>
        /// <param name="cost"></param>
        public Item(string code, string desc, string cost)
        {
            ItemCode = code;
            ItemDesc = desc;
            Cost = cost;
        }

        public override string ToString()
        {
            return $"{ItemCode} - {ItemDesc}";
        }
    }
}
