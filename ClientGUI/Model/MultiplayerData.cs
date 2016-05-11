using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientGui.Model
{
    public class MultiplayerData
    {
        public string Name { get; set; }
        public string MazeName { get; set; }
        public MazeData You { get; set; }
        public MazeData Other { get; set; }
        /// <summary>
        /// Loads up data class which will serve for easy serialization
        /// </summary>
        /// <param name="name"></param>
        /// <param name="mazeName"></param>
        /// <param name="you"></param>
        /// <param name="other"></param>
        public MultiplayerData(string name, string mazeName, MazeData you, MazeData other)
        {
            this.Name = name;
            this.MazeName = mazeName;
            this.You = you;
            this.Other = other;
        }
        public MultiplayerData()
        {

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
