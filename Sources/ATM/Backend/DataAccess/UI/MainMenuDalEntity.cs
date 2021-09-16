using System;
using System.Linq;
using Mohammad.Projects.TariqMal.DataAccess.DataSources;
using Mohammad.Projects.TariqMal.DataAccess.Internals;

namespace Mohammad.Projects.TariqMal.DataAccess.UI
{
    public sealed class MainMenuDalEntity : DalQueryBase<MainMenuDalEntity, HomePageMainMenu>
    {
        public override HomePageMainMenu SelectById(Guid id)
        {
            var q = from homePageMainMenu in this.Select()
                    where homePageMainMenu.Id == id
                    select homePageMainMenu;
            return q.FirstOrDefault();
        }
    }
}