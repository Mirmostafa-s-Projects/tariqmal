using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Mohammad.Helpers;
using Mohammad.Projects.TariqMal.Business.Infrastructure;
using Mohammad.Projects.TariqMal.Business.Model;
using Mohammad.Web.Api;

namespace Mohammad.Projects.TariqMal.WebApi.Internals
{
    public class AtmApiControllerBase : LibraryApiControllerBase
    {
        protected Language Language
        {
            get
            {
                var result = this.GetHeaders("Language")?.FirstOrDefault();
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
        }

        public string TokenId
        {
            get
            {
                var result = this.GetHeaders("TokenId").FirstOrDefault();
                return result.IsNullOrEmpty() ? string.Empty : result;
            }
        }

        protected virtual IHttpActionResult Run<TResult>(Func<(TResult Result, string Message)> action)
        {
            (int Code, string Message, bool IsSucceed, TResult Result) buffer = (200, null, true, default);
            try
            {
                var (result, message) = action();
                buffer.Result         = result;
                buffer.Message        = message;
                buffer.IsSucceed      = true;
            }
            catch (NotImplementedException ex)
            {
                buffer.Code      = HttpStatusCode.NotImplemented.ToInt();
                buffer.Message   = this.Translate(ex.GetBaseException().Message);
                buffer.IsSucceed = false;
            }

            return this.CreateHttpActionResult(buffer.Code, buffer.Message, buffer.IsSucceed, buffer.Result);
        }

        protected virtual async Task<IHttpActionResult> RunAsync<TResult>(Func<(TResult Result, string Message)> action)
        {
            return await this.Async(() => this.Run(action));
        }

        protected virtual IHttpActionResult Run(Func<string> action)
        {
            (int Code, string Message, bool IsSucceed) buffer = (200, null, true);
            try
            {
                buffer.Message   = action();
                buffer.IsSucceed = true;
            }
            catch (NotImplementedException ex)
            {
                buffer.Code      = HttpStatusCode.NotImplemented.ToInt();
                buffer.Message   = this.Translate(ex.GetBaseException().Message);
                buffer.IsSucceed = false;
            }

            return this.CreateHttpActionResult(buffer.Code, buffer.Message, buffer.IsSucceed);
        }

        protected virtual async Task<IHttpActionResult> RunAsync(Func<string> action)
        {
            return await this.Async(() => this.Run(action));
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
    }
}