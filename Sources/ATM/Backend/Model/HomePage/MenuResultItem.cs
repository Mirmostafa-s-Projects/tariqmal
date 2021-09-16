using System;
using System.Collections.Generic;
using Mohammad.Projects.TariqMal.Business.Model.Internals;

namespace Mohammad.Projects.TariqMal.Business.Model.HomePage
{
    public class MenuResultItem : ResultItemBase
    {
        public string               Title    { get; set; }
        public List<MenuResultItem> Children { get; } = new List<MenuResultItem>();
        public string               MenuType { get; set; }
        public Guid?              LinkId      { get; set; }
    }

    public class Slider : ResultItemBase
    {
        public string Title   { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string Image   { get; set; }
    }
}