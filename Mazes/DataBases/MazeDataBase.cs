using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.IO;
namespace Mazes
{
    public class MazeDataBase
    {
        //stores a list of mazes and and solutions while the program is running
        static List<Maze> mazeList;
        static List<SolutionDataClass> solutionsList;
        /// <summary>
        /// initializes both lists when created
        /// </summary>
        public MazeDataBase()
        {
            mazeList = new List<Maze>();
            solutionsList = new List<SolutionDataClass>();
        }
        /// <summary>
        /// adds maze to maze list
        /// </summary>
        public void AddMaze(Maze mazeToAdd)
        {
            mazeList.Add(mazeToAdd);
        }
        /// <summary>
        /// Deletes maze from list
        /// </summary>
        /// <param name="mazeToDelete"></param>
        public void DeleteMaze(Maze mazeToDelete)
        {
            mazeList.Add(mazeToDelete);
        }
        /// <summary>
        /// retrieves maze from list, if exists, in order to solve it
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Maze RetrieveMaze(string name)
        {
            foreach (Maze maze in mazeList)
            {
                if (maze.name == name)
                    return maze;
            }
            //doesn't have that particular maze
            return null;
        }
        /// <summary>
        /// Retrieves solution from current program's solution list. If not there retrieves
        /// solutions that have been previously solved from some file and tries to retrieve 
        /// it from there. Then appends that list to the main list of solutions.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>A Solution Datat Class</returns>
        public SolutionDataClass RetrieveMazeSolution(string name)
        {
            foreach (SolutionDataClass maze in solutionsList)
            {
                if (maze.Name == name)
                    return maze;
            }
            List<SolutionDataClass> savedSolutions;
            try
            {
                //if file exists
                string jsonString = File.ReadAllText("MazeDatabase.json");
                if (jsonString == "")
                    return null;
                JavaScriptSerializer ser = new JavaScriptSerializer();
                savedSolutions = new List<SolutionDataClass>();
                savedSolutions = ser.Deserialize<List<SolutionDataClass>>(jsonString);
            }
            //creates a new file if doesn't exist
            catch (Exception)
            {
                FileStream fs = new FileStream("MazeDatabase.json", FileMode.OpenOrCreate);
                fs.Close();
                //no solutions yet
                return null;
            }
            if (savedSolutions != null)
            {
                solutionsList.AddRange(savedSolutions);
                foreach (SolutionDataClass sol in savedSolutions)
                {
                    if (sol.Name == name)
                        return sol;
                }
            }
            //doesn't have that particular solution
            return null;
        }
        /// <summary>
        /// adds solution to list for quick retrieval and then to file in case of restart
        /// </summary>
        /// <param name="solution"></param>
        public void SaveSolToFile(ISolution solution)
        {
            solutionsList.Add(solution.ToSolutionDataClass());
            JavaScriptSerializer ser = new JavaScriptSerializer();
            String mazeData = ser.Serialize(solutionsList);
            File.WriteAllText("MazeDatabase.json", mazeData);
        }
    }
}
