using Mohammad.Data.BusinessTools;

namespace Mohammad.Projects.TariqMal.Business.Internals
{
    public abstract class AtmBusinessEntityBase<TBusinessEntity> : BusinessEntityBase<TBusinessEntity>
        where TBusinessEntity : BusinessEntityBase<TBusinessEntity>, new()
    {
        protected virtual (TResult Result, string Message) CreateSuccess<TResult>(TResult result) => (result, null);
        protected virtual (object Result, string Message) CreateSuccess() => (null, null);
    }
}