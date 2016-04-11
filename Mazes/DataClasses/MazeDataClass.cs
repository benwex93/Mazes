using System;

namespace Mazes
{
    public class MazeDataClass : IDataClass
    {
        public string Name { get; set; }
        public string Maze { get; set; }
        public NodeDataClass Start { get; set; }
        public NodeDataClass End { get; set; }
        /// <summary>
        /// Loads up data class which will serve for easy serialization
        /// </summary>
        /// <param name="name"></param>
        /// <param name="mazeString"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public MazeDataClass(string name, string mazeString, NodeDataClass start, NodeDataClass end)
        {
            this.Name = name;
            this.Maze = mazeString;
            this.Start = start;
            this.End = end;
        }
        public override string ToString()
        {
            return(Name + "\n" + Start.Row + "\n" + Start.Col + "\n" + End.Row + "\n" + End.Col + "\n" + Maze);
        }

        public string GetMazeName()
        {
            return Name;
        }

        public string GetName()
        {
            return Name;
        }
    }
}
