namespace Mohammad.Projects.TariqMal.Business.Model
{
    public enum Language
    {
        None,
        Ar,
        En,
        Fa
    }

    public class Pagination
    {
        public const int DEFAULT_TAKE = 10;

        public Pagination()
        {
        }

        public Pagination(int? skip, int? take)
        {
            this.Skip = skip;
            this.Take = take;
        }

        public int? Skip { get; set; } = 0;
        public int? Take { get; set; } = DEFAULT_TAKE;
    }
}