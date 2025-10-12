using Microsoft.Extensions.Configuration;
using TransferCenterCore.Interface;
using TransferCenterDbStore.Entity;
using TransferCenterDbStore.UnitOfWork;

namespace TransferCenterCore.Service
{
    public class UserService : IUserService
    {
        public IUnitOfWork _unitOfWork;
        readonly IConfiguration _configuration;
        public UserService(IUnitOfWork unitOfWork, IConfiguration configuration)
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

        public long SaveUser(tblUser user)
        {
            long result = 0;

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
