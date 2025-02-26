using MedicalSystemModule.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalSystemModule.Interfaces;

namespace MedicalSystemModule.Models
{
    public class Clinic : BaseModel
    {
        public IClinic Transform()
        {
            return new TransformToClinic()
            {
                Name = Name,
                Location = Location,
                DoctorClinicServices = DoctorClinicServices,
                Id = Id,
                CreatedAt = CreatedAt,
                UpdatedAt = UpdatedAt,
                DeletedAt = DeletedAt,
            };
        }
        public string Name { get; set; }
        public string Location { get; set; }
        [NotMapped]
        public virtual ICollection<IDoctorClinicService> DoctorClinicServices { get; set; }
    }
}
