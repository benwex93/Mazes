using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Configuration;
using System.Threading;

namespace Client
{
    //
	class MainClass
	{
		private static void ReceiveBack(Socket server)
		{
			try {
                while (true)
                {
                    byte[] data = new byte[5096];
                    int recv = server.Receive(data);
                    string received = Encoding.ASCII.GetString(data, 0, recv);
                    Console.WriteLine(received);
                }
			}
			catch (SocketException e) {
				Console.WriteLine ("Error sending or receiving from server" + e.ToString ());
			}
		}

        public static void Main(string[] args)
        {
            string portStr = ConfigurationManager.AppSettings["port number"];
            string iPStr = ConfigurationManager.AppSettings["IP address"];
            int port = Int32.Parse(portStr);
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(iPStr), port);
            using (Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                try
                {
                    server.Connect(ipep);
                    Thread t = new Thread(() => ReceiveBack(server));
                    t.Start();
                    while (true)
                    {
                        string input = Console.ReadLine();
                        if (input == "exit") break;
                        server.Send(Encoding.ASCII.GetBytes(input));
                    }
                }
                catch (SocketException e) { Console.WriteLine("Unable to connect to server." + e.ToString()); }
            }
        }
	}
}
