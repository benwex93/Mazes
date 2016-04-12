using System;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Server
{
    /// <summary>
    /// The "View" object that implements the IView interface. This object handles
    /// all connections with clients.
    /// </summary>
	public class MyView : IView
	{
        /// <summary>
        /// A reference to the program's "Presenter" object
        /// </summary>
		private IPresenter p;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="p">
        /// A reference to the Presenter object to be stored in this class</param>
		public MyView (IPresenter p)
		{
			this.p = p;
		}

        /// <summary>
        /// This function handles connections with potential clients. It creates a Socket,
        /// listens for connections, and every time a client connects, it creates a new thread
        /// to handle interactions with that client.
        /// </summary>
		public void DoConnections()
		{
			string portStr = ConfigurationManager.AppSettings ["port number"];
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

