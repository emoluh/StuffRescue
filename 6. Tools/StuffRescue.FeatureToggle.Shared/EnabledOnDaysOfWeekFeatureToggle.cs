using StuffRescue.FeatureToggle.Internal;
using System;
using System.Linq;

namespace StuffRescue.FeatureToggle
{
    public class EnabledOnDaysOfWeekFeatureToggle : IFeatureToggle
    {

        protected EnabledOnDaysOfWeekFeatureToggle()
        {
            NowProvider = () => DateTime.Now;

            ToggleValueProvider = new AppSettingsProvider();
        }



        public Func<DateTime> NowProvider { get; set; }

        public virtual IDaysOfWeekToggleValueProvider ToggleValueProvider { get; set; }


        public bool FeatureEnabled
        {
            get
            {
                var dayToday = NowProvider.Invoke().DayOfWeek;

                var daysShouldBeEnabled = ToggleValueProvider.GetDaysOfWeek(this);

                return daysShouldBeEnabled.Contains(dayToday);
            }
        }
    }
    
}
