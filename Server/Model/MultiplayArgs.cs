using System;

namespace Server
{
	public class MultiplayArgs : EventArgs
	{
		public Task t1;
		public Task t2;
        public string nameOfGame;
		public MultiplayArgs (Task t1, Task t2, string name)
		{
			this.t1 = t1;
			this.t2 = t2;
            this.nameOfGame = name;
		}
	}
}

