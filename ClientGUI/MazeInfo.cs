using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientGui
{
    public class MazeInfo
    {
        public int height { get; set; }
        public int length { get; set; }
        public int startRow { get; set; }
        public int startCol { get; set; }
        public int endRow { get; set; }
        public int endCol { get; set; }
        public int currentRow { get; set; }
        public int currentCol { get; set; }
        public string mazeString { get; set; }
        public MazeInfo(int height, int length, int startRow, int startCol, int endRow, int endCol, int currentRow, int currentCol, string mazeString)
        {
            this.height = height;
            this.length = length;
            this.startRow = startRow;
            this.startCol = startCol;
            this.endRow = endRow;
            this.endCol = endCol;
            this.currentCol = currentCol;
            this.currentRow = currentRow;
            this.mazeString = mazeString;
        }
    }
}
