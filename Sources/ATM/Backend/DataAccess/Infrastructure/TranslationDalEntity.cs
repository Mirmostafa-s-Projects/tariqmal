using System;
using System.Linq;
using Mohammad.Projects.TariqMal.DataAccess.DataSources;
using Mohammad.Projects.TariqMal.DataAccess.Internals;

namespace Mohammad.Projects.TariqMal.DataAccess.Infrastructure
{
    public sealed class TranslationDalEntity : DalQueryBase<TranslationDalEntity, Translation>
    {
        public string TranslateById(Guid            id,   string language) => this.Db.Translate(id, language);
        public string TranslateByPersianText(string text, string language) => this.Db.TranslatePersianMesssage(text, language);
        public string TranslateByEnglishText(string text, string language) => this.Db.TranslateEnglishMesssage(text, language);
        public string TranslateByArabicText(string  text, string language) => this.Db.TranslateArabicMesssage(text, language);

        public override Translation SelectById(Guid id)
        {
            var q = from trans in this.Select()
                    where trans.Id == id
                    select trans;
            return q.FirstOrDefault();
        }
    }
}