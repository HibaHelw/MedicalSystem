using MedicalSystemModule.Interfaces;

namespace MedicalSystemAPI.DTOs.Responses
{
    public class DoctorsResponse : IDoctor
    {
        public DoctorsResponse()
        {
            DoctorClinicServices = new List<IDoctorClinicService>();
        }
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string Name { get; set; }
        public string Specialty { get; set; }
        public IEnumerable<IDoctorClinicService>? DoctorClinicServices { get; set; }

        public static DoctorsResponse Transform(IDoctor doctor, IEnumerable<IDoctorClinicService> value)
        {
            return new DoctorsResponse()
            {
                Id = doctor.Id,
                CreatedAt = doctor.CreatedAt,
                UpdatedAt = doctor.UpdatedAt,
                DeletedAt = doctor.DeletedAt,
                Name = doctor.Name,
                Specialty = doctor.Specialty,
                DoctorClinicServices = value,
            };
        }
    }
}
