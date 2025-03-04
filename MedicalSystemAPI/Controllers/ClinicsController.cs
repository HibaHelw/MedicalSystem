using MedicalSystemAPI.DTOs.Requests;
using MedicalSystemAPI.DTOs.Responses;
using MedicalSystemModule.Interfaces.Services;
using MedicalSystemModule.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MedicalSystemAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClinicsController : ControllerBase
    {

        private IClinicServices service;

        public ClinicsController(IClinicServices clinicSer)
        {
            service = clinicSer;
        }

        [HttpGet]
        [Authorize]
        [SwaggerOperation(Summary = "Get all clinics")]
        public async Task<IEnumerable<ClinicsResponse>> GetAll()
        {
            return service.GetAll().Result.Select(c => ClinicsResponse.Transform(c, service.GetClinicServices(c.Id)));
        }

        [HttpGet("{id}")]
        [Authorize]
        [SwaggerOperation(Summary = "Get clinic by id")]
        public ClinicsResponse GeTById(Guid id)
        {
            return ClinicsResponse.Transform(service.GetById(id), service.GetClinicServices(id));
        }

        [HttpPost]
        [Authorize]
        [SwaggerOperation(Summary = "Add clinic")]
        public Guid Create([FromBody] ClinicRequest clinic)
        {
            return service.CreateClinic(clinic);
        }

        [HttpPut("{id}")]
        [Authorize]
        [SwaggerOperation(Summary = "Edit clinic")]
        public void Update(Guid id, [FromBody] ClinicRequest clinic)
        {
            service.UpdateClinic(id, clinic);
        }

        [HttpDelete("{id}")]
        [Authorize]
        [SwaggerOperation(Summary = "Delete clinic")]
        public void Delete(Guid id)
        {
            service.DeleteClinic(id);
        }
    }
}
