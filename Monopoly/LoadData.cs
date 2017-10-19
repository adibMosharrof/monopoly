using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    class LoadData
    {
        private string _projectDirectory;
        public List<Location> FromCsvFiles(string projectDirectory)
        {
            var locations = new List<Location>();
            _projectDirectory = projectDirectory;
            ReadFile("realEstate", locations, LoadRealEstates);
            ReadFile("railways", locations, LoadRailways);
            ReadFile("utilities", locations, LoadUtilities);
            ReadFile("lottery", locations, LoadLottery);
            ReadFile("speciallocations", locations, LoadSpecialLocation);
            ReadFile("tax", locations, LoadTaxLocation);
            return locations;
        }

        private void LoadUtilities(List<Location> locations, string[] values)
        {
            var utility = new Utility(values);
            locations.Add(utility);
        }

        private void LoadRealEstates(List<Location> locations, string[] values)
        {
            var realEstate = new Property(values);
            locations.Add(realEstate);
        }
        private void LoadRailways(List<Location> locations, string[] values)
        {
            var station = new Railway(values);
            locations.Add(station);
        }

        private void LoadLottery(List<Location> locations, string[] values)
        {
            var lottery = new Lottery(values);
            locations.Add(lottery);
        }

        private void LoadSpecialLocation(List<Location> locations, string[] values)
        {
            var specialLocation = new SpecialLocation(values);
            locations.Add(specialLocation);
        }

        private void LoadTaxLocation(List<Location> locations, string[] values)
        {
            var taxLocation = new Tax(values);
            locations.Add(taxLocation);
        }
        private void ReadFile(string pathName, List<Location> locations, Action<List<Location>, string[]> loadingFunction)
        {
            using (var reader = new StreamReader(_projectDirectory + "\\Data\\" + pathName + ".csv"))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    loadingFunction(locations, values);
                }
            }
        }
    }
}
