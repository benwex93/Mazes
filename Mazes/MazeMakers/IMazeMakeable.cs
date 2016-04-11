namespace Mazes
{
    /// <summary>
    /// interface for maze makers
    /// </summary>
    public interface IMazeMakeable
    {
        void CreateStart(Maze mazeToMake);
        void CreateEnd(Maze mazeToMake);
        void CreateMaze(Maze mazeToMake);
    }
}
