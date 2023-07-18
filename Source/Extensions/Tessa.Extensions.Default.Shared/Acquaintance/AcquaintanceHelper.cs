using System;
using System.Collections;
using System.Collections.Generic;
using Tessa.Cards;
using Tessa.Platform;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Shared.Acquaintance
{
    public static class AcquaintanceHelper
    {
        #region Constants

        public const string DefaultRolesKey = CardHelper.SystemKeyPrefix + "DefaultRoles";
        public const string DefaultRolesIDListKey = "IDList";
        public const string DefaultRolesNameListKey = "NameList";

        public const string RolesKey = CardHelper.SystemKeyPrefix + "Roles";
        public const string CommentKey = CardHelper.SystemKeyPrefix + "Comment";
        public const string ExcludeDeputiesKey = CardHelper.SystemKeyPrefix + "ExcludeDeputies";
        public const string PlaceholderAliasesKey = CardHelper.SystemKeyPrefix + "PlaceholderAliases";
        public const string AddSuccessMessageKey = CardHelper.SystemKeyPrefix + "AddSuccessMessage";
        public const string AdditionalInfoKey = CardHelper.SystemKeyPrefix + "AdditionalInfo";

        #endregion

        #region Static Methods

        /// <summary>
        /// Получение ролей по умолчанию для массового ознакомления
        /// </summary>
        /// <param name="info">Info с данными</param>
        /// <returns>Список ролей с идентификаторами</returns>
        public static List<Tuple<Guid, string>> GetAcquaintanceDefaultRoles(Dictionary<string, object> info)
        {
            var result = new List<Tuple<Guid, string>>();

            if (info.ContainsKey(DefaultRolesKey))
            {
                if (info[DefaultRolesKey] is Dictionary<string, object> defaultRoles
                    && defaultRoles.TryGetValue(DefaultRolesIDListKey, out object idsListValue)
                    && idsListValue is IList idsList
                    && defaultRoles.TryGetValue(DefaultRolesNameListKey, out object namesListValue)
                    && namesListValue is IList namesList)
                {
                    if (idsList.Count != namesList.Count)
                    {
                        return result;
                    }

                    for (int i = 0; i < idsList.Count; i++)
                    {
                        var id = idsList[i];
                        var name = namesList[i];

                        result.Add(new Tuple<Guid, string>((Guid)id, (string)name));
                    }
                }

            }
            return result;
        }


        public static void TryGetAcquaintanceInfo(
            Dictionary<string, object> info,
            out List<Guid> roleList,
            out string comment,
            out bool excludeDeputies,
            out string placeholderAliases,
            out Dictionary<string, object> additionalInfo,
            out bool addSuccessMessage)
        {
            roleList = info.TryGet<List<Guid>>(RolesKey);
            comment = info.TryGet<string>(CommentKey);
            excludeDeputies = info.TryGet<bool>(ExcludeDeputiesKey);
            placeholderAliases = info.TryGet<string>(PlaceholderAliasesKey);
            additionalInfo = info.TryGet<Dictionary<string, object>>(AdditionalInfoKey);
            addSuccessMessage = info.TryGet<bool>(AddSuccessMessageKey);
        }


        public static void SetAcquaintanceInfo(
            CardRequest request,
            IReadOnlyList<Guid> roleIDList,
            string comment,
            bool excludeDeputies,
            string placeholderAliases,
            Dictionary<string, object> info,
            bool addSuccessMessage)
        {
            request.Info[CommentKey] = comment;
            request.Info[RolesKey] = roleIDList;
            request.Info[ExcludeDeputiesKey] = excludeDeputies;
            request.Info[PlaceholderAliasesKey] = placeholderAliases;
            request.Info[AdditionalInfoKey] = info;
            request.Info[AddSuccessMessageKey] = BooleanBoxes.Box(addSuccessMessage);
        }


        public static void SetAcquaintanceInfo(
            CardRequest request,
            IReadOnlyList<string> roleIDList,
            string comment,
            bool excludeDeputies,
            string placeholderAliases,
            Dictionary<string, object> info,
            bool addSuccessMessage)
        {
            request.Info[CommentKey] = comment;
            request.Info[RolesKey] = roleIDList;
            request.Info[ExcludeDeputiesKey] = excludeDeputies;
            request.Info[PlaceholderAliasesKey] = placeholderAliases;
            request.Info[AdditionalInfoKey] = info;
            request.Info[AddSuccessMessageKey] = BooleanBoxes.Box(addSuccessMessage);
        }

        #endregion
    }
}
