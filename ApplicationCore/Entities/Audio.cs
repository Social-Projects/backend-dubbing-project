namespace SoftServe.ITAcademy.BackendDubbingProject.ApplicationCore.Entities
{
    public class Audio : BaseEntity
    {
        public string FileName { get; set; }

        public int Duration { get; set; }

        public Speech Speech { get; set; }

        public Language Language { get; set; }

        public Audio(string fileName, int languageId, int speechId, Language language, Speech speech, int duration)
        {
            FileName = fileName;
            Duration = duration;
            Speech = speech;
            Language = language;
        }
    }
}