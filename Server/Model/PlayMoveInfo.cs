using System;

namespace Server
{
    /// <summary>
    /// An object that stores information about a play made in a multiplayer game.
    /// This object will be serialized to make a json to send back to the other player
    /// in the game.
    /// </summary>
	public class PlayMoveInfo
	{
		public string Name { get; set; }
		public string Move { get; set; }
        /// <summary>
        /// A constructor
        /// </summary>
        /// <param name="n">the name of the multiplayer game</param>
        /// <param name="m">the move made by the other player</param>
		public PlayMoveInfo (string n, string m)
		{
			Name = n;
			Move = m;
		}
	}
}

