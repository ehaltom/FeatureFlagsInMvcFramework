using System;
using System.Configuration;

namespace FeatureFlagTests.Utilities
{
    [System.AttributeUsage(System.AttributeTargets.Class |
                               System.AttributeTargets.Struct | System.AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class Feature : Attribute
    {

        private string name;

        public Feature(string name)
        {
            if (FeatureUtilities.FeatureOn(name))
            {

            }
        }

    }
}