using ClientGui.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace ClientGui.ViewModel
{
    public class MainMenuControlViewModel : INotifyPropertyChanged
    {
        public MainMenuControlViewModel()
        {
            model = new MainMenuControlModel(this);
            singlePlayerCommand = new ButtonICommand(model.SinglePlayerOption);
            multiplayerCommand = new ButtonICommand(model.MultiplayerOption);
            settingsCommand = new ButtonICommand(model.SettingsOption);
        }
        MainMenuControlModel model;
        public event PropertyChangedEventHandler PropertyChanged;
        private ButtonICommand singlePlayerCommand;
        private ButtonICommand multiplayerCommand;
        private ButtonICommand settingsCommand;

        public SolidColorBrush backgroundColor { get; set; } = new SolidColorBrush(Color.FromRgb(0, 0, 0));
        public Visibility calRunning1Visibility { get; set; } = Visibility.Visible;
        public Visibility calRunning2Visibility { get; set; } = Visibility.Hidden;
        public Visibility calRunning3Visibility { get; set; } = Visibility.Hidden;
        public Visibility calRunning4Visibility { get; set; } = Visibility.Hidden;
        public Visibility menuSelectionVisibility1 { get; set; } = Visibility.Visible;
        public Visibility menuSelectionVisibility2 { get; set; } = Visibility.Hidden;
        public Visibility menuSelectionVisibility3 { get; set; } = Visibility.Hidden;

        public ICommand goToSinglePlayerScreen
        {
            get
            {
                return singlePlayerCommand;
            }
        }
        public ICommand goToMultiplayerScreen
        {
            get
            {
                return multiplayerCommand;
            }
        }
        public ICommand goToSettings
        {
            get
            {
                return settingsCommand;
            }
        }
        public void MainMenuVM_PropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }
    }
}
