using FeatureFlagTests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatureFlagsOnMvcFramework.Tests.Utilities
{
    using FeatureFlagTests.Toggles;
    using FeatureFlagTests.Utilities.FeatureFlags;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.Extensions.Configuration;

    using Moq;

    /// <summary>
    /// Defines the <see cref="FeatureUtilityTests" />.
    /// </summary>
    [TestClass]
    public class FeatureUtilityTests
    {
        /// <summary>
        /// The IsEnabled_MatchingParameter_YesTheFeatureIsEnabled.
        /// </summary>
        [TestMethod]
        public void IsEnabled_MatchingParameter_YesTheFeatureIsEnabled()
        {
            // arrange
            FeatureUtilities.Flags = new List<FeatureFlag>()
             {
                 new FeatureFlag
                 {
                     Id = "Test",
                     Enabled = true
                 },
                 new FeatureFlag
                 {
                     Id = "Beta",
                     Enabled = true
                 }
             };

            // act
            var result = FeatureUtilities.IsEnabled(FeatureToggles.Test);

            // assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// The IsNotEnabled_MatchingParameter_YesTheFeatureIsNotEnabled.
        /// </summary>
        [TestMethod]
        public void IsNotEnabled_MatchingParameter_YesTheFeatureIsNotEnabled()
        {
            // arrange
            var result = true;
            FeatureUtilities.Flags = new List<FeatureFlag>()
            {
                new FeatureFlag
                {
                    Id = "Test",
                    Enabled = false
                }
            };

            // act
            result = FeatureUtilities.IsEnabled(FeatureToggles.Test);

            // assert
            Assert.IsFalse(result);
        }

        /// <summary>
        /// The IsEnabled_MissingFlag_FeatureDisabled.
        /// </summary>
        [TestMethod]
        public void IsEnabled_MissingFlag_FeatureDisabled()
        {
            // arrange
            var result = true;
            FeatureUtilities.Flags = new List<FeatureFlag>();

            // act
            result = FeatureUtilities.IsEnabled(FeatureToggles.Test);

            // assert
            Assert.IsFalse(result);
        }

        /// <summary>
        /// The IsEnabled_MatchingParameter_YesTheFeatureIsEnabled.
        /// </summary>
        [TestMethod]
        public void PopulateFlags_ConfigurationResults_ListOfFlags()
        {
            // arrange
            var test1Name = "Test1";
            var test2Name = "Test2";
            var oneSectionMock = new Mock<IConfigurationSection>();
            oneSectionMock.Setup(s => s.Value).Returns(test1Name);
            var twoSectionMock = new Mock<IConfigurationSection>();
            twoSectionMock.Setup(s => s.Value).Returns(test2Name);
            var section = new List<IConfigurationSection>
            {
                oneSectionMock.Object,
                twoSectionMock.Object
            };


            // act
            FeatureUtilities.PopulateFlags(section);

            // assert
            Assert.AreEqual(2, FeatureUtilities.Flags.Count(), "Flags do not contain 2 elements, actual count: " + FeatureUtilities.Flags.Count());

            Assert.IsNotNull(FeatureUtilities.Flags.FirstOrDefault(x => x.Id == test1Name), $"There is no flag named {test1Name}");
            Assert.IsNotNull(FeatureUtilities.Flags.FirstOrDefault(x => x.Id == test2Name), $"There is no flag named {test2Name}");

        }
    }
}
