using ClientGui.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientGui.Model
{
    public class HintCalculator
    {
        private PlayerViewModel player, hintBox1, hintBox2, hintBox3;
        public HintCalculator(PlayerViewModel player, PlayerViewModel  hintBox1, PlayerViewModel hintBox2, 
            PlayerViewModel hintBox3)
        {
            this.player = player;
            this.hintBox1 = hintBox1;
            this.hintBox2 = hintBox2;
            this.hintBox3 = hintBox3;
        }
        public PlayerViewModel GetHintBox1()
        {
            //BFStoBox(hi)
            return hintBox1;
        }
        public PlayerViewModel GetHintBox2()
        {
            //BFStoBox(hi)
            return hintBox2;
        }
        public PlayerViewModel GetHintBox3()
        {
            //BFStoBox(hi)
            return hintBox3;
        }
    }
}
