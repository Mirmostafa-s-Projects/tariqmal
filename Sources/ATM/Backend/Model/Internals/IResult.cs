namespace Mohammad.Projects.TariqMal.Business.Model.Internals
{
    public interface IResult
    {

    }

    public sealed class Result : IResult
    {
        public bool IsSucceed { get; set; } = true;
        public string Message { get; set; }
    }
}