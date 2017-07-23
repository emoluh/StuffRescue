using AutoMapper;
using StuffRescue.Business.Entities;
using StuffRescue.Services.Models.FeaturesToggleViewModels;
using System.Collections.Generic;

namespace StuffRescue.Services.Mapping
{
    public static class FeatureMapper
    {
        public static IEnumerable<FeatureViewModel> ConvertToFeatureViewModel(
                                                this IEnumerable<Feature> features)
        {
            return Mapper.Map<IEnumerable<Feature>,
                              IEnumerable<FeatureViewModel>>(features);
        }
    }
}
