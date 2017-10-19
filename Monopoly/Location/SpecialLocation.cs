using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    class SpecialLocation: Location
    {
        enum Position
        {
            Go=1,
            Jail=11,
            FreeParking=21,
            GoToJail=31
        }
        public SpecialLocation(string[] values)
        {
            Name = values[0];
            Id = Convert.ToInt16(values[1]);
        }

        public override string Action(Player player, List<Player> estateOwnerMap, Dictionary<int, int> realEstateOwnerMap, Dictionary<int, Location> locations, int rollOfDice)
        {
            if (Id == (int) Position.GoToJail)
            {
                player.GoToLocation(player.BoardPosition, (int)Position.Jail, locations, false);
                return String.Format("{0} has been sent to jail", player.Name);
            }
            return String.Format("There is no action in {0}", this.Name);
        }
    }
}
