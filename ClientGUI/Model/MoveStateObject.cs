using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientGui.Model
{
    public class MoveStateObject
    {
        public const int BufferSize = 30;
        public byte[] buffer = new byte[BufferSize];
    }
}
