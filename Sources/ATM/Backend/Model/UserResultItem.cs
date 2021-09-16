using System;
using Mohammad.Projects.TariqMal.Business.Model.Internals;
using Newtonsoft.Json;

namespace Mohammad.Projects.TariqMal.Business.Model
{
    public class UserResultItem : ResultItemBase
    {
        public Guid      CountryId         { get; set; }
        public string    Email             { get; set; }
        public string    FirstName         { get; set; }
        public bool?     IsActivated       { get; set; }
        public bool?     IsAdmin           { get; set; }
        public bool?     IsLocked          { get; set; }
        public string    Language          { get; set; }
        public DateTime? LastLoginTime     { get; set; }
        public string    LastName          { get; set; }
        public string    MobileNumber      { get; set; }
        public string    Password          { get; set; }
        public DateTime  RegisterationTime { get; set; }
        public string    UserName          { get; set; }
    }

    public sealed class OnlineUser
    {
        public string   Email     { get; set; }
        public string   FirstName { get; set; }
        public string   LastName  { get; set; }
        public Guid     SessionId { get; set; }
        public string   Language  { get; set; }
        public DateTime LoginTime { get; set; }
    }

    public class UserResultSet : ResultSet<UserResultItem>
    {
    }

    public class UserArgument : ArgumentBase
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class RegisterUserArgument : UserArgument
    {
        public string Email        { get; set; }
        public string MobileNumber { get; set; }
        public Guid   CountryId    { get; set; }
        public string FirstName    { get; set; }
        public string LastName     { get; set; }
        public string Language     { get; set; }

        [JsonIgnore] public Guid?    Id               { get; set; }
        [JsonIgnore] public DateTime RegistrationDate { get; set; }
    }

    public class CountryResultItem : ResultItemBase
    {
        public string Name { get; set; }
    }

    public class CountryResultSet : ResultSet<CountryResultItem>
    {
    }
}