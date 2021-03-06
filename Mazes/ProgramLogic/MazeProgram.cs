﻿using System;

namespace Mazes
{
    public static class MazeProgram
    {
        public static IDataClass data;
        static MazeDataBase MDB = new MazeDataBase();
        static GameDataBase GDB = new GameDataBase();
        static CharVals mazeVals = new CharVals('*', '#', '2', '0', '1');
        //values set here for testing
        static int mazeHeight;
        static int mazeLength;
        public static void ConfigureDimensions(int height, int length)
        {
            mazeHeight = height / 2;
            mazeLength = length / 2;
        }
        public static void GenerateMaze(string name, int generateType)
        {
            Maze maze = new Maze(name, mazeHeight, mazeLength, mazeVals);
            if (generateType == 0)
                maze.CreateMaze(new RandomMazeMaker());
            else if (generateType == 1)
                maze.CreateMaze(new DFSMazeMaker());
            MDB.AddMaze(maze);
            data = new MazeDataClass(maze.name, maze.ToString(), new NodeDataClass(maze.start.location.row, maze.start.location.col),
                new NodeDataClass(maze.end.location.row, maze.end.location.col));
        }
        public static void SolveMaze(string name, int solveType)
        {
            SolutionDataClass mazeSolution = MDB.RetrieveMazeSolution(name);
            //maze is solved according to the solve type
            if (mazeSolution != null)
                data = mazeSolution;
            //maze is not solved according to the solve type
            else
            {
                Maze mazeToSolve = MDB.RetrieveMaze(name);
                if (solveType == 0)
                {
                    BreadthFSSolution breadthFS = new BreadthFSSolution();
                    mazeToSolve.SolveMaze(breadthFS);
                    MDB.SaveSolToFile(breadthFS);
                    data = new SolutionDataClass(name, breadthFS.solutionString,
                        new NodeDataClass(breadthFS.start.location.row, breadthFS.start.location.col),
                        new NodeDataClass(breadthFS.end.location.row, breadthFS.end.location.col));
                }
                else if (solveType == 1)
                {
                    BestFSSolution bestFS = new BestFSSolution();
                    mazeToSolve.SolveMaze(bestFS);
                    MDB.SaveSolToFile(bestFS);
                    data = new SolutionDataClass(name, bestFS.solutionString,
                        new NodeDataClass(bestFS.start.location.row, bestFS.start.location.col),
                        new NodeDataClass(bestFS.end.location.row, bestFS.end.location.col));
                }
            }
        }
        public static void Multiplayer(string name)
        {
            Game game = GDB.RetrieveGame(name);
            if (game == null) // player 16
            {
                game = new Game(name, mazeHeight, mazeLength, mazeVals);
                GDB.AddGame(game);
                data = new MultiplayerDataClass(game.name, game.mazeOne.name,
                    new MazeDataClass(game.mazeOne.name, game.mazeOne.ToString(),
                    new NodeDataClass(game.mazeOne.start.location.row, game.mazeOne.start.location.col),
                    new NodeDataClass(game.mazeOne.end.location.row, game.mazeOne.end.location.col)),
                    new MazeDataClass(game.mazeTwo.name, game.mazeTwo.ToString(),
                    new NodeDataClass(game.mazeTwo.start.location.row, game.mazeTwo.start.location.col),
                    new NodeDataClass(game.mazeTwo.end.location.row, game.mazeTwo.end.location.col)));
            }
            else { // player 2
                MDB.AddMaze(game.mazeOne);
                MDB.AddMaze(game.mazeTwo);
                data = new MultiplayerDataClass(game.name, game.mazeTwo.name,
                    new MazeDataClass(game.mazeTwo.name, game.mazeTwo.ToString(),
                    new NodeDataClass(game.mazeTwo.start.location.row, game.mazeTwo.start.location.col),
                    new NodeDataClass(game.mazeTwo.end.location.row, game.mazeTwo.end.location.col)),
                    new MazeDataClass(game.mazeOne.name, game.mazeOne.ToString(),
                    new NodeDataClass(game.mazeOne.start.location.row, game.mazeOne.start.location.col),
                    new NodeDataClass(game.mazeOne.end.location.row, game.mazeOne.end.location.col)));
            }
        }
        public static void Close(string gameName)
        {
            Game gameToClose = GDB.RetrieveGame(gameName);
            if (gameToClose != null)
            {
                MDB.DeleteMaze(gameToClose.mazeOne);
                MDB.DeleteMaze(gameToClose.mazeTwo);
                GDB.DeleteGame(gameToClose);
            }
        }
        public static IDataClass GetData()
        {
            return data;
        }
    }
}
