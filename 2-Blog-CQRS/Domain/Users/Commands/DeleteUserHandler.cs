using _2_Blog_CQRS.Events;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace _2_Blog_CQRS.Domain.Users.Commands;

public class DeleteUserHandler : IRequestHandler<DeleteUser>
{
    private readonly BlogContext _context;
    private readonly IMediator _mediator;

    public DeleteUserHandler(BlogContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task Handle(DeleteUser request, CancellationToken cancellationToken)
    {
        await _context
            .Users
            .Where(u => u.Id == request.Id)
            .ExecuteUpdateAsync(x => x.SetProperty(u => u.IsDeleted, u => true), cancellationToken);

        await _mediator.Publish(new DeletedUser(request.Id), cancellationToken);
    }
}

public sealed class DeleteUserValidator : AbstractValidator<DeleteUser>
{
    public DeleteUserValidator(BlogContext context)
    {
        RuleFor(p => p.Id).Must(id => context.Users.Any(u => u.Id == id));
    }
}