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

namespace ClientGui.View
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
        }
        /*private void IPAddress_GotFocus(object sender, RoutedEventArgs e)
        {
            if (IPAddress.Text == "IP Address")
                IPAddress.Text = "";
            if (PortNumber.Text.Length == 0)
                PortNumber.Text = "Port Number";
        }

        private void PortNumber_GotFocus(object sender, RoutedEventArgs e)
        {
            if (PortNumber.Text == "Port Number")
                PortNumber.Text = "";
            if (IPAddress.Text.Length == 0)
                IPAddress.Text = "IP Address";
        } */
        /// <summary>
        /// Gets ip address and port number then closes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OKbutton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
