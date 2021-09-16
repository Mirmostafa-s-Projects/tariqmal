using System.Collections.Generic;
using System.Linq;
using Mohammad.Projects.TariqMal.Business.Model;

namespace Mohammad.Projects.TariqMal.Business.Internals
{
    internal static class Extensions
    {
        internal static (int Skip, int Take) Get(this Pagination pagination) => (pagination?.Skip ?? 0, pagination?.Take ?? Pagination.DEFAULT_TAKE);

        internal static IEnumerable<TItem> Page<TItem>(this IEnumerable<TItem> data, Pagination pagination)
        {
            var (skip, take) = pagination.Get();
            return data.Skip(skip).Take(take);
        }

        public static Language Correct(this Language language)
        {
            switch (language)
            {
                case Language.En:
                    return Language.En;
                case Language.Fa:
                    return Language.Fa;
                case Language.Ar:
                    return Language.Ar;
                case Language.None:
                default:
                    return Language.Ar;
            }
        }

        public static Language ToLanguage(string language)
        {
            switch (language)
            {
                case "En":
                    return Language.En;
                case "Fa":
                    return Language.Fa;
                case "Ar":
                    return Language.Ar;
                default:
                    return Language.Ar;
            }
        }

        public static string ToLanguageName(this Language language)
        {
            switch (language)
            {
                case Language.En:
                    return "english";
                case Language.Fa:
                    return "persian";
                case Language.Ar:
                    return "arabic";
                case Language.None:
                default:
                    return "arabic";
            }
        }

        public static string ToUrl(string relativePath)
        {
            var domainName = "http://api.tariqmal.com";
            return relativePath.Replace("~", domainName);
        }
    }
}