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
    public class WorkingHoursController : ControllerBase
    {
        private WorkingHoursServices service;

        public WorkingHoursController(IOptions<AppSettings> appsOptions)
        {
            service = new WorkingHoursServices(appsOptions);
        }


        [HttpGet("{id}")]
        [Authorize]
        [SwaggerOperation(Summary = "Get doctor working hours")]
        public async Task<IEnumerable<WorkingHoursResponse>> GetDoctorWorkingHours(Guid id)
        {
            return service.GetDoctorWorkingHours(id).Select(c => WorkingHoursResponse.Transform(c));
        }

        [HttpPost]
        [Authorize]
        [SwaggerOperation(Summary = "Add working hours to doctor")]
        public Guid CreateDoctor([FromBody] WorkingHoursRequest workingHours)
        {
            return service.CreateWorkingHours(workingHours);
        }

        [HttpPut("{id}")]
        [Authorize]
        [SwaggerOperation(Summary = "Edit doctor working hours")]
        public void UpdateDoctor(Guid id, [FromBody] WorkingHoursRequest workingHours)
        {
            service.UpdateDoctorWorkingHours(id, workingHours);
        }

        [HttpDelete("{id}")]
        [Authorize]
        [SwaggerOperation(Summary = "Delete doctor working hours")]
        public void DeleteDoctor(Guid id)
        {
            service.DeleteWorkingHours(id);
        }
    }
}
