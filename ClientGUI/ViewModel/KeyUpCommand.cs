using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ClientGui.ViewModel
{
    public class KeyUpCommand : ICommand
    {
        private MazeViewModel maze;
        public event EventHandler CanExecuteChanged;
        public KeyUpCommand(MazeViewModel m)
        {
            maze = m;
        }

        public bool CanExecute(object param)
        {
            int row = maze.GetPlayer().Row - 1;
            int col = maze.GetPlayer().Col;
            int length = AppViewModel.GetMazeDimensions().length;
            string mazeStr = maze.GetMazeData().Maze;
            if (row >= 0)
            {
                if (mazeStr[col + row * length] == '0')
                    return true;
            }
            return false;
        }

        public void Execute(object param)
        {
            maze.MoveUp();
        }
    }
}
