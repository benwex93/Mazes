using System;
using System.Collections.Generic;
using System.Threading;

namespace Server
{
    /// <summary>
    /// An object that listens for when a task finishes its work.
    /// </summary>
	public class TaskHandler
	{
		public delegate void ReplyReadyHandler (object source, FinishedTaskEventArgs info);
		public event ReplyReadyHandler ReplyReady;

        /// <summary>
        /// A constructor
        /// </summary>
		public TaskHandler () {}

        /// <summary>
        /// This function is called when a task finishes its work. This function then
        /// tells the ClientHandler that a reply is ready.
        /// </summary>
        /// <param name="source">the object that triggered the event</param>
        /// <param name="info">An instance of FinishedTaskEventArgs,
        /// which inherits from EventArgs, and contains the string
        /// to send back to the client</param>
		public void FinishedTask(object source, FinishedTaskEventArgs info)
		{
			Task task = (Task) source;
			if (info.stopListening)
				task.Finished -= this.FinishedTask;
			ReplyReady (this, info);
		}
	}
}

