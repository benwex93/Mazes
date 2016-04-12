using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mazes
{
    public class Game
    {
        /// <summary>
        /// game class which is used when creating a multiplayer game
        /// </summary>
        public string name { get; set; }
        public Maze mazeOne { get; set; }
        public Maze mazeTwo { get; set; }
        /// <summary>
        /// constructor that creates a new maze for player one using dfs
        /// </summary>
        /// <param name="name"></param>
        /// <param name="mazeSize"></param>
        /// <param name="mazeVals"></param>
        public Game(string name, int mazeSize, CharVals mazeVals)
        {
            this.name = name;
            mazeOne = new Maze(name + "player1", mazeSize, mazeVals);
            mazeOne.CreateMaze(new DFSMazeMaker());
            mazeTwo = mazeOne.Clone();
            mazeTwo.name = name + "player2";
            ShiftStart(mazeOne, mazeTwo);
        }
        /// <summary>
        /// shifts the start poisition over by one
        /// </summary>
        /// <param name="mazeOne"></param>
        /// <param name="mazeTwo"></param>
        public void ShiftStart(Maze mazeOne, Maze mazeTwo)
        {
            mazeTwo.start.specialVal = mazeTwo.mazeVals.pathValue;
            Node temp = mazeTwo.start.Clone();
            if (mazeTwo.start.left != null)
            {
                mazeTwo.start = mazeTwo.start.left;
                temp.left = null;
                mazeTwo.start.right = temp;
            }
            else if (mazeTwo.start.right != null)
            {
                mazeTwo.start = mazeTwo.start.right;
                temp.right = null;
                mazeTwo.start.left = temp;
            }
            else if (mazeTwo.start.up != null)
            {
                mazeTwo.start = mazeTwo.start.up;
                temp.up = null;
                mazeTwo.start.down = temp;
            }
            else if (mazeTwo.start.down != null)
            {
                mazeTwo.start = mazeTwo.start.down;
                temp.down = null;
                mazeTwo.start.up = temp;
            }
            mazeTwo.start.specialVal = mazeTwo.mazeVals.startValue;
        }
    }
}
