using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace SoftServe.ITAcademy.BackendDubbingProject.Models
{
public class Admin : BaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }
    }
}