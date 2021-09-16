using System;
using System.Linq;
using Mohammad.Helpers;
using Mohammad.Projects.TariqMal.DataAccess.DataSources;
using Mohammad.Projects.TariqMal.DataAccess.Internals;

namespace Mohammad.Projects.TariqMal.DataAccess.Infrastructure
{
    public sealed class UserDalEntity : DalQueryBase<UserDalEntity, User>
    {
        public override User SelectById(Guid id) => (from user in this.Select() where user.Id == id select user).FirstOrDefault();

        public bool Exists(string userName, string password)
        {
            var q = from user in this.Select() where user.UserName == userName select user;
            if (!password.IsNullOrEmpty())
                q = from user in q
                    where user.Password == password
                    select user;
            return q.Any();
        }

        public User Select(string userName, string password)
        {
            var q = from user in this.Select() where user.UserName == userName select user;
            if (!password.IsNullOrEmpty())
                q = from user in q
                    where user.Password == password
                    select user;
            return q.FirstOrDefault();
        }
    }

    public sealed class CountryDalEntity : DalQueryBase<CountryDalEntity, Country>
    {
        public override Country SelectById(Guid id) => (from country in this.Select() where country.Id == id select country).FirstOrDefault();
    }
}