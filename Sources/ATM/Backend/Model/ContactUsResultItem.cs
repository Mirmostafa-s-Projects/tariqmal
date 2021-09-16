using System.Collections.Generic;
using Mohammad.Projects.TariqMal.Business.Model.Internals;

namespace Mohammad.Projects.TariqMal.Business.Model
{
    public class ContactUsResultItem : ResultItemBase
    {
        public IEnumerable<(string Key, string Value)> Data { get; set; }
    }
}