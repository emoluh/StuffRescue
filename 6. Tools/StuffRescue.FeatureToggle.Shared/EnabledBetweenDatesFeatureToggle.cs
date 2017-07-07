using StuffRescue.FeatureToggle.Internal;
using System;

namespace StuffRescue.FeatureToggle
{
    public class EnabledBetweenDatesFeatureToggle : IFeatureToggle
    {
        protected EnabledBetweenDatesFeatureToggle()
        {
            NowProvider = () => DateTime.Now;

            ToggleValueProvider = new AppSettingsProvider();
        }

        public Func<DateTime> NowProvider { get; set; }

        public virtual ITimePeriodProvider ToggleValueProvider { get; set; }

        public bool FeatureEnabled
        {
            get
            {
                var dates = ToggleValueProvider.EvaluateTimePeriod((this));

                var now = NowProvider.Invoke();

                return (now >= dates.Item1 && now <= dates.Item2);
            }
        }
    }
}
