using Core;
using Core.Repositories;
using Data.Repositories;
using System;
using System.Threading.Tasks;

namespace Data
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        private IUserRepository _userRepository;

        public IUserRepository User => _userRepository ?? new UserRepository(_context);

        public async Task<int> CommitAsync()
        {
            //return await _context.SaveChangesAsync();

            using (var dbContextTransaction = _context.Database.BeginTransaction()) // rollback ucun 
            {
                try
                {
                    //your db operations
                    dbContextTransaction.Commit();
                    return await _context.SaveChangesAsync();


                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                    return 0;
                }
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task DisposeAsync()
        {
           await _context.DisposeAsync();
        }
    }
}
