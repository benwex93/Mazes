﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;
using ClientGui.ViewModel;

namespace ClientGui.View
{
    public class MazeDisplay
    {
        public MazeInfo mazeInfo { get; set; }
        public Grid grid;
        public Image playerImage;
        public Image hintImage;
        public Image endImage;
        public MazeDisplay(MazeInfo mazeInfo)
        {
            this.mazeInfo = mazeInfo;
            InitializeGrid();
        }
        public void InitializeGrid()
        {
            grid = new Grid();
            for (int j = 0; j < mazeInfo.length; j++)
            {
                ColumnDefinition col = new ColumnDefinition();
                //col.Width = new GridLength(1, GridUnitType.Star);
                col.Width = GridLength.Auto;
                grid.ColumnDefinitions.Add(col);
            }
            for (int i = 0; i < mazeInfo.height; i++)
            {
                RowDefinition row = new RowDefinition();
                //row.Height = new GridLength(1, GridUnitType.Star);
                row.Height = GridLength.Auto;
                grid.RowDefinitions.Add(row);
            }
        }
        public Grid GetMazeToGrid(bool isFirstPlayer, bool isSecondPlayer)
        {
            for (int i = 0; i < mazeInfo.height; i++)
            {
                for (int j = 0; j < mazeInfo.length; j++)
                {
                    Image box = new Image();
                    box.Height = 12;
                    box.Width = 12;
                    box.Stretch = Stretch.UniformToFill;
                    if (mazeInfo.mazeString[j + (i * mazeInfo.length)] == '1')
                        box.Source = new BitmapImage(new Uri(@"/Pictures/blacksquare.jpg", UriKind.Relative));
                    else
                        box.Source = new BitmapImage(new Uri(@"/Pictures/greensquare.png", UriKind.Relative));
                    Grid.SetRow(box, i);
                    Grid.SetColumn(box, j);
                    grid.Children.Add(box);
                }
            }
            PlacePlayer(isFirstPlayer, isSecondPlayer);
            InitializeHintImage();
            InitializeEndImage();
            return grid;
        }
        public void PlacePlayer(bool isFirstPlayer, bool isSecondPlayer)
        {
            playerImage = new Image();
            playerImage.Height = 12;
            playerImage.Width = 12;
            playerImage.Stretch = Stretch.UniformToFill;
            if (isFirstPlayer)
                playerImage.Source = new BitmapImage(new Uri(@"/Pictures/CalFinal.png", UriKind.Relative));
            if (isSecondPlayer)
                playerImage.Source = new BitmapImage(new Uri(@"/Pictures/BenjyFinal.png", UriKind.Relative));
            Grid.SetRow(playerImage, mazeInfo.startRow);
            Grid.SetColumn(playerImage, mazeInfo.startCol);
            grid.Children.Add(playerImage);
        }
        public void MovePlayer(int row, int col)
        {
            hintImage.Visibility = Visibility.Hidden;
            int nextRow = mazeInfo.currentRow + row;
            int nextCol = mazeInfo.currentCol + col;
            if ((nextCol + (nextRow * mazeInfo.length)) > 0 && (nextCol + (nextRow * mazeInfo.length)) < (mazeInfo.height * mazeInfo.length))
            {
                if (nextRow >= 0 && nextRow < mazeInfo.height)
                {
                    if (mazeInfo.mazeString[nextCol + (nextRow * mazeInfo.length)] == '0')
                    {
                        mazeInfo.currentRow = nextRow;
                        Grid.SetRow(playerImage, mazeInfo.currentRow);
                    }
                }
                if (nextCol >= 0 && nextCol < mazeInfo.length)
                {
                    if (mazeInfo.mazeString[nextCol + (nextRow * mazeInfo.length)] == '0')
                    {
                        mazeInfo.currentCol = nextCol;
                        Grid.SetColumn(playerImage, mazeInfo.currentCol);
                    }
                }
            }
        }
        public void InitializeHintImage()
        {
            hintImage = new Image();
            hintImage.Height = 12;
            hintImage.Width = 12;
            hintImage.Visibility = Visibility.Hidden;
            hintImage.Source = new BitmapImage(new Uri(@"/Pictures/yellowsquare.png", UriKind.Relative));
            Grid.SetColumn(hintImage, mazeInfo.startCol);
            Grid.SetRow(hintImage, mazeInfo.startRow);
            grid.Children.Add(hintImage);
        }
        public void GetHint()
        {
            //call function to get hint location here
            int row = 0; int col = 0;
            hintImage.Visibility = Visibility.Visible;
            Grid.SetColumn(hintImage, col);
            Grid.SetRow(hintImage, row);
        }
        public void InitializeEndImage()
        {
            endImage = new Image();
            endImage.Height = 12;
            endImage.Width = 12;
            endImage.Source = new BitmapImage(new Uri(@"/Pictures/redsquare.png", UriKind.Relative));
            Grid.SetColumn(endImage, mazeInfo.endCol);
            Grid.SetRow(endImage, mazeInfo.endRow);
            grid.Children.Add(endImage);
        }
    }
}
