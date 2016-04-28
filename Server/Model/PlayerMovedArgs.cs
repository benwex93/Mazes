using System;

namespace Server
{
    /// <summary>
    /// A class that contains arguments to be sent when a PlayerMoved event occurs
    /// </summary>
	public class PlayerMovedArgs : EventArgs
	{
		public Task player1;
		public string toSend;
        /// <summary>
        /// A constructor
        /// </summary>
        /// <param name="str">the string to send to the other player in the game</param>
        /// <param name="t">a reference to the Task requested by the other player in the
        /// game, to whom the string will be sent to</param>
		public PlayerMovedArgs (string str, Task t)
		{
			toSend = str;
			player1 = t;
		}
	}
}

