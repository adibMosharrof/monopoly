using System;
using System.Collections.Generic;


namespace Monopoly
{
    public class Railway : RealEstate
    {
        public Dictionary<int, int> RentOnNumberOfOwnership;
        public Railway()
        {
            
        }
        public Railway(string[] values)
        {
            Id = Convert.ToInt16(values[5]);
            Name = values[0];
            MortgageAmount = Convert.ToInt16(values[6]);
            Cost = Convert.ToInt16(values[7]);
            var rents = new List<string> {values[1], values[2], values[3], values[4]};
            var counter = 1;
            RentOnNumberOfOwnership = new Dictionary<int, int>();
            foreach (var rent in rents)
            {
                RentOnNumberOfOwnership.Add(counter, Convert.ToInt16(rent));
                counter++;
            }
        }

        public override int GetRent(Dictionary<int, int> realEstateOwnerMap, Dictionary<int, Location> locations, Player player, int rollOfDice)
        {
            var ownerId = realEstateOwnerMap[player.BoardPosition];
            var numOfOwnedRailways = GetNumberOfOwnedPropertyInPropertyGroup(ownerId, this, realEstateOwnerMap, locations);
            return RentOnNumberOfOwnership[numOfOwnedRailways];
        }
    }
}