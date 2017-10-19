using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly;

namespace Monopy.Test.Location
{
    [TestClass]
    public class PropertyTest
    {
        private Dictionary<int, Monopoly.Location> locations;
        private List<Player> players;
        private Dictionary<int, int> realEstateOwnerMap;

        [TestMethod]
        public void RentBeingPaid()
        {
            var player1 = players[0];
            var initialPlayerBalance = player1.Balance;
            var location = (Property)locations[1];
            realEstateOwnerMap.Add(location.Id, player1.Id);

            var player2Balance = players[1].Balance;

            players[1].GoToLocation(1, location.Id, locations);
            location.Action(players[1], players, realEstateOwnerMap, locations, 5);

            Assert.AreEqual(player2Balance - location.SiteRent, players[1].Balance, "Player is paying rent when visiting a owned real estate");
            Assert.AreEqual(initialPlayerBalance + location.SiteRent, player1.Balance, "Owner of property is getting rent");
        }

        [TestMethod]
        public void RentWhenWholeColorGroupOwned()
        {
            var player1 = players[0];
            var initialPlayerBalance = player1.Balance;
            var location = (Property)locations[1];
            realEstateOwnerMap.Add(location.Id, player1.Id);
            realEstateOwnerMap.Add(locations[2].Id, player1.Id);
            
            var player2Balance = players[1].Balance;

            players[1].GoToLocation(1, location.Id, locations);
            location.Action(players[1], players, realEstateOwnerMap, locations, 5);

            Assert.AreEqual(player2Balance - 2*location.SiteRent, players[1].Balance, "Player is paying double rent e");
            Assert.AreEqual(initialPlayerBalance + 2*location.SiteRent, player1.Balance, "Owner of property is getting double rent");
        }

        

        [TestInitialize]
        public void Init()
        {
            locations = new Dictionary<int, Monopoly.Location>()
            {
                {1,new Property() {Id = 1, Cost = 100, SiteRent = 10, Color = "Red"}},
                {2,new Property() {Id = 2, Cost = 200, SiteRent = 12, Color = "Red"}},
                {3,new Property() {Id = 3, Cost = 300, Color = "Green"}}
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
