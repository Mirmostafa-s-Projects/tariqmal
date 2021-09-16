using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mohammad.Projects.TariqMal.Business.Internals;
using Mohammad.Projects.TariqMal.DataAccess.DataSources;

namespace Mohammad.Projects.TariqMal.Business.Entities
{
    public sealed class UserSessionList : IEnumerable<UserSession>
    {
        private readonly List<UserSession> InnerList = new List<UserSession>();

        public UserSession this[Guid sessionId]
        {
            get { return this.InnerList.FirstOrDefault(s => s.SessionId == sessionId); }
        }

        public IEnumerator<UserSession> GetEnumerator() => this.InnerList.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        public UserSession Add(User user)
        {
            var result = new UserSession(user.UserName, user.Id, Extensions.ToLanguage(user.Language), user.FirstName, user.LastName);
            this.InnerList.Add(result);
            return result;
        }

        public void Remove(Guid sessionId)
        {
            var session = this[sessionId];
            if (session != null)
                this.InnerList.Remove(session);
        }
    }

    public static class Vars
    {
        public static UserSessionList Sessions { get; } = new UserSessionList();
    }
}