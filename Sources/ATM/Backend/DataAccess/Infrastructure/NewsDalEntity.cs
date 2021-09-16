using System;
using System.Linq;
using Mohammad.Projects.TariqMal.DataAccess.DataSources;
using Mohammad.Projects.TariqMal.DataAccess.Internals;

namespace Mohammad.Projects.TariqMal.DataAccess.Infrastructure
{
    public sealed class NewsDalEntity : DalQueryBase<NewsDalEntity, New>
    {
        public override New SelectById(Guid id)
        {
            var q = from item in this.Select()
                    where item.Id == id
                    select item;
            return q.FirstOrDefault();
        }

        public void DeleteById(Guid id)
        {
        }
    }
}