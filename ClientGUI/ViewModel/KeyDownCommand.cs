using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ClientGui.ViewModel
{
    public class KeyDownCommand : ICommand
    {
        private MazeViewModel maze;
        public event EventHandler CanExecuteChanged;
        public KeyDownCommand(MazeViewModel mazeObj)
        {
            maze = mazeObj;
        }

        public bool CanExecute(object param)
        {
            Console.WriteLine("CanExecute Function");           // NOT PRINTED!!!
            int row = maze.GetPlayer().Row + 1;
            int col = maze.GetPlayer().Col;
            int height = AppViewModel.GetMazeDimensions().height;
            int length = AppViewModel.GetMazeDimensions().length;
            string mazeStr = maze.GetMazeData().Maze;
            if (row < height)
            {
                if (mazeStr[col + row * length] == '0')
                    return true;
            }
            return false;
        }

        public void Execute(object param)
        {
            Console.WriteLine("Execute function");      // NOT PRINTED!!!
            maze.MoveDown();
        }
    }
}
