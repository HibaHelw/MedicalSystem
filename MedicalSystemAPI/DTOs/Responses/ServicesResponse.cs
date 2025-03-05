using MedicalSystemModule.Interfaces;

namespace MedicalSystemAPI.DTOs.Responses
{
    public class ServicesResponse : IService
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public IEnumerable<IDoctorClinicService>? DoctorClinicServices { get; set; }

        public static ServicesResponse Transform(IService service, IEnumerable<IDoctorClinicService> value)
        {
            return new ServicesResponse
            {
                Id = service.Id,
                DoctorClinicServices = value,
                CreatedAt = service.CreatedAt,
                UpdatedAt = service.UpdatedAt,
                DeletedAt = service.DeletedAt,
                Description = service.Description,
                Name = service.Name,
                Price = service.Price,
            };
        }
    }
}
