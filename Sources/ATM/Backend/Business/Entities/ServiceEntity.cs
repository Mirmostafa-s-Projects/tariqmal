using System;
using System.Collections.Generic;
using Mohammad.Projects.TariqMal.Business.Internals;
using Mohammad.Projects.TariqMal.Business.Model;
using Mohammad.Projects.TariqMal.DataAccess.DataSources;
using Mohammad.Projects.TariqMal.DataAccess.Infrastructure;

namespace Mohammad.Projects.TariqMal.Business.Entities
{
    public sealed class ServiceEntity : AtmManipulationBusinessEntityBase<ServiceEntity, Service, ServiceDalEntity,
        ServiceResultSet, ServiceResultItem,
        ServiceArgument>
    {
        protected override ServiceResultItem ConvertToBusinessModel(Service dalModel, Language lang) => dalModel.Convert(lang);

        protected override Service ConvertToDataModel(ServiceArgument argument) => throw new NotImplementedException();

        protected override IEnumerable<Service> OnGotAll(IEnumerable<Service> dalModels)
        {
            foreach (var dalModel in dalModels)
            {
                dalModel.TextTraslationId = Guid.Empty;
                yield return dalModel;
            }
        }

        protected override Service OnGotById(Service dalModel)
        {
            dalModel.SummaryTraslationId = Guid.Empty;
            return base.OnGotById(dalModel);
        }
    }
}