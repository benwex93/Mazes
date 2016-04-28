using System;

namespace Server
{
    /// <summary>
    /// A class that implements the ICommandable interface and represents a
    /// "play" command.
    /// </summary>
	public class PlayCommand : ICommandable
	{
        /// <summary>
        /// A constructor
        /// </summary>
		public PlayCommand () {}

        /// <summary>
        /// Executes this command
        /// </summary>
        /// <param name="details">The details of this request from the client -
        /// everything after "generate", "play", etc.</param>
        /// <returns>TaskInfo about the command that was executed</returns>
		public TaskInfo Execute(string details)
		{
			// details is simply the move done by the player
			TaskInfo info = new TaskInfo (details);
			info.Set_Multi_Info ((int)MultiplayInfo.Play_Request);
			return info;
		}
	}
}

