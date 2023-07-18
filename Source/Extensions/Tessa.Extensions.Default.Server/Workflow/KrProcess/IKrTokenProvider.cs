using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.ComponentModel;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess
{
    /// <summary>
    /// Объект, обеспечивающий создание и валидацию токена безопасности для типового решения.
    /// </summary>
    public interface IKrTokenProvider
    {
        /// <summary>
        /// Создаёт подписанный токен безопасности для заданной информации по карточке
        /// с указанием прав для процесса согласования.
        /// </summary>
        /// <param name="cardID">Идентификатор карточки, для которой требуется создать токен безопасности.</param>
        /// <param name="cardVersion">
        /// Номер версии карточки, для которой требуется создать токен безопасности.
        /// 
        /// При выписывании токена на сервере можно указать <see cref="CardComponentHelper.DoNotCheckVersion"/>,
        /// чтобы не проверять номер версии карточки (т.е. чтобы токен подходил для любой версии).
        /// 
        /// Не допускайте передачу такого токена до клиента!
        /// </param>
        /// <param name="permissionsVersion">
        /// Номер версии правил доступа для которой создается токен безопасности. 
        /// Если при проверке правил доступа номер версии в токене будет отличаться от текущей, то токен не будет учитываться при проверке прав.
        /// </param>
        /// <param name="permissions">
        /// Права на карточку, сохраняемые в токене безопасности. 
        /// Если не задана, устанавливаются права <see cref="KrPermissionFlagDescriptors.Full"/>
        /// </param>
        /// <param name="extendedCardSettings">
        /// Расширенные настройки прав по карточке
        /// </param>
        /// <param name="modifyTokenAction">
        /// Метод для модификации токена безопасности до его подписи
        /// </param>
        /// <returns>Токен безопасности, полученный для заданной информации по карточке.</returns>
        KrToken CreateToken(
            Guid cardID,
            int cardVersion = CardComponentHelper.DoNotCheckVersion,
            long permissionsVersion = CardComponentHelper.DoNotCheckVersion,
            ICollection<KrPermissionFlagDescriptor> permissions = null,
            IKrPermissionExtendedCardSettings extendedCardSettings = null,
            Action<KrToken> modifyTokenAction = null);

        /// <summary>
        /// Создаёт подписанный токен безопасности для заданной карточки
        /// с указанием прав для процесса согласования.
        /// </summary>
        /// <param name="card">Карточка, для которой требуется создать токен безопасности.</param>
        /// <param name="permissionsVersion">
        /// Номер версии правил доступа для которой создается токен безопасности. 
        /// Если при проверке правил доступа номер версии в токене будет отличаться от текущей, то токен не будет учитываться при проверке прав
        /// </param>
        /// <param name="permissions">
        /// Права на карточку, сохраняемые в токене безопасности. 
        /// Если не задана, устанавливаются права <see cref="KrPermissionFlagDescriptors.Full"/>
        /// </param>
        /// <param name="extendedCardSettings">
        /// Расширенные настройки прав по карточке
        /// </param>
        /// <param name="modifyTokenAction">
        /// Метод для модификации токена безопасности до его подписи
        /// </param>
        /// <returns>Токен безопасности, полученный для заданной карточки.</returns>
        KrToken CreateToken(
            Card card,
            long permissionsVersion = CardComponentHelper.DoNotCheckVersion,
            ICollection<KrPermissionFlagDescriptor> permissions = null,
            IKrPermissionExtendedCardSettings extendedCardSettings = null,
            Action<KrToken> modifyTokenAction = null);

        /// <summary>
        /// Выполняет проверку валидности токена безопасности, что гарантирует его неизменность с момента подписания.
        /// Возвращает признак того, что токен успешно прошёл все проверки.
        /// </summary>
        /// <param name="card">Карточка, для которой был получен токен.</param>
        /// <param name="token">Токен, полученный для карточки.</param>
        /// <param name="validationResult">
        /// Результат валидации, в который будет записано сообщение об ошибке,
        /// или <c>null</c>, если не требуется получать результат в виде сообщений,
        /// достаточно признака успешности, возвращаемого методом.
        /// </param>
        /// <param name="cancellationToken">Токен для отмены асинхронной операции</param>
        /// <returns>
        /// <c>true</c>, если токен валиден и не был изменён с момента его подписания;
        /// <c>false</c> в противном случае.
        /// </returns>
        ValueTask<KrTokenValidationResult> ValidateTokenAsync(
            Card card,
            KrToken token,
            IValidationResultBuilder validationResult = null,
            CancellationToken cancellationToken = default);
    }
}
