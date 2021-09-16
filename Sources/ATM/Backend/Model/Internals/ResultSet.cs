using System;
using System.Collections.Generic;

namespace Mohammad.Projects.TariqMal.Business.Model.Internals
{
    [Serializable]
    public abstract class ResultSet<TItem> : List<TItem>, IResultSet<TItem>
        where TItem : IModel
    {
        public int    TotalCount { get; set; }
        public bool   IsSucceed  { get; set; } = true;
        public string Message    { get; set; }

        public IEnumerable<TItem> Items => this;
    }
}