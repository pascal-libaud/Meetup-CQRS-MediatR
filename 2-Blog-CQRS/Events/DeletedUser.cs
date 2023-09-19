using MediatR;
using Microsoft.EntityFrameworkCore;

namespace _2_Blog_CQRS.Events;

public record DeletedUser(int UserId) : INotification;

public class DeletedUserHandler : INotificationHandler<DeletedUser>
{
    private readonly BlogContext _context;

    public DeletedUserHandler(BlogContext context)
    {
        _context = context;
    }

    public async Task Handle(DeletedUser notification, CancellationToken cancellationToken)
    {
        await _context
            .Posts
            .Where(p => p.Author.Id == notification.UserId)
            .ExecuteUpdateAsync(x => x.SetProperty(p => p.IsDeleted, p => true), cancellationToken);

        await _context
            .Comments
            .Where(c => c.Author.Id == notification.UserId)
            .ExecuteUpdateAsync(x => x.SetProperty(c => c.IsDeleted, p => true), cancellationToken);
    }
}