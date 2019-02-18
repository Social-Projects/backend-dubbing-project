using System.Collections.Generic;

namespace SoftServe.ITAcademy.BackendDubbingProject.ApplicationCore.Entities
{
    public class Performance : BaseEntity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public ICollection<Speech> Speeches { get; set; }

        public Performance(string title, string description, ICollection<Speech> speeches)
        {
            Title = title;
            Description = description;
            Speeches = speeches;
        }
    }
}