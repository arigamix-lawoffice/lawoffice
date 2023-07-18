using System.Threading;
using System.Threading.Tasks;

namespace Tessa.Extensions.Default.Server.Notices
{
    public interface IMessageHandler
    {
        ValueTask<IMessageInfo> TryParseAsync(NoticeMessage message, CancellationToken cancellationToken = default);

        Task HandleAsync(IMessageHandlerContext context);
    }
}
