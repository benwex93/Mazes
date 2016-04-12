using System.Collections.Generic;

namespace Mazes
{
    class GameDataBase
    {
        //creates game list so that playes can meet up for a certain game
        static List<Game> gameList;
        /// <summary>
        /// constructor that creates a new list of games
        /// </summary>
        public GameDataBase()
        {
            gameList = new List<Game>();
        }
        /// <summary>
        /// adds a new game to list. Called by first player to connect
        /// </summary>
        /// <param name="gameToAdd"></param>
        public void AddGame(Game gameToAdd)
        {
            gameList.Add(gameToAdd);
        }
        /// <summary>
        /// Deletes game from database
        /// </summary>
        /// <param name="gameToDelete"></param>
        public void DeleteGame(Game gameToDelete)
        {
            gameList.Remove(gameToDelete);
        }
        /// <summary>
        /// retrieves a game if it's already been created. Called by second player to connect
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Game RetrieveGame(string name)
        {
            foreach (Game game in gameList)
            {
                if (game.name == name)
                    return game;
            }
            //doesn't have that particular maze
            return null;
        }
    }
}
