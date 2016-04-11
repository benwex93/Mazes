using System;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;
using System.Threading;

namespace Server
{//
    /// <summary>
    /// /ddd/
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
			/*manager.AddTask ("generate", new GenerateTask ())/;
			manager.AddTask ("solve", new SolveTask ());
			manager.AddTask ("multiplayer", new MultiplayTask ());
			manager.AddTask ("play", new PlayTask ());
			manager.AddTask ("close", new CloseTask ()); */
		}
		//public void MakeOptions() {
			//options.Add
		//}

		public void ReplyReady(object source, FinishedTaskEventArgs info)
		{	
			TaskHandler handler = (TaskHandler)source;
			//if (info.stopListening)
				//handler.ReplyReady -= this.ReplyReady;
			if (info.toSend != "") {
				byte[] data = new byte[5096];
				data = Encoding.ASCII.GetBytes (info.toSend);
				int bytes = Encoding.ASCII.GetByteCount (info.toSend);
				client.Send (data, bytes, SocketFlags.None);
			}
		}

		public void handle(Object State) {
			//IPresenter presenter = (IPresenter)ThreadContext;
			while (true) {
				byte[] data = new byte[1024];
				int recv = client.Receive (data);
				if (recv == 0)
					break;
				string str = Encoding.ASCII.GetString (
					data, 0, recv);
				p.DoNewTask (str, tHandler);

				//Console.WriteLine (str);
				Console.WriteLine ("continues in parallel");
				//data = Encoding.ASCII.GetBytes (str);
				//client.Send (data, recv, SocketFlags.None);
			}
			Console.WriteLine ("left while");
			client.Close ();
		}
	}
}

