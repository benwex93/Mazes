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
        private MainMenuControlViewModel mainMenuVM;
        private ServerSpeaker speaker;
        private GameNameSetter GNS;
        public MainMenuControlModel(MainMenuControlViewModel mainMenuVM)
        {
            this.mainMenuVM = mainMenuVM;
            InitializeRunningAnimationTimer();
            InitializeBackgroundTimer();
            speaker = AppViewModel.GetServerSpeaker();
            speaker.MultiplayReady += this.MultiplayerReady;
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
            mainMenuVM.MainMenuVM_PropertyChanged("backgroundColor");
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
                animationTickCounter++;
            }
            else if (animationTickCounter == 1)
            {
                mainMenuVM.calRunning2Visibility = Visibility.Hidden;
                mainMenuVM.calRunning3Visibility = Visibility.Visible;
                mainMenuVM.menuSelectionVisibility2 = Visibility.Hidden;
                mainMenuVM.menuSelectionVisibility3 = Visibility.Visible;
                animationTickCounter++;
            }
            else if (animationTickCounter == 2)
            {
                mainMenuVM.calRunning3Visibility = Visibility.Hidden;
                mainMenuVM.calRunning4Visibility = Visibility.Visible;
                mainMenuVM.menuSelectionVisibility3 = Visibility.Hidden;
                mainMenuVM.menuSelectionVisibility1 = Visibility.Hidden;
                animationTickCounter++;
            }
            else
            {
                mainMenuVM.menuSelectionVisibility1 = Visibility.Visible;
                mainMenuVM.calRunning4Visibility = Visibility.Hidden;
                mainMenuVM.calRunning1Visibility = Visibility.Visible;
                animationTickCounter = 0;
            }
        }
        public void SinglePlayerOption()
        {
            speaker.GenerateCommand();
            AppViewModel.CurrentGameIsMulti = false;
            AppModel.SwitchCurrentView(new SinglePlayerControl());
        }
        public void MultiplayerOption()
        {
            GNS.Close();
            speaker.MultiplayerCommand(mainMenuVM.MultiplayGameName);
            Console.WriteLine("Waiting for multiplayer...and the Messiah");
            //AppModel.SwitchCurrentView(new MultiplayerControl());
        }
        public void SettingsOption()
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.ShowDialog();
        }

        public void MultiplayerNameSetter()
        {
            mainMenuVM.waitingVisibility = Visibility.Visible;
            GNS = new GameNameSetter();
            GNS.DataContext = mainMenuVM;
            GNS.ShowDialog();
        }

        public void MultiplayerReady(object source, EventArgs e)
        {
            AppViewModel.CurrentGameIsMulti = true;
            AppModel.SwitchCurrentView(new MultiplayerControl());
        }
    }
}
