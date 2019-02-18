namespace SoftServe.ITAcademy.BackendDubbingProject.ApplicationCore.Entities
{
    public class Language : BaseEntity
    {
        public string Name { get; set; }

        public Language(string name)
        {
            Name = name;
        }
    }
}