using ClientGui.Model;
using ClientGui.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ClientGui.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public UserControl currentView;
        // simplified properties
        public UserControl CurrentView {
            get { return currentView; }
        }
        public MainWindowViewModel()
        {
            //sets main window view model of application
            AppModel.mainWindowViewModel = this;
            AppModel.SwitchCurrentView(new MainMenuControl());
        }

        public void MainWindow_PropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }
    }
}
