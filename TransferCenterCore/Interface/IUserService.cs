
using TransferCenterDbStore.Entity;

namespace TransferCenterCore.Interface
{
    public interface IUserService
    {
        public Task<User> Login(string username, string password);
        public User LoginNew(string username, string password);

        public IEnumerable<User> GetAll();

        public long SaveUser(User user);

        public int CheckDuplicateEmail(string email);

        public int CheckDuplicateLogin(string loginid);

        public User GetUserById(int Id);

        public void UpdateUser(User user);

       // public IEnumerable<CommonModel> GetUserTeam(int Id);
        string GetLoginIdByEmail(string email);
    }
}
