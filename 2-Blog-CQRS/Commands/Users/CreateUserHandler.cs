using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace _2_Blog_CQRS.Commands.Users;

public class CreateUserHandler : IRequestHandler<CreateUser, int>
{
    private readonly BlogContext _context;

    public CreateUserHandler(BlogContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateUser request, CancellationToken cancellationToken)
    {
        var user = new User { Name = request.Name };
        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return user.Id;
    }
}

public sealed class CreateUserValidator : AbstractValidator<CreateUser>
{
    public CreateUserValidator(BlogContext context)
    {
        RuleFor(p => p.Name).MinimumLength(2).MaximumLength(255);
        RuleFor(p => p.Name).MustAsync((name, cancellationToken) => context.Users.AllAsync(u => u.Name != name, cancellationToken));
    }
}