using System;

namespace Server
{
	public class PlayCommand : ICommandable
	{
		public PlayCommand () {}
		public TaskInfo Execute(string details)
		{
			// details is simply the move done by the player
			TaskInfo info = new TaskInfo (details);
			info.Set_Multi_Info ((int)MultiplayInfo.Play_Request);
			return info;
		}
	}
}

