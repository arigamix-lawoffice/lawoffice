#nullable enable

using System.Collections.Generic;
using System.Threading;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Tags;

namespace Tessa.Extensions.Default.Shared.Tags
{
    /// <summary>
    /// Реализация <inheritdoc cref="ITagPermissionsTokenProvider" path="/summary"/> для типового решения.
    /// </summary>
    public class KrTagPermissionsTokenProvider : TagPermissionsTokenProvider
    {
        #region Base Overrides

        /// <inheritdoc/>
        public override Dictionary<string, object?>? TryGetTagPermissionToken(
            Card card,
            CancellationToken cancellationToken = default)
        {
            var token = KrToken.TryGet(card.Info);
            if (token is not null)
            {
                var tokenInfo = new Dictionary<string, object?>();
                token.Set(tokenInfo);
                return tokenInfo;
            }
            return null;
        }

        #endregion
    }
}
