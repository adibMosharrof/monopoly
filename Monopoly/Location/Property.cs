using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Monopoly
{
    public class Property : RealEstate
    {
        public string Color;
        public int SiteRent;
        public int HotelRent;
        public Dictionary<int, int> HouseRents;
        public int HouseCost;

        public Property()
        {
            
        }
        public Property(string[] values)
        {
            Name = values[0];
            Cost = Convert.ToInt16(values[1]);
            MortgageAmount = Convert.ToInt16(values[2]);
            SiteRent = Convert.ToInt16(values[3]);
            HotelRent = Convert.ToInt16(values[8]);
            Id = Convert.ToInt16(values[9]);
            Color = values[10];
            HouseCost = Convert.ToInt16(values[11]);

            var houseRents = new List<string> {values[4], values[5], values[6], values[7]};
            HouseRents = new Dictionary<int, int>();
            for (int i = 0; i < houseRents.Count; i++)
            {
                HouseRents.Add(i + 1, Convert.ToInt16(houseRents[i]));
            }
        }

        public override int GetRent(Dictionary<int, int> realEstateOwnerMap, Dictionary<int, Location> locations, Player player, int rollOfDice)
        {
            if (DoesOwnerOwnFullColorGroup(locations, realEstateOwnerMap))
            {
                return SiteRent*2;
            }
            return SiteRent;
        }

        private bool DoesOwnerOwnFullColorGroup(Dictionary<int, Location> locations, Dictionary<int, int> realEstateOwnerMap)
        {
            var propertyGroup = GetPropertyGroup(locations);
            if (!AreAllRealEstatesInGroupBought(propertyGroup, realEstateOwnerMap))
                return false;
            return AreAllRealEstatesInGroupBoughtBySamePlayer(propertyGroup, realEstateOwnerMap);
        }

        private List<RealEstate> GetPropertyGroup(Dictionary<int, Location> locations)
        {
            return locations.Values.OfType<Property>().Where(l => l.Color == Color).ToList<RealEstate>();
        }
    }
}