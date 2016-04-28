using System;
using System.Threading;
using System.Collections.Generic;

namespace Server
{
    /// <summary>
    /// The "Presenter" object that implements the IPresenter interface.
    /// This object processes the request sent from the client and sends the
    /// appropriate information to the Model component so that the task can
    /// be completed.
    /// </summary>
	public class MyPresenter : IPresenter
	{
        /// <summary>
        /// A reference to the program's View object
        /// </summary>
		private IView v;
        /// <summary>
        /// A reference to the program's Model object
        /// </summary>
		private IModel m;
        /// <summary>
        /// A constructor
        /// </summary>
		public MyPresenter ()
		{
		}

        /// <summary>
        /// Sets the View to the given parameter
        /// </summary>
        /// <param name="v">a reference to the View object that will
        /// be set to the Presenter's v member</param>
		public void SetView(IView v)
		{
			this.v = v;
		}

        /// <summary>
        /// Sets the Model to the given parameter
        /// </summary>
        /// <param name="m">a reference to the Model object that will
        /// be set to the Presenter's m member</param>
		public void SetModel(IModel m)
		{
			this.m = m;
		}

        /// <summary>
        /// A function that makes and returns a new TaskHandler when the ClientHandler
        /// requests one.
        /// </summary>
        /// <returns>the TaskHandler that was just made</returns>
		public TaskHandler MakeNewTaskHandler()
		{
			TaskHandler handler = new TaskHandler ();
			return handler;
		}

        /// <summary>
        /// Processes the string sent from the client, makes a new Task to
        /// carry out the client's request, and adds this Task to the ThreadPool 
        /// </summary>
        /// <param name="fromClient">the string sent from the client that
        /// contains the client's request</param>
        /// <param name="handler">the TaskHandler for the client that
        /// sent this request</param>
		public void DoNewTask(string fromClient, TaskHandler handler)
		{
			string command = "";
			string restOfString = "";
			Task task;
			int firstSpace = fromClient.IndexOf (' ');
			if (firstSpace >= 0) {
				command = fromClient.Substring (0, firstSpace);
				restOfString = fromClient.Substring (firstSpace + 1);
				task = new Task (command, restOfString, this.m, handler);
				task.Finished += new Task.FinishedHandler (handler.FinishedTask);
				ThreadPool.QueueUserWorkItem(m.ExecuteTask, task);
			}
		}
	}
}

