using System;

namespace Server
{
	public class PlayMoveInfo
	{
		public string Name { get; set; }
		public string Move { get; set; }
		public PlayMoveInfo (string n, string m)
		{
			Name = n;
			Move = m;
		}
	}
}

