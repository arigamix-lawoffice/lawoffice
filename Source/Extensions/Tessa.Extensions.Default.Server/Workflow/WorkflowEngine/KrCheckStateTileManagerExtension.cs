using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LinqToDB.Data;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared.Workflow.WorkflowEngine;
using Tessa.Platform.Data;
using Tessa.Platform.Validation;
using Tessa.Workflow;

namespace Tessa.Extensions.Default.Server.Workflow.WorkflowEngine
{
    /// <summary>
    /// Расширение правил доступа для тайлов, которое проверяет Состояние документа для доступа к тайлу
    /// </summary>
    public sealed class KrCheckStateTileManagerExtension : IWorkflowEngineTileManagerExtension
    {
        #region Consts

        /// <summary>
        /// Card type identifier for "KrCheckStateWorkflowTileExtension": {9FCE6311-1746-412A-9346-77394CAEBE90}.
        /// </summary>
        public static readonly Guid KrCheckStateWorkflowTileExtensionTypeID = new Guid(0x9fce6311, 0x1746, 0x412a, 0x93, 0x46, 0x77, 0x39, 0x4c, 0xae, 0xbe, 0x90);

        /// <summary>
        /// Card type name for "KrCheckStateWorkflowTileExtension".
        /// </summary>
        public const string KrCheckStateWorkflowTileExtensionTypeName = "KrCheckStateWorkflowTileExtension";

        #endregion

        #region Fields

        private readonly IDbScope dbScope;

        #endregion

        #region Constructors

        public KrCheckStateTileManagerExtension(IDbScope dbScope)
        {
            this.dbScope = dbScope;
        }

        #endregion

        #region IWorkflowEngineTileManagerExtension Properties

        /// <inheritdoc />
        public Guid ExtensionTypeID => KrCheckStateWorkflowTileExtensionTypeID;

        /// <inheritdoc />
        public string Name => "$KrTileExtensions_CheckState";

        /// <inheritdoc />
        public Guid ID => KrTileManagerExtensionsHelper.KrCheckStateExtensionID;

        /// <inheritdoc />
        public int Order => 10;

        #endregion

        #region IWorkflowEngineTileManagerExtension Methods

        /// <inheritdoc />
        public async ValueTask<IEnumerable<Guid>> CheckTileAccessForVisibilityAsync(
            List<Guid> tileIDs,
            Card card,
            CancellationToken cancellationToken = default)
        {
            if (card == null)
            {
                return tileIDs;
            }

            return await this.GetAccessedTilesAsync(card, card.ID, cancellationToken, tileIDs.ToArray());
        }

        /// <inheritdoc />
        public async ValueTask<ValidationResult> CheckTileAccessForExecuteAsync(
            WorkflowEngineTile tileInfo,
            Guid cardID,
            Func<CancellationToken, ValueTask<Card>> cardGetterAsync,
            CancellationToken cancellationToken = default)
        {
            var result = await this.GetAccessedTilesAsync(null, cardID, cancellationToken, tileInfo.TileID);

            if (result.Count > 0)
            {
                return ValidationResult.Empty;
            }
            else
            {
                return ValidationResult.FromText(
                    this,
                    tileInfo.AccessDeniedMessage,
                    ValidationResultType.Error);
            }
        }

        #endregion

        #region Private Methods

        private async Task<IList<Guid>> GetAccessedTilesAsync(
            Card card,
            Guid cardID,
            CancellationToken cancellationToken = default,
            params Guid[] tileIDs)
        {
            await using (this.dbScope.Create())
            {
                var db = this.dbScope.Db;

                DataParameter? dpTileIDs;
                if (card is not null
                    && card.Sections.TryGetValue("KrApprovalCommonInfoVirtual", out var krSection)
                    && krSection.Fields.TryGetValue("StateID", out var stateIDObj))
                {
                    var stateID = stateIDObj == null ? 0 : (int)stateIDObj;

                    // запрос на проверку состояния карточки
                    return await db.SetCommand(
                        this.dbScope.BuilderFactory
                            .Select().C("bpbe", "ButtonRowID")
                            .From("KrCheckStateTileExtension", "s").NoLock()
                            .InnerJoin("BusinessProcessButtonExtension", "bpbe").NoLock()
                                .On().C("s", "ID").Equals().C("bpbe", "ID")
                            .Where().C("s", "StateID").Equals().P("StateID").And().C("bpbe", "ButtonRowID").InArray(tileIDs, "TileIDs", out dpTileIDs)
                            .Build(),
                        DataParameters.Get(
                            db.Parameter("StateID", stateID, LinqToDB.DataType.Int32),
                            dpTileIDs))
                        .LogCommand()
                        .ExecuteListAsync<Guid>(cancellationToken);

                }

                // запрос на проверку состояния карточки
                return await db.SetCommand(
                    this.dbScope.BuilderFactory
                        .Select().C("bpbe", "ButtonRowID")
                        .From("KrCheckStateTileExtension", "s").NoLock()
                        .InnerJoin("BusinessProcessButtonExtension", "bpbe").NoLock()
                            .On().C("bpbe", "ID").Equals().C("s", "ID")
                        .LeftJoin("KrApprovalCommonInfo", "kr").NoLock()
                            .On().C("kr", "MainCardID").Equals().P("CardID")
                        .Where().C("s", "StateID").Equals().Coalesce(b => b.C("kr", "StateID").V(0))
                            .And().C("bpbe", "ButtonRowID").InArray(tileIDs, "TitleIDs", out dpTileIDs)
                        .Build(),
                    DataParameters.Get(
                        db.Parameter("CardID", card?.ID ?? cardID, LinqToDB.DataType.Guid),
                        dpTileIDs))
                    .LogCommand()
                    .ExecuteListAsync<Guid>(cancellationToken);
            }
        }

        #endregion
    }
}
