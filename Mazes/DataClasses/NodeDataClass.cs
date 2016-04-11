using System;

namespace Mazes
{
    public class NodeDataClass : IDataClass
    {
        public int Row { get; set; }
        public int Col { get; set; }
        /// <summary>
        /// Loads up data class which will serve for easy serialization
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public NodeDataClass(int row, int col)
        {
            this.Row = row * 2;
            this.Col = col * 2;
        }

        public string GetName()
        {
            throw new NotImplementedException();
        }

        public string GetMazeName()
        {
            throw new NotImplementedException();
        }
    }
}
