using System;
using System.Collections.Generic;
using Mohammad.Helpers;
using Mohammad.Projects.TariqMal.Business.Internals;
using Mohammad.Projects.TariqMal.Business.Model;
using Mohammad.Projects.TariqMal.DataAccess.DataSources;
using Mohammad.Projects.TariqMal.DataAccess.Infrastructure;

namespace Mohammad.Projects.TariqMal.Business.Infrastructure
{
    public sealed class TranslationEntity : AtmManipulationBusinessEntityBase<TranslationEntity, Translation,
        TranslationDalEntity,
        TranslationResultSet,
        TranslationResultItem, TranslationArgument>
    {
        private static readonly TranslationDalEntity _DalEntity = new TranslationDalEntity();

        public static string Translate(Guid id, Language language) => _DalEntity.TranslateById(id, language.Correct().ToString());

        public static string Translate(string text, Language source, Language target)
        {
            string result;
            switch (source.Correct())
            {
                case Language.En:
                    result = TranslateEnglish(text, target);
                    if (!result.IsNullOrEmpty())
                        return result;
                    InsertToTable();
                    break;
                case Language.Fa:
                    result = TranslatePersian(text, target);
                    if (!result.IsNullOrEmpty())
                        return result;
                    InsertToTable();
                    break;
                case Language.Ar:
                case Language.None:
                default:
                    result = TranslateArabic(text, target);
                    if (!result.IsNullOrEmpty())
                        return result;
                    InsertToTable();
                    break;
            }

            return text; //BingService.Translate(text, source.ToLanguageName(), target.ToLanguageName());

            void InsertToTable()
            {
                using (var dal = new TranslationDalEntity())
                {
                    dal.Insert(new Translation {Id = Guid.NewGuid(), Arabic = text, English = text, Persian = text});
                }
            }
        }

        private static string TranslatePersian(string text, Language language) =>
            _DalEntity.TranslateByPersianText(text, language.Correct().ToString()) ?? text;

        private static string TranslateEnglish(string text, Language language) =>
            _DalEntity.TranslateByEnglishText(text, language.Correct().ToString()) ?? text;

        private static string TranslateArabic(string text, Language language) =>
            _DalEntity.TranslateByArabicText(text, language.Correct().ToString()) ?? text;

        protected override TranslationResultItem ConvertToBusinessModel(Translation dalModel, Language lang) => throw new NotImplementedException();

        protected override Translation ConvertToDataModel(TranslationArgument argument) => throw new NotImplementedException();

        public TranslationResultItem TranslateToAll(string text, Language source)
        {
            var result = new TranslationResultItem {Translations = new Dictionary<string, string>(), Id = null};
            switch (source)
            {
                case Language.En:
                    result.Translations.Add("Arabic",  Translate(text, source, Language.Ar));
                    result.Translations.Add("Persian", Translate(text, source, Language.Fa));
                    break;
                case Language.Fa:
                    result.Translations.Add("Arabic",  Translate(text, source, Language.Ar));
                    result.Translations.Add("English", Translate(text, source, Language.En));
                    break;
                case Language.Ar:
                case Language.None:
                default:
                    result.Translations.Add("English", Translate(text, source, Language.En));
                    result.Translations.Add("Persian", Translate(text, source, Language.Fa));
                    break;
            }

            return result;
        }
    }
}