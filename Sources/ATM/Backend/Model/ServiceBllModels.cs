using Mohammad.Projects.TariqMal.Business.Model.Internals;

namespace Mohammad.Projects.TariqMal.Business.Model
{
    public class ServiceResultItem : ResultItemBase
    {
        public string Title { get; set; }
        public string Content  { get; set; }

        public string Summary { get; set; }
    }

    public class ServiceResultSet : ResultSet<ServiceResultItem>
    {
        public string Title { get; set; }
    }

    public class ServiceArgument : ArgumentBase
    {
        public string ArTitle { get; set; }
        public string EnTitle { get; set; }
        public string FaTitle { get; set; }
        public string ArText  { get; set; }
        public string EnText  { get; set; }
        public string FaText  { get; set; }
    }
}