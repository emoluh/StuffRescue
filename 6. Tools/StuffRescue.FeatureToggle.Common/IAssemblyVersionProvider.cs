using System;

namespace StuffRescue.FeatureToggle
{
    public interface IAssemblyVersionProvider
    {
        Version EvaluateVersion(IFeatureToggle toggle);
    }
}
