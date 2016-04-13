using System;
using System.Web.Script.Serialization;
using Mazes;

namespace Server
{
	public class SolveCommand : ICommandable
	{
		public SolveCommand ()
		{
		}
		public TaskInfo Execute(string details) 
		{
            int space = details.IndexOf(' ');
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
	}
}

