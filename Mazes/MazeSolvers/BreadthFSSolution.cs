using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mazes
{
    public class BreadthFSSolution : FirstSearcher
    {
        /// <summary>
        /// Solves maze by getting maze info, and traversing the nodes of the maze using BestFS
        /// </summary>
        /// <param name="maze"></param>
        public override void Solve(Maze maze)
        {
            GetMazeInfo(maze);
            TraverseNodes(start, end);
            maze.isSolved = true;
            this.solutionString = maze.ToString();
            this.start = maze.start;
            this.end = maze.end;
        }
        /// <summary>
        /// Traversing nodes using a queue to implement a BFS search but giving priority to
        /// certain nodes that are closer to the end 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public void TraverseNodes(Node start, Node end)
        {
            Node currentNode = start;
            start.specialVal = visitedNodeValue;
            Queue open = new Queue();
            open.Enqueue(currentNode);
            List<Node> nextNodeList = new List<Node>();
            while (currentNode.specialVal != end.specialVal)
            {
                GetNodeSuccessors(currentNode, nextNodeList);
                foreach (Node bestNode in nextNodeList)
                    open.Enqueue(bestNode);
                currentNode = (Node)open.Dequeue();
                nextNodeList.Clear();
            }
            AssignSolutionToGraph(start, currentNode, solvedPathValue);
        }
    }
}
