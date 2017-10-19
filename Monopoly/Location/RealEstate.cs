using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Monopoly
{
    public abstract class RealEstate : Location
    {
        public int MortgageAmount;
        public bool IsMortgaged;
        public int Cost;

        public override string Action(Player player, List<Player> players, Dictionary<int, int> realEstateOwnerMap, Dictionary<int, Location> locations, int rollOfDice)
        {
            if (IsEmptyProperty(player.BoardPosition, realEstateOwnerMap))
            {
                if (!DoesPlayerHaveBalanceToBuyProperty(player))
                    return String.Format("{0} does not have sufficient balance to buy {1}, which costs {2}",
                        player.Name, this.Name, this.Cost);

                BuyProperty(player, realEstateOwnerMap);
                return String.Format("{0} has bought {1}", player.Name, this.Name);
            }
            if (realEstateOwnerMap[player.BoardPosition] == player.Id)
                return String.Format("{0} owns {1}, so no rent needs to be paid", player.Name, this.Name);
            if (IsMortgaged)
                return String.Format("{0} is mortgaged, so no rent needs to be paid", this.Name);

            var rent = GetRent(realEstateOwnerMap, locations, player, rollOfDice);
            player.DecreaseBalance(rent);
            var realEstateOwnerPlayer = players.First(p => p.Id == realEstateOwnerMap[player.BoardPosition]);
            realEstateOwnerPlayer.IncreaseBalance(rent);
            return String.Format("{0} paid ${1} in rent to {2}", player.Name, rent, realEstateOwnerPlayer.Name);
        }

        public abstract int GetRent(Dictionary<int, int> realEstateOwnerMap, Dictionary<int, Location> locations, Player player, int rollOfDice);

        public void BuyProperty(Player player, Dictionary<int, int> propertyOwnerMap)
        {
            player.RealEstates.Add(this);
            propertyOwnerMap.Add(player.BoardPosition, player.Id);
            player.DecreaseBalance(Cost);
        }

        public void MortgageProperty(Player player)
        {
            IsMortgaged = true;
            player.IncreaseBalance(MortgageAmount);
        }
        protected bool IsEmptyProperty(int position, Dictionary<int, int> propertyOwnerMap)
        {
            return !propertyOwnerMap.ContainsKey(position);
        }
        protected bool AreAllRealEstatesInGroupBought(IEnumerable<RealEstate> propertyGroup, Dictionary<int, int> realEstateOwnerMap)
        {
            return propertyGroup.All(prop => realEstateOwnerMap.ContainsKey(prop.Id));
        }
        protected bool AreAllRealEstatesInGroupBoughtBySamePlayer(List<RealEstate> propertyGroup, Dictionary<int, int> realEstateOwnerMap)
        {
            var firstPropOwner = realEstateOwnerMap[propertyGroup.First().Id];
            return propertyGroup.All(prop => realEstateOwnerMap[prop.Id] == firstPropOwner);
        }

        protected virtual int GetNumberOfOwnedPropertyInPropertyGroup<T>(int ownerId, T realEstate, Dictionary<int, int> realEstateOwnerMap, Dictionary<int, Location> locations) where T : RealEstate
        {
            MethodInfo method = typeof(Queryable).GetMethod("OfType");
            MethodInfo generic = method.MakeGenericMethod(new Type[] { realEstate.GetType() });
            // Use .NET 4 covariance
            var propertyGroup = (IEnumerable<RealEstate>)generic.Invoke
                  (null, new object[] { locations.Values.AsQueryable() });

            return propertyGroup.Count(prop => realEstateOwnerMap.ContainsKey(prop.Id) && realEstateOwnerMap[prop.Id] == ownerId);
        }

        public bool DoesPlayerHaveBalanceToBuyProperty(Player player)
        {
            return player.Balance > Cost;
        }
    }
}