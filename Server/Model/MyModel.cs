using System;
using System.Collections.Generic;

namespace Server
{
	public class MyModel : IModel
	{
		private IPresenter p;
		private Dictionary<string, ICommandable> options;
		private MultiplayManager multi_manag;

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

		public MultiplayManager Get_Multi_Manager ()
		{
			return multi_manag;
		}
	}
}

