using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FeatureFlagTests.Configuration;
using FeatureFlagTests.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.FeatureManagement.Mvc;

namespace FeatureFlagTests.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string message = "Beta feature is off";

            if (FeatureUtilities.FeatureOn(FeatureUtilities.Beta))
            {
                message = "Beta feature is on";
            }

            ViewBag.Message = message;

            return View();
        }

        [FeatureGate("Beta")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}