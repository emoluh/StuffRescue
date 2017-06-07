using StuffRescue.FeatureToggle.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace StuffRescue.FeatureToggle
{
    public class EnabledOnOrBeforeDateFeatureToggle : IFeatureToggle
    {

        protected EnabledOnOrBeforeDateFeatureToggle()
        {
            NowProvider = () => DateTime.Now;

            ToggleValueProvider = new AppSettingsProvider();
        }
        

        public Func<DateTime> NowProvider { get; set; }

        public virtual IDateTimeToggleValueProvider ToggleValueProvider { get; set; }


        public bool FeatureEnabled
        {
            get { return NowProvider.Invoke() <= ToggleValueProvider.EvaluateDateTimeToggleValue(this); }
        }
    }
}
