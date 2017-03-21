using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace StuffRescue.Web.Models.StuffViewModels
{
    public class ItemViewModel
    {
        public ICollection<IFormFile> Photos { get; set; }
        public string Make { get; set; }

        public string Category { get; set; }

        public Condition Condition { get; set; }

        public string Description { get; set; }

        public string Price { get; set; }

        public bool Negotiable { get; set; }

        public bool Share { get; set; }

        public bool Location { get; set; }
    }
    public enum Condition
    {
        Other,
        ForParts,
        Used,
        OpenBox,
        Certified,
        New
    }
}
