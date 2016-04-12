using System;
using System.Collections.Generic;
using System.Threading;

namespace Server
{
	public class TaskHandler
	{
		public delegate void ReplyReadyHandler (object source, FinishedTaskEventArgs info);
		public event ReplyReadyHandler ReplyReady;

		public TaskHandler () {}

		public void FinishedTask(object source, FinishedTaskEventArgs info)
		{
			Task task = (Task) source;
			if (info.stopListening)
				task.Finished -= this.FinishedTask;
			ReplyReady (this, info);
		}
	}
}

