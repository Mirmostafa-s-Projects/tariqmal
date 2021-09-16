using System;

namespace Mohammad.Projects.TariqMal.Api.Internals
{
    internal class OnlineUser
    {
        public OnlineUser(string tokenId, Guid databaseId, string userName, string displayName)
        {
            this.TokenId     = tokenId ?? throw new ArgumentNullException(nameof(tokenId));
            this.DatabaseId  = databaseId;
            this.UserName    = userName    ?? throw new ArgumentNullException(nameof(userName));
            this.DisplayName = displayName ?? throw new ArgumentNullException(nameof(displayName));
            this.LogInTime   = DateTime.Now;
        }

        public Guid     DatabaseId  { get; }
        public string   TokenId     { get; }
        public DateTime LogInTime   { get; }
        public string   UserName    { get; }
        public string   DisplayName { get; }
    }
}