using System;

namespace Server
{
    /// <summary>
    /// An enum list to organize the multiplayer information as ints
    /// </summary>
	enum MultiplayInfo {No_Multiplay, First_Request, Second_Request, Play_Request, Close_Request};
    /// <summary>
    /// This class represents the task itself. Every request from the client makes a task.
    /// </summary>
	public class Task
	{
		private string type;
		private string details;
		private TaskInfo info;
		private MultiplayManager multi_manag;
		private TaskHandler handler; // denotes from which client the task came from
		public delegate void FinishedHandler(object source, FinishedTaskEventArgs info);
		public event FinishedHandler Finished;

        /// <summary>
        /// A constructor
        /// </summary>
        /// <param name="t">the type of task: generate, play, close, etc.</param>
        /// <param name="d">other details of the task - the rest of the
        /// string sent by the client</param>
        /// <param name="m">a reference to the program's Model object</param>
        /// <param name="h">a reference to this Task's TaskHandler</param>
		public Task (string t, string d, IModel m, TaskHandler h)
		{
			type = t;
			details = d;
			multi_manag = m.Get_Multi_Manager ();
			handler = h;
		}

        /// <summary>
        /// Returns the command type of this Task
        /// </summary>
        /// <returns>the type member - "generate", "play", "close", etc.</returns>
		public string GetCommandType()
		{
			return type;
		}

        /// <summary>
        /// Returns the other details of this task
        /// </summary>
        /// <returns>the details member - simply stores the rest of the
        /// request from the client, after the task type</returns>
		public string GetDetails()
		{
			return details;
		}

        /// <summary>
        /// Returns the TaskInfo of this Task
        /// </summary>
        /// <returns>the info member, which stores this Task's
        /// TaskInfo</returns>
		public TaskInfo Get_Task_Info()
		{
			return info;
		}

        /// <summary>
        /// Returns the TaskHandler of this Task. This acts as an ID
        /// that identifies which client the task request came from.
        /// </summary>
        /// <returns>the handler member, a reference to this Task's
        /// TaskHandler</returns>
		public TaskHandler GetHandler()
		{
			return handler;
		}
	    
        /// <summary>
        /// Executes this Task
        /// </summary>
        /// <param name="com">the command to be executed, which is retrieved
        /// from a dictionary that matches strings to ICommandable. This
        /// dictionary is implemented by the program's Model object</param>
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

        /// <summary>
        /// A function that is called when an event triggers signifying that a
        /// multiplayer game is ready to start (meaning that a second client requested
        /// to start the game).
        /// </summary>
        /// <param name="source">The object that triggered the event. This will be the
        /// MultiplayManager object</param>
        /// <param name="a">Information about the multiplayer game containing the string
        /// that will be sent back to each client</param>
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

        /// <summary>
        /// A function that is called when an event triggers signifying that a player has made
        /// a move in a multiplayer game
        /// </summary>
        /// <param name="source">The MultiplayManager object that triggered the event</param>
        /// <param name="p">Information about the move containing which player made the move,
        /// as well as the string that will be sent to the other player.</param>
		public void PlayerMoved(object source, PlayerMovedArgs p)
		{
			if (p.player1 == this) {
				string json = p.toSend;
				FinishedTaskEventArgs info = new FinishedTaskEventArgs (json, false);
				Finished (this, info);
			}
		}

        /// <summary>
        /// A function that is called when an event triggers signifying that a multiplayer game
        /// has ended
        /// </summary>
        /// <param name="source">The MultiplayManager object that triggered the event</param>
        /// <param name="a">Information about the multiplayer game that ended</param>
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