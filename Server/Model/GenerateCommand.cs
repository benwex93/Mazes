using System;
using System.Web.Script.Serialization;
using Mazes;

namespace Server
{
	public class GenerateCommand : ICommandable
	{
		public GenerateCommand ()
		{

		}
		public TaskInfo Execute(string details)
		{
			int space = details.IndexOf (' ');
			string name = details.Substring (0, space);
			string typeStr = details.Substring (space + 1);
			int type = Int32.Parse (typeStr);
            Mazes.MazeProgram.GenerateMaze(name, type);
            Mazes.IDataClass data = Mazes.MazeProgram.GetData();
            //MazeGenerator.GenerateMaze (name, type);
            //Data data = DataSender.GetData ();
            JavaScriptSerializer serializer = new JavaScriptSerializer ();
			string json = serializer.Serialize (data);
			TaskInfo info = new TaskInfo (json);
			return info;
		}
	}
}

