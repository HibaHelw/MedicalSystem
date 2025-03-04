using MedicalSystemModule.Interfaces;

namespace MedicalSystemAPI.DTOs.Responses
{
    public class DoctorClinicServicesResponse : IDoctorClinicService
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Guid? DoctorId { get; set; }
        public IDoctor? Doctor { get; set; }
        public Guid ClinicId { get; set; }
        public IClinic Clinic { get; set; }
        public Guid ServiceId { get; set; }
        public IService Service { get; set; }

        public static DoctorClinicServicesResponse Transform(IDoctorClinicService value)
        {
            return new DoctorClinicServicesResponse
            {
                CreatedAt = value.CreatedAt,
                UpdatedAt = value.UpdatedAt,
                DeletedAt = value.DeletedAt,
                Id = value.Id,
                ServiceId = value.ServiceId,
                ClinicId = value.ClinicId,
                DoctorId = value.DoctorId,
            };
        }
    }
}
