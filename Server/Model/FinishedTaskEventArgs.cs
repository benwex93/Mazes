using System;

namespace Server
{
	public class FinishedTaskEventArgs : EventArgs
	{
		public string toSend;
		public bool stopListening;
		public FinishedTaskEventArgs (string s, bool stop)
		{
			this.toSend = s;
			this.stopListening = stop;
		}
	}
}

