using System;

namespace Server
{
	public class TaskInfo
	{
		private string json;
		private int mult_Info = (int) MultiplayInfo.No_Multiplay;
		private string multGameName = null;

		public TaskInfo (string str)
		{
			json = str;
		}

		public void Set_Multi_Info(int i)
		{
			mult_Info = i;
		}

		public string GetJson()
		{
			return json;
		}

		public int Get_Multi_Info()
		{
			return mult_Info;
		}

		public void SetJson(string str)
		{
			json = str;
		}

		public void SetMultGame(string str)
		{
			multGameName = str;
		}

		public string GetGameName()
		{
			return multGameName;
		}
	}
}

