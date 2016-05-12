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
        private PlayerViewModel player, hintBox1, hintBox2;
        private MazeData data;
        private string mazeString;
        public HintCalculator(PlayerViewModel player, PlayerViewModel  hintBox1, PlayerViewModel hintBox2)
        {
            this.player = player;
            this.hintBox1 = hintBox1;
            this.hintBox2 = hintBox2;
        }
        public PlayerViewModel GetHintBox1(PlayerViewModel hintBox)
        {
            hintBox1 = new PlayerViewModel(hintBox1.Image, player.Row, player.Col, player.displayMazeHeight, player.displayMazeWidth);
            hintBox2 = new PlayerViewModel(hintBox2.Image, player.Row, player.Col, player.displayMazeHeight, player.displayMazeWidth);
            mazeString = MakeMazeData(AppViewModel.GetServerSpeaker().Get_Reply());
            if (mazeString[player.Col + (player.Row * AppViewModel.GetMazeDimensions().length) + 1] == '2')
            {
                hintBox1.Col++;
                hintBox2.Col += 2;
            }
            else if (mazeString[player.Col + (player.Row * AppViewModel.GetMazeDimensions().length) - 1] == '2')
            {
                hintBox1.Col--;
                hintBox2.Col -= 2;
            }
            else if (mazeString[player.Col + ((player.Row + 1) * AppViewModel.GetMazeDimensions().length)] == '2')
            {
                hintBox1.Row++;
                hintBox2.Row += 2;
            }
            else if (mazeString[player.Col + ((player.Row - 1) * AppViewModel.GetMazeDimensions().length)] == '2')
            {
                hintBox1.Row--;
                hintBox2.Row -= 2;
            }
            return hintBox1;
        }
        public PlayerViewModel GetHintBox2()
        {
            return hintBox2;
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
