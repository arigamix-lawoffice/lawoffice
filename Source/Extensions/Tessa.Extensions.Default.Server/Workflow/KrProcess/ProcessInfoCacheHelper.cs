using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Tessa.Cards;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Serialization;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess
{
    public static class ProcessInfoCacheHelper
    {
        private const string CachedProcessInfo = CardHelper.SystemKeyPrefix + nameof(CachedProcessInfo);

        public static ISerializableObject Get(
            IKrStageSerializer serializer,
            Card satellite)
        {
            Check.ArgumentNotNull(serializer, nameof(serializer));
            Check.ArgumentNotNull(satellite, nameof(satellite));

            AssertKrSatellite(satellite);
            var processInfo = GetCachedProcessInfo(satellite);
            if (processInfo is null)
            {
                processInfo = new SerializableObject();
                var storage = serializer.GetProcessInfo(satellite) as Dictionary<string, object>
                    ?? new Dictionary<string, object>(StringComparer.Ordinal);
                processInfo.SetStorage(storage);
                SetCachedProcessInfo(satellite, processInfo);
            }
            return processInfo;
        }

        public static bool Update(
            IKrStageSerializer serializer,
            Card satellite)
        {
            Check.ArgumentNotNull(serializer, nameof(serializer));
            Check.ArgumentNotNull(satellite, nameof(satellite));

            AssertKrSatellite(satellite);
            var processInfo = GetCachedProcessInfo(satellite);
            if (processInfo is null
                || !processInfo.IsModified())
            {
                return false;
            }
            processInfo.SetModified(false);
            UpdateProcessInfo(serializer, satellite, processInfo);
            return true;
        }

        public static void Reset(
            Card satellite)
        {
            Check.ArgumentNotNull(satellite, nameof(satellite));

            AssertKrSatellite(satellite);
            RemoveProcessInfo(satellite);
        }

        private static ISerializableObject GetCachedProcessInfo(
            Card card)
        {
            return card.Info.TryGet<ISerializableObject>(CachedProcessInfo);
        }

        private static void SetCachedProcessInfo(
            Card card,
            ISerializableObject cachedProcessInfo)
        {
            card.Info[CachedProcessInfo] = cachedProcessInfo;
        }

        private static IDictionary<string, object> GetProcessInfo(
            this IKrStageSerializer serializer,
            Card satellite)
        {
            if (!satellite.TryGetKrApprovalCommonInfoSection(out var aci)
                || !aci.RawFields.TryGetValue(KrConstants.KrApprovalCommonInfo.Info, out var infoObj)
                || !(infoObj is string infoJson))
            {
                return null;
            }

            var info = serializer.Deserialize<Dictionary<string, object>>(infoJson);
            return info;
        }

        private static void UpdateProcessInfo(
            this IKrStageSerializer serializer,
            Card satellite,
            IDictionary<string, object> info)
        {
            if (satellite.TryGetKrApprovalCommonInfoSection(out var aci))
            {
                aci.Fields[KrConstants.KrApprovalCommonInfo.Info] = serializer.Serialize(info);
            }
        }

        private static void RemoveProcessInfo(
            Card satellite)
        {
            if (satellite.TryGetKrApprovalCommonInfoSection(out var aci))
            {
                aci.Fields.Remove(KrConstants.KrApprovalCommonInfo.Info);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void AssertKrSatellite(
            Card satellite)
        {
            var typeID = satellite.TypeID;
            if (typeID != DefaultCardTypes.KrSatelliteTypeID
                && typeID != DefaultCardTypes.KrSecondarySatelliteTypeID)
            {
                throw new ArgumentException($"{nameof(satellite)} is not satellite.");
            }
        }
    }
}