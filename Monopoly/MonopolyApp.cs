using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public class MonopolyApp
    {
        public Board board;
        public List<Player> Players;
        private Dictionary<int, int> playerTurnsMap;
        private int turnCounter;

        public MonopolyApp(string projectDirectory)
        {
            Players = new List<Player>();
            board = new Board(projectDirectory);
            playerTurnsMap = new Dictionary<int, int>();
        }

        public void Init()
        {
            //TakePlayersInformation();
            Players = new List<Player>() { new Player() { Id = 1, Name = "Adib", PieceName = "car" }, new Player() { Id = 2, Name = "Riasat", PieceName = "dog" }, new Player() { Id = 3, Name = "Oyshik", PieceName = "hat" }, new Player() { Id = 4, Name = "Sadit", PieceName = "battleship" }, new Player() { Id = 5, Name = "Azmaeen", PieceName = "shoe" } };
            Players.ForEach(p => p.InitPlayer(board.locations));
            playerTurnsMap = board.GeneratePlayerTurns(Players);
            turnCounter = 0;
            //Game();
            //board.DisplayBoard();
            //Console.ReadKey();
        }

        public string Game()
        {
            var currentPlayer = GetCurrentPlayer();
            //Console.WriteLine("It is {0}'s turn. {0}'s current location is {1}:{2}", currentPlayer.Name, currentPlayer.BoardPosition, board.locations[currentPlayer.BoardPosition].Name);
            //Console.WriteLine("Press Enter to roll the dice");
            //Console.ReadKey();
            var rollOfDice = board.ThrowDie();
            //Console.WriteLine("{0} has rolled {1}", currentPlayer.Name, rollOfDice);

            //currentPlayer.GoToLocation(rollOfDice);
            currentPlayer.BeginTurn(rollOfDice, board.locations);
            //Console.WriteLine("{0}'s new location is {1}:{2}", currentPlayer.Name, currentPlayer.BoardPosition, board.locations[currentPlayer.BoardPosition].Name);

            var actionMessage = board.locations[currentPlayer.BoardPosition].Action(currentPlayer, Players, board.RealEstateOwnerMap, board.locations, rollOfDice);

            //Console.WriteLine("****************************");

            //IncreaseTurnCounter();
            return actionMessage;
            //Game();
        }
        public string IncreaseTurnCounter()
        {
            turnCounter = (turnCounter + 1) % Players.Count;
            return String.Format("It is {0}'s turn", GetCurrentPlayer().Name);
        }

        public Player GetCurrentPlayer()
        {
            return Players[playerTurnsMap[turnCounter]];
        }

        public Player GetPreviousTurnsPlayer()
        {
            var index = Modulus(turnCounter - 1, Players.Count);
            return Players[playerTurnsMap[index]];
        }
        
        public void TakePlayersInformation()
        {
            Console.WriteLine();
            var input = Helper.TakeNumberInput("Enter 0 to Add New Player, 1 to Exit", "Please Enter a number");
            if (input == 0)
            {
                var playerName = Helper.TakeStringInput("Enter Player Name");
                Players.Add(new Player() { Id = Players.Count + 1, Name = playerName });
                TakePlayersInformation();
            }
        }

        private int Modulus(int number, int total)
        {
            var result = number % total;
            if ((number < 0 && total > 0) || (number > 0 && total < 0))
                result += total;
            return result;
        }
    }
}
