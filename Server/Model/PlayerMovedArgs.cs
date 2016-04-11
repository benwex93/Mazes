using System;

namespace Server
{
	public class PlayerMovedArgs : EventArgs
	{
		public Task player1;
		public string toSend;
		public PlayerMovedArgs (string str, Task t)
		{
			toSend = str;
			player1 = t;
		}
	}
}

