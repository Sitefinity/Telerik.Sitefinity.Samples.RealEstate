using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Telerik.StarterKit.Modules.RealEstate.Model
{
    public class Photo
    {
        internal Photo()
        {

        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
    }
}
