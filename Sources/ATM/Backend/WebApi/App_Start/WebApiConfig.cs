using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Filters;
using Mohammad.Projects.TariqMal.Api.Internals;
using Mohammad.Projects.TariqMal.Business.Model.Internals;
using Mohammad.Projects.TariqMal.Business.Site;
using Mohammad.Web.Api;
using Newtonsoft.Json;
using WebApiContrib.Formatting.Jsonp;

namespace Mohammad.Projects.TariqMal
{
    public class WebApiConfig : WebApiConfigBase<WebApiConfig>
    {
        private WebApiConfig()
        {
        }

        protected override void OnInitializing(HttpConfiguration config)
        {
            var corsAttr = new EnableCorsAttribute("http://api.tariqmal.com", "*", "*");
            config.EnableCors(corsAttr);

            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            var jsonpFormatter = new JsonpMediaTypeFormatter(config.Formatters.JsonFormatter);    
            config.Formatters.Add(jsonpFormatter);

            CurrentContext.Initialize(HttpContext.Current);

            base.OnInitializing(config);
        }

        protected override HttpResponseMessage OnHandedException(HttpActionExecutedContext            actionExecutedContext,
                                                                 (HttpStatusCode Code, string Message) error) =>
            actionExecutedContext.Request.CreateResponse(error.Code, new Result
            {
                IsSucceed = false,
                Message   = error.Message
            });
    }
}