using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ClientGui.View
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ClientGui.View.MainWindow window = new MainWindow();
            //ClientGui.MazeControlTest window = new MazeControlTest();
            ViewModel.AppViewModel.ConfigureInfo();
            //window.DataContext = VM;
            //MazeViewModel vm = new MazeViewModel();

            window.Show();
        }
    }
}
