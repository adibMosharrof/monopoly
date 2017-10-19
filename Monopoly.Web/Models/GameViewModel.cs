using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Monopoly.Web.Models
{
    public class GameViewModel
    {
        public List<Location> Locations;
        public List<Player> Players;
        public Player CurrentPlayer;
        public int DieValue;
        public string ActionMessage;
        public bool IsThrowDie;
    }
}