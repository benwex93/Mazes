using System;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Server
{
	public class MyView : IView
	{
		private IPresenter p;
		public MyView (IPresenter p)
		{
			this.p = p;
		}
		public void DoConnections()
		{
			string portStr = ConfigurationManager.AppSettings ["port number"];
			//string portStr = File.ReadAllText (@"/home/caleb/Desktop/app.config");
			int port = Int32.Parse (portStr);
			try {
				IPEndPoint ipep = new IPEndPoint(IPAddress.Any, port);
				using (Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
				{
					newsock.Bind(ipep);
					newsock.Listen(10);
					while (true) {
						Socket client = newsock.Accept();
						ClientHandler handler = new ClientHandler(client, this.p);
						//handler.MakeOptions ();
						//ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(handler.handle));
						Thread t = new Thread(handler.handle);
						t.Start();
					}
				}
			}
			catch (Exception e) {
				Console.WriteLine ("Server error" + e.StackTrace);
			}
		}
	}
}

