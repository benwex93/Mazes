using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mazes
{
    public class CharVals
    {
        /// <summary>
        /// Contains character values for representing maze as array
        /// </summary>
        public char startValue { get; set; }
        public char endValue { get; set; }
        public char solValue { get; set; }
        public char pathValue { get; set; }
        public char wallValue { get; set; }
        public List<char> usedChars { get; set; }
        public CharVals(char startValue, char endValue, char solValue, char pathValue, char wallValue)
        {
            this.startValue = startValue;
            this.endValue = endValue;
            this.solValue = solValue;
            this.pathValue = pathValue;
            this.wallValue = wallValue;
            InitializeUsedCharList();
        }
        /// <summary>
        /// puts all used chars in a list
        /// </summary>
        public void InitializeUsedCharList()
        {
            this.usedChars = new List<char>();
            this.usedChars.Add(startValue);
            this.usedChars.Add(endValue);
            this.usedChars.Add(solValue);
            this.usedChars.Add(pathValue);
            this.usedChars.Add(wallValue);
        }
        /// <summary>
        /// returns a char that has not been used previously for any other values pertaining to the maze
        /// as to not mess up any maze creation or maze search algorithms
        /// </summary>
        /// <returns></returns>
        public char GetRandomUnusedChar()
        {
            Random randomNumberGenerator = new Random();
            char randomChar = '\0';
            bool foundUnusedChar = false;
            while (!foundUnusedChar)
            {
                randomChar = (char)randomNumberGenerator.Next(65, 122);
                if (!usedChars.Contains(randomChar))
                {
                    foundUnusedChar = true;
                    usedChars.Add(randomChar);
                }
            }
            return randomChar;
        }
    }
}
