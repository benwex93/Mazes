using System;

namespace Server
{
	public class CloseCommand : ICommandable
	{
		public CloseCommand ()
		{
		}
		public TaskInfo Execute(string details)
		{
			// details stores the name of the game to close
			TaskInfo info = new TaskInfo (""); // we're not going to print anything for the close command
			info.Set_Multi_Info ((int)MultiplayInfo.Close_Request);
			info.SetMultGame (details);
			return info;
		}
	}
}

