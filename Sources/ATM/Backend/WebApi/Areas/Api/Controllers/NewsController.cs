using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using Mohammad.BusinessModel.MessageExchange.PrimaryActionResults;
using Mohammad.Projects.TariqMal.Api.Internals;
using Mohammad.Projects.TariqMal.Business.Entities;
using Mohammad.Projects.TariqMal.Business.Model;

namespace Mohammad.Projects.TariqMal.Api.Controllers
{
    [RoutePrefix("news")]
    [EnableCors("*", "*", "*")]
    public class NewsController : AtmApiControllerBase
    {
        /// <summary>مهمترین خبر روز را بازمی‌گداند.</summary>
        /// <returns>مهمترین خبر روز را بازمی‌گداند.</returns>
        [Route("top")]
        [ResponseType(typeof(ActionResult<NewsResultSet>))]
        public async Task<IHttpActionResult> GetTopNews() => await this.OnGetting(new NewsEntity().GetTopNews);


        /// <summary>
        ///     تمام اخبار سایت را صفحه به صفحه بازمیکرداند
        /// </summary>
        /// <param name="pagination">صفحه مورد نظر از اخبار سایت</param>
        /// <returns>تمام اخبار سایت را صفحه به صفحه بازمیکرداند</returns>
        [ResponseType(typeof(ActionResult<NewsResultSet>))]
        public async Task<IHttpActionResult> Get(Pagination pagination) => await this.OnGetting(pagination, new NewsEntity().Get);


        /// <summary>
        ///     تمام اخبار سایت را بازمیکرداند
        /// </summary>
        /// <returns>تمام اخبار سایت را بازمیکرداند</returns>
        [Route("")]
        [ResponseType(typeof(ActionResult<NewsResultSet>))]
        [EnableCors("*", "*", "*")]
        public async Task<IHttpActionResult> Get() => await this.OnGetting(new Pagination(), new NewsEntity().Get);


        /// <summary>
        ///     یک خبر را با شناسه خبر، بازمی‌گرداند
        /// </summary>
        /// <param name="id">شناسه خبر</param>
        /// <returns>یک خبر را با شناسه خبر، بازمی‌گرداند</returns>
        [Route("{Id:guid}")]
        [ResponseType(typeof(ActionResult<NewsResultItem>))]
        public async Task<IHttpActionResult> Get(Guid id) => await this.OnGettingById(new NewsEntity().GetById, id);


        /// <summary>
        ///     یک خبر جدید را ثبت میکند.
        /// </summary>
        /// <param name="value">مشخصات خبر</param>
        /// <returns>شناسه خبر را بازمی‌گرداند.</returns>
        [Route("")]
        public async Task<IHttpActionResult> Post(NewsArgument value) => await this.OnPosting(new NewsEntity().Add, value);


        /// <summary>
        ///     یک خبر را که قبلا ثبت شده، ویرایش میکند.
        /// </summary>
        /// <param name="id">شناسه خبر</param>
        /// <param name="value">مشخصات جدید خبر</param>
        /// <returns>پیام نتیجه ویرایش خبر را بازمی‌گرداند</returns>
        [Route("")]
        public async Task<IHttpActionResult> Put(Guid id, NewsArgument value) => await this.OnPutting(new NewsEntity().Update, id, value);


        /// <summary>
        ///     یک خبر را که قبلا ثبت شده، حذف میکند
        /// </summary>
        /// <param name="id">شناسه خبر</param>
        /// <returns>پیام نتیجه حذف خبر را بازمی‌گرداند</returns>
        [Route("{Id:guid}")]
        public async Task<IHttpActionResult> Delete(Guid id) => await this.OnDeleting(new NewsEntity().Delete, id);
    }
}