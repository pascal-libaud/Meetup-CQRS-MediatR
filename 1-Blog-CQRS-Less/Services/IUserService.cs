using _1_Blog_CQRS_Less.Models;

namespace _1_Blog_CQRS_Less.Services
{
    public interface IUserService
    {
        Task<int> CreateUser(CreateUser request, CancellationToken cancellationToken);
        Task DeleteUser(int id, CancellationToken cancellationToken);
        Task RenameUser(int id, string name, CancellationToken cancellationToken);
    }
}