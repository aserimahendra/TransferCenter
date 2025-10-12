
using TransferCenterDbStore.Entity;

namespace TransferCenterCore.Interface
{
    public interface IUserService
    {
        public Task<tblUser> Login(string username, string password);
        public tblUser LoginNew(string username, string password);

        public IEnumerable<tblUser> GetAll();

        public long SaveUser(tblUser user);

        public int CheckDuplicateEmail(string email);

        public int CheckDuplicateLogin(string loginid);

        public tblUser GetUserById(int Id);

        public void UpdateUser(tblUser user);

       // public IEnumerable<CommonModel> GetUserTeam(int Id);
        string GetLoginIdByEmail(string email);
    }
}
