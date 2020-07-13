using System.Web.Mvc;
using FeatureFlagTests.Toggles;

namespace FeatureFlagTests.Utilities.FeatureFlags
{
    public class FeatureGate : FilterAttribute, IActionFilter
    {
        private readonly FeatureToggles _featureToggle;
        public FeatureGate(FeatureToggles featureToggle)
        {
            _featureToggle = featureToggle;
        }
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!FeatureUtilities.IsEnabled(_featureToggle))
            {
                filterContext.Result = new HttpNotFoundResult();
            }
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }
    }
}