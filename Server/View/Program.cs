using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Configuration;
using Mazes;

namespace Server
{
    /// <summary>
    /// The Main class of the Server program
    /// </summary>
	class MainClass
	{
        /// <summary>
        /// The server's main method. It creates the model, presenter, and view and tells the view to
        /// start handling connections with clients. It also sets the dimensions of all mazes made in
        /// this program.
        /// </summary>
        /// <param name="args"></param>
		public static void Main (string[] args)
		{
            string lenStr = ConfigurationManager.AppSettings["maze length"];
            string heightStr = ConfigurationManager.AppSettings["maze height"];
            int height = Int32.Parse(heightStr);
            int len = Int32.Parse(lenStr);
            Mazes.MazeProgram.ConfigureDimensions(height, len);

			IPresenter p = new MyPresenter ();
			IModel m = new MyModel (p);
			IView v = new MyView (p);
			p.SetView (v);
			p.SetModel (m);
			v.DoConnections (); // Connect with clients
		}
	}
}

