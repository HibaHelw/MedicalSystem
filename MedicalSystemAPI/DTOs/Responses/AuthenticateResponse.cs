using MedicalSystemModule.Interfaces;

namespace MedicalSystemAPI.DTOs.Responses
{
    public class AuthenticateResponse : IAuthenticateResponse
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }

        public static AuthenticateResponse Transform(IAuthenticateResponse user)
        {
            return new AuthenticateResponse()
            {
                Id = user.Id,
                Email = user.Email,
                Token = user.Token,
                Username = user.Username,
            };
        }
    }
}
