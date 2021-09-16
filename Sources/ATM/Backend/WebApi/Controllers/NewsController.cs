using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Mohammad.BusinessModel.MessageExchange.PrimaryActionResults;
using Mohammad.Projects.TariqMal.Business.Model;
using Mohammad.Projects.TariqMal.Business.Site;
using Mohammad.Projects.TariqMal.WebApi.Internals;

namespace Mohammad.Projects.TariqMal.WebApi.Controllers
{
    [RoutePrefix("news")]
    public class NewsController : AtmApiControllerBase
    {
        [Route("top")]
        [ResponseType(typeof(ActionResult<NewsResult>))]
        public async Task<IHttpActionResult> GetTopNews() => await this.OnGetting(new NewsEntity().GetTopNews);


        [ResponseType(typeof(ActionResult<NewsResult>))]
        public async Task<IHttpActionResult> Get(Pagination pagination) =>
            await this.OnGetting(pagination, new NewsEntity().Get);


        [Route("")]
        [ResponseType(typeof(ActionResult<NewsResult>))]
        public async Task<IHttpActionResult> Get() => await this.OnGetting(new Pagination(), new NewsEntity().Get);


        [Route("{Id:guid}")]
        [ResponseType(typeof(ActionResult<NewsResultItem>))]
        public async Task<IHttpActionResult> Get(Guid id) => await this.OnGettingById(new NewsEntity().GetById, id);


        [Route("")]
        public async Task<IHttpActionResult> Post(NewsArgument value) => await this.OnPosting(new NewsEntity().Add, value);


        [Route("")]
        public async Task<IHttpActionResult> Put(Guid id, NewsArgument value) =>
            await this.OnPutting(new NewsEntity().Update, id, value);


        [Route("{Id:guid}")]
        public async Task<IHttpActionResult> Delete(Guid id) => await this.OnDeleting(new NewsEntity().Delete, id);
    }
}