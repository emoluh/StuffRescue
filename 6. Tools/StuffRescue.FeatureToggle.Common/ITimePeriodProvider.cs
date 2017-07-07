using System;

namespace StuffRescue.FeatureToggle
{
    public interface ITimePeriodProvider
    {
        Tuple<DateTime, DateTime> EvaluateTimePeriod(IFeatureToggle toggle);
    }
}
