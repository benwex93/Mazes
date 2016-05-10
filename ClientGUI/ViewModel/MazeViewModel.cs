using System;
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

namespace ClientGui.ViewModel
{
    public class MazeViewModel : INotifyPropertyChanged
    {
        private ServerSpeaker speaker;
        private ObservableCollection<MazeBoxViewModel> boxList;
        private PlayerViewModel player;
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
                GetMazeData(speaker.Get_Reply());
                boxList = MakeBoxList();
                CallPropertyChanged("BoxList");
                player = new PlayerViewModel(@"/Pictures/CalFinal.png", data.Start.Row, data.Start.Col);
                CallPropertyChanged("PlayerMargin");
                keyUp = new KeyUpCommand(this);
                keyDown = new KeyDownCommand(this);
                keyRight = new KeyRightCommand(this);
                keyLeft = new KeyLeftCommand(this);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error MazeViewModel Constructor " + e.ToString());
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

        private void GetMazeData(string str)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            data = serializer.Deserialize<MazeData>(str);
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

        /*public double PlayerMarginLeft
        {
            get
            {
                return player.Col * 13.5;
            }
            set
            {

            }
        }

        public double PlayerMarginRight
        {
            get
            {
                return (40 - 1 - player.Col) * 13.5;
            }
            set
            {

            }
        }

        public double PlayerMarginTop
        {
            get
            {
                return player.Row * 13.8;
            }
        }

        public double PlayerMarginBott
        {
            get
            {
                return (50 - 1 - player.Row) * 13.8;
            }
        } */

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
    }
}
