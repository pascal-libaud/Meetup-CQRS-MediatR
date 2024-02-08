using MediatR;
using Microsoft.EntityFrameworkCore;

namespace _2_Blog_CQRS.Domain.Users.Queries;

public class GetActiveUsersHandler : IRequestHandler<GetActiveUsers, UserDTO[]>
{
    private readonly BlogContext _context;

    public GetActiveUsersHandler(BlogContext context)
    {
        _context = context;
    }

    public Task<UserDTO[]> Handle(GetActiveUsers request, CancellationToken cancellationToken)
    {
        return _context.Users
            .Where(u => !u.IsDeleted)
            .Select(u => new UserDTO(u.Id, u.Name))
            .ToArrayAsync(cancellationToken);
    }
}