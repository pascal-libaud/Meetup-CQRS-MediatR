using MediatR;
using Microsoft.EntityFrameworkCore;

namespace _2_Blog_CQRS.Domain.Users.Queries;

public class GetAllUsersHandler : IRequestHandler<GetAllUsers, UserDTO[]>
{
    private readonly BlogContext _context;

    public GetAllUsersHandler(BlogContext context)
    {
        _context = context;
    }

    public Task<UserDTO[]> Handle(GetAllUsers request, CancellationToken cancellationToken)
    {
        return _context.Users
            .Select(u => new UserDTO(u.Id, u.Name))
            .ToArrayAsync(cancellationToken);
    }
}