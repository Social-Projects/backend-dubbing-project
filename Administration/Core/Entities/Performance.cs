using System.Collections.Generic;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities
{
    public class Performance : BaseEntity
    {
        public string Title { get; set; }

        public ICollection<Speech> Speeches { get; set; }
    }
}