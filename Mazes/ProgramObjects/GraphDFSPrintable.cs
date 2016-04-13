using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mazes
{
    public abstract class GraphDFSPrintable
    {
        //class for printing graphs that are printable using dfs (i.e. acyclic)
        //placing it in a char array and then saving that char array as a string
        char[,] visualMazeArray;
        int mazeSize;
        CharVals mazeVals;
        /// <summary>
        /// gets printable graph's info
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="mazeSize"></param>
        /// <param name="mazeVals"></param>
        /// <returns></returns>
        public string GetString(Node start, Node end, int mazeSize, CharVals mazeVals)
        {
            this.mazeSize = mazeSize;
            this.mazeVals = mazeVals;
            visualMazeArray = new char[mazeSize * 2, mazeSize * 2];
            TraverveNodes(start, start.location.col * 2, start.location.row * 2);
            visualMazeArray[start.location.col * 2, start.location.row * 2] = mazeVals.pathValue;
            visualMazeArray[end.location.col * 2, end.location.row * 2] = mazeVals.pathValue;
            return GetStringFromArray();
        }
        /// <summary>
        /// turns graph into string representation
        /// </summary>
        /// <returns></returns>
        public string GetStringFromArray()
        {
            string mazeString = "";
            for (int col = 0; col < mazeSize * 2; col++)
            {
                for (int row = 0; row < mazeSize * 2; row++)
                {
                    //if wall
                    if (visualMazeArray[row, col] == '\0')
                        mazeString += mazeVals.wallValue;
                    else
                        mazeString +=(visualMazeArray[row, col]);
                }
                //mazeString += "\n";
            }
            return mazeString;
        }
        /// <summary>
        /// traverses nodes and places their char values in a char array
        /// </summary>
        /// <param name="node"></param>
        /// <param name="col"></param>
        /// <param name="row"></param>
        public void TraverveNodes(Node node, int col, int row)
        {
            //go left
            if (node.left != null)
            {
                visualMazeArray[col - 1, row] = node.left.value;
                visualMazeArray[col - 2, row] = node.left.value;
                TraverveNodes(node.left, col-2, row);
            }
            //go right
            if (node.right != null)
            {
                visualMazeArray[col + 1, row] = node.right.value;
                visualMazeArray[col + 2, row] = node.right.value;
                TraverveNodes(node.right, col + 2, row);
            }
            //go up
            if (node.up != null)
            {
                visualMazeArray[col, row - 1] = node.up.value;
                visualMazeArray[col, row - 2] = node.up.value;
                TraverveNodes(node.up, col, row - 2);
            }
            //go down
            if (node.down != null)
            {
                visualMazeArray[col, row + 1] = node.down.value;
                visualMazeArray[col, row + 2] = node.down.value;
                TraverveNodes(node.down, col, row + 2);
            }
        }
    }
}
