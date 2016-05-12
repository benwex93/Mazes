using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Web.Script.Serialization;

namespace ClientGui.Model
{
    public class MoveListener
    {
        private ServerSpeaker speaker;
        private Socket server;
        private bool stop;
        //private IPEndPoint ipep;

        public MoveListener(Socket s, ServerSpeaker sp)
        {
            server = s;
            speaker = sp;
            stop = false;
        }

        public void ListenForMoves()
        {
            while (!stop)
            {
                byte[] data = new byte[5096];
                int recv = server.Receive(data);
                string received = Encoding.ASCII.GetString(data, 0, recv);
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                MoveData md = serializer.Deserialize<MoveData>(received);
                speaker.MoveOccured(md);
            }
        }

        public void StopListening(object source, EventArgs e)
        {
            stop = true;
        }
    }
}
