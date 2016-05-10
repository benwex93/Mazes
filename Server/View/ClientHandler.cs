using System;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;
using System.Threading;

namespace Server
{//
    /// <summary>
    /// A class that handles a specific client's requests. It also handles
    /// the event signifying when the reply for this request is ready to be
    /// sent back, and it sends the reply back to the client.
    /// </summary>
	public class ClientHandler
	{
		private Socket client;
		private IPresenter p;
		private TaskHandler tHandler;
		public ClientHandler (Socket s, IPresenter p)
		{
			this.client = s;
			this.p = p;
			tHandler = p.MakeNewTaskHandler ();
			tHandler.ReplyReady += new TaskHandler.ReplyReadyHandler (this.ReplyReady);
		}

        /// <summary>
        /// A function to be called when a reply is ready to be sent back to
        /// the client. It handles the event ReplyReady.
        /// </summary>
        /// <param name="source"> The object that triggered the event</param>
        /// <param name="info">An instance of FinishedTaskEventArgs,
        /// which inherits from EventArgs, and contains the string
        /// to send back to the client</param>
		public void ReplyReady(object source, FinishedTaskEventArgs info)
		{	
			TaskHandler handler = (TaskHandler)source;
			if (info.toSend != "") {
				byte[] data = new byte[5096];
				data = Encoding.ASCII.GetBytes (info.toSend);
				int bytes = Encoding.ASCII.GetByteCount (info.toSend);
				client.Send (data, bytes, SocketFlags.None);
			}
		}

        /// <summary>
        /// Receives requests from the client and sends them to the TaskHandler,
        /// so that the requested task is carried out.
        /// </summary>
        /// <param name="State">this parameter is not used in the function.
        /// The Thread class, which calls this function, requires this parameter</param>
		public void handle(Object State)
        {
			while (true) {
                try
                {
                    byte[] data = new byte[1024];
                    int recv = client.Receive(data);
                    if (recv == 0)
                        break;
                    string str = Encoding.ASCII.GetString(
                        data, 0, recv);
                    p.DoNewTask(str, tHandler);
                } catch
                {

                }
			}
			client.Close ();
		}
	}
}