using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Mohammad.BusinessModel.MessageExchange.PrimaryActionResults;
using Mohammad.Projects.TariqMal.Business;
using Mohammad.Projects.TariqMal.Business.Model;
using Mohammad.Projects.TariqMal.Business.Model.HomePage;
using Mohammad.Projects.TariqMal.Business.Site;
using Mohammad.Projects.TariqMal.Business.UI;
using Mohammad.Projects.TariqMal.WebApi.Internals;
using Mohammad.Web.Api.MessageExchange;

namespace Mohammad.Projects.TariqMal.WebApi.Controllers
{
    [RoutePrefix("pages/home")]
    public class HomePageController : AtmApiControllerBase
    {
        [Route("menu")]
        [ResponseType(typeof(ActionResult<IEnumerable<Menu>>))]
        public async Task<IHttpActionResult> GetMainMenu() => await this.OnGetting(new HomePageEntity().GetMainMenu);

        [Route("topnews")]
        [ResponseType(typeof(ActionResult<NewsResult>))]
        public async Task<IHttpActionResult> GetTopNews() => await this.OnGetting(new NewsEntity().GetTopNews);
    }
}