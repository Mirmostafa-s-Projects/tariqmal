using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Mohammad.Projects.TariqMal.Api.Internals;
using Mohammad.Projects.TariqMal.Business.Infrastructure;
using Mohammad.Projects.TariqMal.Business.Model;

namespace Mohammad.Projects.TariqMal.Api.Controllers
{
    /// <summary>
    ///     سرویس ترجمه داخلی متون و شناسه‌ها
    /// </summary>
    [RoutePrefix("")]
    public class TranslationController : AtmApiControllerBase
    {
        /// <summary>
        ///     متن داده شده را ترجمه میکند
        /// </summary>
        /// <param name="text">متن برای ترجمه</param>
        /// <param name="fromLanguage">زبان منبع</param>
        /// <returns>ترجمه متن داده شده را بازمی‌گرداند.</returns>
        /// <example>
        ///     یک مثال
        /// </example>
        [Route("translate")]
        [ResponseType(typeof(string))]
        [HttpGet]
        public async Task<string> Translate(string text, string fromLanguage)
        {
            return await this.Async(() => TranslationEntity.Translate(text, ToEnum(fromLanguage), this.Language));
        }

        /// <summary>
        ///     متن داده شده را ترجمه میکند
        /// </summary>
        /// <param name="text">متن برای ترجمه</param>
        /// <param name="fromLanguage">زبان منبع</param>
        /// <returns>ترجمه متن داده شده را بازمی‌گرداند.</returns>
        /// <example>
        ///     یک مثال
        /// </example>
        [Route("translate_all")]
        [ResponseType(typeof(TranslationResultItem))]
        [HttpGet]
        public async Task<TranslationResultItem> TranslateAll(string text, string fromLanguage)
        {
            return await this.Async(() => new TranslationEntity().TranslateToAll(text, ToEnum(fromLanguage)));
        }

    }
}