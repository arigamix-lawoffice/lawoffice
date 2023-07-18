








using System;

namespace Tessa.Extensions.Shared.Info
{// ReSharper disable InconsistentNaming
    #region Administrator

    /// <summary>
    ///     ID: {ee693038-a5aa-4c3f-aa54-c230b4960d3f}
    ///     Alias: Administrator
    ///     Caption: $Workplaces_Administrator
    /// </summary>
    public class AdministratorWorkplaceInfo
    {
        #region Common

        /// <summary>
        ///     Workplace identifier for "$Workplaces_Administrator": {ee693038-a5aa-4c3f-aa54-c230b4960d3f}.
        /// </summary>
        public readonly Guid ID = new Guid(0xee693038,0xa5aa,0x4c3f,0xaa,0x54,0xc2,0x30,0xb4,0x96,0x0d,0x3f);

        /// <summary>
        ///     Workplace name for "$Workplaces_Administrator".
        /// </summary>
        public readonly string Alias = "Administrator";

        /// <summary>
        ///     Workplace Caption for "$Workplaces_Administrator".
        /// </summary>
        public readonly string Caption = "$Workplaces_Administrator";

        #endregion

        #region ToString

        public static implicit operator string(AdministratorWorkplaceInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region LawOffice

    /// <summary>
    ///     ID: {c323a4ab-c3ca-4369-906e-18d8845971ac}
    ///     Alias: LawOffice
    ///     Caption: $Workplaces_LawOffice
    /// </summary>
    public class LawOfficeWorkplaceInfo
    {
        #region Common

        /// <summary>
        ///     Workplace identifier for "$Workplaces_LawOffice": {c323a4ab-c3ca-4369-906e-18d8845971ac}.
        /// </summary>
        public readonly Guid ID = new Guid(0xc323a4ab,0xc3ca,0x4369,0x90,0x6e,0x18,0xd8,0x84,0x59,0x71,0xac);

        /// <summary>
        ///     Workplace name for "$Workplaces_LawOffice".
        /// </summary>
        public readonly string Alias = "LawOffice";

        /// <summary>
        ///     Workplace Caption for "$Workplaces_LawOffice".
        /// </summary>
        public readonly string Caption = "$Workplaces_LawOffice";

        #endregion

        #region ToString

        public static implicit operator string(LawOfficeWorkplaceInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region User

    /// <summary>
    ///     ID: {c3d72683-f6c0-4766-a3d4-1fd9a7fe6827}
    ///     Alias: User
    ///     Caption: $Workplaces_User
    /// </summary>
    public class UserWorkplaceInfo
    {
        #region Common

        /// <summary>
        ///     Workplace identifier for "$Workplaces_User": {c3d72683-f6c0-4766-a3d4-1fd9a7fe6827}.
        /// </summary>
        public readonly Guid ID = new Guid(0xc3d72683,0xf6c0,0x4766,0xa3,0xd4,0x1f,0xd9,0xa7,0xfe,0x68,0x27);

        /// <summary>
        ///     Workplace name for "$Workplaces_User".
        /// </summary>
        public readonly string Alias = "User";

        /// <summary>
        ///     Workplace Caption for "$Workplaces_User".
        /// </summary>
        public readonly string Caption = "$Workplaces_User";

        #endregion

        #region ToString

        public static implicit operator string(UserWorkplaceInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    public static class WorkplaceInfo
    {
        #region Workplaces

        public static readonly AdministratorWorkplaceInfo Administrator = new AdministratorWorkplaceInfo();
        public static readonly LawOfficeWorkplaceInfo LawOffice = new LawOfficeWorkplaceInfo();
        public static readonly UserWorkplaceInfo User = new UserWorkplaceInfo();

        #endregion
    }
}