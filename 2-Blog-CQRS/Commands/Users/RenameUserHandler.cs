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
        // TODO Comment valider qu'on le retrouve bien ici ? Car la bdd a pu changer depuis la validation
        (await _context.Users.FirstAsync(u => u.Id == request.Id, cancellationToken: cancellationToken)).Name = request.Name;
        await _context.SaveChangesAsync(cancellationToken);
    }
}

public sealed class RenameUserValidator : AbstractValidator<RenameUser>
{
    public RenameUserValidator(BlogContext context)
    {
        RuleFor(p => p.Name).MinimumLength(2).MaximumLength(255);
        RuleFor(p => p.Name).MustAsync((name, cancellationToken) => context.Users.AllAsync(u => u.Name != name, cancellationToken));
        RuleFor(p => p.Id).MustAsync((id, cancellationToken) => context.Users.AnyAsync(u => u.Id == id, cancellationToken));
    }
}