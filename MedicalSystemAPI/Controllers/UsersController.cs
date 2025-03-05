using Azure.Core;
using MedicalSystemAPI.DTOs.Requests;
using MedicalSystemAPI.DTOs.Responses;
using MedicalSystemAPI.Filters;
using MedicalSystemModule.Interfaces;
using MedicalSystemModule.Interfaces.Services;
using MedicalSystemModule.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MedicalSystemAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserServices service;

        public UsersController(IUserServices userservice)
        {
            service = userservice;
        }

        [HttpGet]
        [Authorize]
        [SwaggerOperation(Summary = "Get all users")]
        public async Task<IEnumerable<UserResponse>> GetAll()
        {
            return service.GetAll().Result.Select(c => UserResponse.Transform(c));
        }

        [HttpGet("{id}")]
        //[Authorize]
        [SwaggerOperation(Summary = "Get user by id")]
        public UserResponse GeTById(Guid id)
        {
            return UserResponse.Transform(service.GetById(id));
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Add new user")]
        public Guid Register([FromBody] UserRequest user)
        {
            return service.CreateUser(user);
        }


        [HttpPost("LogIn")]
        [SwaggerOperation(Summary = "Log in user")]
        public IAuthenticateResponse LogIn([FromBody] UserRequest request)
        {
            if (CheckIfEmail(request.Email))
            {
                request.Username = "";
            }
            else
            {
                request.Email = "";
            }

            var userInfo = service.Authenticate(request);
            return AuthenticateResponse.Transform(userInfo);
        }


        [HttpPut("{id}")]
        //[Authorize]
        [SwaggerOperation(Summary = "Edit user")]
        public void Update(Guid id, [FromBody] UserRequest user)
        {
            service.UpdateUser(id, user);
        }

        [HttpDelete("{id}")]
        //[Authorize]
        [SwaggerOperation(Summary = "Delete user")]
        public void Delete(Guid id)
        {
            service.DeleteUser(id);
        }


        private bool CheckIfEmail(string email)
        {
            var trimmedEmail = email.Trim();
            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }
    }
}
