using System.Collections.Generic;
using Tessa.Extensions.Default.Server.Workflow.KrObjectModel;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers.UserAPI
{
    // ReSharper disable once InconsistentNaming
    public static class UserAPIIOHelper
    {
        public static void Show(IKrScript script, object obj)
        {
            ValidationSequence
                .Begin(script.ValidationResult)
                .SetObjectName(script)
                .InfoText(obj?.ToString() ?? "null")
                .End();
        }

        public static void Show(IKrScript script, string message, string details = "")
        {
            ValidationSequence
                .Begin(script.ValidationResult)
                .SetObjectName(script)
                .InfoDetails(message, details)
                .End();
        }

        public static void Show(IKrScript script, Stage stage)
        {
            ValidationSequence
                .Begin(script.ValidationResult)
                .SetObjectName(script)
                .InfoText(stage.ToStringSummary(), stage.ToStringDetailed())
                .End();
        }

        public static void Show(IKrScript script, IEnumerable<Stage> stages)
        {
            foreach (var stage in stages)
            {
                ValidationSequence
                    .Begin(script.ValidationResult)
                    .SetObjectName(script)
                    .InfoDetails(stage.ToStringSummary(), stage.ToStringDetailed())
                    .End();
            }
        }

        public static void Show(IKrScript script, Performer performer)
        {
            ValidationSequence
                .Begin(script.ValidationResult)
                .SetObjectName(script)
                .InfoText(performer.ToStringSummary(), performer.ToStringDetailed())
                .End();
        }

        public static void Show(IKrScript script, IEnumerable<Performer> performers)
        {
            foreach (var approver in performers)
            {
                ValidationSequence
                    .Begin(script.ValidationResult)
                    .SetObjectName(script)
                    .InfoDetails(approver.ToStringSummary(), approver.ToStringDetailed())
                    .End();
            }
        }

        public static void Show(IKrScript script, IDictionary<string, object> storage)
        {
            ValidationSequence
                .Begin(script.ValidationResult)
                .SetObjectName(script)
                .InfoText(StorageHelper.Print(storage))
                .End();
        }

        public static void Show(IKrScript script, IStorageDictionaryProvider storage)
        {
            ValidationSequence
                .Begin(script.ValidationResult)
                .SetObjectName(script)
                .InfoText(StorageHelper.PrintObject(storage))
                .End();
        }

        public static void AddError(IKrScript script, string text) => script.ValidationResult.AddError(script, text);

        public static void AddWarning(IKrScript script, string text) => script.ValidationResult.AddWarning(script, text);

        public static void AddInfo(IKrScript script, string text) => script.ValidationResult.AddInfo(script, text);
    }
}