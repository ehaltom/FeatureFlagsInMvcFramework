using System.Collections;
using System.Collections.Generic;

namespace FeatureFlagTests.Utilities.FeatureFlags
{
    public class FeatureFlag
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public bool Enabled { get; set; }
        //public ICollection<ClientFilters> Conditions { get; set; }
    }

    public class ClientFilters
    {
        public string Name { get; set; }
    }
}



