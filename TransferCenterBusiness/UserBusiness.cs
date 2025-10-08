using System.Data;
using TransferCenterBusinessInterface;
using TransferCenterCore.UnitOfWork;
using TransferCenterModel;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace TransferCenterBusiness
{
    public class UserBusiness : IUserBusiness
    {
        public IUnitOfWork _unitOfWork;
        readonly IConfiguration _configuration;
        public UserBusiness(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }
        public async Task<tblUser> Login(string username, string password)
        {
            return await _unitOfWork.UserRepository.GetAsync(x => x.LoginId == username && x.Password == password);
        }

        public tblUser LoginNew(string username, string password)
        {
            return _unitOfWork.UserRepository.Get(x => x.LoginId == username && x.Password == password);
        }

        public IEnumerable<tblUser> GetAll()
        {
            return _unitOfWork.UserRepository.GetAll();
        }

        public int SaveUser(tblUser user)
        {
            int result = 0;

            try
            {

                _unitOfWork.UserRepository.Add(user);
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

        public tblUser GetUserById(int Id)
        {
            return _unitOfWork.UserRepository.Get(x => x.UserId == Id);
        }

        public void UpdateUser(tblUser user)
        {
            int result = 0;

            try
            {

                _unitOfWork.UserRepository.Update(user);
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
    }
}
