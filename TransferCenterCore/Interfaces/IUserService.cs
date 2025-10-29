

using TransferCenterCore.Models;

namespace TransferCenterCore.Interfaces;

public interface IUserService
{
    public Task<User> Login(string username, string password);
    public User LoginNew(string username, string password);

    public long SaveUser(User user);

    public int CheckDuplicateEmail(string email);

    public int CheckDuplicateLogin(string loginid);

    public User GetUserById(int Id);

    public void UpdateUser(User user);

    // public IEnumerable<CommonModel> GetUserTeam(int Id);
    string GetLoginIdByEmail(string email);

    public Task<(IEnumerable<User> Users,int TotalCount)> GetAllUsersAsync(int page, int pageSize);
    
    
        
}