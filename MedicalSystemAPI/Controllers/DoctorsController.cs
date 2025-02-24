using MedicalSystemAPI.DTOs.Requests;
using MedicalSystemAPI.DTOs.Responses;
using MedicalSystemModule.Interfaces;
using MedicalSystemModule.MedicalContext;
using MedicalSystemModule.Services;
using MedicalSystemModule.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;

namespace MedicalSystemAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class DoctorsController : ControllerBase
    {
        private DoctorServices service;

        public DoctorsController(IOptions<AppSettings> appsOptions)
        {
            service = new DoctorServices(appsOptions);
        }

        [HttpGet]
        [Authorize]
        [SwaggerOperation(Summary = "Get all doctors")]
        public async Task<IEnumerable<DoctorsResponse>> GetAllDoctors()
        {
            return service.GetAll().Result.Select(c => DoctorsResponse.Transform(c));
        }

        [HttpGet("{id}")]
        [Authorize]
        [SwaggerOperation(Summary = "Get doctor by id")]
        public DoctorsResponse GeTDoctorById(Guid id)
        {
            return DoctorsResponse.Transform(service.GetById(id));
        }

        [HttpPost]
        [Authorize]
        [SwaggerOperation(Summary = "Add doctor")]
        public Guid CreateDoctor([FromBody] DoctorRequest doctor)
        {
            return service.CreateDoctor(doctor);
        }

        [HttpPut("{id}")]
        [Authorize]
        [SwaggerOperation(Summary = "Edit doctor")]
        public void UpdateDoctor(Guid id, [FromBody] DoctorRequest doctor)
        {
            service.UpdateDoctor(id, doctor);
        }

        [HttpDelete("{id}")]
        [Authorize]
        [SwaggerOperation(Summary = "Delete doctor")]
        public void DeleteDoctor(Guid id)
        {
            service.DeleteDoctor(id);
        }
    }
}
