using System;
using System.Threading;
using System.Collections.Generic;

namespace Server
{
	public class MyPresenter : IPresenter
	{
		private IView v;
		private IModel m;
		public MyPresenter ()
		{
		}

		public void SetView(IView v)
		{
			this.v = v;
		}

		public void SetModel(IModel m)
		{
			this.m = m;
		}

		public TaskHandler MakeNewTaskHandler()
		{
			TaskHandler handler = new TaskHandler ();
			return handler;
		}

		public void DoNewTask(string fromClient, TaskHandler handler)
		{
			string command = "";
			string restOfString = "";
			Task task;
			int firstSpace = fromClient.IndexOf (' ');
			//Console.WriteLine (firstSpace);
			if (firstSpace >= 0) {
				command = fromClient.Substring (0, firstSpace);
				restOfString = fromClient.Substring (firstSpace + 1);
				task = new Task (command, restOfString, this.m, handler);
				task.Finished += new Task.FinishedHandler (handler.FinishedTask);
				ThreadPool.QueueUserWorkItem(m.ExecuteTask, task);
			}
			//Console.WriteLine (command);
			//string[] stringParts = {command, restOfString};

			/*ICommandable task;
			if (!options.TryGetValue(command, out task))
				Console.WriteLine("404 command not found");
			else */

		}
	}
}

