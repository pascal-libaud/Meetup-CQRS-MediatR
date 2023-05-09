using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace _2_Blog_CQRS.Commands.Users;

public class DeleteUserHandler : IRequestHandler<DeleteUser>
{
    private readonly BlogContext _context;

    public DeleteUserHandler(BlogContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteUser request, CancellationToken cancellationToken)
    {
        await _context.Users.Where(u => u.Id == request.Id).ExecuteDeleteAsync(cancellationToken);
    }
}

public sealed class DeleteUserValidator : AbstractValidator<DeleteUser>
{
    public DeleteUserValidator(BlogContext context)
    {
        RuleFor(p => p.Id).MustAsync((id, cancellationToken) => context.Users.AnyAsync(u => u.Id == id, cancellationToken));
    }
}