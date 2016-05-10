using ClientGui.View;
using ClientGui.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace ClientGui.Model
{
    public class MainMenuControlModel
    {
        private int animationTickCounter = 0;
        MainMenuControlViewModel mainMenuVM;
        public MainMenuControlModel(MainMenuControlViewModel mainMenuVM)
        {
            this.mainMenuVM = mainMenuVM;
            InitializeRunningAnimationTimer();
            InitializeBackgroundTimer();
        }
        void InitializeBackgroundTimer()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Tick += timerBackgroundChange;
            timer.Start();
        }
        void timerBackgroundChange(object sender, EventArgs e)
        {
            SolidColorBrush brush = mainMenuVM.backgroundColor;
            if (brush.Color == Color.FromRgb(255, 255, 255))
                brush = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            else
                brush = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            mainMenuVM.backgroundColor = brush;
            mainMenuVM.MainMenuVM_PropertyChanged(new PropertyChangedEventArgs("backgroundColor"));
        }
        void InitializeRunningAnimationTimer()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += timerChangeRunner;
            timer.Start();
        }
        void timerChangeRunner(object sender, EventArgs e)
        {
            if (animationTickCounter == 0)
            {
                mainMenuVM.calRunning1Visibility = Visibility.Hidden;
                mainMenuVM.calRunning2Visibility = Visibility.Visible;
                mainMenuVM.menuSelectionVisibility1 = Visibility.Hidden;
                mainMenuVM.menuSelectionVisibility2 = Visibility.Visible;
                mainMenuVM.MainMenuVM_PropertyChanged(new PropertyChangedEventArgs("menuSelectionVisibility1"));
                mainMenuVM.MainMenuVM_PropertyChanged(new PropertyChangedEventArgs("menuSelectionVisibility2"));
                mainMenuVM.MainMenuVM_PropertyChanged(new PropertyChangedEventArgs("calRunning1Visibility"));
                mainMenuVM.MainMenuVM_PropertyChanged(new PropertyChangedEventArgs("calRunning2Visibility"));
                animationTickCounter++;
            }
            else if (animationTickCounter == 1)
            {
                mainMenuVM.calRunning2Visibility = Visibility.Hidden;
                mainMenuVM.calRunning3Visibility = Visibility.Visible;
                mainMenuVM.menuSelectionVisibility2 = Visibility.Hidden;
                mainMenuVM.menuSelectionVisibility3 = Visibility.Visible;
                mainMenuVM.MainMenuVM_PropertyChanged(new PropertyChangedEventArgs("menuSelectionVisibility2"));
                mainMenuVM.MainMenuVM_PropertyChanged(new PropertyChangedEventArgs("menuSelectionVisibility3"));
                mainMenuVM.MainMenuVM_PropertyChanged(new PropertyChangedEventArgs("calRunning2Visibility"));
                mainMenuVM.MainMenuVM_PropertyChanged(new PropertyChangedEventArgs("calRunning3Visibility"));
                animationTickCounter++;
            }
            else if (animationTickCounter == 2)
            {
                mainMenuVM.calRunning3Visibility = Visibility.Hidden;
                mainMenuVM.calRunning4Visibility = Visibility.Visible;
                mainMenuVM.menuSelectionVisibility3 = Visibility.Hidden;
                mainMenuVM.menuSelectionVisibility1 = Visibility.Visible;
                mainMenuVM.MainMenuVM_PropertyChanged(new PropertyChangedEventArgs("menuSelectionVisibility3"));
                mainMenuVM.MainMenuVM_PropertyChanged(new PropertyChangedEventArgs("menuSelectionVisibility1"));
                mainMenuVM.MainMenuVM_PropertyChanged(new PropertyChangedEventArgs("calRunning3Visibility"));
                mainMenuVM.MainMenuVM_PropertyChanged(new PropertyChangedEventArgs("calRunning4Visibility"));
                animationTickCounter++;
            }
            else
            {
                mainMenuVM.calRunning4Visibility = Visibility.Hidden;
                mainMenuVM.calRunning1Visibility = Visibility.Visible;
                mainMenuVM.MainMenuVM_PropertyChanged(new PropertyChangedEventArgs("calRunning4Visibility"));
                mainMenuVM.MainMenuVM_PropertyChanged(new PropertyChangedEventArgs("calRunning1Visibility"));
                animationTickCounter = 0;
            }
        }
        public void SinglePlayerOption()
        {
            AppModel.SwitchCurrentView(new MazeControl());
        }
        public void MultiplayerOption()
        {
            AppModel.SwitchCurrentView(new MazeControl());
        }
        public void SettingsOption()
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.ShowDialog();
        }
    }

}
