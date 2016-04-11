using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mazes
{
    public class Node
    {
        //a node that is part of a graph that is used for
        //creating and traversing a maze
        public Node(int col, int row, char val, int lenFromStart)
        {
            this.location = new Location(col, row);
            this.value = val;
            this.lengthFromStart = lenFromStart;
        }
        //pointers to other nodes that represent it's location in the graph
        public Node left { get; set; }
        public Node right { get; set; }
        public Node up { get; set; }
        public Node down { get; set; }
        public Node prevNode { get; set; }

        //location in the node array
        public Location location { get; set; }
        //distance from start
        public int weight { get; set; }
        //char value identifying it's role in the maze
        public char value { get; set; }
        //used for assigning start and end nodes and also for creating the branching off of the main path when creating the random maze
        public char specialVal { get; set; }
        //used for calculating end so that it is far away from start
        public int lengthFromStart { get; set; }
        /// <summary>
        /// calculates and sets the weight of the node from the start for finding the furthest node from the start
        /// in order to make the end
        /// </summary>
        /// <param name="start"></param>
        public void setWeight(Node end)
        {
            this.weight = Math.Abs(this.location.col - end.location.col) + Math.Abs(this.location.row - end.location.row);
        }
        /// <summary>
        /// method for return a deep copy of a node
        /// </summary>
        /// <returns>a cloned node</returns>
        public Node Clone()
        {
            Node nodeClone = new Node(this.location.col, this.location.row, this.value, this.lengthFromStart);
            nodeClone.weight = this.weight;
            nodeClone.specialVal = this.specialVal;
            return nodeClone;
        }
    }
}
