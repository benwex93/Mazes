﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Web.Script.Serialization;
using System.ComponentModel;
using ClientGui.Model;
using System.Windows;
using System.Windows.Input;
using ClientGui.View;

namespace ClientGui.ViewModel
{
    public class MazeViewModel : INotifyPropertyChanged
    {
        private ServerSpeaker speaker;
        private double mainWinHeight = SystemParameters.PrimaryScreenHeight;
        private double mainWinWidth = SystemParameters.PrimaryScreenWidth;
        private ObservableCollection<MazeBoxViewModel> boxList;
        private PlayerViewModel player;
        private PlayerViewModel end;
        private PlayerViewModel firstHint;
        private PlayerViewModel secondHint;
        private PlayerViewModel thirdHint;
        private HintCalculator hintCalculator;
        private MazeData data;
        private ICommand keyUp;
        private ICommand keyDown;
        private ICommand keyRight;
        private ICommand keyLeft;
        public event PropertyChangedEventHandler PropertyChanged;
        public MazeViewModel()
        {
            try
            {
                speaker = AppViewModel.GetServerSpeaker();
                MakeMazeData();
                boxList = MakeBoxList();
                CallPropertyChanged("BoxList");
                player = new PlayerViewModel(@"/Pictures/CalFinal.png", data.Start.Row, data.Start.Col, DisplayMazeHeight, DisplayMazeWidth);
                end = new PlayerViewModel(@"/Pictures/redsquare.png", data.End.Row, data.End.Col, DisplayMazeHeight, DisplayMazeWidth);
                firstHint = new PlayerViewModel(@"/Pictures/yellowsquare.png", data.Start.Row, data.Start.Col-1, DisplayMazeHeight, DisplayMazeWidth);
                secondHint = new PlayerViewModel(@"/Pictures/yellowsquare.png", data.Start.Row, data.Start.Col-2, DisplayMazeHeight, DisplayMazeWidth);
                thirdHint = new PlayerViewModel(@"/Pictures/yellowsquare.png", data.Start.Row, data.Start.Col-3, DisplayMazeHeight, DisplayMazeWidth);
                hintCalculator = new HintCalculator(player, firstHint, secondHint, thirdHint);
                keyUp = new KeyUpCommand(this);
                keyDown = new KeyDownCommand(this);
                keyRight = new KeyRightCommand(this);
                keyLeft = new KeyLeftCommand(this);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in MazeViewModel constructor. " + e.ToString());
            }  
        }
        public double DisplayMazeHeight
        {
            get
            {
                //if height reaches bounds of screen first
                if (AppViewModel.GetMazeDimensions().height * mainWinHeight < AppViewModel.GetMazeDimensions().length * mainWinWidth)
                    return (mainWinHeight - 155); //takes height of pic on top into account;
                else
                    return AppViewModel.GetMazeDimensions().height * ((mainWinWidth) / AppViewModel.GetMazeDimensions().length);
            }
        }
        public double DisplayMazeWidth
        {
            get
            {
                //if height reaches bounds of screen first
                if (AppViewModel.GetMazeDimensions().height * mainWinHeight < AppViewModel.GetMazeDimensions().length * mainWinWidth)
                    return AppViewModel.GetMazeDimensions().length * ((mainWinHeight - 155) / AppViewModel.GetMazeDimensions().height);
                else
                    return mainWinWidth;
            }
        }
        public ObservableCollection<MazeBoxViewModel> BoxList
        {
            get
            {
                return boxList;
            }
            set { }
        }

        private void MakeMazeData()
        {
            string str = speaker.Get_Reply();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            if (AppViewModel.CurrentGameIsMulti == false)
            {
                data = serializer.Deserialize<MazeData>(str);
            } else
            {
                MultiplayerData multiData = serializer.Deserialize<MultiplayerData>(str);
                data = multiData.You;        
            }
            DoTheThing();
        }

        private ObservableCollection<MazeBoxViewModel> MakeBoxList()
        {
            ObservableCollection<MazeBoxViewModel> bList = new ObservableCollection<MazeBoxViewModel>();
            string mazeStr = data.Maze;

            //Console.WriteLine(mazeStr);

            MazeDimensions dims = AppViewModel.GetMazeDimensions();
            int height = dims.height;
            int length = dims.length;
            int i, j;
            for (i = 0; i < height; i++)
            {
                for (j = 0; j < length; j++)
                {
                    char currChar = mazeStr[j + i * length];
                    if (currChar == '1')
                    {
                        bList.Add(new MazeBoxViewModel() {colorImg = @"/Pictures/blacksquare.jpg" });
                    }
                    else
                    {
                        bList.Add(new MazeBoxViewModel() {colorImg = @"/Pictures/greensquare.png" });
                    }
                }
            }
            return bList;
        }
        public Thickness PlayerMargin
        {
            get
            {
                return new Thickness(player.MargLeft, player.MargTop, player.MargRight, player.MargBott);
            }
        }

        public string PlayerImg
        {
            get
            {
                return player.Image;
            }
            set { }
        }
        public string EndImg
        {
            get
            {
                return end.Image;
            }
            set { }
        }
        public Thickness EndMargin
        {
            get
            {
                return new Thickness(end.MargLeft, end.MargTop, end.MargRight, end.MargBott);
            }
        }
        public void GetHintOnMaze()
        {
            AppModel.SwitchCurrentView(new MainMenuControl());
        }
        public string HintImg
        {
            get
            {
                return firstHint.Image;
            }
            set { }
        }
        public Thickness FirstHintMargin
        {
            get
            {
                firstHint = hintCalculator.GetHintBox1();
                return new Thickness(firstHint.MargLeft, firstHint.MargTop, firstHint.MargRight, firstHint.MargBott);
            }
        }
        public Thickness SecondHintMargin
        {
            get
            {
                secondHint = hintCalculator.GetHintBox2();
                return new Thickness(secondHint.MargLeft, secondHint.MargTop, secondHint.MargRight, secondHint.MargBott);
            }
        }
        public Thickness ThirdHintMargin
        {
            get
            {
                thirdHint = hintCalculator.GetHintBox3();
                return new Thickness(thirdHint.MargLeft, thirdHint.MargTop, thirdHint.MargRight, thirdHint.MargBott);
            }
        }
        public ICommand KeyUp
        {
            get
            {
                return keyUp;
            }
        }

        public ICommand KeyDown
        {
            get
            {
                Console.WriteLine("Gets key down");               // PRINTED
                return keyDown;
            }
        }

        public ICommand KeyRight
        {
            get
            {
                return keyRight;
            }
        }

        public ICommand KeyLeft
        {
            get
            {
                return keyLeft;
            }
        }

        public void MoveLeft()
        {
            player.Col = player.Col - 1;
            CallPropertyChanged("PlayerMargin");
            speaker.PlayCommand("left");
            CheckIfWon();
        }
        
        public void MoveRight()
        {
            player.Col = player.Col + 1;
            CallPropertyChanged("PlayerMargin");
            speaker.PlayCommand("right");
            CheckIfWon();
        }

        public void MoveUp()
        {
            player.Row = player.Row - 1;
            CallPropertyChanged("PlayerMargin");
            speaker.PlayCommand("up");
            CheckIfWon();
        }

        public void MoveDown()
        {
            player.Row = player.Row + 1;
            CallPropertyChanged("PlayerMargin");
            speaker.PlayCommand("down");
            CheckIfWon();
        }

        private void CallPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        public PlayerViewModel GetPlayer()
        {
            return player;
        }

        public MazeData GetMazeData()
        {
            return data;
        }

        private void DoTheThing()
        {
            int temp = data.Start.Col;
            data.Start.Col = data.Start.Row;
            data.Start.Row = temp;
            temp = data.End.Col;
            data.End.Col = data.End.Row;
            data.End.Row = temp;
        }
        private void CheckIfWon()
        {
            if (player.Col == data.End.Col && player.Row == data.End.Row)
            {
                VictoryScreen SW = new VictoryScreen();
                SW.ShowDialog();
            }
        }
    }
}
