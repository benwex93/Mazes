﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ClientGui.ViewModel
{
    public class KeyLeftCommand : ICommand
    {
        private MazeViewModel maze;
        public event EventHandler CanExecuteChanged;
        public KeyLeftCommand(MazeViewModel m)
        {
            maze = m;
        }

        public bool CanExecute(object param)
        {
            int row = maze.GetPlayer().Row;
            int col = maze.GetPlayer().Col - 1;
            int length = AppViewModel.GetMazeDimensions().length;
            string mazeStr = maze.GetMazeData().Maze;
            if (col >= 0)
            {
                if (mazeStr[col + row * length] == '0')
                    return true;
            }
            return false;
        }

        public void Execute(object param)
        {
            maze.MoveLeft();
        }
    }
}
