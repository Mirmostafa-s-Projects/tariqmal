using System;
using System.Collections;
using System.Collections.Generic;

namespace Mohammad.Projects.TariqMal.WebApi.Internals
{
    internal class OnlineUsers : IEnumerable<OnlineUser>
    {
        private readonly List<OnlineUser> _InnerList = new List<OnlineUser>();

        public IEnumerator<OnlineUser> GetEnumerator() => this._InnerList.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        public OnlineUser ByTokenId(string tokenId) => this._InnerList.Find(u => u.TokenId == tokenId);

        public OnlineUser ByUserName(string userName) => this._InnerList.Find(u => u.UserName == userName);

        public OnlineUser Add(string tokenId, Guid databaseId, string userName, string displayName)
        {
            var newUser = new OnlineUser(tokenId, databaseId, userName, displayName);
            this._InnerList.Add(newUser);
            return newUser;
        }

        public void Remove(OnlineUser onlineUser)
        {
            this._InnerList.Remove(onlineUser);
        }
    }
}