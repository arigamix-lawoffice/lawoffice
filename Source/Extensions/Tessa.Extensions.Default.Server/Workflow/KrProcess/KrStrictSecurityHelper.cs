using System.Collections.ObjectModel;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess
{
    public static class KrStrictSecurityHelper
    {
        #region KrStageTemplate

        public static readonly ReadOnlyCollection<string> KrStageTemplateFields =
            new ReadOnlyCollection<string>(
                new[]
                {
                    KrConstants.KrStageTemplates.SqlCondition,
                    KrConstants.KrStageTemplates.SourceCondition,
                    KrConstants.KrStageTemplates.SourceBefore,
                    KrConstants.KrStageTemplates.SourceAfter,
                });

        public static readonly ReadOnlyCollection<string> KrStageTemplateTables =
            new ReadOnlyCollection<string>(
                new[]
                {
                    KrConstants.KrStages.Name,
                    KrConstants.KrStages.Virtual,
                });

        public static readonly ReadOnlyCollection<string> KrStageTemplateTableFields =
            new ReadOnlyCollection<string>(
                new[]
                {
                    KrConstants.KrStages.RuntimeSqlCondition,
                    KrConstants.KrStages.RuntimeSourceCondition,
                    KrConstants.KrStages.RuntimeSourceBefore,
                    KrConstants.KrStages.RuntimeSourceAfter,
                    KrConstants.KrStages.SqlApproverRole,
                });

        #endregion

        #region KrStageGroup

        public static readonly ReadOnlyCollection<string> KrStageGroupFields =
            new ReadOnlyCollection<string>(
                new[]
                {
                    KrConstants.KrStageGroups.SqlCondition,
                    KrConstants.KrStageGroups.SourceCondition,
                    KrConstants.KrStageGroups.SourceBefore,
                    KrConstants.KrStageGroups.SourceAfter,
                    KrConstants.KrStageGroups.RuntimeSqlCondition,
                    KrConstants.KrStageGroups.RuntimeSourceCondition,
                    KrConstants.KrStageGroups.RuntimeSourceBefore,
                    KrConstants.KrStageGroups.RuntimeSourceAfter,
                });

        #endregion

        #region KrProcessButton

        public static readonly ReadOnlyCollection<string> KrSecondaryProcessFields =
            new ReadOnlyCollection<string>(
                new[]
                {
                    KrConstants.KrSecondaryProcesses.ExecutionSourceCondition,
                    KrConstants.KrSecondaryProcesses.ExecutionSqlCondition,
                    KrConstants.KrSecondaryProcesses.VisibilitySourceCondition,
                    KrConstants.KrSecondaryProcesses.VisibilitySqlCondition,
                });
        
        public static readonly ReadOnlyCollection<string> KrSecondaryProcessTables =
            new ReadOnlyCollection<string>(
                new[]
                {
                    KrConstants.KrStages.Name,
                    KrConstants.KrStages.Virtual,
                });

        public static readonly ReadOnlyCollection<string> KrSecondaryProcessTableFields =
            new ReadOnlyCollection<string>(
                new[]
                {
                    KrConstants.KrStages.RuntimeSqlCondition,
                    KrConstants.KrStages.RuntimeSourceCondition,
                    KrConstants.KrStages.RuntimeSourceBefore,
                    KrConstants.KrStages.RuntimeSourceAfter,
                    KrConstants.KrStages.SqlApproverRole,
                });
        
        #endregion

        #region KrStageCommonMethod

        public const string KrStageCommonMethodSectionName = KrConstants.KrStageCommonMethods.Name;

        public const string KrStageCommonMethodFieldName = KrConstants.KrStageCommonMethods.Source;

        #endregion  
    }
}
