using System;
using System.Collections.Generic;

namespace Tessa.Extensions.Default.Shared
{
    /// <summary>
    /// Алиасы некоторых системных представлений типового решения.
    /// </summary>
    public static class DefaultViewAliases
    {
        #region Constants

        /// <summary>
        /// Состояния документов.
        /// </summary>
        public const string KrDocStateCards = "KrDocStateCards";

        #endregion

        #region Static Methods
        
        public static bool CanDeleteCard(string viewAlias) => !viewsWithoutDelete.Contains(viewAlias);

        // ReSharper disable once CollectionNeverUpdated.Local
        private static readonly HashSet<string> viewsWithoutDelete =
            new HashSet<string>(StringComparer.Ordinal)
            {
                KrDocStateCards,
            };
        
        
        public static bool CanExportCard(string viewAlias) => !viewsWithoutExport.Contains(viewAlias);

        private static readonly HashSet<string> viewsWithoutExport =
            new HashSet<string>(StringComparer.Ordinal)
            {
                KrDocStateCards,
            };
                
        
        public static bool CanViewCardStorage(string viewAlias) => !viewsWithoutViewStorage.Contains(viewAlias);

        private static readonly HashSet<string> viewsWithoutViewStorage =
            new HashSet<string>(StringComparer.Ordinal)
            {
                KrDocStateCards,
            };

        #endregion
    }
}