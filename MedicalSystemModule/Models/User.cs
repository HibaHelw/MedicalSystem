using MedicalSystemModule.DTO;
using MedicalSystemModule.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalSystemModule.Models
{
    public class User : BaseModel
    {
        public IUser Transform()
        {
            return new TransformToUSer()
            {
                Id = Id,
                CreatedAt = CreatedAt,
                UpdatedAt = UpdatedAt,
                DeletedAt = DeletedAt,
                Username = Username,
                Password = Password,
                Email = Email,
            };
        }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
