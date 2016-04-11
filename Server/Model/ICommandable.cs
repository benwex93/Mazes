using System;

namespace Server
{
	public interface ICommandable
	{
		TaskInfo Execute(string details);
	}
}

