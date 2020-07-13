using System;

namespace FeatureFlagTests.Utilities.FeatureFlags
{
    /// <summary>
    /// The feature toggle not configured exception.
    /// </summary>
    public class FeatureToggleNotConfiguredException : Exception
    {
        /// <summary>
        /// The default message.
        /// </summary>
        private static readonly string DefaultMessage =
            "The feature toggle used was not located in the Azure App Configuration service for this environment";

        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureToggleNotConfiguredException"/> class.
        /// </summary>
        public FeatureToggleNotConfiguredException() : base(DefaultMessage)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureToggleNotConfiguredException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public FeatureToggleNotConfiguredException(string message) : base(message + ": " + DefaultMessage)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureToggleNotConfiguredException"/> class.
        /// </summary>
        /// <param name="innerException">
        /// The inner exception.
        /// </param>
        public FeatureToggleNotConfiguredException(Exception innerException) : base(DefaultMessage, innerException)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureToggleNotConfiguredException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="innerException">
        /// The inner exception.
        /// </param>
        public FeatureToggleNotConfiguredException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}