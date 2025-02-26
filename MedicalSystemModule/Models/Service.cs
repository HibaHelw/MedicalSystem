using MedicalSystemModule.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalSystemModule.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalSystemModule.Models
{
    public class Service : BaseModel
    {
        public IService Transform()
        {
            return new TransformToService()
            {
                Name = Name,
                Description = Description,
                Price = Price,
                DoctorClinicServices = DoctorClinicServices,
                Id = Id,
                CreatedAt = CreatedAt,
                UpdatedAt = UpdatedAt,
                DeletedAt = DeletedAt,
            };
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        [NotMapped]
        public virtual ICollection<IDoctorClinicService> DoctorClinicServices { get; set; }
    }
}
