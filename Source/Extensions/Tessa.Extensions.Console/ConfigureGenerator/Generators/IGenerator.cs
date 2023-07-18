using System.Threading;
using System.Threading.Tasks;

namespace Tessa.Extensions.Console.ConfigureGenerator.Generators
{
    public interface IGenerator
    {
        Task<string> GenerateAsync(string path, CancellationToken cancellationToken = default);
        Task<string> GenerateWebAsync(string path, CancellationToken cancellationToken = default);
    }
}