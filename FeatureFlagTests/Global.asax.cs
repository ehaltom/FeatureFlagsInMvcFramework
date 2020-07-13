using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using FeatureFlagTests.Configuration;
using FeatureFlagTests.Controllers;
using FeatureFlagTests.Utilities;
using FeatureFlagTests.Utilities.FeatureFlags;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Newtonsoft.Json;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace FeatureFlagTests
{
    using Microsoft.Extensions.Logging;

    using NLog;

    using Unity.NLog;

    using ILogger = Microsoft.Extensions.Logging.ILogger;

    /// <summary>The mvc application.</summary>
    public class MvcApplication : HttpApplication
    {
        /// <summary>The _configuration.</summary>
        private static IConfiguration _configuration = null;

        /// <summary>The _refresher.</summary>
        private static IConfigurationRefresher _refresher = null;

        /// <summary>The application_ start.</summary>
        public void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            var container = new UnityContainer();
            container.AddNewExtension<NLogExtension>();
        }

        /// <summary>The application_ begin request.</summary>
        protected void Application_BeginRequest()
        {
            _configuration = new ConfigurationBuilder()
                .AddAzureAppConfiguration(options =>
                {
                    options.Connect(Environment.GetEnvironmentVariable("AppConfigConnectionString"))
                        .UseFeatureFlags()
                        .ConfigureRefresh(refresh =>
                        {
                            refresh.Register("FeatureManagement")
                                .SetCacheExpiration(TimeSpan.FromSeconds(10));
                        });

                    _refresher = options.GetRefresher();
                }).Build();
            var results = _configuration.GetSection("FeatureManagement").GetChildren();
            FeatureUtilities.PopulateFlags(results);
        }
    }
}
