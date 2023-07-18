using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Forums;
using Tessa.Forums.Models;
using Tessa.Localization;
using Tessa.Platform;
using Tessa.Platform.Collections;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Tessa.UI;
using Tessa.UI.Cards;
using Tessa.UI.Controls.Forums;
using Tessa.UI.Menu;
using Tessa.UI.Views;
using Tessa.UI.Views.Content;

namespace Tessa.Extensions.Default.Client.Forums
{
    /// <remarks>
    /// У расширения есть конфигуратор <see cref="OpenForumContextMenuViewExtensionConfigurator" />
    /// </remarks>
    public sealed class OpenForumContextMenuViewExtension : IWorkplaceViewComponentExtension
    {
        #region Constructors

        public OpenForumContextMenuViewExtension(
            IForumDialogManager forumDialogManager,
            ISession session,
            IForumProvider forumProvider,
            IForumPermissionsProvider permissionsProvider)
        {
            this.forumDialogManager = forumDialogManager;
            this.session = session;
            this.forumProvider = forumProvider;
            this.permissionsProvider = permissionsProvider;
        }

        #endregion

        #region Private Fields

        private readonly IForumProvider forumProvider;

        private readonly IForumDialogManager forumDialogManager;

        private readonly ISession session;

        private readonly IForumPermissionsProvider permissionsProvider;

        #endregion

        #region IWorkplaceViewComponentExtension Implements

        public void Clone(IWorkplaceViewComponent source, IWorkplaceViewComponent cloned, ICloneableContext context)
        {
        }

        public void Initialize(IWorkplaceViewComponent model)
        {
            if (this.session.ApplicationID == ApplicationIdentifiers.TessaAdmin || model.RefSection is not null)
            {
                // в TessaAdmin не будем ничего менять, т.к. там предпросмотр представлений
                // в режиме выборки не нужно создавать новую карточку.
                return;
            }

            model.ContextMenuGenerators.AddRange(this.GetParticipantsMenuAction());
        }

        public void Initialized(IWorkplaceViewComponent model)
        {
        }

        #endregion

        #region Private Members

        /// <summary>
        /// Возвращает действие создающее элемент меню
        /// </summary>
        /// <returns>
        /// Действие создающее элемент меню
        /// </returns>
        private Func<ViewContextMenuContext, ValueTask> GetParticipantsMenuAction()
        {
            return c =>
            {
                if (c.ViewContext.RefSection is not null)
                {
                    return new ValueTask();
                }

                IViewContext viewContext = c.ViewContext;

                IEnumerable<IDictionary<string, object>> selectedRowsEnumerable;
                IDictionary<string, object>[] selectedRows;

                if ((selectedRowsEnumerable = viewContext.SelectedRows) == null
                    || (selectedRows = selectedRowsEnumerable.ToArray()).Length == 0)
                {
                    return new ValueTask();
                }

                List<ForumOperationItem> items = TryGetRows(selectedRows);
                if (items.Any())
                {
                    c.MenuActions.Add(
                        new MenuAction(
                            "ChangeParticipant",
                            "$Forum_MenuAction_ChangeParticipants",
                            Icon.Empty,
                            new DelegateCommand(
                                async o => await c.MenuContext.UIContextExecutorAsync(
                                    async (ctx, ct) => await this.ChangeParticipantsCommandAsync(viewContext, items, ct)))));

                    c.MenuActions.Add(
                        new MenuAction(
                            "RemoveParticipants",
                            "$Forum_MenuAction_RemoveParticipants",
                            Icon.Empty,
                            new DelegateCommand(
                                async o => await c.MenuContext.UIContextExecutorAsync(
                                    async (ctx, ct) => await this.RemoveParticipantsCommandAsync(viewContext, items, ct)))));
                }

                return new ValueTask();
            };
        }

        private static bool HasModeratorPermissions(IViewContext context)
        {
            var participantType = (ParticipantType) context.Parameters[1].CriteriaValues[0].Values[0].Value;
            return participantType != ParticipantType.Participant && participantType != ParticipantType.ParticipantFromRole;
        }

        private static bool HasSuperModeratorPermissions(IViewContext context)
        {
            var participantType = (ParticipantType) context.Parameters[1].CriteriaValues[0].Values[0].Value;
            return participantType == ParticipantType.SuperModerator;
        }

        private static Guid GetCardID(IViewContext context)
        {
            return (Guid) context.Parameters[2].CriteriaValues[0].Values[0].Value;
        }

        private async Task RemoveParticipantsCommandAsync(IViewContext context, List<ForumOperationItem> items, CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(items, nameof(items));
            Check.ArgumentNotNull(context, nameof(context));

            if (!HasModeratorPermissions(context))
            {
                await TessaDialog.ShowErrorAsync(
                    string.Format(
                        await LocalizationManager.LocalizeAsync(
                            "$Forum_UI_Cards_RemoveParticipants_NoRequiredPermissions", cancellationToken),
                        ForumHelper.ConcatUsersName(items.Select(i => i.RoleName).ToList(), ",")));
                return;
            }

            var currentUserID = this.session.User.ID;
            if (!items.TryFirst(i => i.CardID == currentUserID, out _))
            {
                try
                {
                    List<Guid> participants = items.Select(i => i.RoleID).ToList();

                    ValidationResult validationResult = await this.RemoveParticipants(
                        items,
                        participants,
                        GetCardID(context),
                        HasSuperModeratorPermissions(context), cancellationToken);

                    if (validationResult.IsSuccessful)
                    {
                        if (context.CanRefreshView())
                        {
                            await context.RefreshViewAsync(cancellationToken).ConfigureAwait(false);
                        }
                    }
                    else
                    {
                        await TessaDialog.ShowNotEmptyAsync(validationResult);
                    }
                }
                catch (OperationCanceledException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    await TessaDialog.ShowExceptionAsync(ex);
                }
            }
            else
            {
                await TessaDialog.ShowErrorAsync("$Forum_UI_Cards_RemoveParticipants_CantRemoveCurrentParticipant");
            }
        }

        private async Task<ValidationResult> RemoveParticipants(
            List<ForumOperationItem> items,
            IReadOnlyCollection<Guid> participants,
            Guid cardID,
            bool isSuperModerator = false,
            CancellationToken cancellationToken = default)
        {
            // проверяем что строки из с одним RoleType либо это роли, либо участники.
            IEnumerable<IGrouping<bool, ForumOperationItem>> groupedItems = items.GroupBy(i => i.RoleType == 1);
            if (groupedItems.Count() > 1)
            {
                return new ValidationResultBuilder()
                    .Add(ForumValidationKeys.PermissionError, ValidationResultType.Error,
                        await LocalizationManager.LocalizeAsync("$Forum_UI_Cards_RemoveParticipants_CannotDeleteParticipantsAndRoles", cancellationToken))
                    .Build();
            }

            ForumOperationItem item = items[0];
            var validationResult = ValidationResult.Empty;
            if (item.ParticipantType == ParticipantType.ParticipantFromRole)
            {
                validationResult = await this.ProcessingRemoveRoleAsync(items, participants, cardID, isSuperModerator, cancellationToken);
            }
            else
            {
                bool result = await TessaDialog.ConfirmAsync(
                    string.Format(
                        await LocalizationManager.LocalizeAsync("$Forum_UI_Cards_RemoveParticipant_ConfirmSingle", cancellationToken),
                        ForumHelper.ConcatUsersName(items.Select(u => u.RoleName).ToList(), ",")));
                if (result)
                {
                    (_, validationResult) = await this.forumProvider.RemoveParticipantsAsync(item.TopicID, participants, cancellationToken).ConfigureAwait(false);
                }
            }

            return validationResult;
        }

        private async Task<ForumOperationItem> ProcessingChangeRoleAsync(
            ForumOperationItem item,
            Guid cardID,
            bool isSuperModerator = false,
            CancellationToken cancellationToken = default)
        {
            // для редактирования ролей нужны права супермодератора
            // смотрим есть ли у нас права из карточки
            if (isSuperModerator)
            {
                return await this.forumDialogManager.ChangeRoleParticipantsShowDialogAsync(item, cancellationToken);
            }

            // если нет, то спрашиваем у пользователя, и пробуем получить права с сервера
            bool result = await TessaDialog.ConfirmAsync("$Forum_UI_Cards_ChangeParticipants_TryGetSupermoderatorPermission");
            if (result)
            {
                (bool isEnableSuperModeratorMode, ValidationResult vr) = await this.permissionsProvider.CheckSuperModeratorPermissionAsync(
                    cardID,
                    null,
                    cancellationToken);

                if (!vr.IsSuccessful)
                {
                    await TessaDialog.ShowNotEmptyAsync(vr);
                    return null;
                }

                if (isEnableSuperModeratorMode)
                {
                    return await this.forumDialogManager.ChangeRoleParticipantsShowDialogAsync(item, cancellationToken);
                }
            }

            return null;
        }

        private async Task<ValidationResult> ProcessingRemoveRoleAsync(
            List<ForumOperationItem> items,
            IReadOnlyCollection<Guid> participants,
            Guid cardID,
            bool isSuperModerator = false,
            CancellationToken cancellationToken = default)
        {
            // для редактирования ролей нужны права супермодератора
            // смотрим есть ли у нас права из карточки
            if (isSuperModerator)
            {
                bool confirmed = await TessaDialog.ConfirmAsync(
                    string.Format(
                        await LocalizationManager.LocalizeAsync("$Forum_UI_Cards_RemoveParticipant_ConfirmSingle", cancellationToken),
                        ForumHelper.ConcatUsersName(items.Select(u => u.RoleName).ToList(), ",")));
                if (confirmed)
                {
                    (ForumResponse _, ValidationResult validationResult) = await this.forumProvider.RemoveRolesAsync(items[0].TopicID, participants, cancellationToken).ConfigureAwait(false);
                    return validationResult;
                }

                return ValidationResult.Empty;
            }
            
            // если нет, то спрашиваем у пользователя, и пробуем получить права с сервера
            bool result = await TessaDialog.ConfirmAsync("$Forum_UI_Cards_ChangeParticipants_TryGetSupermoderatorPermission");
            if (result)
            {
                (bool isEnableSuperModeratorMode, ValidationResult validationResult) = await this.permissionsProvider.CheckSuperModeratorPermissionAsync(
                    cardID,
                    null,
                    cancellationToken);

                if (!validationResult.IsSuccessful)
                {
                    return validationResult;
                }

                if (isEnableSuperModeratorMode)
                {
                    result = await TessaDialog.ConfirmAsync(
                        string.Format(
                            await LocalizationManager.LocalizeAsync("$Forum_UI_Cards_RemoveParticipant_ConfirmSingle", cancellationToken),
                            ForumHelper.ConcatUsersName(items.Select(u => u.RoleName).ToList(), ",")));

                    if (result)
                    {
                        (ForumResponse _, ValidationResult removeRolesResult) = await this.forumProvider.RemoveRolesAsync(items[0].TopicID, participants, cancellationToken).ConfigureAwait(false);
                        return removeRolesResult;
                    }
                }
            }

            return ValidationResult.Empty;
        }


        private async Task ChangeParticipantsCommandAsync(
            IViewContext context,
            List<ForumOperationItem> items,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(items, nameof(items));
            Check.ArgumentNotNull(context, nameof(context));

            if (!HasModeratorPermissions(context))
            {
                await TessaDialog.ShowErrorAsync(
                    string.Format(
                        await LocalizationManager.LocalizeAsync(
                            "$Forum_UI_Cards_RemoveParticipants_NoRequiredPermissions", cancellationToken),
                        ForumHelper.ConcatUsersName(items.Select(i => i.RoleName).ToList(), ",")));
                return;
            }

            var currentUserID = this.session.User.ID;
            if (items.TryFirst(i => i.CardID == currentUserID, out _))
            {
                await TessaDialog.ShowErrorAsync("$Forum_UI_Cards_ChangeParticipants_CannotEditYourself").ConfigureAwait(false);
                return;
            }

            //пока делаем так, что можно редактировать только по одной строке.
            if (items.Count > 1)
            {
                await TessaDialog.ShowErrorAsync("$Forum_UI_Cards_ChangeParticipants_YouCanEditOnlyOneEntry").ConfigureAwait(false);
                return;
            }

            ForumOperationItem item = items[0];

            ForumOperationItem newItem;
            if (item.ParticipantType == ParticipantType.ParticipantFromRole)
            {
                newItem = await this.ProcessingChangeRoleAsync(item, GetCardID(context), HasSuperModeratorPermissions(context), cancellationToken);
            }
            else
            {
                newItem = await this.forumDialogManager.ChangeParticipantsShowDialogAsync(item, cancellationToken);
            }

            //нажали отмена
            if (newItem == null)
            {
                return;
            }

            var participants = new List<Guid> { newItem.RoleID };

            ValidationResult validationResult = newItem.ParticipantType == ParticipantType.ParticipantFromRole
                ? await this.forumProvider.UpdateRolesAsync(newItem.TopicID, participants, newItem.IsReadOnly, newItem.IsSubscribed, cancellationToken).ConfigureAwait(false)
                : await this.forumProvider.UpdateParticipantsAsync(newItem.TopicID, participants, newItem.IsReadOnly, newItem.IsSubscribed, newItem.ParticipantType, cancellationToken)
                    .ConfigureAwait(false);

            if (validationResult.IsSuccessful)
            {
                if (context.CanRefreshView())
                {
                    await context.RefreshViewAsync(cancellationToken).ConfigureAwait(false);
                }
            }
            else
            {
                await TessaDialog.ShowNotEmptyAsync(validationResult);
            }
        }

        private static List<ForumOperationItem> TryGetRows(IDictionary<string, object>[] rows)
        {
            var result = new List<ForumOperationItem>();
            foreach (IDictionary<string, object> row in rows)
            {
                result.Add(TryGetSelectedItemsFromViewContext(row));
            }

            return result;
        }

        private static ForumOperationItem TryGetSelectedItemsFromViewContext(IDictionary<string, object> selectedRows)
        {
            ForumOperationItem forumItem = new(
                selectedRows.Get<Guid>("RoleID"),
                selectedRows.Get<string>("RoleName"))
            {
                RoleType = selectedRows.Get<int>("TypeID"),
                TopicID = selectedRows.Get<Guid>("TopicID"),
                IsReadOnly = selectedRows.Get<bool>("ReadOnly"),
                ParticipantType = (ParticipantType) selectedRows.Get<int>("TypeParticipant"),
                IsSubscribed = selectedRows.Get<bool>("Subscribed")
            };

            return forumItem;
        }

        #endregion
    }
}
