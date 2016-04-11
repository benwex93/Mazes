using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mazes
{
    public class Location
    {
        //stores location of node in array
        public int col { get;}
        public int row { get;}
        public Location(int col, int row)
        {
            this.col = col;
            this.row = row;
        }
    }
}
