using System.ComponentModel.DataAnnotations;

namespace SoftServe.ITAcademy.BackendDubbingProject.Models
{
    public class Language : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            return (Id == (obj as Language).Id) && (Name == (obj as Language).Name);
        }
    }
}
