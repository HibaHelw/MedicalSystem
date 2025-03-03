using MedicalSystemModule.DTO;
using MedicalSystemModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalSystemModule.Interfaces;
using Microsoft.EntityFrameworkCore;
using MedicalSystemModule.Utilities;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace MedicalSystemModule.Storage
{
    internal class UserStorage
    {
        private MedicalContext.MedicalContext _context;
        public UserStorage(IOptions<AppSettings> appsOptions)
        {
            _context = new MedicalContext.MedicalContext(appsOptions.Value.ConnectionString);
        }

        public async Task<IEnumerable<IUser>> GetAll()
        {
            return await _context.Users.Where(d => !d.DeletedAt.HasValue).Select(c => c.Transform()).ToListAsync();
        }

        public IUser GetById(Guid id)
        {
            return _context.Users.FirstOrDefault(d => !d.DeletedAt.HasValue && d.Id == id)?.Transform();
        }

        public Guid CreateUser(IUser user)
        {
            var newUser = new User()
            {
                Username = user.Username,
                Password = MD5Hash(user.Password),
                Email = user.Email,
                CreatedAt = DateTime.UtcNow,
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();
            return newUser.Id;
        }

        public void UpdateUser(Guid id, IUser user)
        {
            var userToUpdate = _context.Users.First(c => c.Id == id);
            userToUpdate.Username = user.Username;
            userToUpdate.Password = MD5Hash(user.Password);
            userToUpdate.Email = user.Email;
            userToUpdate.UpdatedAt = DateTime.UtcNow;
            _context.Users.Update(userToUpdate);
            _context.SaveChanges();
        }

        public void DeleteUSer(Guid id)
        {
            var userToDelete = _context.Users.First(c => c.Id == id);
            userToDelete.DeletedAt = DateTime.UtcNow;
            _context.Users.Update(userToDelete);
            _context.SaveChanges();
        }

        public bool Exist(Guid id)
        {
            return _context.Users.Any(c => c.Id == id && !c.DeletedAt.HasValue);
        }

        public IUser GetByUsernameAndPassword(string username, string password)
        {
            var pass = MD5Hash(password);
            return _context.Users
                .FirstOrDefault(c => c.Username == username && c.Password == pass && !c.DeletedAt.HasValue)
                ?.Transform();
        }

        public IUser GetByEmailAndPassword(string email, string password)
        {
            var pass = MD5Hash(password);
            return _context.Users
                .FirstOrDefault(c => c.Email == email && c.Password == pass && !c.DeletedAt.HasValue)
                ?.Transform();
        }

        private string MD5Hash(string text)
        {
            MD5 md5H = MD5.Create();
            //convert the input string to a byte array and compute its hash
            byte[] data = md5H.ComputeHash(Encoding.UTF8.GetBytes(text));
            // create a new stringbuilder to collect the bytes and create a string
            StringBuilder sB = new StringBuilder();
            //loop through each byte of hashed data and format each one as a hexadecimal string
            for (int i = 0; i < data.Length; i++)
            {
                sB.Append(data[i].ToString("x2"));
            }
            //return hexadecimal string
            return sB.ToString();
        }
    }
}
