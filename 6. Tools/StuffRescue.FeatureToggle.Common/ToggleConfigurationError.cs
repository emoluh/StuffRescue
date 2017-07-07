﻿using System;

namespace StuffRescue.FeatureToggle
{
    public class ToggleConfigurationError : Exception
    {
        public ToggleConfigurationError(string message) : base(message)
        {
        }

        public ToggleConfigurationError(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
