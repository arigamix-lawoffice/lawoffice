using System;
using Tessa.Extensions.Default.Shared;
using Tessa.Test.Default.Shared.Kr;

namespace Tessa.Test.Default.Client.Kr
{
    /// <summary>
    /// Базовый абстрактный класс для клиентских тестов с поддержкой типового решения и маршрутов.
    /// </summary>
    public abstract class KrClientTestBase :
        ClientTestBase,
        IKrTest
    {
        #region IKrTest Members

        /// <inheritdoc/>
        public virtual Guid TestCardTypeID => DefaultCardTypes.DocumentTypeID;

        /// <inheritdoc/>
        public virtual string TestCardTypeName => DefaultCardTypes.DocumentTypeName;

        /// <inheritdoc/>
        public virtual Guid TestDocTypeID => new Guid(0x93A392E7, 0x097C, 0x4420, 0x85, 0xC4, 0xDB, 0x10, 0xB2, 0xDF, 0x3C, 0x1D);

        /// <inheritdoc/>
        public virtual string TestDocTypeName => "Contract";

        /// <inheritdoc/>
        public CardLifecycleCompanion CreateCardLifecycleCompanion(
            Guid? id = null) =>
            new CardLifecycleCompanion(
                id ?? Guid.NewGuid(),
                this.TestCardTypeID,
                this.TestCardTypeName,
                this.CardLifecycleDependencies);

        #endregion
    }
}
