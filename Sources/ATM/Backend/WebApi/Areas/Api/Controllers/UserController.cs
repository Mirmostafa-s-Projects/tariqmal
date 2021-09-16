    using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Mohammad.BusinessModel.MessageExchange.PrimaryActionResults;
using Mohammad.Projects.TariqMal.Api.Internals;
using Mohammad.Projects.TariqMal.Business.Entities;
using Mohammad.Projects.TariqMal.Business.Model;

namespace Mohammad.Projects.TariqMal.Api.Controllers
{
    [RoutePrefix("user")]
    public class UserController : AtmApiControllerBase
    {
        [Route("register")]
        [Route("")]
        [ResponseType(typeof(ActionResult<UserResultItem>))]
        [HttpPost]
        public async Task<IHttpActionResult> RegisterUser(RegisterUserArgument args)
        {
            return await this.RunAsync(() => new UserEntity().Register(args, this.Language));
        }

        [Route("login")]
        [ResponseType(typeof(ActionResult<UserResultItem>))]
        [HttpPost]
        public async Task<IHttpActionResult> LoginUser(UserArgument args)
        {
            return await this.RunAsync(() => new UserEntity().Login(args.UserName, args.Password, this.Language));
        }

        [Route("logout")]
        [Route("logout/{sessionId:guid}")]
        [ResponseType(typeof(ActionResult<Guid>))]
        [HttpPost]
        public async Task<IHttpActionResult> LogoutUser(Guid sessionId)
        {
            return await this.RunAsync(() => new UserEntity().Logout(sessionId, this.Language));
        }

        [Route("countries")]
        [ResponseType(typeof(ActionResult<CountryResultSet>))]
        [HttpGet]
        public async Task<IHttpActionResult> Countries()
        {
            return await this.RunAsync(() => new CountryEntity().GetAll(this.Language));
        }

        [ResponseType(typeof(ActionResult<UserResultItem>))]
        [HttpPost]
        [Route("edit/{id:guid}")]
        [Route("edit")]
        public async Task<IHttpActionResult> EditUser(Guid id, RegisterUserArgument args)
        {
            return await this.RunAsync(() => new UserEntity().Edit(id, args, this.Language));
        }
    }
}