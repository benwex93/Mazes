using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mazes
{
    public class Maze : GraphDFSPrintable
    {
        //maze class
        public Node start { get; set; }
        public Node end { get; set; }
        public string name { get; set; }
        public int mazeHeight { get; set; }
        public int mazeLength { get; set; }
        public CharVals mazeVals { get; set; }
        public bool isSolved { get; set; }
        public Maze(string name, int mazeHeight, int mazeLength, CharVals mazeVals)
        {
            this.name = name;
            this.mazeHeight = mazeHeight;
            this.mazeLength = mazeLength;
            this.mazeVals = mazeVals;
        }
        /// <summary>
        /// gets a maze maker and uses it to initialize the maze
        /// </summary>
        /// <param name="makeType"></param>
        public void CreateMaze(IMazeMakeable makeType)
        {
            makeType.CreateMaze(this);
        }
        /// <summary>
        /// gets a maze solver and uses it to solve the maze
        /// </summary>
        /// <param name="solveType"></param>
        public void SolveMaze(ISolution solveType)
        {
            solveType.Solve(this);
        }
        /// <summary>
        /// returns a string representation of the maze using printable
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return GetString(start, end, mazeHeight, mazeLength, mazeVals);
        }
        /// <summary>
        /// deeply copies a maze
        /// </summary>
        /// <returns>a maze clone</returns>
        public Maze Clone()
        {
            Maze mazeClone = new Maze(this.name, mazeHeight, mazeLength, this.mazeVals);
            mazeClone.start = this.start.Clone();
            CloneNodes(this.start, mazeClone.start);
            mazeClone.end = this.end.Clone();
            return mazeClone;
        }
        /// <summary>
        /// returns a deep copy of the graph by copying all the nodes recursively
        /// </summary>
        /// <param name="currentNode"></param>
        /// <param name="nodeToClone"></param>
        public void CloneNodes(Node currentNode, Node nodeToClone)
        {
            if(currentNode.left != null)
            {
                nodeToClone.left = currentNode.left.Clone();
                CloneNodes(currentNode.left, nodeToClone.left);
            }
            if (currentNode.right != null)
            {
                nodeToClone.right = currentNode.right.Clone();
                CloneNodes(currentNode.right, nodeToClone.right);
            }
            if (currentNode.up != null)
            {
                nodeToClone.up = currentNode.up.Clone();
                CloneNodes(currentNode.up, nodeToClone.up);
            }
            if (currentNode.down != null)
            {
                nodeToClone.down = currentNode.down.Clone();
                CloneNodes(currentNode.down, nodeToClone.down);
            }
        }
    }
}
