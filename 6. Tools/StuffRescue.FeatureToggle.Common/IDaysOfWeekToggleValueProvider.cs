using System;
using System.Collections.Generic;

namespace StuffRescue.FeatureToggle
{
    public interface IDaysOfWeekToggleValueProvider
    {
        IEnumerable<DayOfWeek> GetDaysOfWeek(IFeatureToggle toggle);
    }
}
