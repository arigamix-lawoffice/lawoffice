using System.Threading;
using System.Threading.Tasks;

namespace Tessa.Extensions.Default.Server.Notices
{
    public interface IMessageProcessor
    {
        Task ProcessMessageAsync(NoticeMessage message, CancellationToken cancellationToken = default);
    }
}