using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MedicalSystemModule.DTO;
using MedicalSystemModule.Interfaces;
using MedicalSystemModule.Interfaces.Services;
using MedicalSystemModule.Models;
using MedicalSystemModule.Storage;
using MedicalSystemModule.Utilities;
using Microsoft.Extensions.Options;

namespace MedicalSystemModule.Services
{
    public class UserServices : IUserServices
    {
        private UserStorage storage;
        private JWTAuthentication _jwtAuthentication;
        public UserServices(IOptions<AppSettings> appsOptions)
        {
            storage = new UserStorage(appsOptions);
            _jwtAuthentication = new JWTAuthentication(appsOptions);
        }

        public async Task<IEnumerable<IUser>> GetAll()
        {
            return await storage.GetAll();
        }

        public IUser GetById(Guid id)
        {
            if (!Exist(id)) throw new Exception("Id doesn't exist");
            return storage.GetById(id);
        }

        public void CreateUserValidation(IUser user)
        {
            if (string.IsNullOrWhiteSpace(user.Username)) throw new Exception("User name cannot be empty");
            if (string.IsNullOrWhiteSpace(user.Password)) throw new Exception("Password cannot be empty");
            if (string.IsNullOrWhiteSpace(user.Email)) throw new Exception("Email cannot be empty");
            if (GetByUsernameAndPassword(user.Username, user.Password) != null)
                throw new Exception("User name already exist");
            if (GetByEmailAndPassword(user.Email, user.Password) != null)
                throw new Exception("User email already exist");
        }

        public Guid CreateUser(IUser user)
        {
            CreateUserValidation(user);
            return storage.CreateUser(user);
        }

        public void UpdateUserValidation(Guid id, IUser user)
        {
            if (!Exist(id)) throw new Exception("Id doesn't exist");
            if (string.IsNullOrWhiteSpace(user.Username)) throw new Exception("User name cannot be empty");
            if (string.IsNullOrWhiteSpace(user.Password)) throw new Exception("Password cannot be empty");
            if (string.IsNullOrWhiteSpace(user.Email)) throw new Exception("Email cannot be empty");
            if (GetByUsernameAndPassword(user.Username, user.Password) != null)
                throw new Exception("User name already exist");
            if (GetByEmailAndPassword(user.Email, user.Password) != null)
                throw new Exception("User email already exist");
        }

        public void UpdateUser(Guid id, IUser user)
        {
            UpdateUserValidation(id, user);
            storage.UpdateUser(id, user);
        }

        public void DeleteUserValidation(Guid id)
        {
            if (!Exist(id)) throw new Exception("Id doesn't exist");
        }

        public void DeleteUser(Guid id)
        {
            DeleteUserValidation(id);
            storage.DeleteUSer(id);
        }

        public bool Exist(Guid id)
        {
            return storage.Exist(id);
        }

        public IUser GetByUsernameAndPassword(string username, string password)
        {
            return storage.GetByUsernameAndPassword(username, password);
        }

        public IUser GetByEmailAndPassword(string email, string password)
        {
            return storage.GetByEmailAndPassword(email, password);
        }

        public void AuthenticateValidation(IUser model)
        {
            if (string.IsNullOrWhiteSpace(model.Email) && string.IsNullOrWhiteSpace(model.Username))
                throw new Exception("You should pass email or username");
            if (!string.IsNullOrWhiteSpace(model.Email) && !string.IsNullOrWhiteSpace(model.Username))
                throw new Exception("You should pass email or username");
            if (string.IsNullOrWhiteSpace(model.Password)) throw new Exception("Password cannot be null or empty");
            if (string.IsNullOrWhiteSpace(model.Email)) return;
            var emailFormat = Regex.IsMatch(model.Email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            if (!emailFormat) throw new Exception("Email is invalid");
        }

        public IAuthenticateResponse Authenticate(IUser model)
        {
            AuthenticateValidation(model);
            IUser user = new TransformToUSer();
            if (!string.IsNullOrWhiteSpace(model.Email))
                user = storage.GetByEmailAndPassword(model.Email, model.Password);
            if (!string.IsNullOrWhiteSpace(model.Username) && string.IsNullOrWhiteSpace(model.Email))
                user = storage.GetByUsernameAndPassword(model.Username, model.Password);
            if (user.Id == default) throw new Exception("User not exist");
            var token = _jwtAuthentication.GenerateJwtToken(user.Id);
            return new AuthenticateResponse()
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username,
                Token = token,
            };
        }
    }
}
