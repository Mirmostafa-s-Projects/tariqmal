using System;
using Mohammad.Projects.TariqMal.Business.Model.Internals;

namespace Mohammad.Projects.TariqMal.Business.Model
{
    public class NewsResultItem : ResultItemBase
    {
        public string Title     { get; set; }
        public string Thumbnail { get; set; }

        public string Content { get; set; }

        public string Summary { get; set; }
    }

    public class NewsResultSet : ResultSet<NewsResultItem>
    {
        public string Title { get; set; }
    }

    public class NewsArgument : ArgumentBase
    {
        public string    ArTitle   { get; set; }
        public string    EnTitle   { get; set; }
        public string    FaTitle   { get; set; }
        public string    ArText    { get; set; }
        public string    EnText    { get; set; }
        public string    FaText    { get; set; }
        public DateTime? StartDate { get; set; } = null;
        public DateTime? EndDate   { get; set; } = null;
        public bool?     IsTopNews { get; set; } = null;
    }
}