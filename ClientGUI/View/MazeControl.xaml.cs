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
using ClientGui.ViewModel;

namespace ClientGui.View
{
    /// <summary>
    /// Interaction logic for MazeControl.xaml
    /// </summary>
    public partial class MazeControl : UserControl
    {
        public MazeControl()
        {
            InitializeComponent();
        }

        private void UserKeyDown(object sender, KeyEventArgs e)
        {
            MazeViewModel maze = (MazeViewModel)this.DataContext;
            ICommand comm = null;
            if (e.Key == Key.Down)
            {
                comm = maze.KeyDown;   
            } else if (e.Key == Key.Up)
            {
                comm = maze.KeyUp;
            } else if (e.Key == Key.Right)
            {
                comm = maze.KeyRight;
            } else if (e.Key == Key.Left)
            {
                comm = maze.KeyLeft;
            }
            try
            {
                if (comm.CanExecute(new object()))
                    comm.Execute(new object());
            }
            catch (Exception exc) { }
        }

        /*private void LeftMouseClick(object sender, MouseButtonEventArgs e)
        {
            Keyboard.Focus(sender as MazeControl);
        } */

        /*public void MoveKeyCommandToWindow()
        {
            MazeViewModel maze = new MazeViewModel();
            this.DataContext = maze;
            var window = FindAncestor<Window>(this);
            window.InputBindings.Add(new KeyBinding(maze.KeyDown, Key.Down, ModifierKeys.None));
        }

        private T FindAncestor<T>(DependencyObject d) where T : DependencyObject
        {
            for (var parent = VisualTreeHelper.GetParent(d); parent != null; parent = VisualTreeHelper.GetParent(parent))
            {
                var result = parent as T;
                if (result != null)
                    return result;
            }
            return null;
        } */
    }
}
