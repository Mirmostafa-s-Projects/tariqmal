using System.Collections.Generic;

namespace Mohammad.Projects.TariqMal.Business.Model.Internals
{
    public interface IResultSet<out TResultItem> : IEnumerable<TResultItem>, IResult
        where TResultItem : IModel
    {
        int                      TotalCount { get; set; }
        IEnumerable<TResultItem> Items      { get; }
    }
}