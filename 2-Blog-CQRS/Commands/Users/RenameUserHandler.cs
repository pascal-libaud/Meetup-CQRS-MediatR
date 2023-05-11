using _2_Blog_CQRS.Common;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace _2_Blog_CQRS.Commands.Users;

public class RenameUserHandler : IRequestHandler<RenameUser>
{
    private readonly BlogContext _context;

    public RenameUserHandler(BlogContext context)
    {
        _context = context;
    }

    public async Task Handle(RenameUser request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);
        if (user == null)
            throw new NotFoundException();

        user.Name = request.Name;
        await _context.SaveChangesAsync(cancellationToken);
    }
}

public sealed class RenameUserValidator : AbstractValidator<RenameUser>
{
    public RenameUserValidator()
    {
        RuleFor(p => p.Name).MinimumLength(2).MaximumLength(255);
    }
}