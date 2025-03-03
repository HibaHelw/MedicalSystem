using MedicalSystemModule.Interfaces;

namespace MedicalSystemAPI.DTOs.Responses
{
    public class UserResponse : IUser
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public static UserResponse Transform(IUser user)
        {
            return new UserResponse()
            {
               Id = user.Id,
               Username = user.Username,
               Password = user.Password,
               Email = user.Email,
               CreatedAt = user.CreatedAt,
               UpdatedAt = user.UpdatedAt,
               DeletedAt = user.DeletedAt,
            };
        }
    }
}
