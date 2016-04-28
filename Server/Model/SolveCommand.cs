using System;
using System.Web.Script.Serialization;
using Mazes;

namespace Server
{
    /// <summary>
    /// A class that implements the ICommandable interface and represents a
    /// "solve" command.
    /// </summary>
	public class SolveCommand : ICommandable
	{
        /// <summary>
        /// A constructor
        /// </summary>
		public SolveCommand ()
		{
		}

        /// <summary>
        /// Executes this command
        /// </summary>
        /// <param name="details">The details of this request from the client -
        /// everything after "generate", "play", etc.</param>
        /// <returns>TaskInfo about the command that was executed</returns>
		public TaskInfo Execute(string details) 
		{
            int space = details.IndexOf(' ');
            if (space >= 0)
            {
                string name = details.Substring(0, space);
                string typeStr = details.Substring(space + 1);
                int type = Int32.Parse(typeStr);
                Mazes.MazeProgram.SolveMaze(name, type);
                Mazes.IDataClass data = Mazes.MazeProgram.GetData();
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string json = serializer.Serialize(data);
                TaskInfo info = new TaskInfo(json);
                return info;
            }
            return null;
		}
	}
}

