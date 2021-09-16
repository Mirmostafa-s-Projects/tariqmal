using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Mohammad.BusinessModel.MessageExchange.PrimaryActionResults;
using Mohammad.Helpers;
using Mohammad.Projects.TariqMal.Business.Infrastructure;
using Mohammad.Projects.TariqMal.WebApi.Internals;
using Mohammad.Web.Api.MessageExchange;

namespace Mohammad.Projects.TariqMal.WebApi.Controllers
{
    [RoutePrefix("")]
    public class TranslationController : AtmApiControllerBase
    {
        [Route("translate")]
        [ResponseType(typeof(ActionResult<string>))]
        public async Task<IHttpActionResult> Translate(string text)
        {
            return await this.RunAsync(() => TranslationEntity.TranslatePersian(text, this.Language));
        }
    }
}