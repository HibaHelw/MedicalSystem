using MedicalSystemAPI.DTOs.Requests;
using MedicalSystemAPI.DTOs.Responses;
using MedicalSystemModule.MedicalContext;
using MedicalSystemModule.Models;
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
    public class WorkingHoursController : ControllerBase
    {
        private WorkingHoursServices service;

        public WorkingHoursController(IOptions<AppSettings> appsOptions)
        {
            service = new WorkingHoursServices(appsOptions);
        }


        [HttpGet("{doctorId}")]
        //[Authorize]
        [SwaggerOperation(Summary = "Get doctor working hours")]
        public async Task<IEnumerable<WorkingHoursResponse>> GetDoctorWorkingHours(Guid doctorId)
        {
            return service.GetDoctorWorkingHours(doctorId).Select(c => WorkingHoursResponse.Transform(c));
        }

        [HttpPost]
        // [Authorize]
        [SwaggerOperation(Summary = "Add working hours to doctor")]
        public Guid CreateWorkingHour([FromBody] WorkingHoursRequest workingHours)
        {
            return service.CreateWorkingHours(workingHours);
        }

        [HttpPut("{id}")]
        //[Authorize]
        [SwaggerOperation(Summary = "Edit doctor working hours")]
        public void UpdateWorkingHour(Guid id, [FromBody] WorkingHoursRequest workingHours)
        {
            service.UpdateDoctorWorkingHours(id, workingHours);
        }

        [HttpDelete("{id}")]
        //[Authorize]
        [SwaggerOperation(Summary = "Delete doctor working hours")]
        public void DeleteWorkingHour(Guid id)
        {
            service.DeleteWorkingHours(id);
        }
    }
}
