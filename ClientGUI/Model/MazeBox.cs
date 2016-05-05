using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientGui
{
    public class MazeBox
    {
        private int row;
        private int col;
        private string color;

        public MazeBox(int r, int c, string str)
        {
            row = r;
            col = c;
            color = str;
        }
    }
}
