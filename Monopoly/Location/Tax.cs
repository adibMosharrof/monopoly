using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public class Tax : Location
    {
        //key = Position (Id), value = Tax Amount
        private Dictionary<int, int> TaxAmountByLocation;
        public Tax(string[] values)
        {
            Name = values[0];
            Id = Convert.ToInt16(values[1]);
            TaxAmountByLocation = new Dictionary<int, int>(){
                {5 , 100},
                {39, 75}
            };
        }

        public override string Action(Player player, List<Player> estateOwnerMap, Dictionary<int, int> realEstateOwnerMap, Dictionary<int, Location> locations, int rollOfDice)
        {
            player.DecreaseBalance(TaxAmountByLocation[Id]);
            return String.Format("{0} have been charged with income tax $100", player.Name);
        }
    }
}
