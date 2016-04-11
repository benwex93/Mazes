using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Configuration;

namespace Server
{
	class MainClass
	{
		public static void Main (string[] args)
		{

			IPresenter p = new MyPresenter ();
			IModel m = new MyModel (p);
			IView v = new MyView (p);
			p.SetView (v);
			p.SetModel (m);
			v.DoConnections ();
			/*string portStr = ConfigurationManager.AppSettings ["port number"];
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
						ClientHandler handler = new ClientHandler(client);
						//handler.MakeOptions ();
						//ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(handler.handle));
						Thread t = new Thread(handler.handle);
						t.Start();
					}
				}
			}
			catch (Exception e) {
				Console.WriteLine ("Server error" + e.StackTrace);
			} */
		}
	}
}

