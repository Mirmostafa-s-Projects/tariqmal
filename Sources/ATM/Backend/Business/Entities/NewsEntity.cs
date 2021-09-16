using System;
using System.Collections.Generic;
using System.Linq;
using Mohammad.Dynamic;
using Mohammad.Projects.TariqMal.Business.Infrastructure;
using Mohammad.Projects.TariqMal.Business.Internals;
using Mohammad.Projects.TariqMal.Business.Model;
using Mohammad.Projects.TariqMal.DataAccess.DataSources;
using Mohammad.Projects.TariqMal.DataAccess.Infrastructure;

namespace Mohammad.Projects.TariqMal.Business.Entities
{
    public sealed class NewsEntity : AtmManipulationBusinessEntityBase<NewsEntity, New, NewsDalEntity, NewsResultSet,
        NewsResultItem,
        NewsArgument>
    {
        public (NewsResultItem result, string) GetTopNews(Language lang)
        {
            using (var dal = new NewsDalEntity())
            {
                var topNews = dal.Select().FirstOrDefault(n => n.IsTopNews ?? false);
                var result  = topNews?.Convert(lang);

                if (result == null) this.ThrowObjectNotFound(lang);
                return (result, null);
            }
        }

        protected override IEnumerable<New> OnGotAll(IEnumerable<New> dalModels)
        {
            foreach (var dalModel in dalModels)
            {
                dalModel.TextTranslationId = Guid.Empty;
                yield return dalModel;
            }
        }

        protected override New OnGotById(New dalModel)
        {
            dalModel.SummaryTranslationId = Guid.Empty;
            return base.OnGotById(dalModel);
        }

        protected override NewsResultItem ConvertToBusinessModel(New dalModel, Language lang) => dalModel.Convert(lang);

        protected override New ConvertToDataModel(NewsArgument argument) => throw new NotImplementedException();

        protected override (Guid? Result, string Message) OnAdding(NewsArgument argument, Language lang)
        {
            Guid result;

            dynamic ids = new Expando();
            using (var translationDal = new TranslationDalEntity())
            {
                var titleTranslation = new Translation
                {
                    Arabic  = argument.ArTitle,
                    English = argument.EnTitle,
                    Persian = argument.FaTitle
                };
                translationDal.Insert(titleTranslation, false);

                var textTranslation = new Translation
                {
                    Arabic  = argument.ArText,
                    English = argument.EnText,
                    Persian = argument.FaText
                };
                translationDal.Insert(textTranslation, false);
                translationDal.SaveChanges();
                ids.TitleTranslation = titleTranslation.Id;
                ids.TextTranslation  = textTranslation.Id;
            }

            using (var newsDal = new NewsDalEntity())
            {
                var news = new New
                {
                    TitleTranslationId = ids.TitleTranslation.Id,
                    TextTranslationId  = ids.TextTranslation.Id,
                    StartDate          = argument.StartDate,
                    EndDate            = argument.EndDate,
                    IsTopNews          = argument.IsTopNews
                };
                newsDal.Insert(news);
                result = news.Id;
            }

            return (result, TranslationEntity.Translate("Item successfully added.", Language.En, lang));
        }

        protected override string OnUpdating(Guid id, NewsArgument argument, Language lang)
        {
            New news;
            using (var newsDal = new NewsDalEntity())
            {
                news = newsDal.SelectById(id);
                if (news == null)
                {
                    this.ThrowObjectNotFound(lang);
                    return null;
                }

                news.StartDate = argument.StartDate;
                news.EndDate   = argument.EndDate;
                news.IsTopNews = argument.IsTopNews;
                newsDal.SaveChanges();
            }

            using (var translationDal = new TranslationDalEntity())
            {
                var title = translationDal.SelectById(news.TitleTranslationId ?? Guid.Empty);
                if (title != null)
                {
                    title.Arabic  = argument.ArTitle;
                    title.English = argument.EnTitle;
                    title.Persian = argument.FaTitle;
                }

                var text = translationDal.SelectById(news.TextTranslationId ?? Guid.Empty);
                if (text != null)
                {
                    text.Arabic  = argument.ArText;
                    text.English = argument.EnText;
                    text.Persian = argument.FaText;
                }

                translationDal.SaveChanges();
            }

            return TranslationEntity.Translate("Item successfully updated.", Language.En, lang);
        }
    }
}