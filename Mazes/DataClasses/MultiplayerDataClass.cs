using System;

namespace Mazes
{
    public class MultiplayerDataClass : IDataClass
    {
        public string Name { get; set; }
        public string MazeName { get; set; }
        public MazeDataClass You { get; set; }
        public MazeDataClass Other { get; set; }
        /// <summary>
        /// Loads up data class which will serve for easy serialization
        /// </summary>
        /// <param name="name"></param>
        /// <param name="mazeName"></param>
        /// <param name="you"></param>
        /// <param name="other"></param>
        public MultiplayerDataClass(string name, string mazeName, MazeDataClass you, MazeDataClass other)
        {
            this.Name = name;
            this.MazeName = mazeName;
            this.You = you;
            this.Other = other;
        }
        public override string ToString()
        {
            return (Name + "\n" + MazeName + "\n" + You + "\n" + Other);
        }
        public string GetName()
        {
            return Name;
        }
        public string GetMazeName()
        {
            return MazeName;
        }
    }
}
