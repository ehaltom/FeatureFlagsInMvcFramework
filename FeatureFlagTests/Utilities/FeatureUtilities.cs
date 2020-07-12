using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using FeatureFlagTests.Utilities.FeatureFlags;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace FeatureFlagTests.Utilities
{
    public static class FeatureUtilities
    {
        public static IEnumerable<FeatureFlag> Flags { get; set; } = new List<FeatureFlag>();
        public static bool FeatureOn(string feature)
        {
            var val = Flags.FirstOrDefault(x => x.Id == feature);

            return val?.Enabled ?? false;
        }

        public static void PopulateFlags(IEnumerable<IConfigurationSection> section)
        {
            var list = section.Select(item =>
                    new FeatureFlag { Id = item.Key, Enabled = bool.Parse(item.Value) })
                .ToList();


            Flags = list;
        }

        public const string Beta = "Beta";
        public const string NewFeature = "NewFeature";
    }
}
