using System.Collections;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using FeatureFlagTests.Utilities.FeatureFlags;

namespace FeatureFlagTests.Configuration
{
    public class AppConfiguration
    {
        public IEnumerable<FeatureFlag> FeatureFlags { get; set; }

    }
}