using Core.Model;
using Core.Repositories;

namespace Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private DataContext _context => Context as DataContext;
        public UserRepository(DataContext context) : base(context)
        {

        }


    }
}
