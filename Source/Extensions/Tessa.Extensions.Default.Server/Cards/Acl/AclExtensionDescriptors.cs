using System;
using Tessa.Roles.Acl.Extensions;

namespace Tessa.Extensions.Default.Server.Cards.Acl
{
    /// <summary>
    /// Дескрипторы для расширений правил расчёта ACL.
    /// </summary>
    public static class AclExtensionDescriptors
    {
        /// <summary>
        /// Дескриптор для расширения правил расчёта ACL, проверяющего состояния.
        /// </summary>
        public static readonly AclGenerationRuleExtensionDescriptor KrDocStates = new AclGenerationRuleExtensionDescriptor
        {
            DialogTypeID = new Guid("cca319ab-ed7a-4715-9b56-d0d5bd9d41ab"),
            ID = new Guid("cca319ab-ed7a-4715-9b56-d0d5bd9d41ab"),
            Name = "$AclExtensions_CheckDocState",
        };
    }
}
