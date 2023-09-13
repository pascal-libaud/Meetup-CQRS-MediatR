using _1_Blog_CQRS_Less.Common;
using _1_Blog_CQRS_Less.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace _1_Blog_CQRS_Less.Services;

public class UserService : IUserService
{
    private readonly BlogContext _context;
    private readonly ILogger<UserService> _logger;

    public UserService(BlogContext context, ILogger<UserService> logger)
    {
        _context = context;
        _logger = logger;
    }


    public async Task<int> CreateUser(CreateUser request, CancellationToken cancellationToken)
    {
        var user = new User { Name = request.Name };
        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return user.Id;
    }

    public async Task DeleteUser(int id, CancellationToken cancellationToken)
    {
        await _context.Users.Where(u => u.Id == id).ExecuteDeleteAsync(cancellationToken);
    }

    public async Task RenameUser(int id, string name, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
        if (user == null)
            throw new NotFoundException();

        user.Name = name;
        await _context.SaveChangesAsync(cancellationToken);
    }
}
