using System.Web.Mvc;

namespace Mohammad.Projects.TariqMal.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// </summary>
    /// <seealso cref="T:System.Web.Mvc.Controller" />
    public class HomeController : Controller
    {
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            this.ViewBag.Title = "أمن طريق مال - WebAPI";

            return this.View();
        }
    }
}
