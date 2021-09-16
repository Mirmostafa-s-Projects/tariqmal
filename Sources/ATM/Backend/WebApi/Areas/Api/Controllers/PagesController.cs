using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Mohammad.BusinessModel.MessageExchange.PrimaryActionResults;
using Mohammad.Projects.TariqMal.Api.Internals;
using Mohammad.Projects.TariqMal.Business.Entities;
using Mohammad.Projects.TariqMal.Business.Model;
using Mohammad.Projects.TariqMal.Business.Model.HomePage;
using Mohammad.Projects.TariqMal.Business.Site;

namespace Mohammad.Projects.TariqMal.Api.Controllers
{
    [RoutePrefix("pages")]
    public class PagesController : AtmApiControllerBase
    {
        /// <summary>
        ///     منوی اصلی برنامه را بسته با دسترسی‌های کاربر جاری، بازمی‌گرداند.
        /// </summary>
        /// <returns>منوی اصلی برنامه را بسته با دسترسی‌های کاربر جاری، بازمی‌گرداند.</returns>
        [Route("home/menu")]
        [ResponseType(typeof(ActionResult<IEnumerable<MenuResultItem>>))]
        public async Task<IHttpActionResult> GetMainMenu()
        {
            return await OnGetting(new PagesEntity().GetMainMenu);
        }

        /// <summary>مهمترین خبر روز را بازمی‌گداند.</summary>
        /// <returns>مهمترین خبر روز را بازمی‌گداند.</returns>
        [Route("home/topnews")]
        [ResponseType(typeof(ActionResult<NewsResultSet>))]
        public async Task<IHttpActionResult> GetTopNews()
        {
            return await OnGetting(new NewsEntity().GetTopNews);
        }


        /// <summary>
        ///     اسلایدهای اسلایدر را بازمی‌گرداند
        /// </summary>
        /// <returns>اسلایدهای اسلایدر را بازمی‌گرداند</returns>
        [Route("home/slides")]
        [ResponseType(typeof(ActionResult<IEnumerable<Slider>>))]
        public async Task<IHttpActionResult> GetSlider()
        {
            return await OnGetting(new PagesEntity().GetHomeSliderItems);
        }

        /// <summary>
        ///     لیست سرویسها را جهت نمایش در رابط کاربری آماده میکند.
        /// </summary>
        /// <returns>لیست سرویسها را در قالب قابل ارائه در صفحه اصلی بازمی‌گرداند.</returns>
        [Route("home/services")]
        [ResponseType(typeof(ActionResult<IEnumerable<ServiceResultItem>>))]
        public async Task<IHttpActionResult> GetServices()
        {
            return await OnGetting(new PagesEntity().GetHomeServices);
        }

        /// <summary>
        ///     اخبار سایت را جهت نمایش در رابط کاربری آماده میکند
        /// </summary>
        /// <returns>چهار خبر آخر سایت را در قالب قابل ارائه در صفحه اصلی بازمی‌گرداند</returns>
        [Route("home/news")]
        [ResponseType(typeof(ActionResult<IEnumerable<NewsResultItem>>))]
        public async Task<IHttpActionResult> GetNews()
        {
            return await OnGetting(new PagesEntity().GetHomeNews);
        }

        /// <summary>
        ///     اطلاعات "درباره ما" را بازمی‌گرداند
        /// </summary>
        /// <returns></returns>
        [Route("contact_us")]
        [ResponseType(typeof(ActionResult<IEnumerable<ContactUsResultItem>>))]
        public async Task<IHttpActionResult> GetContactUs()
        {
            return await OnGetting(new PagesEntity().GetContactUs);
        }
    }
}