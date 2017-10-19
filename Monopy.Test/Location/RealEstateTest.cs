using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly;

namespace Monopy.Test
{
    [TestClass]
    public class RealEstateTest
    {
        private Dictionary<int, Monopoly.Location> locations;
        private List<Player> players;
        private Dictionary<int, int> realEstateOwnerMap;

        [TestMethod]
        public void SuccessfullyBuyingRealEstate()
        {

            var player = players[0];
            var location = (Property)locations[1];
            var initialPlayerBalance = player.Balance;
            player.GoToLocation(location.Id, locations);
            location.Action(player, players, realEstateOwnerMap, locations, 5);

            Assert.AreEqual(1, player.RealEstates.Count, "Bought RealEstate Added to player's list of real estate");
            Assert.AreEqual(initialPlayerBalance - location.Cost, player.Balance, "Updated Player balance after buying real estate");
            Assert.AreEqual(1, realEstateOwnerMap.Count, "Bought real estate map updated");
            Assert.IsTrue(realEstateOwnerMap.ContainsKey(location.Id), "Correct location added to map when real estate bought");
            Assert.AreEqual(player.Id, realEstateOwnerMap[location.Id], "Correct player added to map when real estate bought");
        }
        [TestMethod]
        public void DoesNotBuyPropertyDueToInsufficientBalance()
        {
            var player = players[0];
            player.Balance = 50;
            player.GoToLocation(1, 2, locations);
            locations[2].Action(player, players, realEstateOwnerMap, locations, 5);

            Assert.IsTrue(realEstateOwnerMap.Count == 0, "Did not buy property due to insufficient balance");
        }

        [TestMethod]
        public void NoRentWhenPlayerIsInOwnedRealEstate()
        {
            var player1 = players[0];
            var initialPlayerBalance = player1.Balance;
            var location = (Property)locations[1];
            realEstateOwnerMap.Add(location.Id, player1.Id);

            location.Action(player1, players, realEstateOwnerMap, locations, 5);
            Assert.AreEqual(initialPlayerBalance, player1.Balance, "Rent is not payed in real estate owned by player");
        }

        [TestMethod]
        public void NoRentWhenRealEstateIsMortgaged()
        {
            var initialPlayerBalance = players[0].Balance;
            var location = (Property)locations[1];
            location.IsMortgaged = true;
            realEstateOwnerMap.Add(location.Id, players[0].Id);
            players[1].GoToLocation(1, location.Id, locations);
            location.Action(players[1], players, realEstateOwnerMap, locations, 5);
            Assert.AreEqual(initialPlayerBalance, players[1].Balance, "Rent is not payed in real estate is mortgaged");
        }

        [TestInitialize]
        public void Init()
        {
            locations = new Dictionary<int, Monopoly.Location>()
            {
                {1,new Property() {Id = 1, Cost = 100}},
                {2,new Property() {Id = 2, Cost = 200}},
                {3,new Property() {Id = 3, Cost = 300}}
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
