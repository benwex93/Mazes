using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Configuration;

namespace Server
{
    /// <summary>
    /// The Main class of the Server program
    /// </summary>
	class MainClass
	{
        /// <summary>
        /// The server's main method. It creates the model, presenter, and view and tells the view to
        /// start handling connections with clients.
        /// </summary>
        /// <param name="args"></param>
		public static void Main (string[] args)
		{
			IPresenter p = new MyPresenter ();
			IModel m = new MyModel (p);
			IView v = new MyView (p);
			p.SetView (v);
			p.SetModel (m);
			v.DoConnections (); // Connect with clients
		}
	}
}

