using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Mohammad.BusinessModel.MessageExchange.PrimaryActionResults;
using Mohammad.Projects.TariqMal.Api.Internals;
using Mohammad.Projects.TariqMal.Business.Entities;
using Mohammad.Projects.TariqMal.Business.Model;
using Mohammad.Projects.TariqMal.Business.Site;

namespace Mohammad.Projects.TariqMal.Api.Controllers
{
    [RoutePrefix("service")]
    public class ServiceController : AtmApiControllerBase
    {
        /// <summary>
        ///     لیست سرویسها را جهت نمایش در رابط کاربری آماده میکند.
        /// </summary>
        /// <returns>لیست سرویسها را در قالب قابل ارائه در صفحه اصلی بازمی‌گرداند.</returns>
        [Route("")]
        [ResponseType(typeof(ActionResult<IEnumerable<ServiceResultItem>>))]
        public async Task<IHttpActionResult> Get()
        {
            return await OnGetting(null, new ServiceEntity().Get);
        }

        /// <summary>
        ///     یک سرویس را با شناسه سرویس، بازمی‌گرداند
        /// </summary>
        /// <param name="id">شناسه سرویس</param>
        /// <returns>یک سرویس را با شناسه سرویس، بازمی‌گرداند</returns>
        [Route("{Id:guid}")]
        [ResponseType(typeof(ActionResult<ServiceResultItem>))]
        public async Task<IHttpActionResult> Get(Guid id)
        {
            return await OnGettingById(new ServiceEntity().GetById, id);
        }
    }
}