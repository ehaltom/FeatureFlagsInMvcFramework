namespace FeatureFlagTests.Utilities
{
    using FeatureFlagTests.Toggles;
    using FeatureFlagTests.Utilities.FeatureFlags;
    using Microsoft.Extensions.Configuration;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.Extensions.Logging;

    /// <summary>The feature utilities.</summary>
    public static class FeatureUtilities
    {
        /// <summary>Gets or sets the flags.</summary>
        public static IEnumerable<FeatureFlag> Flags { get; set; } = new List<FeatureFlag>();

        /// <summary>The is enabled.</summary>
        /// <param name="feature">The feature.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <exception cref="FeatureToggleNotConfiguredException">a feature toggle not configured in azure exception. This will trigger if a particular feature flag is not present within the Azure App Configuration project for a particular environment</exception>
        public static bool IsEnabled(FeatureToggles feature)
        {
            var featureName = feature.ToString();
            var val = Flags.FirstOrDefault(x => x.Id == featureName);
            if (val == null)
            {
                throw new FeatureToggleNotConfiguredException(featureName);
            }

            return val.Enabled;
        }

        /// <summary>The is not enabled.</summary>
        /// <param name="feature">The feature.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool IsNotEnabled(FeatureToggles feature)
        {
            return !IsEnabled(feature);
        }

        /// <summary>The populate flags.</summary>
        /// <param name="section">The section.</param>
        public static void PopulateFlags(IEnumerable<IConfigurationSection> section)
        {
            var list = section.Select(item => new FeatureFlag { Id = item.Key, Enabled = bool.Parse(item.Value) })
                .ToList();

            Flags = list;
        }
    }
}