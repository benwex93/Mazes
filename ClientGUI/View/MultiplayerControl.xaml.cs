﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClientGui.View
{
    /// <summary>
    /// Interaction logic for MultiplayerControl.xaml
    /// </summary>
    public partial class MultiplayerControl : UserControl
    {
        public MultiplayerControl()
        {
            InitializeComponent();
        }
        private void Maze_Loaded(object sender, RoutedEventArgs e)
        {
            maze1.Focus();
        }
    }
}
