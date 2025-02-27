using MedicalSystemAPI.DTOs.Requests;
using MedicalSystemAPI.DTOs.Responses;
using MedicalSystemModule.MedicalContext;
using MedicalSystemModule.Services;
using MedicalSystemModule.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;

namespace MedicalSystemAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServicesController : ControllerBase
    {
        private ServiceServices _service;
        private ClinicServiceServises clinicService;

        public ServicesController(IOptions<AppSettings> appsOptions)
        {
            _service = new ServiceServices(appsOptions);
            clinicService = new ClinicServiceServises(appsOptions);
        }

        #region Services

        [HttpGet]
        //[Authorize]
        [SwaggerOperation(Summary = "Get all services")]
        public async Task<IEnumerable<ServicesResponse>> GetAll()
        {
            return _service.GetAll().Result.Select(c => ServicesResponse.Transform(c));
        }

        [HttpGet("{id}")]
        //[Authorize]
        [SwaggerOperation(Summary = "Get service by id")]
        public ServicesResponse GeTById(Guid id)
        {
            return ServicesResponse.Transform(_service.GetById(id));
        }

        [HttpPost]
        //[Authorize]
        [SwaggerOperation(Summary = "Add service")]
        public Guid Create([FromBody] ServiceRequest service)
        {
            return _service.CreateService(service);
        }

        [HttpPut("{id}")]
        //[Authorize]
        [SwaggerOperation(Summary = "Edit service")]
        public void Update(Guid id, [FromBody] ServiceRequest service)
        {
            _service.UpdateService(id, service);
        }

        [HttpDelete("{id}")]
        //[Authorize]
        [SwaggerOperation(Summary = "Delete service")]
        public void Delete(Guid id)
        {
            _service.DeleteService(id);
        }

        #endregion

        #region clinic Services

        [HttpPost("ClinicService")]
        //[Authorize]
        [SwaggerOperation(Summary = "Add service to clinic")]
        public Guid AddServiceToClinic(Guid clinicId, Guid serviceId, Guid? doctorId)
        {
            return clinicService.AddServiceToClinic(clinicId, serviceId, doctorId);
        }

        [HttpPut("ClinicService/{id}")]
        //[Authorize]
        [SwaggerOperation(Summary = "Edit service in clinic")]
        public void UpdateServiceToClinic(Guid id, Guid clinicId, Guid serviceId, Guid? doctorId)
        {
            clinicService.UpdateServiceClinic(id, clinicId, serviceId, doctorId);
        }

        [HttpDelete("ClinicService/{id}")]
        //[Authorize]
        [SwaggerOperation(Summary = "Delete service from clinic")]
        public void DeleteServiceFromClinic(Guid id)
        {
            clinicService.DeleteClinicService(id);
        }

        #endregion

        #region DoctorServices

        [HttpPut("DoctorService/{id}")]
        //[Authorize]
        [SwaggerOperation(Summary = "Edit service for doctor")]
        public void UpdateDoctorService(Guid id, Guid clinicId, Guid serviceId, Guid doctorId)
        {
            clinicService.UpdateServiceClinic(id, clinicId, serviceId, doctorId);
        }

        [HttpDelete("DoctorService/{id}")]
        //[Authorize]
        [SwaggerOperation(Summary = "delete service from doctor")]
        public void DeleteServiceFromDoctor(Guid id, Guid clinicId, Guid serviceId)
        {
            clinicService.UpdateServiceClinic(id, clinicId, serviceId, null);
        }
        #endregion


    }
}
