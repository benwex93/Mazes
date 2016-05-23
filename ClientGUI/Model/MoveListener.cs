using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Web.Script.Serialization;

namespace ClientGui.Model
{
    public class MoveListener
    {
        private ServerSpeaker speaker;
        private Socket server;
        private bool stop;
        private ManualResetEvent done;
        //private IPEndPoint ipep;

        public MoveListener(Socket s, ServerSpeaker sp)
        {
            server = s;
            speaker = sp;
            stop = false;
            done = new ManualResetEvent(false);
        }

        public void ListenForMoves()
        {
            MoveStateObject state = null;
            IAsyncResult result = null;
            while (!stop)
            {
                done.Reset();
                state = new MoveStateObject();
                result = server.BeginReceive(state.buffer, 0, MoveStateObject.BufferSize, 0, new AsyncCallback(ReadCallBack), state);
                done.WaitOne();
            }
            Console.WriteLine("Broke out of while");
            server.Close();            // Stop waiting for another move
            speaker.MakeNewSocket();
        }

        private void ReadCallBack(IAsyncResult ar)
        {
            Console.WriteLine("Enters here again");
            MoveStateObject obj = (MoveStateObject)ar.AsyncState;
            int recv = 0;
            try
            {
                recv = server.EndReceive(ar);
            } catch (Exception e)
            {
                Console.WriteLine("After Close: " + e.ToString());
            }
            if (recv > 0)
            {
                string received = Encoding.ASCII.GetString(obj.buffer, 0, recv);
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                MoveData md = serializer.Deserialize<MoveData>(received);
                speaker.MoveOccured(md);
            }
            done.Set();
        }

        public void StopListening(object source, EventArgs e)
        {
            stop = true;
            done.Set();
        }
    }
}
