using _2_Blog_CQRS.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace _2_Blog_CQRS.Domain.Users.Events;

public record DeletedUser(int UserId) : INotification;

public class DeletedUserTreatPostsHandler : INotificationHandler<DeletedUser>, IOrder
{
    private readonly BlogContext _context;

    public DeletedUserTreatPostsHandler(BlogContext context)
    {
        _context = context;
    }

    public async Task Handle(DeletedUser notification, CancellationToken cancellationToken)
    {
        await _context
            .Posts
            .Where(p => p.Author.Id == notification.UserId)
            .ExecuteUpdateAsync(x => x.SetProperty(p => p.IsDeleted, p => true), cancellationToken);
    }

    public int Order => 1;
}

public class DeletedUserTreatCommentsHandler : INotificationHandler<DeletedUser>, IOrder
{
    private readonly BlogContext _context;

    public DeletedUserTreatCommentsHandler(BlogContext context)
    {
        _context = context;
    }

    public async Task Handle(DeletedUser notification, CancellationToken cancellationToken)
    {
        await _context
            .Comments
            .Where(c => c.Author.Id == notification.UserId)
            .ExecuteUpdateAsync(x => x.SetProperty(c => c.IsDeleted, p => true), cancellationToken);
    }

    public int Order => 2;
}