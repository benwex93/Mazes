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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ClientGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeBackgroundTimer();
            InitializeRunningAnimationTimer();
        }
        void InitializeBackgroundTimer()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Tick += timerBackgroundChange;
            timer.Start();
        }
        void InitializeRunningAnimationTimer()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += timerChangeRunner;
            timer.Start();
        }
        void timerBackgroundChange(object sender, EventArgs e)
        {
            SolidColorBrush brush = (SolidColorBrush)MainPanel.Background;
            if (brush.Color == Color.FromRgb(255, 255, 255))
                brush = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            else
                brush = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            MainPanel.Background = brush;
        }
        void timerChangeRunner(object sender, EventArgs e)
        {
            if (CalRunning1.Visibility == Visibility.Visible)
            {
                CalRunning1.Visibility = Visibility.Hidden;
                CalRunning2.Visibility = Visibility.Visible;
            }
            else if (CalRunning2.Visibility == Visibility.Visible)
            {
                CalRunning2.Visibility = Visibility.Hidden;
                CalRunning3.Visibility = Visibility.Visible;
            }
            else if (CalRunning3.Visibility == Visibility.Visible)
            {
                CalRunning3.Visibility = Visibility.Hidden;
                CalRunning4.Visibility = Visibility.Visible;
            }
            else
            {
                CalRunning4.Visibility = Visibility.Hidden;
                CalRunning1.Visibility = Visibility.Visible;
            }
        }

        private void SinglePlayer_IsMouseDirectlyOverChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(Select1.Visibility == Visibility.Hidden)
                Select1.Visibility = Visibility.Visible;
            else
                Select1.Visibility = Visibility.Hidden;
        }

        private void Select2_IsMouseDirectlyOverChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Select2.Visibility == Visibility.Hidden)
                Select2.Visibility = Visibility.Visible;
            else
                Select2.Visibility = Visibility.Hidden;
        }
        private void Multiplayer_IsMouseDirectlyOverChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Select2.Visibility == Visibility.Hidden)
                Select2.Visibility = Visibility.Visible;
            else
                Select2.Visibility = Visibility.Hidden;
        }

        private void Settings_IsMouseDirectlyOverChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Select3.Visibility == Visibility.Hidden)
                Select3.Visibility = Visibility.Visible;
            else
                Select3.Visibility = Visibility.Hidden;
        }

        private void SinglePlayer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SinglePlayer.Visibility = Visibility.Hidden;
        }
        private void Multiplayer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Multiplayer.Visibility = Visibility.Hidden;
        }
        private void Settings_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.ShowDialog();

        }
    }
}
