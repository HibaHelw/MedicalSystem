using System.Text.Json.Serialization;
using MedicalSystemModule.Interfaces;

namespace MedicalSystemAPI.DTOs.Requests
{
    public class UserRequest : IUser
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        [JsonIgnore]
        public DateTime CreatedAt { get; set; }
        [JsonIgnore]
        public DateTime? UpdatedAt { get; set; }
        [JsonIgnore]
        public DateTime? DeletedAt { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
