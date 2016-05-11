using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientGui.Model
{
    public class PlayerMovedEventArgs : EventArgs
    {
        private MoveData info;
        public PlayerMovedEventArgs(MoveData md)
        {
            info = md;
        }

        public MoveData Info
        {
            get
            {
                return info;
            }
        }
    }
}
