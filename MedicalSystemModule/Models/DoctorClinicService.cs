using MedicalSystemModule.DTO;
using MedicalSystemModule.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MedicalSystemModule.Models
{
    public class DoctorClinicService : BaseModel
    {
        public IDoctorClinicService Transform()
        {
            return new TransformToDoctorClinicService()
            {
                CreatedAt = CreatedAt,
                UpdatedAt = UpdatedAt,
                DeletedAt = DeletedAt,
                DocId = DocId,
                ClinicId = ClinicId,
                ServiceId = ServiceId,
                Id = Id,
                Clinic = Clinic?.Transform(),
                Service = Service?.Transform(),
            };
        }
        [Column("DocId")]
        public Guid? DocId { get; set; }
        //public Doctor Doctor { get; set; }
        public Guid ClinicId { get; set; }
        public Clinic? Clinic { get; set; }
        public Guid ServiceId { get; set; }
        public Service? Service { get; set; }
    }
}
