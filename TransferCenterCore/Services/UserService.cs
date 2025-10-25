using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using TransferCenterCore.Interfaces;
using TransferCenterCore.Models;
using TransferCenterCore.Translators;
using TransferCenterDbStore.UnitOfWork;

namespace TransferCenterCore.Services;

public class UserService : IUserService
{
    public IUnitOfWork _unitOfWork;
    readonly IConfiguration _configuration;
    public UserService(IUnitOfWork unitOfWork, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
    }
    public async Task<User> Login(string username, string password)
    {
        var user = await _unitOfWork.UserRepository.GetAsync(x => x.LoginId == username && x.Password == password);
        return user.ToCoreModel();
    }

    public User LoginNew(string username, string password)
    {
        var user = _unitOfWork.UserRepository.Get(x => x.LoginId == username && x.Password == password);
        return user.ToCoreModel();
    }

    public long SaveUser(User user)
    {
        long result = 0;

        try
        {
            _unitOfWork.UserRepository.Add(user.ToEntity());
            _unitOfWork.Commit();
            result = user.UserId;
        }
        catch
        {
            _unitOfWork.Rollback();
        }

        return result;
    }

    public int CheckDuplicateEmail(string email)
    {
        int result = 0;
        var obj = _unitOfWork.UserRepository.Get(x => x.EmailId == email && x.IsActive);

        if (obj != null)
        {
            result = 1;
        }
        return result;
    }

    public int CheckDuplicateLogin(string loginid)
    {
        int result = 0;
        var obj = _unitOfWork.UserRepository.Get(x => x.LoginId == loginid && x.IsActive);
        if (obj != null)
        {
            result = 1;
        }
        return result;
    }

    public User GetUserById(int Id)
    {
        var user = _unitOfWork.UserRepository.Get(x => x.UserId == Id);
        return user.ToCoreModel();
    }

    public void UpdateUser(User user)
    {
        int result = 0;

        try
        {

            _unitOfWork.UserRepository.Update(user.ToEntity());
            _unitOfWork.Commit();
        }
        catch
        {
            _unitOfWork.Rollback();
        }

        //return result;
    }

      

    public string GetLoginIdByEmail(string email)
    {
        var user = _unitOfWork.UserRepository.Get(x => x.EmailId == email && x.IsActive);
        return user?.LoginId;
    }

    public async Task<(IEnumerable<User>,int)> GetAllUsersAsync(int page, int pageSize)
    {
        int totalCount = await GetTotalCount();
        var users = _unitOfWork.UserRepository.Query()
            .Where(u => u.IsActive)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
        return (users.Select(u => u.ToCoreModel()).ToList(),totalCount);
    }
    private async Task<int> GetTotalCount()
    {
        return await _unitOfWork.UserRepository.CountAsync(x => x.IsActive);
    }
       
}