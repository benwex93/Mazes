using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientGui.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Input;
using System.Threading;

namespace ClientGui.ViewModel
{
    public class Player2ViewModel : INotifyPropertyChanged
    {
        private ServerSpeaker speaker;
        private ObservableCollection<MazeBoxViewModel> boxList;
        private PlayerViewModel player;
        private PlayerViewModel end;
        private MazeData data;
        private double mainWinHeight = SystemParameters.PrimaryScreenHeight;
        private double mainWinWidth = SystemParameters.PrimaryScreenWidth;
        public event PropertyChangedEventHandler PropertyChanged;

        public Player2ViewModel()
        {
            try
            {
                speaker = AppViewModel.GetServerSpeaker();
                MakeMazeData();
                boxList = MakeBoxList();
                CallPropertyChanged("BoxList");
                player = new PlayerViewModel(@"/Pictures/BenjyFinal.png", data.Start.Row, data.Start.Col, DisplayMazeHeight, DisplayMazeWidth);
                end = new PlayerViewModel(@"/Pictures/redsquare.png", data.End.Row, data.End.Col, DisplayMazeHeight, DisplayMazeWidth);
                speaker.OtherMoved += this.OtherPlayerMoved;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in MazeViewModel constructor. " + e.ToString());
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
            MultiplayerData multiData = serializer.Deserialize<MultiplayerData>(str);
            data = multiData.Other;
            DoTheThing();
        }

        private ObservableCollection<MazeBoxViewModel> MakeBoxList()
        {
            ObservableCollection<MazeBoxViewModel> bList = new ObservableCollection<MazeBoxViewModel>();
            string mazeStr = data.Maze;

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
                        bList.Add(new MazeBoxViewModel() { colorImg = @"/Pictures/blacksquare.jpg" });
                    }
                    else
                    {
                        bList.Add(new MazeBoxViewModel() { colorImg = @"/Pictures/greensquare.png" });
                    }
                }
            }
            return bList;
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
        public Thickness EndMargin
        {
            get
            {
                return new Thickness(end.MargLeft, end.MargTop, end.MargRight, end.MargBott);
            }
        }
        public string EndImg
        {
            get
            {
                return end.Image;
            }
            set { }
        }

        private void OtherPlayerMoved(object source, PlayerMovedEventArgs p)
        {
            MoveData data = p.Info;
            string dir = data.Move;
            if (dir == "left")
                this.MoveLeft();
            else if (dir == "right")
                this.MoveRight();
            else if (dir == "up")
                this.MoveUp();
            else if (dir == "down")
                this.MoveDown();
        }

        public void MoveLeft()
        {
            player.Col = player.Col - 1;
            CallPropertyChanged("PlayerMargin");
        }

        public void MoveRight()
        {
            player.Col = player.Col + 1;
            CallPropertyChanged("PlayerMargin");
        }

        public void MoveUp()
        {
            player.Row = player.Row - 1;
            CallPropertyChanged("PlayerMargin");
        }

        public void MoveDown()
        {
            player.Row = player.Row + 1;
            CallPropertyChanged("PlayerMargin");
        }

        private void CallPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
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
    }
}
