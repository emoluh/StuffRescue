using StuffRescue.Services.Models.FeaturesToggleViewModels;
using System.Collections.Generic;

namespace StuffRescue.Services.Messaging.FeaturesService
{
    public class GetAllFeaturesResponse
    {
        public IEnumerable<FeatureViewModel> Features { get; set; }
    }
}
