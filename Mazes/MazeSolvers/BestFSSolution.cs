using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mazes
{
    public class BestFSSolution : FirstSearcher
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
            currentNode.setWeight(start);
            Queue open = new Queue();
            open.Enqueue(currentNode);
            List<Node> nextBestNodeList = new List<Node>();
            while (open.Count != 0)
            {
                currentNode = (Node) open.Dequeue();
                if (currentNode.specialVal == end.specialVal)
                {
                    AssignSolutionToGraph(start, currentNode, solvedPathValue);
                    break;
                }
                currentNode.specialVal = visitedNodeValue;
                GetNodeSuccessors(currentNode, nextBestNodeList);
                nextBestNodeList.Sort((x, y) => x.weight.CompareTo(y.weight));
                foreach (Node bestNode in nextBestNodeList)
                {
                   open.Enqueue(bestNode);
                }
                nextBestNodeList.Clear();
            }            
        }
    }
}
