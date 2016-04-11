using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mazes
{
    public class CharVals
    {
        /// <summary>
        /// Contains character values for representing maze as array
        /// </summary>
        public char startValue { get; set; }
        public char endValue { get; set; }
        public char solValue { get; set; }
        public char pathValue { get; set; }
        public char wallValue { get; set; }
        public CharVals(char startValue, char endValue, char solValue, char pathValue, char wallValue)
        {
            this.startValue = startValue;
            this.endValue = endValue;
            this.solValue = solValue;
            this.pathValue = pathValue;
            this.wallValue = wallValue;
        }
    }
}
