using ClientGui.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientGui.Model
{
    public static class HintShow
    {
        private static MazeViewModel MVM;
        public static void LoadMazeViewModel(MazeViewModel mvm)
        {
            MVM = mvm;
        }
        public static void DisplayHint()
        {
            MVM.CallPropertyChanged("FirstHintMargin");
            MVM.CallPropertyChanged("SecondHintMargin");
            MVM.HintVisibility = System.Windows.Visibility.Visible;
        }
        public static void HideHint()
        {
            MVM.HintVisibility = System.Windows.Visibility.Hidden;
        }
    }
}
