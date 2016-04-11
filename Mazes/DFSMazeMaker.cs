using System;
using System.Collections.Generic;
using System.Linq;

namespace MazeEx1
{
    class DFSMazeMaker:IMazeMakeable
    {
        //uses a a 2d array of nodes to create a maze by assigning one of them randomly
        //to be the start and then traverses through the array from the start connecting nodes
        //in a one way acyclic graph in a dFS manner
        Node[,] mazeArray;
        static Random randomNumberGenerator = new Random();
        int mazeSize;
        char pathValue;
        /// <summary>
        /// algorithm that saves all details of the maze that it will need to create it and then creates it
        /// </summary>
        /// <param name="mazeToMake"></param>
        public void CreateMaze(Maze mazeToMake)
        {
            this.mazeSize = mazeToMake.mazeSize;
            this.pathValue = mazeToMake.mazeVals.pathValue;
            mazeArray = new Node[mazeSize, mazeSize];
            CreateStart(mazeToMake);
            TraverveNodes(mazeToMake.start, mazeToMake.start.location.col, mazeToMake.start.location.row);
            CreateEnd(mazeToMake);
        }
        /// <summary>
        /// creates starting node randomly
        /// </summary>
        /// <param name="mazeToMake"></param>
        public void CreateStart(Maze mazeToMake)
        {
            //randomNumberGenerator
            int col = randomNumberGenerator.Next(0, mazeToMake.mazeSize);
            int row = randomNumberGenerator.Next(0, mazeToMake.mazeSize);

            mazeToMake.start = new Node(col, row, mazeToMake.mazeVals.pathValue, 0);
            mazeToMake.start.specialVal = mazeToMake.mazeVals.startValue;
            mazeArray[mazeToMake.start.location.col, mazeToMake.start.location.row] = mazeToMake.start;
        }
        /// <summary>
        /// creates end by looping through array looking for node furthest from the end point
        /// </summary>
        /// <param name="mazeToMake"></param>
        public void CreateEnd(Maze mazeToMake)
        {
            int greatestLengthFromStart = 0;
            Node end = null;
            for (int col = 0; col < mazeSize; col++)
            {
                for (int row = 0; row < mazeSize; row++)
                {
                    if (greatestLengthFromStart < mazeArray[col, row].lengthFromStart)
                    {
                        greatestLengthFromStart = mazeArray[col, row].lengthFromStart;
                        end = mazeArray[col, row];
                    }
                }
            }
            if(end != null)
            {
                end.specialVal = mazeToMake.mazeVals.endValue;
                mazeToMake.end = end;
            }
        }
        /// <summary>
        /// Traverses nodes recursively from start, connecting nodes that have not been initialized yet until 
        /// it explores the whole maze
        /// </summary>
        /// <param name="node"></param>
        /// <param name="col"></param>
        /// <param name="row"></param>
        public void TraverveNodes(Node node, int col, int row)
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
                        else if (mazeArray[col - 1, row] == null) {
                            mazeArray[col - 1, row] = new Node(col - 1, row, pathValue, node.lengthFromStart + 1);
                            node.left = mazeArray[col - 1, row];
                            TraverveNodes(mazeArray[col - 1, row], col - 1, row);
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
                            mazeArray[col + 1, row] = new Node(col + 1, row, pathValue, node.lengthFromStart + 1);
                            node.right = mazeArray[col + 1, row];
                            TraverveNodes(mazeArray[col + 1, row], col + 1, row);
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
                            mazeArray[col, row - 1] = new Node(col, row - 1, pathValue, node.lengthFromStart + 1);
                            node.up = mazeArray[col, row - 1];
                            TraverveNodes(mazeArray[col, row - 1], col, row - 1);
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
                            mazeArray[col, row + 1] = new Node(col, row + 1, pathValue, node.lengthFromStart + 1);
                            node.down = mazeArray[col, row + 1];
                            TraverveNodes(mazeArray[col, row + 1], col, row + 1);
                        }
                        //otherwise reached already visited node
                        else { node.down = null; }
                        break;
                    default:
                        break;
                }
                directionsList.RemoveAt(randomIndex);
            }
        }
    }
}
