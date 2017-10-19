using System;
using System.Collections.Generic;

namespace Monopoly
{
    public class Location
    {
        public int Id;
        public string Name;
        public Dictionary<int, Player> Players;

        public Location()
        {
            Players = new Dictionary<int, Player>();
        }

        public virtual string Action(Player player, List<Player> estateOwnerMap, Dictionary<int, int> realEstateOwnerMap, Dictionary<int, Location> locations, int rollOfDice)
        {
            return "There is no action in this location";
        }
    }
}