using Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IUserService
    {
        Task<int> Create(User user);
        Task<bool> Update(User user);
        Task<User> GetUser(int id);
        Task<IEnumerable<User>> GetAllUser(int page, int count, string key);
        Task<int> GetAllUserCount(string key);
        Task<bool> Delete(int id);
    }
}
