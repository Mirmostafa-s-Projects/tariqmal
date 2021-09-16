using System.Collections.Generic;
using Mohammad.Projects.TariqMal.Business.Model.Internals;

namespace Mohammad.Projects.TariqMal.Business.Model
{
    public class TranslationResultItem : ResultItemBase
    {
        public Dictionary<string, string> Translations { get; set; }
    }

    public class TranslationResultSet : ResultSet<TranslationResultItem>
    {
    }

    public class TranslationArgument : ArgumentBase
    {
    }
}