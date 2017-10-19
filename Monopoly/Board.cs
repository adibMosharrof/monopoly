using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Office.Interop;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Monopoly
{
    public class Board
    {
        public Dictionary<int, int> RealEstateOwnerMap;
        public Dictionary<int , Location> locations;
        public int CurrentDieValue;
        private Random random;
        private const int maxDiceValue = 6;
        
        public Board(string projectDirectory)
        {
            locations = new Dictionary<int, Location>();
            RealEstateOwnerMap = new Dictionary<int, int>();
            InitDefaultValues(projectDirectory);
            random = new Random();
        }

        public int ThrowDie()
        {
            var die1 = random.Next(1,maxDiceValue);
            var die2 = random.Next(1,maxDiceValue);
            CurrentDieValue = die1 + die2;
            return CurrentDieValue;
        }
        public Dictionary<int, int> GeneratePlayerTurns(List<Player> players)
        {
            var playersTurnMap = new Dictionary<int, int>();
            var playerIds = players.Select(player => player.Id).ToList();
            
            for (int i=0; playerIds.Count> 0; i++)
            {
                var playerIdIndex = random.Next(playerIds.Count);
                playersTurnMap.Add(i, playerIds[playerIdIndex]-1);
                playerIds.RemoveAt(playerIdIndex);
            }
            Console.WriteLine("{0} Players will play the game", players.Count);
            foreach (KeyValuePair<int, int> keyValuePair in playersTurnMap)
            {
                Console.WriteLine("Turn number of {0} is {1}",players[keyValuePair.Value].Name, keyValuePair.Key+1);
            }
            return playersTurnMap;
        }
        public void InitDefaultValues(string projectDirectory)
        {
            var loadData = new LoadData();
            var data = loadData.FromCsvFiles(projectDirectory);
            data.Sort((location1, location2) => location1.Id.CompareTo(location2.Id));
            locations = data.ToDictionary(x => x.Id, y => y);
        }
    }
}
