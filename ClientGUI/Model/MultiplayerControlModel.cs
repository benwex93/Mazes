﻿using ClientGui.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientGui.Model
{
    public class MultiplayerControlModel
    {
        public void SinglePlayerOption()
        {
            AppModel.SwitchCurrentView(new MainMenuControl());
        }
        public void getHintOnMaze()
        {
            AppModel.SwitchCurrentView(new MainMenuControl());
        }
    }
}
