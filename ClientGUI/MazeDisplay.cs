using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;

namespace ClientGui
{
    public class MazeDisplay
    {
        public MazeInfo mazeInfo { get; set; }
        public Grid grid;
        public MazeDisplay(MazeInfo mazeInfo)
        {
            this.mazeInfo = mazeInfo;
        }
        public Grid GetMazeToGrid()
        {
            InitializeGrid();
            for (int i = 0; i < mazeInfo.length; i++)
            {
                for (int j = 0; j < mazeInfo.height; j++)
                {
                    Image box = new Image();
                    box.Height = 12;
                    box.Width = 12;
                    if(mazeInfo.mazeString[i + (j * mazeInfo.length)] == '1')
                        box.Source = new BitmapImage(new Uri(@"/Pictures/blacksquare.jpg", UriKind.Relative));
                    else
                        box.Source = new BitmapImage(new Uri(@"/Pictures/greensquare.png", UriKind.Relative));
                    Grid.SetRow(box, i);
                    Grid.SetColumn(box, j);
                    grid.Children.Add(box);
                }
            }
            return grid;
        }
        public void InitializeGrid()
        {
            grid = new Grid();
            for (int i = 0; i < mazeInfo.height; i++)
            {
                ColumnDefinition col = new ColumnDefinition();
                col.Width = new GridLength(1, GridUnitType.Star);
                grid.ColumnDefinitions.Add(col);
            }
            for (int j = 0; j < mazeInfo.length; j++)
            {
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(1, GridUnitType.Star);
                grid.RowDefinitions.Add(row);
            }
        }
    }
}
