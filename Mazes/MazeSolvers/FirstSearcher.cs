using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mazes
{
    public class FirstSearcher : ISolution
    {
        int mazeHeight;
        int mazeLength;
        protected char solvedPathValue;
        protected char visitedNodeValue;
        /// <summary>
        /// Gets maze info for all first searchers
        /// </summary>
        /// <param name="mazeToSolve"></param>
        public void GetMazeInfo(Maze mazeToSolve)
        {
            this.name = mazeToSolve.name;
            this.mazeHeight = mazeToSolve.mazeHeight;
            this.mazeLength = mazeToSolve.mazeLength;
            this.start = mazeToSolve.start;
            this.end = mazeToSolve.end;
            this.solvedPathValue = mazeToSolve.mazeVals.solValue;
            this.visitedNodeValue = mazeToSolve.mazeVals.GetRandomUnusedChar();
        }
        /// <summary>
        /// gets nodes successors of each node and returns them in a list
        /// </summary>
        /// <param name="currentNode"></param>
        /// <param name="nextBestNodeList"></param>
        public void GetNodeSuccessors(Node currentNode, List<Node> nextBestNodeList)
        {
            List<Node> nodesToCheck = new List<Node>();
            nodesToCheck.InsertRange(nodesToCheck.Count, new Node[] { currentNode.left, currentNode.right, currentNode.up, currentNode.down });
            foreach (Node node in nodesToCheck)
            {
                if (node != null)
                {
                    node.setWeight(end);
                    if (node.specialVal != visitedNodeValue)
                    {
                        if (node.specialVal != end.specialVal)
                            node.specialVal = visitedNodeValue;
                        node.prevNode = currentNode;
                        nextBestNodeList.Add(node);
                    }
                }
            }
        }
        /// <summary>
        /// starts from end assigning the found final path to the maze itself as a solution
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="solvedPathValue"></param>
        public void AssignSolutionToGraph(Node start, Node end, char solvedPathValue)
        {
            Node currentNode = end;
            while (currentNode != start)
            {
                currentNode.value = solvedPathValue;
                currentNode = currentNode.prevNode;
            }
        }
    }
}
