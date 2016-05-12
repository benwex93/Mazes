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
        public Visibility calRun1 = Visibility.Visible;
        public Visibility calRun2 = Visibility.Hidden;
        public Visibility calRun3 = Visibility.Hidden;
        public Visibility calRun4 = Visibility.Hidden;
        public Visibility waiting = Visibility.Hidden;
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
        public Visibility calRunning1Visibility
        {
            get
            {
                return calRun1;
            }
            set
            {
                calRun1 = value;
                MainMenuVM_PropertyChanged("calRunning1Visibility");
            }
        }
        public Visibility calRunning2Visibility
        {
            get
            {
                return calRun2;
            }
            set
            {
                calRun2 = value;
                MainMenuVM_PropertyChanged("calRunning2Visibility");
            }
        }
        public Visibility calRunning3Visibility
        {
            get
            {
                return calRun3;
            }
            set
            {
                calRun3 = value;
                MainMenuVM_PropertyChanged("calRunning3Visibility");
            }
        }
        public Visibility calRunning4Visibility
        {
            get
            {
                return calRun4;
            }
            set
            {
                calRun4 = value;
                MainMenuVM_PropertyChanged("calRunning4Visibility");
            }
        }
        public Visibility waitingVisibility
        {
            get
            {
                return waiting;
            }
            set
            {
                waiting = value;
                MainMenuVM_PropertyChanged("waitingVisibility");
            }
        }
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
        public void MainMenuVM_PropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
