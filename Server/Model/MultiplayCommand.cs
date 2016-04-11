using System;
using Mazes;
using System.Web.Script.Serialization;


namespace Server
{
	public class MultiplayCommand : ICommandable
	{
		public MultiplayCommand ()
		{
		}
		public TaskInfo Execute(string details)
		{
            Mazes.MazeProgram.Multiplayer(details);
            Mazes.IDataClass data = Mazes.MazeProgram.GetData();
            JavaScriptSerializer serial = new JavaScriptSerializer();
            string json = serial.Serialize(data);
            string name = data.GetMazeName();
            bool isFirst = name.Contains("player1");
            TaskInfo info = new TaskInfo(json);
            if (isFirst)
                info.Set_Multi_Info((int)MultiplayInfo.First_Request);
            else
                info.Set_Multi_Info((int)MultiplayInfo.Second_Request);
            info.SetMultGame(data.GetName());
			/*bool isFirst = MazeButNot.Multiplayer.IsFirst ();
			if (isFirst) {
				Console.WriteLine ("is first");
				info.Set_Multi_Info ((int) MultiplayInfo.First_Request);
			} else {
				Console.WriteLine ("is second");
				info.Set_Multi_Info ((int) MultiplayInfo.Second_Request);
			}
			info.SetMultGame ("blerrrrf"); // Get name of multiplayer game */
			return info;
		}
	}
}

