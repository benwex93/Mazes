using System;

namespace Server
{
	enum MultiplayInfo {No_Multiplay, First_Request, Second_Request, Play_Request, Close_Request};
	public class Task
	{
		private string type;
		private string details;
		private TaskInfo info;
		private MultiplayManager multi_manag;
		private TaskHandler handler; // denotes from which client the task came from
		public delegate void FinishedHandler(object source, FinishedTaskEventArgs info);
		public event FinishedHandler Finished;

		public Task (string t, string d, IModel m, TaskHandler h)
		{
			type = t;
			details = d;
			multi_manag = m.Get_Multi_Manager ();
			handler = h;
		}

		public string GetCommandType()
		{
			return type;
		}

		public string GetDetails()
		{
			return details;
		}

		public TaskInfo Get_Task_Info()
		{
			return info;
		}

		public TaskHandler GetHandler()
		{
			// serves to identify which client the task request came from
			return handler;
		}
	
		public void Execute(ICommandable com)
		{
			info = com.Execute (this.details);
            if (info != null)
            {
                int mult_Info = info.Get_Multi_Info();
                if (mult_Info == (int)MultiplayInfo.No_Multiplay)
                {
                    string json = info.GetJson();
                    FinishedTaskEventArgs finalInfo = new FinishedTaskEventArgs(json, true);
                    Finished(this, finalInfo);
                }
                else if (mult_Info < (int)MultiplayInfo.Play_Request)
                {
                    multi_manag.MultiplayReady += new MultiplayManager.MultiplayReadyHandler(MultiplayReady);
                    multi_manag.PlayerMoved += new MultiplayManager.PlayerMovedHandler(PlayerMoved);
                    multi_manag.EndGame += new MultiplayManager.EndGameHandler(EndGame);
                    if (mult_Info == (int)MultiplayInfo.First_Request)
                        multi_manag.FirstGameRequest(this);
                    else if (mult_Info == (int)MultiplayInfo.Second_Request)
                        multi_manag.SecondGameRequest(this);
                }
                else if (mult_Info == (int)MultiplayInfo.Play_Request)
                {
                    multi_manag.PlayRequest(this);
                }
                else // mult_Info == MultiplayInfo.Close_Request
                { 
                    multi_manag.CloseRequest(this);
                }
            }
		}

		public void MultiplayReady(object source, MultiplayArgs a)
		{
			MultiplayManager manag = (MultiplayManager)source;
			if (a.t1 == this || a.t2 == this) {
				manag.MultiplayReady -= this.MultiplayReady;
				string json = info.GetJson ();
				FinishedTaskEventArgs finalInfo = new FinishedTaskEventArgs (json, false);
				Finished (this, finalInfo);
			}
		}

		public void PlayerMoved(object source, PlayerMovedArgs p)
		{
			if (p.player1 == this) {
				string json = p.toSend;
				FinishedTaskEventArgs info = new FinishedTaskEventArgs (json, false);
				Finished (this, info);
			}
		}

		public void EndGame(object source, MultiplayArgs a)
		{
			MultiplayManager manag = (MultiplayManager)source;
			if (a.t1 == this || a.t2 == this) {
				manag.PlayerMoved -= this.PlayerMoved;
				manag.EndGame -= this.EndGame;
				FinishedTaskEventArgs info = new FinishedTaskEventArgs ("", true);
				Finished (this, info);
			}
		}
	}
}