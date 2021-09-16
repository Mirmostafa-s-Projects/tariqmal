using System;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Mohammad.Helpers;
using Mohammad.Projects.TariqMal.Business.Exceptions;
using Mohammad.Projects.TariqMal.Business.Infrastructure;
using Mohammad.Projects.TariqMal.Business.Internals;
using Mohammad.Projects.TariqMal.Business.Model;
using Mohammad.Projects.TariqMal.DataAccess.DataSources;
using Mohammad.Projects.TariqMal.DataAccess.Infrastructure;
using Mohammad.Web.Api.Exceptions;

namespace Mohammad.Projects.TariqMal.Business.Entities
{
    public class UserEntity : AtmManipulationBusinessEntityBase<UserEntity, User, UserDalEntity,
        UserResultSet, UserResultItem,
        UserArgument>
    {
        protected override UserResultItem ConvertToBusinessModel(User dalModel, Language lang) =>
            new UserResultItem
            {
                CountryId         = dalModel.CountryId,
                Email             = dalModel.Email,
                UserName          = dalModel.UserName,
                FirstName         = dalModel.FirstName,
                Id                = dalModel.Id,
                IsActivated       = dalModel.IsActivated,
                IsAdmin           = dalModel.IsAdmin,
                IsLocked          = dalModel.IsLocked,
                Language          = dalModel.Language,
                LastLoginTime     = dalModel.LastLoginTime,
                LastName          = dalModel.LastName,
                MobileNumber      = dalModel.MobileNumber,
                RegisterationTime = dalModel.RegisterationTime
            };

        protected override User ConvertToDataModel(UserArgument argument) =>
            argument is RegisterUserArgument args
                ? new User
                {
                    CountryId    = args.CountryId,
                    Email        = args.Email,
                    UserName     = args.UserName,
                    FirstName    = args.FirstName,
                    Id           = args.Id ?? Guid.Empty,
                    IsActivated  = true,
                    IsAdmin      = false,
                    IsLocked     = false,
                    Language     = args.Language,
                    LastName     = args.LastName,
                    MobileNumber = args.MobileNumber,
                }
                : new User
                {
                    UserName = argument.UserName
                };

        public (Guid Result, string Message) Register(RegisterUserArgument args, Language language)
        {
            CheckIfNull(args.UserName,     nameof(args.UserName),     language);
            CheckIfNull(args.Password,     nameof(args.Password),     language);
            CheckIfNull(args.FirstName,    nameof(args.FirstName),    language);
            CheckIfNull(args.LastName,     nameof(args.LastName),     language);
            CheckIfNull(args.MobileNumber, nameof(args.MobileNumber), language);
            CheckIfNull(args.Email,        nameof(args.Email),        language);
            CheckIfNull(args.Language,     nameof(args.Language),     language);
            args.RegistrationDate = DateTime.Now;
            if (Exists(args.UserName))
                throw new DuplicateException(TranslationEntity.Translate("User already exists.", Language.En, language));
            args.Id               = Guid.NewGuid();
            var (result, message) = this.Add(args, language);
            if (result.HasValue)
                return this.CreateSuccess(result.Value);
            throw new TariqMalException(message);
        }

        protected override (Guid? Result, string Message) OnAdding(UserArgument argument, Language lang)
        {
            var data = (RegisterUserArgument) argument;
            using (var dal = new UserDalEntity())
            {
                var user = new User
                {
                    Id                = data.Id.Value,
                    CountryId         = data.CountryId,
                    Email             = data.Email,
                    FirstName         = data.FirstName,
                    IsActivated       = true,
                    IsAdmin           = false,
                    IsLocked          = false,
                    Language          = data.Language,
                    LastName          = data.LastName,
                    MobileNumber      = data.MobileNumber,
                    Password          = EncryptPassword(data.Password),
                    RegisterationTime = DateTime.Now,
                    UserName          = data.UserName
                };
                dal.Insert(user);
                return (user.Id, TranslationEntity.Translate("Item added successfully", Language.En, lang));
            }
        }

        // ReSharper disable once StringLiteralTypo
        private static string EncryptPassword(string password) =>
            Encoding.ASCII.GetString(new SHA256Managed().ComputeHash(Encoding.ASCII.GetBytes(password)));

        private static bool Exists(string userName, string password = null)
        {
            using (var dal = new UserDalEntity())
            {
                return dal.Exists(userName, password.IsNullOrEmpty() ? null : EncryptPassword(password));
            }
        }

        public User Authenticate(string userName, string password, Language language)
        {
            using (var dal = new UserDalEntity())
            {
                var user = dal.Select(userName, EncryptPassword(password));
                if (user == null) throw new NotFoundException(TranslationEntity.Translate("User not found.", Language.En, language));
                if (user.IsLocked ?? false)
                    throw new ApiException(TranslationEntity.Translate("User is locked.", Language.En, language), HttpStatusCode.Forbidden);
                if (!(user.IsActivated ?? true))
                    throw new ApiException(TranslationEntity.Translate("User is not activated.", Language.En, language), HttpStatusCode.Forbidden);
                return user;
            }
        }

        public (OnlineUser Result, string Message) Login(string userName, string password, Language language)
        {
            var user = this.Authenticate(userName, password, language);

            var    session = Vars.Sessions.FirstOrDefault(s => s.UserName == userName);
            string message;
            if (session == null)
            {
                session = Vars.Sessions.Add(user);
                message = TranslationEntity.Translate("Logged in successfully.", Language.En, language);
            }
            else
            {
                message = TranslationEntity.Translate("User already is logged in.", Language.En, language);
            }

            return (new OnlineUser
            {
                Email     = user.Email,
                FirstName = session.FirstName,
                Language  = session.Language.ToString(),
                LastName  = session.LastName,
                SessionId = session.SessionId,
                LoginTime = session.LoginTime
            }, message);
        }

        public string Logout(Guid sessionId, Language language)
        {
            var session = Vars.Sessions.FirstOrDefault(s => s.SessionId == sessionId);
            if (session == null)
                throw new TariqMalException(TranslationEntity.Translate("User is not logged in yet.", Language.En, language));
            Vars.Sessions.Remove(sessionId);
            return TranslationEntity.Translate("Logged out successfully.", Language.En, language);
        }

        public (object Result, string Message) Edit(Guid id, RegisterUserArgument args, Language language)
        {
            CheckIfNull(id,                nameof(id),                language);
            CheckIfNull(args.Password,     nameof(args.Password),     language);
            CheckIfNull(args.FirstName,    nameof(args.FirstName),    language);
            CheckIfNull(args.LastName,     nameof(args.LastName),     language);
            CheckIfNull(args.MobileNumber, nameof(args.MobileNumber), language);
            CheckIfNull(args.Email,        nameof(args.Email),        language);
            CheckIfNull(args.Language,     nameof(args.Language),     language);
            args.RegistrationDate = DateTime.Now;
            if (!Exists(args.UserName))
                throw new NotFoundException(TranslationEntity.Translate("User already exists.", Language.En, language));
            var (user, getMessage) = this.GetById(id, language);
            if (user == null)
                throw new TariqMalException(getMessage);
            args.UserName = user.UserName;
            var message = this.Update(id, args, language);
            if (!message.IsNullOrEmpty())
                return this.CreateSuccess();
            throw new TariqMalException(message);
        }

        private static void CheckIfNull(string variable, string name, Language language)
        {
            if (variable.IsNullOrEmpty())
                throw new ArgumentException(
                    $"{TranslationEntity.Translate(name, Language.En, language)} {TranslationEntity.Translate("cannot be null.", Language.En, language)}");
        }

        private static void CheckIfNull(Guid variable, string name, Language language)
        {
            if (variable == Guid.Empty)
                throw new ArgumentException(
                    $"{TranslationEntity.Translate(name, Language.En, language)} {TranslationEntity.Translate("cannot be null.", Language.En, language)}");
        }

        private static void CheckIfNull(Guid? variable, string name, Language language)
        {
            if (variable == null || variable == Guid.Empty)
                throw new ArgumentException(
                    $"{TranslationEntity.Translate(name, Language.En, language)} {TranslationEntity.Translate("cannot be null.", Language.En, language)}");
        }
    }

    public sealed class CountryEntity : AtmBusinessEntityBase<CountryEntity>
    {
        public (CountryResultSet Result, string Message) GetAll(Language language)
        {
            var result = new CountryResultSet();
            using (var dal = new CountryDalEntity())
            {
                result.AddRange(dal.Select().Select(country => new CountryResultItem
                {
                    Id   = country.Id,
                    Name = TranslationEntity.Translate(country.NameTranslationId, language)
                }));
            }

            return (result, "");
        }
    }
}