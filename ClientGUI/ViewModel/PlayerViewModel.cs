using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClientGui.ViewModel
{
    public class PlayerViewModel
    {
        private string img;
        private int row;
        private int col;
        public double displayMazeHeight {
            get; }
        public double displayMazeWidth
        {
            get;
        }
        //private double margTop;
        //private double margLeft;
        //private double margRight;
        //private double margBott;

        public PlayerViewModel(string i, int r, int c, double displayMazeHeight, double displayMazeWidth)
        {
            img = i;
            row = r;
            col = c;
            this.displayMazeHeight = displayMazeHeight;
            this.displayMazeWidth = displayMazeWidth;
        }

        public string Image
        {
            get
            {
                return img;
            }
            set { }
        }

        public int Row
        {
            get
            {
                return row;
            }
            set
            {
                row = value;
            }
        }

        public int Col
        {
            get
            {
                return col;
            }
            set
            {
                col = value;
            }
        }

        public double MargTop
        {
            get
            {
                //return 13.7 * row;
                return (displayMazeHeight / AppViewModel.GetMazeDimensions().height - .35) * row;
            }
        }

        public double MargBott
        {
            get
            {
                //return 13.7 * (50 - 1 - row);
                return (displayMazeHeight / AppViewModel.GetMazeDimensions().height - .35) * (AppViewModel.GetMazeDimensions().height - 1 - row);
            }
        }

        public double MargLeft
        {
            get
            {
                //return 13.45 * col;
                return (displayMazeWidth / AppViewModel.GetMazeDimensions().length - .35) * col;
            }
        }

        public double MargRight
        {
            get
            {
                // return 13.45 * (40 - 1 - col);
                return (displayMazeWidth / AppViewModel.GetMazeDimensions().length - .35) * (AppViewModel.GetMazeDimensions().length - 1 - col);
            }
        }
    }
}
