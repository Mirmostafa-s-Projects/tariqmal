using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Mohammad.Helpers;
using Mohammad.Projects.TariqMal.Business.Infrastructure;
using Mohammad.Projects.TariqMal.Business.Model;
using Mohammad.Web.Api;

namespace Mohammad.Projects.TariqMal.Api.Internals
{
    //[CorsPolicy]
    [EnableCors("http://api.tariqmal.com", "*", "*")]
    public class AtmApiControllerBase : LibraryApiControllerBase
    {
        protected Language Language
        {
            get
            {
                var result = this.GetHeaders("Language")?.FirstOrDefault();
                return ToEnum(result);
            }
        }

        protected string TokenId
        {
            get
            {
                var result = this.GetHeaders("TokenId").FirstOrDefault();
                return result.IsNullOrEmpty() ? string.Empty : result;
            }
        }

        protected static Language ToEnum(string result)
        {
            if (result.IsNullOrEmpty()) return Language.None;

            switch (result?.ToLower())
            {
                case "ar":
                    return Language.Ar;
                case "en":
                    return Language.En;
                case "fa":
                    return Language.Fa;
                default:
                    return Language.None;
            }
        }


        private string Translate(string message)
        {
            switch (message)
            {
                case "The method or operation is not implemented.":
                    return "متد هنوز تمام نشده است.";
                default:
                    return TranslationEntity.Translate(message, Language.Fa, this.Language);
            }
        }

        protected virtual async Task<IHttpActionResult> OnGettingById<TResult>(
            Func<Guid, Language, (TResult Result, string Message)> businessMethod, Guid id)
        {
            return await this.RunAsync(() => businessMethod(id, this.Language));
        }

        protected virtual async Task<IHttpActionResult> OnGetting<TResult>(
            Func<Language, (TResult Result, string Message)> businessMethod)
        {
            return await this.RunAsync(() => businessMethod(this.Language));
        }

        protected virtual async Task<IHttpActionResult> OnGetting<TResult>(
            Pagination pagination, Func<Language, Pagination, (TResult Result, string Message)> businessMethod)
        {
            return await this.RunAsync(() => businessMethod(this.Language, pagination));
        }

        protected virtual async Task<IHttpActionResult> OnPosting<TArgument>(
            Func<TArgument, Language, (Guid? Result, string message)> businessMethod, TArgument argument)
        {
            return await this.RunAsync(() => businessMethod(argument, this.Language));
        }

        protected virtual async Task<IHttpActionResult> OnPutting<TArgument>(
            Func<Guid, TArgument, Language, string> businessMethod, Guid id, TArgument argument)
        {
            return await this.RunAsync(() => businessMethod(id, argument, this.Language));
        }

        protected virtual async Task<IHttpActionResult> OnDeleting(
            Func<Guid, Language, string> businessMethod, Guid id)
        {
            return await this.RunAsync(() => businessMethod(id, this.Language));
        }

        public HttpResponseMessage Options() => new HttpResponseMessage {StatusCode = HttpStatusCode.OK};
    }

    //[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    //public class MyCorsPolicyAttribute : Attribute, ICorsPolicyProvider
    //{
    //    private readonly CorsPolicy _policy;

    //    public MyCorsPolicyAttribute()
    //    {
    //        // Create a CORS policy.
    //        this._policy = new CorsPolicy
    //        {
    //            AllowAnyMethod = true,
    //            AllowAnyHeader = true
    //        };

    //        // Add allowed origins.
    //        this._policy.Origins.Add("http://myclient.azurewebsites.net");
    //        this._policy.Origins.Add("http://www.contoso.com");
    //    }

    //    public Task<CorsPolicy> GetCorsPolicyAsync(HttpRequestMessage request, CancellationToken cancellationToken) => throw new NotImplementedException();

    //    public Task<CorsPolicy> GetCorsPolicyAsync(HttpRequestMessage request) => Task.FromResult(this._policy);
    //}

    //public class CorsPolicyFactory : ICorsPolicyProviderFactory
    //{
    //    private readonly ICorsPolicyProvider _provider = new MyCorsPolicyAttribute();

    //    public ICorsPolicyProvider GetCorsPolicyProvider(HttpRequestMessage request) => this._provider;
    //}
}