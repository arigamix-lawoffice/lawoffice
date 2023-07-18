#nullable enable
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.UI.Cards;
using Tessa.UI.Cards.Controls;

namespace Tessa.Extensions.Default.Client.UI
{
    public delegate ValueTask<IViewCardControlInitializationStrategy?> TryGetControlInitializationStrategyAsync(
        ITypeExtensionContext extensionContext,
        ICardModel model,
        CancellationToken cancellationToken = default);
}
