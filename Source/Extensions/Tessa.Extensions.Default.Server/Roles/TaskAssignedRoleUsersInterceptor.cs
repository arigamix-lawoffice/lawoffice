#nullable enable

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.ComponentModel;
using Tessa.Roles;
using Tessa.Views;
using Tessa.Views.Metadata.Criteria;
using Tessa.Views.Parser;

namespace Tessa.Extensions.Default.Server.Roles
{
    /// <summary>
    /// Перехватывает представление, отображающее пользователей для диалога "Роли задания".
    /// Используются отдельное представление для "контекстных ролей" и отдельное представление для всех остальных типов ролей.
    /// </summary>
    public sealed class TaskAssignedRoleUsersInterceptor :
        ViewInterceptorBase
    {
        #region Constructors

        public TaskAssignedRoleUsersInterceptor(
            Func<IViewService> viewService,
            ICardGetStrategy cardGetStrategy)
            : base(new[] { InterceptedViewAlias })
        {
            
            this.viewService = NotNullOrThrow(viewService);
            this.cardGetStrategy = NotNullOrThrow(cardGetStrategy);
        }

        #endregion

        #region Constants and Fields

        private const string InterceptedViewAlias = "TaskAssignedRoleUsers";
        private const string UsersViewAlias = "Users";
        private const string TarRowIDParameterName = "TaskAssignedRoleRowID";
        private const string ViewNotFoundText = "Can't find view with alias: \"{0}\".";

        private readonly Func<IViewService> viewService;
        private readonly ICardGetStrategy cardGetStrategy;

        #endregion

        #region Base Overrides

        /// <inheritdoc />
        public override async Task<ITessaViewResult> GetDataAsync(ITessaViewRequest request, CancellationToken cancellationToken = default)
        {
            if (request.ViewAlias is null)
            {
                throw new InvalidOperationException("View alias isn't specified.");
            }

            if (!ParserNames.IsEquals(request.ViewAlias, InterceptedViewAlias))
            {
                throw new InvalidOperationException($"Unknown view. Expected: \"{InterceptedViewAlias}\".");
            }

            var roleIDParameter = request.Values?.FirstOrDefault(x => x.Name?.Equals(CardTaskAssignedRole.RoleIDKey, StringComparison.Ordinal) ?? false);

            var roleTypeId = roleIDParameter?.CriteriaValues
                .FirstOrDefault(x => x.CriteriaName?.Equals(CriteriaOperatorConst.Equality, StringComparison.Ordinal) ?? false)
                ?.Values.FirstOrDefault()?.Value is Guid roleID
                ? await this.cardGetStrategy.GetTypeIDAsync(roleID, CardInstanceType.Card, cancellationToken)
                : null;

            ITessaViewResult result;

            if (roleTypeId == RoleHelper.ContextRoleTypeID)
            {
                if (this.InterceptedViews.TryGetValue(
                        request.ViewAlias,
                        out ITessaView? view))
                {
                    // Удалить лишний параметр в запросе.
                    request.Values?.Remove(roleIDParameter);
                    result = await view.GetDataAsync(request, cancellationToken);
                }
                else
                {
                    throw new InvalidOperationException(string.Format(ViewNotFoundText, request.ViewAlias));
                }
            }
            else
            {
                // Удалить лишний параметр в запросе.
                var tarRowIDParameter = request.Values?.FirstOrDefault(x => x.Name?.Equals(TarRowIDParameterName, StringComparison.Ordinal) ?? false);
                request.Values?.Remove(tarRowIDParameter);
                var view = await this.viewService().GetByNameAsync(UsersViewAlias, cancellationToken);
                if (view is null)
                {
                    throw new InvalidOperationException(string.Format(ViewNotFoundText, request.ViewAlias));
                }
                result = await view.GetDataAsync(request, cancellationToken);
            }

            return result;
        }

        #endregion
    }
}
