using System;

namespace Server
{
    /// <summary>
    /// A class that contains information about a Task
    /// </summary>
	public class TaskInfo
	{
        /// <summary>
        /// The json string to be sent back to the client after this task finishes
        /// </summary>
		private string json;
        /// <summary>
        /// An int containing necessary info regarding multiplayer requests.
        /// The default setting is No_Multiplay, meaning that this request
        /// has nothing to do with a multiplayer game.
        /// </summary>
		private int mult_Info = (int) MultiplayInfo.No_Multiplay;
        /// <summary>
        /// The name of the multiplyer game connected with this task.
        /// If not applicable, this member is set to null.
        /// </summary>
		private string multGameName = null;

        /// <summary>
        /// A constructor
        /// </summary>
        /// <param name="str">the json string that should be
        /// sent back to the client when this task finishes.
        /// The json member takes the value of this parameter</param>
		public TaskInfo (string str)
		{
			json = str;
		}

        /// <summary>
        /// Sets the multi_info according to the given parameter.
        /// </summary>
        /// <param name="i">The info to be set</param>
		public void Set_Multi_Info(int i)
		{
			mult_Info = i;
		}

        /// <summary>
        /// Returns the json member of this object
        /// </summary>
        /// <returns>The json string that should be sent back to the client
        /// when this task finishes</returns>
		public string GetJson()
		{
			return json;
		}

        /// <summary>
        /// Returns this TaskInfo's multiplayer information
        /// </summary>
        /// <returns>This object's mult_info member, which stores information
        /// about any multiplayer nature of this task</returns>
		public int Get_Multi_Info()
		{
			return mult_Info;
		}

        /// <summary>
        /// Sets the json member
        /// </summary>
        /// <param name="str">the string to set the json member to</param>
		public void SetJson(string str)
		{
			json = str;
		}

        /// <summary>
        /// Sets the multGameName member
        /// </summary>
        /// <param name="str">The string to set the name to</param>
		public void SetMultGame(string str)
		{
			multGameName = str;
		}

        /// <summary>
        /// Returns the name of this task's multiplayer game
        /// </summary>
        /// <returns>the multGameName member</returns>
		public string GetGameName()
		{
			return multGameName;
		}
	}
}

