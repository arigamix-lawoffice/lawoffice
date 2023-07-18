import { DotNetType } from "tessa/platform";
import { SchemeInfo } from "../info/schemeInfo";
import { ViewInfo } from "../info/viewInfo";

/**
 * Хелпер для работы с карточкой "Дело"
 */
export abstract class LawCaseHelper {

    /**
     * Маппинг полей при инициализации компаний
     */
    public static partnerInitializeMapping = new Map<string, [string, DotNetType]>([
        [SchemeInfo.LawPartnersDialogVirtual.ID, [SchemeInfo.LawPartners.PartnerID, DotNetType.Guid]],
        [SchemeInfo.LawPartnersDialogVirtual.AddressID, [SchemeInfo.LawPartners.PartnerAddressID, DotNetType.Guid]],
        [SchemeInfo.LawPartnersDialogVirtual.Name, [SchemeInfo.LawPartners.PartnerName, DotNetType.String]],
        [SchemeInfo.LawPartnersDialogVirtual.TaxNumber, [SchemeInfo.LawPartners.PartnerTaxNumber, DotNetType.String]],
        [SchemeInfo.LawPartnersDialogVirtual.RegistrationNumber, [SchemeInfo.LawPartners.PartnerRegistrationNumber, DotNetType.String]],
        [SchemeInfo.LawPartnersDialogVirtual.Contacts, [SchemeInfo.LawPartners.PartnerContacts, DotNetType.String]],
        [SchemeInfo.LawPartnersDialogVirtual.Street, [SchemeInfo.LawPartners.PartnerStreet, DotNetType.String]],
        [SchemeInfo.LawPartnersDialogVirtual.PostalCode, [SchemeInfo.LawPartners.PartnerPostalCode, DotNetType.String]],
        [SchemeInfo.LawPartnersDialogVirtual.City, [SchemeInfo.LawPartners.PartnerCity, DotNetType.String]],
        [SchemeInfo.LawPartnersDialogVirtual.Country, [SchemeInfo.LawPartners.PartnerCountry, DotNetType.String]],
        [SchemeInfo.LawPartnersDialogVirtual.PoBox, [SchemeInfo.LawPartners.PartnerPoBox, DotNetType.String]]
    ]);

    /**
     * Маппинг полей при добавлении новых компаний
     */
    public static partnerInsertMapping = new Map<string, [string, DotNetType]>([
        [SchemeInfo.LawPartners.PartnerID, [SchemeInfo.LawPartnersDialogVirtual.ID, DotNetType.Guid]],
        [SchemeInfo.LawPartners.PartnerName, [SchemeInfo.LawPartnersDialogVirtual.Name, DotNetType.String]],
        [SchemeInfo.LawPartners.PartnerTaxNumber, [SchemeInfo.LawPartnersDialogVirtual.TaxNumber, DotNetType.String]],
        [SchemeInfo.LawPartners.PartnerRegistrationNumber, [SchemeInfo.LawPartnersDialogVirtual.RegistrationNumber, DotNetType.String]],
        [SchemeInfo.LawPartners.PartnerContacts, [SchemeInfo.LawPartnersDialogVirtual.Contacts, DotNetType.String]],
        [SchemeInfo.LawPartners.PartnerStreet, [SchemeInfo.LawPartnersDialogVirtual.Street, DotNetType.String]],
        [SchemeInfo.LawPartners.PartnerPostalCode, [SchemeInfo.LawPartnersDialogVirtual.PostalCode, DotNetType.String]],
        [SchemeInfo.LawPartners.PartnerCity, [SchemeInfo.LawPartnersDialogVirtual.City, DotNetType.String]],
        [SchemeInfo.LawPartners.PartnerCountry, [SchemeInfo.LawPartnersDialogVirtual.Country, DotNetType.String]],
        [SchemeInfo.LawPartners.PartnerPoBox, [SchemeInfo.LawPartnersDialogVirtual.PoBox, DotNetType.String]]
    ]);

    /**
     * Маппинг полей при изменении компаний
     */
    public static partnerModifyMapping = new Map<string, [string, DotNetType]>([
        [SchemeInfo.LawPartners.PartnerID, [SchemeInfo.LawPartnersDialogVirtual.ID, DotNetType.Guid]],
        [SchemeInfo.LawPartners.PartnerName, [SchemeInfo.LawPartnersDialogVirtual.Name, DotNetType.String]],
        [SchemeInfo.LawPartners.PartnerTaxNumber, [SchemeInfo.LawPartnersDialogVirtual.TaxNumber, DotNetType.String]],
        [SchemeInfo.LawPartners.PartnerRegistrationNumber, [SchemeInfo.LawPartnersDialogVirtual.RegistrationNumber, DotNetType.String]],
        [SchemeInfo.LawPartners.PartnerContacts, [SchemeInfo.LawPartnersDialogVirtual.Contacts, DotNetType.String]],
        [SchemeInfo.LawPartners.PartnerStreet, [SchemeInfo.LawPartnersDialogVirtual.Street, DotNetType.String]],
        [SchemeInfo.LawPartners.PartnerPostalCode, [SchemeInfo.LawPartnersDialogVirtual.PostalCode, DotNetType.String]],
        [SchemeInfo.LawPartners.PartnerCity, [SchemeInfo.LawPartnersDialogVirtual.City, DotNetType.String]],
        [SchemeInfo.LawPartners.PartnerCountry, [SchemeInfo.LawPartnersDialogVirtual.Country, DotNetType.String]],
        [SchemeInfo.LawPartners.PartnerPoBox, [SchemeInfo.LawPartnersDialogVirtual.PoBox, DotNetType.String]],
        [SchemeInfo.LawPartners.PartnerAddressID, [SchemeInfo.LawPartnersDialogVirtual.AddressID, DotNetType.Guid]]
    ]);

    /**
     * Маппинг полей при инициализации представителей компаний
     */
    public static partnerRepInitializeMapping = new Map<string, [string, DotNetType]>([
        [SchemeInfo.LawPartnersDialogVirtual.ID, [SchemeInfo.LawPartnerRepresentatives.RepresentativeID, DotNetType.Guid]],
        [SchemeInfo.LawPartnersDialogVirtual.AddressID, [SchemeInfo.LawPartnerRepresentatives.RepresentativeAddressID, DotNetType.Guid]],
        [SchemeInfo.LawPartnersDialogVirtual.Name, [SchemeInfo.LawPartnerRepresentatives.RepresentativeName, DotNetType.String]],
        [SchemeInfo.LawPartnersDialogVirtual.TaxNumber, [SchemeInfo.LawPartnerRepresentatives.RepresentativeTaxNumber, DotNetType.String]],
        [SchemeInfo.LawPartnersDialogVirtual.RegistrationNumber, [SchemeInfo.LawPartnerRepresentatives.RepresentativeRegistrationNumber, DotNetType.String]],
        [SchemeInfo.LawPartnersDialogVirtual.Contacts, [SchemeInfo.LawPartnerRepresentatives.RepresentativeContacts, DotNetType.String]],
        [SchemeInfo.LawPartnersDialogVirtual.Street, [SchemeInfo.LawPartnerRepresentatives.RepresentativeStreet, DotNetType.String]],
        [SchemeInfo.LawPartnersDialogVirtual.PostalCode, [SchemeInfo.LawPartnerRepresentatives.RepresentativePostalCode, DotNetType.String]],
        [SchemeInfo.LawPartnersDialogVirtual.City, [SchemeInfo.LawPartnerRepresentatives.RepresentativeCity, DotNetType.String]],
        [SchemeInfo.LawPartnersDialogVirtual.Country, [SchemeInfo.LawPartnerRepresentatives.RepresentativeCountry, DotNetType.String]],
        [SchemeInfo.LawPartnersDialogVirtual.PoBox, [SchemeInfo.LawPartnerRepresentatives.RepresentativePoBox, DotNetType.String]]
    ]);

    /**
     * Маппинг полей при добавлении представителей компаний
     */
    public static partnerRepInsertMapping = new Map<string, [string, DotNetType]>([
        [SchemeInfo.LawPartnerRepresentatives.RepresentativeID, [SchemeInfo.LawPartnersDialogVirtual.ID, DotNetType.Guid]],
        [SchemeInfo.LawPartnerRepresentatives.RepresentativeName, [SchemeInfo.LawPartnersDialogVirtual.Name, DotNetType.String]],
        [SchemeInfo.LawPartnerRepresentatives.RepresentativeTaxNumber, [SchemeInfo.LawPartnersDialogVirtual.TaxNumber, DotNetType.String]],
        [SchemeInfo.LawPartnerRepresentatives.RepresentativeRegistrationNumber, [SchemeInfo.LawPartnersDialogVirtual.RegistrationNumber, DotNetType.String]],
        [SchemeInfo.LawPartnerRepresentatives.RepresentativeContacts, [SchemeInfo.LawPartnersDialogVirtual.Contacts, DotNetType.String]],
        [SchemeInfo.LawPartnerRepresentatives.RepresentativeStreet, [SchemeInfo.LawPartnersDialogVirtual.Street, DotNetType.String]],
        [SchemeInfo.LawPartnerRepresentatives.RepresentativePostalCode, [SchemeInfo.LawPartnersDialogVirtual.PostalCode, DotNetType.String]],
        [SchemeInfo.LawPartnerRepresentatives.RepresentativeCity, [SchemeInfo.LawPartnersDialogVirtual.City, DotNetType.String]],
        [SchemeInfo.LawPartnerRepresentatives.RepresentativeCountry, [SchemeInfo.LawPartnersDialogVirtual.Country, DotNetType.String]],
        [SchemeInfo.LawPartnerRepresentatives.RepresentativePoBox, [SchemeInfo.LawPartnersDialogVirtual.PoBox, DotNetType.String]]
    ]);

    /**
     * Маппинг полей при изменении представителей компаний
     */
    public static partnerRepModifyMapping = new Map<string, [string, DotNetType]>([
        [SchemeInfo.LawPartnerRepresentatives.RepresentativeID, [SchemeInfo.LawPartnersDialogVirtual.ID, DotNetType.Guid]],
        [SchemeInfo.LawPartnerRepresentatives.RepresentativeName, [SchemeInfo.LawPartnersDialogVirtual.Name, DotNetType.String]],
        [SchemeInfo.LawPartnerRepresentatives.RepresentativeTaxNumber, [SchemeInfo.LawPartnersDialogVirtual.TaxNumber, DotNetType.String]],
        [SchemeInfo.LawPartnerRepresentatives.RepresentativeRegistrationNumber, [SchemeInfo.LawPartnersDialogVirtual.RegistrationNumber, DotNetType.String]],
        [SchemeInfo.LawPartnerRepresentatives.RepresentativeContacts, [SchemeInfo.LawPartnersDialogVirtual.Contacts, DotNetType.String]],
        [SchemeInfo.LawPartnerRepresentatives.RepresentativeStreet, [SchemeInfo.LawPartnersDialogVirtual.Street, DotNetType.String]],
        [SchemeInfo.LawPartnerRepresentatives.RepresentativePostalCode, [SchemeInfo.LawPartnersDialogVirtual.PostalCode, DotNetType.String]],
        [SchemeInfo.LawPartnerRepresentatives.RepresentativeCity, [SchemeInfo.LawPartnersDialogVirtual.City, DotNetType.String]],
        [SchemeInfo.LawPartnerRepresentatives.RepresentativeCountry, [SchemeInfo.LawPartnersDialogVirtual.Country, DotNetType.String]],
        [SchemeInfo.LawPartnerRepresentatives.RepresentativePoBox, [SchemeInfo.LawPartnersDialogVirtual.PoBox, DotNetType.String]],
        [SchemeInfo.LawPartnerRepresentatives.RepresentativeAddressID, [SchemeInfo.LawPartnersDialogVirtual.AddressID, DotNetType.Guid]]
    ]);

    /**
     * Маппинг полей при добавлении компаний из справочника
     */
    public static partnerDictionaryMapping = new Map<string, [string, DotNetType]>([
        [SchemeInfo.LawPartnersDialogVirtual.AddressID, [ViewInfo.LawPartners.ColumnPartnerAddressID.Alias, DotNetType.Guid]],
        [SchemeInfo.LawPartnersDialogVirtual.Name, [ViewInfo.LawPartners.ColumnPartnerName.Alias, DotNetType.String]],
        [SchemeInfo.LawPartnersDialogVirtual.TaxNumber, [ViewInfo.LawPartners.ColumnPartnerTaxNumber.Alias, DotNetType.String]],
        [SchemeInfo.LawPartnersDialogVirtual.RegistrationNumber, [ViewInfo.LawPartners.ColumnPartnerRegistrationNumber.Alias, DotNetType.String]],
        [SchemeInfo.LawPartnersDialogVirtual.Contacts, [ViewInfo.LawPartners.ColumnPartnerContacts.Alias, DotNetType.String]],
        [SchemeInfo.LawPartnersDialogVirtual.Street, [ViewInfo.LawPartners.ColumnPartnerStreet.Alias, DotNetType.String]],
        [SchemeInfo.LawPartnersDialogVirtual.PostalCode, [ViewInfo.LawPartners.ColumnPartnerPostalCode.Alias, DotNetType.String]],
        [SchemeInfo.LawPartnersDialogVirtual.City, [ViewInfo.LawPartners.ColumnPartnerCity.Alias, DotNetType.String]],
        [SchemeInfo.LawPartnersDialogVirtual.Country, [ViewInfo.LawPartners.ColumnPartnerCountry.Alias, DotNetType.String]],
        [SchemeInfo.LawPartnersDialogVirtual.PoBox, [ViewInfo.LawPartners.ColumnPartnerPoBox.Alias, DotNetType.String]]
    ]);

    /**
     * Маппинг полей при добавлении представителей из справочника
     */
    public static partnerRepDictionaryMapping = new Map<string, [string, DotNetType]>([
        [SchemeInfo.LawPartnersDialogVirtual.AddressID, [ViewInfo.LawPartnerRepresentatives.ColumnRepresentativeAddressID.Alias, DotNetType.Guid]],
        [SchemeInfo.LawPartnersDialogVirtual.Name, [ViewInfo.LawPartnerRepresentatives.ColumnRepresentativeName.Alias, DotNetType.String]],
        [SchemeInfo.LawPartnersDialogVirtual.TaxNumber, [ViewInfo.LawPartnerRepresentatives.ColumnRepresentativeTaxNumber.Alias, DotNetType.String]],
        [SchemeInfo.LawPartnersDialogVirtual.RegistrationNumber, [ViewInfo.LawPartnerRepresentatives.ColumnRepresentativeRegistrationNumber.Alias, DotNetType.String]],
        [SchemeInfo.LawPartnersDialogVirtual.Contacts, [ViewInfo.LawPartnerRepresentatives.ColumnRepresentativeContacts.Alias, DotNetType.String]],
        [SchemeInfo.LawPartnersDialogVirtual.Street, [ViewInfo.LawPartnerRepresentatives.ColumnRepresentativeStreet.Alias, DotNetType.String]],
        [SchemeInfo.LawPartnersDialogVirtual.PostalCode, [ViewInfo.LawPartnerRepresentatives.ColumnRepresentativePostalCode.Alias, DotNetType.String]],
        [SchemeInfo.LawPartnersDialogVirtual.City, [ViewInfo.LawPartnerRepresentatives.ColumnRepresentativeCity.Alias, DotNetType.String]],
        [SchemeInfo.LawPartnersDialogVirtual.Country, [ViewInfo.LawPartnerRepresentatives.ColumnRepresentativeCountry.Alias, DotNetType.String]],
        [SchemeInfo.LawPartnersDialogVirtual.PoBox, [ViewInfo.LawPartnerRepresentatives.ColumnRepresentativePoBox.Alias, DotNetType.String]]
    ]);
}