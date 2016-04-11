using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mazes
{
    public class ISolution
    {
        public string name { get; set; }
        public string solutionString { get; set; }
        public Node start { get; set; }
        public Node end { get; set; }
        /// <summary>
        /// Base class for all solutions which all contain a name, a solution string,
        /// a start and end, and a way to solve it which must be implemented
        /// </summary>
        /// <param name="maze"></param>
        public virtual void Solve(Maze maze)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// saves solution as dataclass for serialization to saved solutions file
        /// </summary>
        /// <returns></returns>
        public SolutionDataClass ToSolutionDataClass()
        {
            return new SolutionDataClass(name, solutionString, new NodeDataClass(start.location.row, start.location.col),
                new NodeDataClass(end.location.row, end.location.col));
        }
    }
}
