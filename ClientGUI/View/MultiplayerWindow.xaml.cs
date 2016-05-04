using System;
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
using System.Windows.Shapes;

namespace ClientGui
{
    public partial class MultiplayerWindow : Window
    {
        MazeDisplay mazeDisplay;
        private MediaPlayer player;
        public MultiplayerWindow()
        {
            InitializeComponent();
            InitializeMusic();
            //GetMazeInfo();
            //AddGrid();
        }
        public void InitializeMusic()
        {
            player = new MediaPlayer();
            player.Open(new Uri("../../Music/PenguinWars.mp3", UriKind.Relative));
            player.MediaEnded += new EventHandler(Media_Ended);
            player.Play();
        }
        private void Media_Ended(object sender, EventArgs e)
        {
            player.Position = TimeSpan.Zero;
            player.Play();
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            player.Stop();
        }

        private void hint_Click(object sender, RoutedEventArgs e)
        {
            //mazeDisplay.GetHint();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            /* if (e.Key == Key.Left)
            {
                mazeDisplay.MovePlayer(-1, 0);
            }
            if (e.Key == Key.Right)
            {
                mazeDisplay.MovePlayer(1, 0);
            }
            if (e.Key == Key.Up)
            {
                mazeDisplay.MovePlayer(0, -1);
            }
            if (e.Key == Key.Down)
            {
                mazeDisplay.MovePlayer(0, 1);
            } */
        }
    }
}