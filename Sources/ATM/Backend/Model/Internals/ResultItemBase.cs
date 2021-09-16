using System;

namespace Mohammad.Projects.TariqMal.Business.Model.Internals
{
    [Serializable]
    public abstract class ResultItemBase : IResultItem
    {
        public Guid? Id { get; set; }
    }
}