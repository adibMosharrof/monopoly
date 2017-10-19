using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public class Utility : RealEstate
    {
        public Dictionary<int, int> DiceMultiplierForRent;
        public Utility()
        {
            
        }
        public Utility(string[] values)
        {
            Name = values[0];
            Id = Convert.ToInt16(values[3]);
            MortgageAmount = Convert.ToInt16(values[4]);
            Cost = Convert.ToInt16(values[5]);
            var multipliers = new List<string> {values[1], values[2]};
            DiceMultiplierForRent = new Dictionary<int, int>();
            var counter = 1;
            foreach (var multiplier in multipliers)
            {
                DiceMultiplierForRent[counter] = Convert.ToInt16(multiplier);
                counter++;
            }
        }

        public override int GetRent(Dictionary<int, int> realEstateOwnerMap, Dictionary<int, Location> locations, Player player, int rollOfDice)
        {
            var ownerId = realEstateOwnerMap[player.BoardPosition];
            var numOfOwnedUtility = GetNumberOfOwnedPropertyInPropertyGroup(ownerId, this, realEstateOwnerMap, locations);
            return DiceMultiplierForRent[numOfOwnedUtility] * rollOfDice;
        }
    }
}
