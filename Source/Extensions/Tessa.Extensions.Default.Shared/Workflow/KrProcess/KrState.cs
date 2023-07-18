using System;
using System.Collections.ObjectModel;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess
{
    /// <summary>
    /// Cостояние согласования документа.
    /// </summary>
    public readonly struct KrState :
        IEquatable<KrState>,
        IEquatable<int>
    {
        #region Constructors

        /// <summary>
        /// Создаёт экземпляр состояния документа.
        /// </summary>
        /// <param name="id">Идентификатор состояния.</param>
        public KrState(int id)
        {
            this.ID = id;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Идентификатор местоположения.
        /// </summary>
        public int ID { get; }

        #endregion

        #region Methods

        public bool IsDefault() => 0 <= this.ID && this.ID < DefaultStates.Count;

        public string TryGetDefaultName() => this.IsDefault() ? DefaultStateNames[this.ID] : null;

        #endregion

        #region Base Overrides

        /// <doc path='info[@type="object" and @item="ToString"]'/>
        public override string ToString() => this.ID.ToString();

        /// <doc path='info[@type="object" and @item="Equals"]'/>
        public override bool Equals(object obj) => obj is KrState state && this.Equals(state);

        /// <doc path='info[@type="object" and @item="GetHashCode"]'/>
        public override int GetHashCode() => this.ID.GetHashCode();

        #endregion

        #region Operators

        /// <doc path='info[@type="object" and @item="OperatorEquals"]'/>
        public static bool operator ==(KrState first, KrState second) => first.Equals(second);

        /// <doc path='info[@type="object" and @item="OperatorNotEquals"]'/>
        public static bool operator !=(KrState first, KrState second) => !first.Equals(second);

        public static bool operator ==(KrState first, int second) => first.Equals(second);

        public static bool operator !=(KrState first, int second) => !first.Equals(second);

        public static bool operator ==(int first, KrState second) => second.Equals(first);

        public static bool operator !=(int first, KrState second) => !second.Equals(first);

        public static explicit operator int(KrState state) => state.ID;

        public static explicit operator KrState(int stateID) => new KrState(stateID);

        #endregion

        #region IEquatable<KrState> Members

        /// <doc path='info[@type="IEquatable`1" and @item="Equals"]'/>
        public bool Equals(KrState other) => this.ID == other.ID;

        /// <inheritdoc />
        public bool Equals(int other) => this.ID == other;
        
        #endregion

        #region Static Fields

        /// <summary>
        ///  Проект
        /// </summary>
        public static readonly KrState Draft = new KrState(0);

        /// <summary>
        ///  На согласовании
        /// </summary>
        public static readonly KrState Active = new KrState(1);

        /// <summary>
        ///  Согласован
        /// </summary>
        public static readonly KrState Approved = new KrState(2);

        /// <summary>
        ///  Не согласован
        /// </summary>
        public static readonly KrState Disapproved = new KrState(3);

        /// <summary>
        /// На доработке.
        /// </summary>
        public static readonly KrState Editing = new KrState(4);

        /// <summary>
        /// Отменено
        /// </summary>
        public static readonly KrState Cancelled = new KrState(5);

        /// <summary>
        /// Зарегистрирован
        /// </summary>
        public static readonly KrState Registered = new KrState(6);

        /// <summary>
        /// На регистрации
        /// </summary>
        public static readonly KrState Registration = new KrState(7);

        /// <summary>
        /// Подписан
        /// </summary>
        public static readonly KrState Signed = new KrState(8);

        /// <summary>
        /// Отказан
        /// </summary>
        public static readonly KrState Declined = new KrState(9);

        /// <summary>
        /// На подписании
        /// </summary>
        public static readonly KrState Signing = new KrState(10);

        /// <summary>
        /// Список всех стандартных состояния карточки.
        /// </summary>
        public static readonly ReadOnlyCollection<KrState> DefaultStates = new ReadOnlyCollection<KrState>(
            new[]
            {
                Draft,
                Active,
                Approved,
                Disapproved,
                Editing,
                Cancelled,
                Registered,
                Registration,
                Signed,
                Declined,
                Signing,
            });

        /// <summary>
        /// Список всех стандартных названий состояний карточки.
        /// </summary>
        public static readonly ReadOnlyCollection<string> DefaultStateNames = new ReadOnlyCollection<string>(
            new[]
            {
                "$KrStates_Doc_Draft",
                "$KrStates_Doc_Active",
                "$KrStates_Doc_Approved",
                "$KrStates_Doc_Disapproved",
                "$KrStates_Doc_Editing",
                "$KrStates_Doc_Canceled",
                "$KrStates_Doc_Registered",
                "$KrStates_Doc_Registration",
                "$KrStates_Doc_Signed",
                "$KrStates_Doc_Declined",
                "$KrStates_Doc_Signing",
            });

        #endregion
    }
}
