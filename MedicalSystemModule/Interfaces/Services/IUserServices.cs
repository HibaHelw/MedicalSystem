namespace MedicalSystemModule.Interfaces.Services
{
    public interface IUserServices
    {
        public Task<IEnumerable<IUser>> GetAll();
        public IUser GetById(Guid id);
        public void CreateUserValidation(IUser user);

        public Guid CreateUser(IUser user);

        public void UpdateUserValidation(Guid id, IUser user);
        public void UpdateUser(Guid id, IUser user);

        public void DeleteUserValidation(Guid id);
        public void DeleteUser(Guid id);

        public bool Exist(Guid id);

        public IUser GetByUsernameAndPassword(string username, string password);
        public IUser GetByEmailAndPassword(string email, string password);

        public void AuthenticateValidation(IUser model);

        public IAuthenticateResponse Authenticate(IUser model);
    }
}
