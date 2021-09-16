using System;
using Mohammad.Projects.TariqMal.Business.Model;

namespace Mohammad.Projects.TariqMal.Business.Entities
{
    public sealed class UserSession
    {
        public UserSession(string userName, Guid dbId, Language language, string firstName, string lastName)
        {
            this.UserName  = userName;
            this.DbId      = dbId;
            this.Language  = language;
            this.FirstName = firstName;
            this.LastName  = lastName;
            this.SessionId = Guid.NewGuid();
            this.LoginTime = DateTime.Now;
        }

        public string   UserName  { get; }
        public Guid     DbId      { get; }
        public Guid     SessionId { get; }
        public DateTime LoginTime { get; }
        public Language Language  { get; }
        public string   FirstName { get; }
        public string   LastName  { get; }
    }
}