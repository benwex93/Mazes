using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Web.Script.Serialization;

namespace ClientGui.Model
{
    public class ServerSpeaker
    {
        private SettingsInfo configs;
        private string server_Reply;
        private Socket server;
        private IPEndPoint ipep;
        private string singleMazeName;
        private int mazeCount;

        public delegate void MultiplayReadyHandler(object source, EventArgs info);
        public event MultiplayReadyHandler MultiplayReady;

        public delegate void OtherMovedHandler(object source, PlayerMovedEventArgs info);
        public event OtherMovedHandler OtherMoved;

        public ServerSpeaker(SettingsInfo info)
        {
            configs = info;
            server_Reply = null;
            //server_Reply = "{ \"Name\":\"maze\",\"Maze\":\"00000000000000000100000001000000000000010111111101111111010111110101111111111101000100010000000101000001000100010000010111010111111111011111110111110101011101010101000000000100000100010000010101000101010101111111011111010111010111010111110100010100000101000001010001010000010000010111011111010101110101111101111111011101010000000001010100010000000100010001000101111111110101010111011111010101011111010000000100010101010001000101010001000101111111010111010111011101010101111101011101000101010001010000010101010001000100010101010111010101011111010111110101111101010100010001010101000101010001010100010101011111011101010101010101010101010111010001000001000100010101010101010101010001011111110101111111010101010101010101010100000000010000000101010101010100000101011111111111111101110101010101011111110101000000010100000100010101000101000000010101111101010111110111010111110101111111010100010001010100010000010000010001000101010101111101010111111111011111010101110101010100010101010000010001000001010000010101010101010101011101011111011101011111000101010001010101000101000101000100000101110101111101010101110101011101110111110001000100000100010001000100000100010001110111110111111111110111111111011111010101010000000000000001010001000001000001010101111101111111110101011101111101111101010000010100010000010100010100000101000101111101010111011101010101010111110101110100000101010001000101010001010001010101010111110101011101110101111101011101010100010000000101000100010001000101000001010111011111110101110111110101110101111101010001000100010101010000010000010100000101011101010111010101111111111111010111110101010100010001010000000001000101010001010101011111011101111111010101010101010101000101000101000000000101010101010001010111110101010111110111110101110101111101000000010100010001000100010100010100010111111111011111010111010111010101010101010100000100000101000001000100010101010001010111011111010111110111011111110101110100000100000000010000000100000000000100011111111111111111111111111111111111111111\",\"Start\":{ \"Row\":32,\"Col\":34},\"End\":{ \"Row\":16,\"Col\":38} }";
            ipep = new IPEndPoint(IPAddress.Parse(configs.ip), configs.port);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);
            } catch (Exception e)
            {
                Console.WriteLine("Error connecting to server. " + e.ToString());
            }
            mazeCount = 0;
        }

        public void SetInfo(SettingsInfo info)
        {
            configs = info;
        }

        public string Get_Reply()
        {
            return server_Reply;
        }

        public void GenerateCommand()
        {
            mazeCount++;
            singleMazeName = "maze" + mazeCount;
            string toSend = "generate " + singleMazeName + " 1";
            //Console.WriteLine(toSend);
            server.Send(Encoding.ASCII.GetBytes(toSend));
            ReceiveBack(false);
        }

        public void MultiplayerCommand(string nameOfGame)
        {
            string toSend = "multiplayer " + nameOfGame;
            server.Send(Encoding.ASCII.GetBytes(toSend));
            ReceiveBack(true);
        }

        public void PlayCommand(string dir)
        {
            string toSend = "play " + dir;
            server.Send(Encoding.ASCII.GetBytes(toSend));
        }

        public void SolveCommand()
        {
            string name = "maze" + mazeCount;
            string toSend = "solve " + name + " 1";
            server.Send(Encoding.ASCII.GetBytes(toSend));
            ReceiveBack(false);
        }
        private void ReceiveBack(bool multi)
        {
            byte[] data = new byte[5096];
            int recv = server.Receive(data);
            server_Reply = Encoding.ASCII.GetString(data, 0, recv);
            Console.WriteLine(server_Reply);
            if (multi)
            {
                MultiplayReady(this, EventArgs.Empty);
                Thread thr = new Thread(() => this.ListenForMoves());
                thr.Start();
            }
        }

        private void ListenForMoves()
        {
            while (true)
            {
                byte[] data = new byte[100];
                int recv = server.Receive(data);
                string received = Encoding.ASCII.GetString(data, 0, recv);
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                MoveData md = serializer.Deserialize<MoveData>(received);
                OtherMoved(this, new PlayerMovedEventArgs(md));
            }
        }
    }
}
