using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly;

namespace Monopy.Test.Location
{
    [TestClass]
    public class UtilityTest
    {
        private Dictionary<int, Monopoly.Location> locations;
        private List<Player> players;
        private Dictionary<int, int> realEstateOwnerMap;
        
        [TestMethod]
        public void GetUtilityRent()
        {
            realEstateOwnerMap.Add(locations[1].Id, players[0].Id);
            players[1].GoToLocation(1, locations[1].Id, locations);
            var playerInitialBalance = players[1].Balance;
            locations[1].Action(players[1], players, realEstateOwnerMap, locations, 5);

            Assert.AreEqual(playerInitialBalance - 20, players[1].Balance, "Player rent for one utility");
        }

        [TestMethod]
        public void GetUtilityRentWhenBothOwnedBySamePlayer()
        {
            realEstateOwnerMap.Add(locations[1].Id, players[0].Id);
            realEstateOwnerMap.Add(locations[2].Id, players[0].Id);
            players[1].GoToLocation(1, locations[1].Id, locations);
            var playerInitialBalance = players[1].Balance;
            locations[1].Action(players[1], players, realEstateOwnerMap, locations, 5);

            Assert.AreEqual(playerInitialBalance - 50, players[1].Balance, "Player rent for one utility");
        }

        [TestInitialize]
        public void Init()
        {
            locations = new Dictionary<int, Monopoly.Location>()
            {
                {1,new Utility() {Id = 1, Cost = 150, DiceMultiplierForRent = new Dictionary<int, int>(){{1,4},{2,10}}}},
                {2,new Utility() {Id = 2, Cost = 150, DiceMultiplierForRent = new Dictionary<int, int>(){{1,4},{2,10}}}},
                {3,new Railway() {Id = 3, Cost = 200,RentOnNumberOfOwnership = new Dictionary<int, int>(){{1,25},{2,50},{3,100},{4,200}}}},
                {4,new Railway() {Id = 4, Cost = 200,RentOnNumberOfOwnership = new Dictionary<int, int>(){{1,25},{2,50},{3,100},{4,200}}}},
                {5,new Property() {Id = 5, Cost = 100}},
                {6,new Property() {Id = 6, Cost = 200}},
            };
            players = new List<Player>()
            {
                new Player() {Id = 1},
                new Player() {Id = 2}
            };
            realEstateOwnerMap = new Dictionary<int, int>();
        }
    }
}
