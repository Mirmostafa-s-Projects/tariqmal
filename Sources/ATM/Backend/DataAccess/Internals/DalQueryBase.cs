using System;
using Mohammad.Data.DataAccessTools;
using Mohammad.Projects.TariqMal.DataAccess.DataSources;

namespace Mohammad.Projects.TariqMal.DataAccess.Internals
{
    public abstract class DalQueryBase<TDalEntity, TDalQuery> : DataAccessEntityOnLinq<TDalQuery, TariqMalDataClassesDataContext, TDalEntity>
        where TDalQuery : class, new()
        where TDalEntity : DalQueryBase<TDalEntity, TDalQuery>, new()
    {
        public abstract TDalQuery SelectById(Guid id);

        protected override TariqMalDataClassesDataContext GetDb() => new TariqMalDataClassesDataContext();
//#if DEBUG
//        protected override TariqMalDataClassesDataContext GetDb() => new TariqMalDataClassesDataContext();
//#else
//        protected override TariqMalDataClassesDataContext GetDb()
//        {
//            var connectionString = ApplicationHelper.GetConnectionString<DalQueryBase<TDalEntity, TDalQuery>>("Local");
//            throw new Exception(connectionString);
//            return new TariqMalDataClassesDataContext(connectionString);
//        }
//#endif
    }
}