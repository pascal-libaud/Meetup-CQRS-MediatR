using MediatR;

namespace _2_Blog_CQRS;

public class MyNotificationPublisher : INotificationPublisher
{
    public async Task Publish(IEnumerable<NotificationHandlerExecutor> handlerExecutors, INotification notification, CancellationToken cancellationToken)
    {
        foreach (var handler in handlerExecutors)
        {
            if (cancellationToken.IsCancellationRequested)
                return;

            await handler.HandlerCallback(notification, cancellationToken).ConfigureAwait(false);
        }
    }
}