namespace FeatureFlagTests.Utilities
{
    using Microsoft.Extensions.Logging;


    /// <summary>The application logging.</summary>
    internal static class ApplicationLogging
    {
        /// <summary>The _logger factory.</summary>
        private static ILoggerFactory _loggerFactory;

        /// <summary>Gets the logger factory.</summary>
        internal static ILoggerFactory LoggerFactory => _loggerFactory ?? (_loggerFactory = new LoggerFactory());

        /// <summary>The create logger.</summary>
        /// <typeparam name="T">the type that the logger will be reporting on</typeparam>
        /// <returns>The <see cref="ILogger"/>.</returns>
        internal static ILogger CreateLogger<T>() => LoggerFactory.CreateLogger<T>();

        /// <summary>The create logger.</summary>
        /// <param name="categoryName">The category name.</param>
        /// <returns>The <see cref="ILogger"/>.</returns>
        internal static ILogger CreateLogger(string categoryName) => LoggerFactory.CreateLogger(categoryName);

    }
}