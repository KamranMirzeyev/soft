using Core;
using Core.Model;
using Core.Services;
using CryptoHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Data
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
        
        public async Task<int> Create(User user)
        {
            user.Status = true;
            user.Password = Crypto.HashPassword(user.Password);
            user.Phone = 994 + user.Phone.Replace("(", "").Replace("(","");
            await _unitOfWork.User.AddAsync(user);
            try
            {
                await _unitOfWork.CommitAsync();
                return user.Id;
            }
            catch (Exception)
            {

                return 0;
            }
        }

        public async Task<bool> Delete(int id)
        {
            User user = await _unitOfWork.User.SingleOrDefaultAsync(x => x.Id == id);
            if (user!=null)
            {
                user.Status = false;
                await _unitOfWork.CommitAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<User>> GetAllUser(int page, int count, string key)
        {
            int id = 0;
            bool isid = int.TryParse(key, out id);
            return await _unitOfWork.User.GetAllFilterAsync(x =>
            (!string.IsNullOrEmpty(key) ? isid == false ? x.LastName.ToLower().Contains(key.Trim().ToLower()) : x.Id == id : true),
            x => x.OrderByDescending(z => z.Id),
            null,
            count * page - count,
            count
            );
        }

        public async Task<User> GetUser(int id) => await _unitOfWork.User.GetByIdAsync(id);
            
        

        public async  Task<bool> Update(User user)
        {
            User u = await _unitOfWork.User.GetByIdAsync(user.Id);
            if (u!=null)
            {
                u.FirstName = user.FirstName;
                u.LastName = user.LastName;
                u.Phone = user.Phone;
                u.Email = user.Email;
                u.Password = Crypto.HashPassword(user.Password);
                await _unitOfWork.CommitAsync();
                return true;
            }
            return false;
        }
        public async Task<int> GetAllUserCount(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                var models = await _unitOfWork.User.GetAllAsync();
                return models.Count;
            }
            else
            {
                int id = 0;
                bool isid = int.TryParse(key, out id);
                var model = await _unitOfWork.User.Where(x => (!string.IsNullOrEmpty(key) ? isid == false ? x.FirstName.ToLower().Contains(key.Trim().ToLower()) : x.Id == id : true));
                return model.Count();
            }
        }
    }
}
