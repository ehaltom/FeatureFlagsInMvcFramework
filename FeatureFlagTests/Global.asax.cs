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
    public class MvcApplication : System.Web.HttpApplication
    {
        private static IConfiguration _configuration = null;
        private static IConfigurationRefresher _refresher = null;

        public void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            _configuration = new ConfigurationBuilder()
                .AddAzureAppConfiguration(options =>
                {
                    options.Connect(Environment.GetEnvironmentVariable("AppConfigConnectionString"))
                        .UseFeatureFlags().ConfigureRefresh(refresh =>
                        {
                            refresh.Register("TestApp:Settings:Message")
                                .SetCacheExpiration(TimeSpan.FromSeconds(10));
                        });

                    _refresher = options.GetRefresher();
                }).Build();
        }

        protected void Application_BeginRequest()
        {
            FeatureUtilities.PopulateFlags(_configuration.GetSection("FeatureManagement").GetChildren());
        }
    }
}
