using System;

namespace Server
{
	public interface IModel
	{
		void ExecuteTask(Object ThreadContext);
		MultiplayManager Get_Multi_Manager();
	}
}

