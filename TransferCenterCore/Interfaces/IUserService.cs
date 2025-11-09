

using TransferCenterCore.Models;

namespace TransferCenterCore.Interfaces;

public interface IUserService
{
    public Task<User> Login(string username, string password);

    public long SaveUser(User user);

    public bool CheckDuplicateEmailAndLogin(string emailId, string loginId);
    public User GetUserById(long Id);

    public void UpdateUser(User user);

    public Task<(IEnumerable<User> Users,int TotalCount)> GetAllUsersAsync(int page, int pageSize, string? search = null);
    
    
        
}