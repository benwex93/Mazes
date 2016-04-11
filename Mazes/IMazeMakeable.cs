namespace MazeEx1
{
    /// <summary>
    /// interface for maze makers
    /// </summary>
    interface IMazeMakeable
    {
        void CreateStart(Maze mazeToMake);
        void CreateEnd(Maze mazeToMake);
        void CreateMaze(Maze mazeToMake);
    }
}
