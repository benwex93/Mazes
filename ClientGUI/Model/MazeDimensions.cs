using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientGui.Model
{
    public class MazeDimensions
    {
        public int height;
        public int length;

        public MazeDimensions(int h, int l)
        {
            height = h;
            length = l;
        }
    }
}
