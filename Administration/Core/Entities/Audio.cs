namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities
{
    public class Audio : BaseEntity
    {
        public string FileName { get; set; }

        public int Duration { get; set; }

        public Speech Speech { get; set; }

        public Language Language { get; set; }
    }
}