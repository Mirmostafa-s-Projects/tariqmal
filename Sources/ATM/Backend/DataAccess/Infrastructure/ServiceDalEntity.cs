using System;
using System.Linq;
using Mohammad.Projects.TariqMal.DataAccess.DataSources;
using Mohammad.Projects.TariqMal.DataAccess.Internals;

namespace Mohammad.Projects.TariqMal.DataAccess.Infrastructure
{
    public sealed class ServiceDalEntity : DalQueryBase<ServiceDalEntity, Service>
    {
        public override Service SelectById(Guid id)
        {
            var q = from service in Select() where service.Id == id select service;
            return q.FirstOrDefault();
        }
    }
}