using System;
using System.Collections.Generic;

namespace Server
{
    /// <summary>
    /// The Model object that implements the IModel interface. This component
    /// of the program uses the referenced DLL to carry out all of the client's
    /// requests and make appropriate responses to send back to the client
    /// </summary>
	public class MyModel : IModel
	{
		private IPresenter p;
		private Dictionary<string, ICommandable> options;
		private MultiplayManager multi_manag;

        /// <summary>
        /// A constructor
        /// </summary>
        /// <param name="p">a reference to the program's Presenter object</param>
		public MyModel (IPresenter p)
		{
			this.p = p;
			this.options = new Dictionary<string, ICommandable> ();
			options.Add ("generate", new GenerateCommand ());
			options.Add ("solve", new SolveCommand ());
			options.Add ("multiplayer", new MultiplayCommand ());
			options.Add ("play", new PlayCommand ());
			options.Add ("close", new CloseCommand ());
			multi_manag = new MultiplayManager ();
		}

        /// <summary>
        /// This function receives a Task from the Presenter and executes it.
        /// This function is placed in the Threadpool by the Presenter.
        /// </summary>
        /// <param name="ThreadContext">a reference to the Task that needs to
        /// be executed</param>
		public void ExecuteTask(Object ThreadContext)
		{
			Task toDo = (Task)ThreadContext;
			string commandType = toDo.GetCommandType ();
			ICommandable command;
			if (!options.TryGetValue (commandType, out command))
				Console.WriteLine ("404 command not found");
			else
				toDo.Execute (options [commandType]);
		}

        /// <summary>
        /// Returns the MultiplayManager object. One MultiplayManager object serves
        /// the entire Model component of the program
        /// </summary>
        /// <returns>the multi_manag member, a reference to the program's
        /// MultiplayManager object</returns>
		public MultiplayManager Get_Multi_Manager ()
		{
			return multi_manag;
		}
	}
}

