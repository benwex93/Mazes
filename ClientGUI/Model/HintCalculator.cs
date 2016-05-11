using ClientGui.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ClientGui.Model
{
    public class HintCalculator
    {
        private PlayerViewModel player, hintBox1, hintBox2, hintBox3;
        private MazeData data;
        private string mazeString;
        int[,] MazeArray;
        public HintCalculator(PlayerViewModel player, PlayerViewModel  hintBox1, PlayerViewModel hintBox2, 
            PlayerViewModel hintBox3, string MazeString)
        {
            this.player = player;
            this.hintBox1 = hintBox1;
            this.hintBox2 = hintBox2;
            this.hintBox3 = hintBox3;
            mazeString = MakeMazeData(MazeString);
            this.MazeArray = new int[AppViewModel.GetMazeDimensions().height,AppViewModel.GetMazeDimensions().length];
            for(int i = 0; i < AppViewModel.GetMazeDimensions().height; i++)
            {
                for (int j = 0; j < AppViewModel.GetMazeDimensions().length; j++)
                    MazeArray[i, j] = MazeString[j + i * AppViewModel.GetMazeDimensions().height];
            }

        }
        public PlayerViewModel GetHintBox1()
        {
            BFStoBox1(hintBox1);
            return hintBox1;
        }
        public PlayerViewModel GetHintBox2()
        {
            BFStoBox2(hintBox2);
            return hintBox2;
        }
        public PlayerViewModel GetHintBox3()
        {
            BFStoBox3(hintBox3);
            return hintBox3;
        }
        private void BFStoBox1(PlayerViewModel hint1)
        {
            hint1.Col = 5;
            hint1.Row = 5;
        }
        private void BFStoBox2(PlayerViewModel hint2)
        {
            hint2.Col = 5;
            hint2.Row = 5;
        }
        private void BFStoBox3(PlayerViewModel hint3)
        {
            hint3.Col = 5;
            hint3.Row = 5;
        }
        private string MakeMazeData(string str)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            if (AppViewModel.CurrentGameIsMulti == false)
            {
                data = serializer.Deserialize<MazeData>(str);
            }
            else
            {
                MultiplayerData multiData = serializer.Deserialize<MultiplayerData>(str);
                data = multiData.You;
            }
            DoTheThing();
            return data.Maze;
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
