using System;
using System.Collections.Generic;
using System.Linq;
using Mohammad.Data.BusinessTools;
using Mohammad.Exceptions;
using Mohammad.Helpers;
using Mohammad.Projects.TariqMal.Business.Infrastructure;
using Mohammad.Projects.TariqMal.Business.Model;
using Mohammad.Projects.TariqMal.Business.Model.Internals;
using Mohammad.Projects.TariqMal.DataAccess.Infrastructure;
using Mohammad.Projects.TariqMal.DataAccess.Internals;
using Mohammad.Web.Api.Exceptions;

namespace Mohammad.Projects.TariqMal.Business.Internals
{
    // TODO Use interfaces and extension methods to be plug-ing-able.
    public abstract class AtmManipulationBusinessEntityBase<TBusinessEntity, TDalModel, TDalEntity, TResult, TResultItem,
                                                            TArgument>
        : AtmBusinessEntityBase<TBusinessEntity> where TDalEntity : DalQueryBase<TDalEntity, TDalModel>, new()
                                                 where TResult : IList<TResultItem>, IResultSet<TResultItem>, new()
                                                 where TResultItem : IResultItem
                                                 where TDalModel : class, new()
                                                 where TBusinessEntity : BusinessEntityBase<TBusinessEntity>, new()
                                                 where TArgument : IArgument
    {
        public (TResult Result, string Message) Get(Language lang, Pagination pagination = null) => this.OnGettingAll(lang, pagination);

        protected virtual (TResult Result, string Message) OnGettingAll(Language lang, Pagination pagination)
        {
            var result = new TResult();
            using (var dal = new TDalEntity())
            {
                var dalModels = dal.Select().Page(pagination);
                dalModels = this.OnGotAll(dalModels);
                var bizModels = dalModels.Select(data => this.ConvertToBusinessModel(data, lang));
                foreach (var model in bizModels) result.Add(model);
            }

            if (!result.Any()) this.ThrowObjectNotFound(lang);
            return (result, null);
        }

        protected virtual IEnumerable<TDalModel> OnGotAll(IEnumerable<TDalModel> dalModels) => dalModels;

        protected virtual void ThrowObjectNotFound(Language lang)
        {
            throw new ObjectNotFoundException(TranslationEntity.Translate("No item found.", Language.En, lang));
        }

        public (TResultItem Result, string message) GetById(Guid id, Language lang) => this.OnGettingById(id, lang);

        protected virtual (TResultItem Result, string message) OnGettingById(Guid id, Language lang)
        {
            TResultItem result;
            using (var dal = new TDalEntity())
            {
                var dalModel = dal.SelectById(id);
                if (dalModel == null)
                    throw new NotFoundException(TranslationEntity.Translate("No item found.", Language.En, lang));
                dalModel = this.OnGotById(dalModel);
                result   = this.ConvertToBusinessModel(dalModel, lang);
            }

            if (result == null) this.ThrowObjectNotFound(lang);
            return (result, null);
        }

        protected virtual TDalModel OnGotById(TDalModel dalModel) => dalModel;

        protected abstract TResultItem ConvertToBusinessModel(TDalModel dalModel, Language lang);

        protected abstract TDalModel ConvertToDataModel(TArgument argument);

        public (Guid? Result, string Message) Add(TArgument arg, Language lang) => this.OnAdding(arg, lang);

        protected virtual (Guid? Result, string Message) OnAdding(TArgument argument, Language lang)
        {
            this.ValidateForAdd(argument, lang);
            var data = this.ConvertToDataModel(argument);
            using (var dal = new TDalEntity())
            {
                dal.Insert(data);
            }

            return (ObjectHelper.GetProp<Guid>(data, "Id"),
                    TranslationEntity.Translate("Item added successfully", Language.En, lang));
        }

        protected virtual void ValidateForAdd(TArgument argument, Language lang)
        {
        }

        public string Update(Guid id, TArgument arg, Language lang) => this.OnUpdating(id, arg, lang);

        protected virtual string OnUpdating(Guid id, TArgument argument, Language lang)
        {
            this.ValidateForUpdate(argument, lang);
            var data = this.ConvertToDataModel(argument);
            using (var dal = new TDalEntity())
            {
                var item = dal.SelectById(id);
                if (item == null)
                {
                    this.ThrowObjectNotFound(lang);
                    return null;
                }

                foreach (var property in ObjectHelper.GetPropertiesName<TDalModel>().Except("Id"))
                    ObjectHelper.SetProperty(item, property, ObjectHelper.GetProp(data, property));
                dal.SaveChanges();
            }

            return TranslationEntity.Translate("Item updated successfully.", Language.En, lang);
        }

        protected virtual void ValidateForUpdate(TArgument argument, Language lang)
        {
        }

        public string Delete(Guid id, Language lang) => this.OnDeleting(id, lang);

        protected virtual string OnDeleting(Guid id, Language lang)
        {
            this.ValidateForDelete(id, lang);
            using (var dal = new NewsDalEntity())
            {
                var item = dal.SelectById(id);
                if (item == null)
                {
                    this.ThrowObjectNotFound(lang);
                    return null;
                }

                dal.Delete(item);
            }

            return TranslationEntity.Translate("Item deleted successfully.", Language.En, lang);
        }

        protected virtual void ValidateForDelete(Guid id, Language lang)
        {
        }
    }
}