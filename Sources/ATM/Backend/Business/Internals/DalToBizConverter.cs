using System;
using Mohammad.Projects.TariqMal.Business.Infrastructure;
using Mohammad.Projects.TariqMal.Business.Model;
using Mohammad.Projects.TariqMal.Business.Model.HomePage;
using Mohammad.Projects.TariqMal.DataAccess.DataSources;

namespace Mohammad.Projects.TariqMal.Business.Internals
{
    internal static class DalToBizConverter
    {
        public static MenuResultItem Convert(this HomePageMainMenu mainMenu, Language lang) =>
            new MenuResultItem
            {
                Id       = mainMenu.Id,
                Title    = TranslationEntity.Translate(mainMenu.TitleTranslationId, lang),
                MenuType = mainMenu.MenuType,
                LinkId   = mainMenu.LinkId
            };

        public static NewsResultItem Convert(this New news, Language lang) =>
            news == null
                ? null
                : new NewsResultItem
                {
                    Id        = news.Id,
                    Title     = TranslationEntity.Translate(news.TitleTranslationId   ?? Guid.Empty, lang),
                    Content   = TranslationEntity.Translate(news.TextTranslationId    ?? Guid.Empty, lang),
                    Summary   = TranslationEntity.Translate(news.SummaryTranslationId ?? Guid.Empty, lang),
                    Thumbnail = Extensions.ToUrl("~/Images/news-default.png")
                };

        public static ServiceResultItem Convert(this Service service, Language lang) =>
            service == null
                ? null
                : new ServiceResultItem
                {
                    Id      = service.Id,
                    Title   = TranslationEntity.Translate(service.TitleTransaltionId,                lang),
                    Content = TranslationEntity.Translate(service.TextTraslationId,                  lang),
                    Summary = TranslationEntity.Translate(service.SummaryTraslationId ?? Guid.Empty, lang)
                };
    }
}