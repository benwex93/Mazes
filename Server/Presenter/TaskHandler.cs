using System;
using System.Collections.Generic;
using System.Threading;

namespace Server
{
	public class TaskHandler
	{
		public delegate void ReplyReadyHandler (object source, FinishedTaskEventArgs info);
		public event ReplyReadyHandler ReplyReady;

		public TaskHandler ()
		{
	
		}

		public void FinishedTask(object source, FinishedTaskEventArgs info)
		{
			Task task = (Task) source;
			if (info.stopListening)
				task.Finished -= this.FinishedTask;
			ReplyReady (this, info);

		}
	
		/*public void AddTask(string s, ICommandable t)
		{
			options.Add (s, t);
		} */
		/*public void DoTask(string received)
		{
			string command = "";
			string restOfString = "";
			int firstSpace = received.IndexOf (' ');
			//Console.WriteLine (firstSpace);
			if (firstSpace >= 0) {
				command = received.Substring (0, firstSpace);
				restOfString = received.Substring (firstSpace + 1);
			}
			ICommandable task;
			if (!options.TryGetValue(command, out task))
				Console.WriteLine("404 command not found");
			else
			    //ThreadPool.QueueUserWorkItem(options[command].Execute, restOfString);
			//options [command].execute (restOfString);
		} */
	}
}

