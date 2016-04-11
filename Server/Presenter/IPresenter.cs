using System;

namespace Server
{
	public interface IPresenter
	{
		void SetView(IView v);
		void SetModel(IModel m);
		void DoNewTask(string s, TaskHandler t);
		TaskHandler MakeNewTaskHandler();
	}
}

