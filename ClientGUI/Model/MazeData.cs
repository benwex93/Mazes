using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientGui.Model
{
    public class MazeData
    {
        public string Name { get; set; }
        public string Maze { get; set; }
        public MazeNodeData Start { get; set; }
        public MazeNodeData End { get; set; }
    }
}
