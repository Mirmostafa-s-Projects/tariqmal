using System.Web;

namespace Mohammad.Projects.TariqMal.Business.Site
{
    public static class CurrentContext
    {
        public static HttpContext HttpContext { get; private set; }

        public static void Initialize(HttpContext httpContext)
        {
            HttpContext = httpContext;
        }
    }
}