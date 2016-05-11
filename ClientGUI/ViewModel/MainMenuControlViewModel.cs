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
        public event PropertyChangedEventHandler PropertyChanged;
        public Visibility calRunning1Visibility { get; set; } = Visibility.Visible;
        public Visibility calRunning2Visibility { get; set; } = Visibility.Hidden;
        public Visibility calRunning3Visibility { get; set; } = Visibility.Hidden;
        public Visibility calRunning4Visibility { get; set; } = Visibility.Hidden;
        public Visibility menuSelectionVisibility1 { get; set; } = Visibility.Visible;
        public Visibility menuSelectionVisibility2 { get; set; } = Visibility.Hidden;
        public Visibility menuSelectionVisibility3 { get; set; } = Visibility.Hidden;
        public Visibility waitingVisibility { get; set; } = Visibility.Hidden;
        private string multiGameName;
        public MainMenuControlViewModel()
        {
            model = new MainMenuControlModel(this);
            singlePlayerCommand = new ButtonICommand(model.SinglePlayerOption);
            setNameCommand = new ButtonICommand(model.MultiplayerNameSetter);
            multiplayerCommand = new ButtonICommand(model.MultiplayerOption);
            settingsCommand = new ButtonICommand(model.SettingsOption);
        }
        MainMenuControlModel model;

        public ButtonICommand singlePlayerCommand;
        public ButtonICommand setNameCommand;
        public ButtonICommand settingsCommand;
        public ButtonICommand multiplayerCommand;

        public SolidColorBrush backgroundColor { get; set; } = new SolidColorBrush(Color.FromRgb(0, 0, 0));
        public ICommand goToSinglePlayerScreen
        {
            get
            {
                return singlePlayerCommand;
            }
        }
        public ICommand goToSetNameScreen
        {
            get
            {
                return setNameCommand;
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
        public string MultiplayGameName
        {
            get
            {
                return multiGameName;
            }
            set
            {
                multiGameName = value;
            }
        }
        public void MainMenuVM_PropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }
    }
}
