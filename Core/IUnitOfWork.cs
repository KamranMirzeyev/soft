using Core.Repositories;
using System;
using System.Threading.Tasks;

namespace Core
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository User { get; }
        Task<int> CommitAsync();
        Task DisposeAsync();
    }
}
