using System;

namespace Server
{
    /// <summary>
    /// The IPresenter interface for the Presenter component of
    /// the MVP design
    /// </summary>
	public interface IPresenter
	{
		void SetView(IView v);
		void SetModel(IModel m);
		void DoNewTask(string s, TaskHandler t);
		TaskHandler MakeNewTaskHandler();
	}
}

