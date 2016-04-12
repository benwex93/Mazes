using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Server
{
	public class MultiplayManager
	{
		private List<Task> pending_requests;
		private List<MultiplayArgs> current_games;
			public delegate void MultiplayReadyHandler(object source, MultiplayArgs e);
			public event MultiplayReadyHandler MultiplayReady;

			public delegate void PlayerMovedHandler (object source, PlayerMovedArgs e);
			public event PlayerMovedHandler PlayerMoved;

			public delegate void EndGameHandler (object source, MultiplayArgs e);
			public event EndGameHandler EndGame;

		public MultiplayManager ()
		{
			pending_requests = new List<Task> ();
			current_games = new List<MultiplayArgs> ();
		}

		public void FirstGameRequest(Task task)
		{
			pending_requests.Add (task);
		}

		public void SecondGameRequest(Task task)
		{
			Task first_Request = null;
            string gameName = task.GetDetails();
			foreach (Task t in pending_requests) {
				if (t.GetDetails () == gameName) {
					first_Request = t;
					break;
				}
			}
			if (first_Request != null) {
                MultiplayArgs args = new MultiplayArgs (first_Request, task, gameName);
				current_games.Add (args);
                pending_requests.Remove(first_Request);
				MultiplayReady (this, args);
			}
		}

		public void PlayRequest (Task task)
		{
			TaskHandler taskID = task.GetHandler ();
			Task otherPlayer = null;
			foreach (MultiplayArgs mult in current_games) {
				if (taskID == mult.t1.GetHandler ()) {
					otherPlayer = mult.t2;
					break;
				}
				if (taskID == mult.t2.GetHandler ()) {
					otherPlayer = mult.t1;
					break;
				}
			}
			if (otherPlayer != null) {
				string move = task.Get_Task_Info ().GetJson ();
				string nameOfGame = otherPlayer.Get_Task_Info ().GetGameName ();
				PlayMoveInfo info = new PlayMoveInfo (nameOfGame, move);
				JavaScriptSerializer serializer = new JavaScriptSerializer ();
				string json = serializer.Serialize (info);
				PlayerMovedArgs args = new PlayerMovedArgs (json, otherPlayer);
				PlayerMoved (this, args); 
			}
		}

		public void CloseRequest (Task task)
		{
			string gameName = task.Get_Task_Info ().GetGameName ();
			MultiplayArgs args = null;
			foreach (MultiplayArgs mult in current_games) {
				if (gameName == mult.t1.Get_Task_Info ().GetGameName ()) {
					args = mult;
					current_games.Remove (mult);
					break;
				}
			}
			if (args != null) {
				EndGame (this, args);
			}
		}
	}
}

