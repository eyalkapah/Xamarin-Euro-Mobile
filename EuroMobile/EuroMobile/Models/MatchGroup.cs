using System.Collections.ObjectModel;

namespace EuroMobile.Models
{
    public class MatchGroup : ObservableCollection<Match>
    {
        public MatchGroup(string name, string shotName)
        {
            Name = name;
            ShotName = shotName;
        }

        public string Name { get; }
        public string ShotName { get; }
    }
}