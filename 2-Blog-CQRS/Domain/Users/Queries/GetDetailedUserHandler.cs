using _2_Blog_CQRS.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace _2_Blog_CQRS.Domain.Users.Queries;

public class GetDetailedUserHandler : IRequestHandler<GetDetailedUser, UserDetailDTO>
{
    private readonly BlogContext _context;

    public GetDetailedUserHandler(BlogContext context)
    {
        _context = context;
    }

    public async Task<UserDetailDTO> Handle(GetDetailedUser request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.Include(u => u.Posts)
            .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken: cancellationToken);

        if (user == null)
            throw new NotFoundException();

        return new UserDetailDTO(user.Id, user.Name,
            user.Posts.Select(p => new PostDTO(p.Id, p.Title, user.Name)).ToArray());
    }
}