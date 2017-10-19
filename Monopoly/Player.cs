using System;
using System.Collections.Generic;
using Monopoly;

namespace Monopoly
{
    public class Player
    {
        public int Id;
        public string Name;
        public List<RealEstate> RealEstates;
        public int Balance;
        public int BoardPosition;
        public string PieceName;
        private const int NumberOfBoardLocations = 40;
        public Player()
        {
            Balance = 1500;
            BoardPosition = 1;
            RealEstates = new List<RealEstate>();
            
        }

        public void InitPlayer(Dictionary<int, Location> locations)
        {
            locations[BoardPosition].Players.Add(Id, this);
        }
        public void BeginTurn(int rollOfDice, Dictionary<int, Location> locations)
        {
            GoToLocation(rollOfDice, locations);
        }

        public int GoToLocation(int rollOfDice, Dictionary<int, Location> locations)
        {
            var newLocation = GetNewLocation(BoardPosition, rollOfDice);
            return GoToLocation(BoardPosition, newLocation, locations);
        }

        public int GoToLocation(int currentPosition, int newPosition, Dictionary<int, Location> locations, bool isMovingForward = true)
        {
            if (CheckIfPassedGo(currentPosition, newPosition, isMovingForward))
            {
                PassedGo();
            }
            UpdatePlayerLocations(BoardPosition, newPosition, locations);
            BoardPosition = newPosition;
            return BoardPosition;
        }

        private void UpdatePlayerLocations(int currentPosition, int newPosition, Dictionary<int, Location> locations)
        {
            locations[currentPosition].Players.Remove(Id);
            locations[newPosition].Players.Add(Id, this);
        }

        public int IncreaseBalance(int amount)
        {
            Balance += amount;
            return Balance;
        }

        public int DecreaseBalance(int amount)
        {
            Balance -= amount;
            return Balance;
        }

        private int GetNewLocation(int currentLocation, int rollOfDice)
        {
            var sum = currentLocation + rollOfDice;
            if (sum == NumberOfBoardLocations)
            {
                return sum;
            }
            return sum % NumberOfBoardLocations;
        }
        private bool CheckIfPassedGo(int currentPosition, int newPosition, bool isMovingForward)
        {
            if (isMovingForward == false)
                return false;
            if (currentPosition > newPosition)
                return true;
            return false;
        }
        private void PassedGo()
        {
            Balance += 200;
            //Console.WriteLine("You have passed go, $200 have been added to your account, your new balance is {0}", Balance);
        }
    }
}