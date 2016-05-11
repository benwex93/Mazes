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

namespace ClientGui.ViewModel
{
    public class Player2ViewModel : INotifyPropertyChanged
    {
        private ServerSpeaker speaker;
        private ObservableCollection<MazeBoxViewModel> boxList;
        private PlayerViewModel player;
        private PlayerViewModel end;
        private MazeData data;
        private ICommand keyUp;
        private ICommand keyDown;
        private ICommand keyRight;
        private ICommand keyLeft;
        public event PropertyChangedEventHandler PropertyChanged;

        public Player2ViewModel()
        {
            try
            {
                speaker = AppViewModel.GetServerSpeaker();
                MakeMazeData();
                boxList = MakeBoxList();
                CallPropertyChanged("BoxList");
                player = new PlayerViewModel(@"/Pictures/BenjyFinal.png", data.Start.Row, data.Start.Col);
                end = new PlayerViewModel(@"/Pictures/redsquare.png", data.End.Row, data.End.Col);
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
