using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeEx1
{
    class RandomMazeMaker : IMazeMakeable
    {
        //uses a a 2d array of nodes to create a maze by assigning one of them randomly
        //to be the start and then performs a drunkards walk to traverse through the array, 
        //connecting nodes for a length of maze size and then assigns the last node reached
        // as the end. It then branches out from the main path, node by node performing
        //a dfs search on each node thus retaining the acyclic nature of the graph
        static Random randomNumberGenerator = new Random();
        int mazeSize;
        Node[,] mazeArray;
        char pathValue;
        char mainPathValue = 'M';
        public void CreateMaze(Maze mazeToMake)
        {
            this.mazeSize = mazeToMake.mazeSize;
            this.pathValue = mazeToMake.mazeVals.pathValue;
            mazeArray = new Node[mazeSize, mazeSize];
            CreateStart(mazeToMake);
            CreateEnd(mazeToMake);
            RandomizeRemainingNodes();
        }
        public void CreateStart(Maze mazeToMake)
        {
            //randomNumberGenerator
            int col = randomNumberGenerator.Next(0, mazeToMake.mazeSize);
            int row = randomNumberGenerator.Next(0, mazeToMake.mazeSize);

            mazeToMake.start = new Node(col, row, pathValue, 0);
            mazeToMake.start.specialVal = mazeToMake.mazeVals.startValue;
            mazeArray[mazeToMake.start.location.col, mazeToMake.start.location.row] = mazeToMake.start;
        }
        public void CreateEnd(Maze mazeToMake)
        {
            MakeStartEndPath(mazeToMake.start, mazeToMake.start.location.col, mazeToMake.start.location.row);
            int greatestLengthFromStart = 0;
            Node end = null;
            for (int col = 0; col < mazeSize; col++)
            {
                for (int row = 0; row < mazeSize; row++)
                {
                    if (mazeArray[col, row] != null)
                    {
                        if (greatestLengthFromStart < mazeArray[col, row].lengthFromStart)
                        {
                            greatestLengthFromStart = mazeArray[col, row].lengthFromStart;
                            end = mazeArray[col, row];
                        }
                    }
                }
            }
            if (end != null)
            {
                end.specialVal = mazeToMake.mazeVals.endValue;
                mazeToMake.end = end;
            }
        }
        public bool MakeStartEndPath(Node node, int col, int row)
        {
            //if not at starting node
            List<int> directionsList = new List<int>();
            for (int directionCases = 0; directionCases < 4; directionCases++)
                directionsList.Add(directionCases);
            //visits all nodes in order starting with random one 
            while (directionsList.Count > 0)
            {
                int randomIndex = randomNumberGenerator.Next(0, directionsList.Count);
                switch (directionsList.ElementAt(randomIndex))
                {
                    //go left
                    case 0:
                        if (col - 1 < 0) //if out of bounds
                            node.left = null;
                        //if in bounds can safely run check on maze to see if found available node
                        else if (mazeArray[col - 1, row] == null)
                        {
                            //end after making path of size mazesize*mazesize/2
                            if (node.lengthFromStart >= mazeSize)
                                return true;
                            else
                            {
                                mazeArray[col - 1, row] = new Node(col - 1, row, pathValue, node.lengthFromStart + 1);
                                node.left = mazeArray[col - 1, row];
                                node.left.specialVal = mainPathValue;
                                if (MakeStartEndPath(mazeArray[col - 1, row], col - 1, row))
                                    return true;
                                mazeArray[col - 1, row] = null;
                            }
                        }
                        //otherwise reached already visited node
                        else { node.left = null; }
                        break;
                    //go right
                    case 1:
                        if (col + 1 >= mazeSize) //if out of bounds
                            node.right = null;
                        //if in bounds can safely run check on maze to see if found available node
                        else if (mazeArray[col + 1, row] == null)
                        {
                            if (node.lengthFromStart >= mazeSize)
                                return true;
                            else
                            {
                                mazeArray[col + 1, row] = new Node(col + 1, row, pathValue, node.lengthFromStart + 1);
                                node.right = mazeArray[col + 1, row];
                                node.right.specialVal = mainPathValue;
                                if(MakeStartEndPath(mazeArray[col + 1, row], col + 1, row)) //if led to final path
                                    return true;
                                mazeArray[col + 1, row] = null;
                            }
                        }
                        //otherwise reached already visited node
                        else { node.right = null; }
                        break;
                    //go up
                    case 2:
                        if (row - 1 < 0) //if out of bounds
                            node.up = null;
                        //if in bounds can safely run check on maze to see if found available node
                        else if (mazeArray[col, row - 1] == null)
                        {
                            if (node.lengthFromStart >= mazeSize)
                                return true;
                            else
                            {
                                mazeArray[col, row - 1] = new Node(col, row - 1, pathValue, node.lengthFromStart + 1);
                                node.up = mazeArray[col, row - 1];
                                node.up.specialVal = mainPathValue;
                                if (MakeStartEndPath(mazeArray[col, row - 1], col, row - 1))
                                    return true;
                                mazeArray[col, row - 1] = null;
                            }
                        }
                        //otherwise reached already visited node
                        else { node.up = null; }
                        break;
                    //go down
                    case 3:
                        if (row + 1 >= mazeSize) //if out of bounds
                            node.down = null;
                        //if in bounds can safely run check on maze to see if found available node
                        else if (mazeArray[col, row + 1] == null)
                        {
                            if (node.lengthFromStart >= mazeSize)
                                return true;
                            else
                            {
                                mazeArray[col, row + 1] = new Node(col, row + 1, pathValue, node.lengthFromStart + 1);
                                node.down = mazeArray[col, row + 1];
                                node.down.specialVal = mainPathValue;
                                if (MakeStartEndPath(mazeArray[col, row + 1], col, row + 1))
                                    return true;
                                mazeArray[col, row + 1] = null;
                            }
                        }
                        //otherwise reached already visited node
                        else { node.down = null; }
                        break;
                    default:
                        break;
                }
                directionsList.RemoveAt(randomIndex);
            }
            //else reached dead end
            return false;
        }
        /// <summary>
        /// Traverses through original path node by node randomly exploring outward until it reaches itself again or a wall
        /// </summary>
        public void RandomizeRemainingNodes()
        {
            Node nodeToBranchOut;
            for (int col = 0; col < mazeSize; col++)
            {
                for (int row = 0; row < mazeSize; row++)
                {
                    if (mazeArray[col, row] != null)
                    {
                        //if found one of the nodes on the main path
                        if (mazeArray[col, row].specialVal == mainPathValue)
                        {
                            nodeToBranchOut = mazeArray[col, row];
                            //makes list so it can loop through all direction in random order
                            List<int> directionsList = new List<int>();
                            for (int directionCases = 0; directionCases < 4; directionCases++)
                                directionsList.Add(directionCases);
                            //visits all nodes in order starting with random one 
                            while (directionsList.Count > 0)
                            {
                                int randomIndex = randomNumberGenerator.Next(0, directionsList.Count);
                                switch (directionsList.ElementAt(randomIndex))
                                {
                                    //go left
                                    case 0:
                                        if (nodeToBranchOut.location.col - 1 < 0)
                                        { //if out of bounds removes direction from possible direction list
                                            nodeToBranchOut.left = null;
                                            directionsList.RemoveAt(randomIndex);
                                        }
                                        //if node to left is null, initializes it and explores it
                                        else if (mazeArray[nodeToBranchOut.location.col - 1, nodeToBranchOut.location.row] == null)
                                        {
                                            mazeArray[nodeToBranchOut.location.col - 1, nodeToBranchOut.location.row]
                                                = new Node(nodeToBranchOut.location.col - 1, nodeToBranchOut.location.row, pathValue, nodeToBranchOut.lengthFromStart + 1);
                                            nodeToBranchOut.left = mazeArray[nodeToBranchOut.location.col - 1, nodeToBranchOut.location.row];
                                            nodeToBranchOut = nodeToBranchOut.left;
                                            //can explore all directions now
                                            directionsList.Clear();
                                            for (int directionCases = 0; directionCases < 4; directionCases++)
                                                directionsList.Add(directionCases);
                                        }
                                        //if node to left has already been initialized and is not wall does not do anything
                                        else
                                            directionsList.RemoveAt(randomIndex);
                                        break;
                                    //go right
                                    case 1:
                                        if (nodeToBranchOut.location.col + 1 >= mazeSize)  { //if out of bounds
                                            nodeToBranchOut.right = null;
                                            directionsList.RemoveAt(randomIndex);
                                        }
                                        //if node from main path allow it to connect to other nodes
                                        else if (mazeArray[nodeToBranchOut.location.col + 1, nodeToBranchOut.location.row] == null)
                                        {
                                            mazeArray[nodeToBranchOut.location.col + 1, nodeToBranchOut.location.row]
                                                = new Node(nodeToBranchOut.location.col + 1, nodeToBranchOut.location.row, pathValue, nodeToBranchOut.lengthFromStart + 1);
                                            nodeToBranchOut.right = mazeArray[nodeToBranchOut.location.col + 1, nodeToBranchOut.location.row];
                                            nodeToBranchOut = nodeToBranchOut.right;
                                            directionsList.Clear();
                                            for (int directionCases = 0; directionCases < 4; directionCases++)
                                                directionsList.Add(directionCases);
                                        }
                                        else
                                            directionsList.RemoveAt(randomIndex);
                                        break;
                                    //go up
                                    case 2:
                                        if (nodeToBranchOut.location.row - 1 < 0)  { //if out of bounds
                                            nodeToBranchOut.up = null;
                                            directionsList.RemoveAt(randomIndex);
                                        }
                                        //if node from main path allow it to connect to other nodes
                                        else if (mazeArray[nodeToBranchOut.location.col, nodeToBranchOut.location.row - 1] == null)
                                        {
                                            mazeArray[nodeToBranchOut.location.col, nodeToBranchOut.location.row - 1]
                                                = new Node(nodeToBranchOut.location.col, nodeToBranchOut.location.row - 1, pathValue, nodeToBranchOut.lengthFromStart + 1);
                                            nodeToBranchOut.up = mazeArray[nodeToBranchOut.location.col, nodeToBranchOut.location.row - 1];
                                            nodeToBranchOut = nodeToBranchOut.up;
                                            directionsList.Clear();
                                            for (int directionCases = 0; directionCases < 4; directionCases++)
                                                directionsList.Add(directionCases);
                                        }
                                        else
                                            directionsList.RemoveAt(randomIndex);
                                        break;
                                    //go down
                                    case 3:
                                        if (nodeToBranchOut.location.row + 1 >= mazeSize) {  //if out of bounds
                                            nodeToBranchOut.down = null;
                                            directionsList.RemoveAt(randomIndex);
                                        }
                                        //if node from main path allow it to connect to other nodes
                                        else if (mazeArray[nodeToBranchOut.location.col, nodeToBranchOut.location.row + 1] == null)
                                        {
                                            mazeArray[nodeToBranchOut.location.col, nodeToBranchOut.location.row + 1]
                                                = new Node(nodeToBranchOut.location.col, nodeToBranchOut.location.row + 1, pathValue, nodeToBranchOut.lengthFromStart + 1);
                                            nodeToBranchOut.down = mazeArray[nodeToBranchOut.location.col, nodeToBranchOut.location.row + 1];
                                            nodeToBranchOut = nodeToBranchOut.down;
                                            directionsList.Clear();
                                            for (int directionCases = 0; directionCases < 4; directionCases++)
                                                directionsList.Add(directionCases);
                                        }
                                        else
                                            directionsList.RemoveAt(randomIndex);
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
