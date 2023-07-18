using System;
using System.Diagnostics.Contracts;
using Tessa.Cards;

namespace Tessa.Extensions.Default.Shared.Cards
{
    public static class DefaultCardTypeExtensionTypes
    {
        #region Static Fields

        /// <summary>
        /// Расширение, которое устанавливает поле в состояние "только для чтения" после первого сохранения карточки
        /// </summary>
        public static readonly CardTypeExtensionType InitializeFilesView =
            new CardTypeExtensionType(
                new Guid(0x5e2f5766, 0xb107, 0x4dd1, 0xa7, 0x41, 0x65, 0x5e, 0x83, 0x91, 0xfa, 0xe5),
                nameof(InitializeFilesView),
                new[] { CardInstanceType.Card, CardInstanceType.Task });

        /// <summary>
        /// Расширение, позволяющее открывать карточки из представления.
        /// </summary>
        public static readonly CardTypeExtensionType OpenCardInView =
            new CardTypeExtensionType(
                new Guid(0x9df93a75, 0xf788, 0x4b06, 0xbd, 0x17, 0x88, 0x1a, 0x4, 0x58, 0xd0, 0x46),
                nameof(OpenCardInView),
                new[] { CardInstanceType.Card, CardInstanceType.Task });

        #endregion

        #region RegisterInternal Method

        /// <summary>
        /// Регистрирует все стандартные типы посредством заданного метода.
        /// </summary>
        /// <param name="registerAction">Метод, выполняющий регистрацию типа.</param>
        public static void Register(Action<CardTypeExtensionType> registerAction)
        {
            Contract.Requires(registerAction != null);

            foreach (CardTypeExtensionType extensionType
                in new[]
                {
                    InitializeFilesView,
                    OpenCardInView
                })
            {
                registerAction(extensionType);
            }
        }

        #endregion
    }
}
