using System;
using Tessa.Cards;

namespace Tessa.Extensions.Default.Shared
{
    /// <summary>
    /// Класс, в котором содержится информация по некоторым секциям и полям для карточек документов.
    /// </summary>
    public static class DefaultSchemeHelper
    {
        #region Constants

        /// <summary>
        /// Максимальная длина имени валюты <c>Currencies.Name</c>.
        /// </summary>
        public const int CurrencyNameMaxLength = 128;

        /// <summary>
        /// Максимальная длина имени категории файла <c>FileCategories.Name</c>.
        /// </summary>
        public const int FileCategoryNameMaxLength = 255;

        /// <summary>
        /// Максимальная длина имени контрагента <c>Partners.Name</c>.
        /// </summary>
        public const int PartnerNameMaxLength = 255;

        /// <summary>
        /// Максимальная длина имени типа протокола <c>ProtocolTypes.Name</c>.
        /// </summary>
        public const int ProtocolTypeNameMaxLength = 128;

        #endregion

        #region Static Fields

        /// <summary>
        /// Идентификатор секции <c>DocumentCommonInfo</c>.
        /// </summary>
        public static Guid DocumentCommonInfoSectionID =    // a161e289-2f99-4699-9e95-6e3336be8527
            new Guid(0xa161e289, 0x2f99, 0x4699, 0x9e, 0x95, 0x6e, 0x33, 0x36, 0xbe, 0x85, 0x27);

        /// <summary>
        /// Идентификатор секции <c>OutgoingRefDocs</c>.
        /// </summary>
        public static Guid OutgoingRefDocsSectionID =       // 73320234-fc44-4126-a7a6-5dd0bdaa4880
            new Guid(0x73320234, 0xfc44, 0x4126, 0xa7, 0xa6, 0x5d, 0xd0, 0xbd, 0xaa, 0x48, 0x80);

        /// <summary>
        /// Идентификатор секции <c>Performers</c>.
        /// </summary>
        public static Guid PerformersSectionID =            // d0f5547b-b2f5-4a08-8cd9-b34138d35125
            new Guid(0xd0f5547b, 0xb2f5, 0x4a08, 0x8c, 0xd9, 0xb3, 0x41, 0x38, 0xd3, 0x51, 0x25);

        /// <summary>
        /// Идентификатор секции <c>Recipients</c>.
        /// </summary>
        public static Guid RecipientsSectionID =            // 386509d9-4130-467f-9a52-0004aa15247e
            new Guid(0x386509d9, 0x4130, 0x467f, 0x9a, 0x52, 0x00, 0x04, 0xaa, 0x15, 0x24, 0x7e);

        /// <summary>
        /// Идентификатор комплексной колонки <c>DocumentCommonInfo.Partner</c>.
        /// </summary>
        public static Guid PartnerComplexColumnID =         // 22ed59ec-4939-4d96-a7a8-cbf0bda55ec0
            new Guid(0x22ed59ec, 0x4939, 0x4d96, 0xa7, 0xa8, 0xcb, 0xf0, 0xbd, 0xa5, 0x5e, 0xc0);

        /// <summary>
        /// Идентификатор комплексной колонки <c>DocumentCommonInfo.Author</c>.
        /// </summary>
        public static Guid AuthorComplexColumnID =          // aa152ba8-dc1f-4efa-8c68-03ba804ef6f1
            new Guid(0xaa152ba8, 0xdc1f, 0x4efa, 0x8c, 0x68, 0x03, 0xba, 0x80, 0x4e, 0xf6, 0xf1);

        /// <summary>
        /// Идентификатор комплексной колонки <c>DocumentCommonInfo.CardType</c>.
        /// </summary>
        public static Guid CardTypeComplexColumnID =         // 99037f7d-c376-4cb5-a06a-006fb96548d5
            new Guid(0x99037f7d, 0xc376, 0x4cb5, 0xa0, 0x6a, 0x00, 0x6f, 0xb9, 0x65, 0x48, 0xd5);

        /// <summary>
        /// Идентификатор физической колонки <c>DocumentCommonInfo.SecondaryNumber</c>.
        /// </summary>
        public static Guid SecondaryNumberColumnID =        // 46115821-4126-40ab-a14f-64f33d17d271
            new Guid(0x46115821, 0x4126, 0x40ab, 0xa1, 0x4f, 0x64, 0xf3, 0x3d, 0x17, 0xd2, 0x71);

        /// <summary>
        /// Идентификатор физической колонки <c>DocumentCommonInfo.SecondaryFullNumber</c>.
        /// </summary>
        public static Guid SecondaryFullNumberColumnID =    // e43c836d-63d7-460d-ab81-b5823383e150
            new Guid(0xe43c836d, 0x63d7, 0x460d, 0xab, 0x81, 0xb5, 0x82, 0x33, 0x83, 0xe1, 0x50);

        /// <summary>
        /// Идентификатор физической колонки <c>DocumentCommonInfo.SecondarySequence</c>.
        /// </summary>
        public static Guid SecondarySequenceColumnID =      // dadb1130-034d-493c-b7a0-a7c74eae078b
            new Guid(0xdadb1130, 0x034d, 0x493c, 0xb7, 0xa0, 0xa7, 0xc7, 0x4e, 0xae, 0x07, 0x8b);

        /// <summary>
        /// Идентификатор секции <c>KrAdditionalApprovalUsersCardVirtual</c>.
        /// </summary>
        public static Guid KrAdditionalApprovalUsersCardVirtual =  // fed14580-062d-4f30-a344-23c8d2a427d4
            new Guid(0xfed14580, 0x062d, 0x4f30, 0xa3, 0x44, 0x23, 0xc8, 0xd2, 0xa4, 0x27, 0xd4);

        /// <summary>
        /// Идентификатор секции <c>KrStages</c>.
        /// </summary>
        public static Guid KrStages =                       // 92caadca-2409-40ff-b7d8-1d4fd302b1e9
            new Guid(0x92caadca, 0x2409, 0x40ff, 0xb7, 0xd8, 0x1d, 0x4f, 0xd3, 0x02, 0xb1, 0xe9);

        /// <summary>
        /// Идентификатор секции <c>KrStagesVirtual</c>.
        /// </summary>
        public static readonly Guid KrStagesVirtual =
            new Guid(0x89d78d5c, 0xf8dd, 0x48e7, 0x86, 0x8c, 0x88, 0xbb, 0xaf, 0xe7, 0x42, 0x57);

        /// <summary>
        /// Идентификатор секции <c>KrStageTemplates</c>.
        /// </summary>
        public static Guid KrStageTemplates =               // 5a33ac72-f6f5-4e5a-8d8c-4a94ed7bf324
            new Guid(0x5a33ac72, 0xf6f5, 0x4e5a, 0x8d, 0x8c, 0x4a, 0x94, 0xed, 0x7b, 0xf3, 0x24);

        /// <summary>
        /// Идентификатор секции <c>KrSecondaryProcesses</c>.
        /// </summary>
        public static Guid KrSecondaryProcesses =               // caac66aa-0cbb-4e2b-83fd-7c368e814d64
            new Guid(0xCAAC66AA, 0x0CBB, 0x4E2B, 0x83, 0xFD, 0x7C, 0x36, 0x8E, 0x81, 0x4D, 0x64);


        /// <summary>
        /// Идентификатор секции <c>KrDocStates</c>.
        /// </summary>
        public static readonly Guid KrDocStateSectionID =
            new Guid(0x47107D7A, 0x3A8C, 0x47F0, 0xB8, 0x00, 0x2A, 0x45, 0xDA, 0x22, 0x2F, 0xF4);

        /// <summary>
        /// Идентификатор секции <c>KrStageStates</c>.
        /// </summary>
        public static readonly Guid KrStageStateSectionID =
            new Guid(0xBEEE4F3D, 0xA385, 0x4FC8, 0x88, 0x4F, 0xBC, 0x1C, 0xCF, 0x55, 0xFC, 0x5B);


        #endregion

        #region Static Methods

        /// <summary>
        /// Возвращает признак того, что в схеме типа карточки присутствуют все поля с Secondary-номером.
        /// </summary>
        /// <param name="cardType">Тип карточки, схему которого требуется проверить.</param>
        /// <returns>
        /// <c>true</c>, если в схеме типа карточки присутствуют все поля с Secondary-номером;
        /// <c>false</c> в противном случае.
        /// </returns>
        public static bool CardTypeHasSecondaryNumber(CardType cardType)
        {
            if (cardType == null)
            {
                throw new ArgumentNullException("cardType");
            }

            foreach (CardTypeSchemeItem schemeItem in cardType.SchemeItems)
            {
                if (schemeItem.SectionID == DocumentCommonInfoSectionID)
                {
                    int secondaryColumns = 0;
                    foreach (Guid columnID in schemeItem.ColumnIDList)
                    {
                        if (columnID == SecondaryNumberColumnID
                            || columnID == SecondaryFullNumberColumnID
                            || columnID == SecondarySequenceColumnID)
                        {
                            // если это третья колонка из искомых, то выходим с true
                            if (secondaryColumns == 2)
                            {
                                return true;
                            }

                            secondaryColumns++;
                        }
                    }

                    return false;
                }
            }

            return false;
        }

        #endregion
    }
}
