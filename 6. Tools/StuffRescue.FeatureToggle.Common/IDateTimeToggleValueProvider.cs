using System;

namespace StuffRescue.FeatureToggle
{
    public interface IDateTimeToggleValueProvider
    {
        DateTime EvaluateDateTimeToggleValue(IFeatureToggle toggle);
    }
}
