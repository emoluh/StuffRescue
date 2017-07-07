namespace StuffRescue.FeatureToggle
{
    public interface IBooleanToggleValueProvider
    {
        bool EvaluateBooleanToggleValue(IFeatureToggle toggle);
    }
}
