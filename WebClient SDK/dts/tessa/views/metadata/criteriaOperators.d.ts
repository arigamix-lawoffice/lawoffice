import { SchemeDbType } from 'tessa/platform/schemeDbType';
export declare enum CriteriaOperatorConst {
    Equality = "Equality",
    NonEquality = "NonEquality",
    GreatThan = "GreatThan",
    LessThan = "LessThan",
    GreatOrEquals = "GreatOrEquals",
    LessOrEquals = "LessOrEquals",
    IsNull = "IsNull",
    IsNotNull = "IsNotNull",
    Between = "Between",
    Contains = "Contains",
    StartWith = "StartWith",
    EndWith = "EndWith",
    IsTrue = "IsTrue",
    IsFalse = "IsFalse",
    Link = "Link"
}
export interface CriteriaOperator {
    name: string;
    text: string;
    valuesCount: 0 | 1 | 2;
}
export declare const equalsCriteriaOperator: () => CriteriaOperator;
export declare const nonEqualsCriteriaOperator: () => CriteriaOperator;
export declare const greatThanCriteriaOperator: () => CriteriaOperator;
export declare const lessThanCriteriaOperator: () => CriteriaOperator;
export declare const greatOrEqualsCriteriaOperator: () => CriteriaOperator;
export declare const lessOrEqualsCriteriaOperator: () => CriteriaOperator;
export declare const isNullCriteriaOperator: () => CriteriaOperator;
export declare const isNotNullCriteriaOperator: () => CriteriaOperator;
export declare const betweenCriteriaOperator: () => CriteriaOperator;
export declare const containsCriteriaOperator: () => CriteriaOperator;
export declare const startsWithCriteriaOperator: () => CriteriaOperator;
export declare const endsWithCriteriaOperator: () => CriteriaOperator;
export declare const isTrueCriteriaOperator: () => CriteriaOperator;
export declare const isFalseCriteriaOperator: () => CriteriaOperator;
export declare const linkCriteriaOperator: () => CriteriaOperator;
export declare function getCriteria(name: string): CriteriaOperator;
export declare function getCriteriaCaption(name: string): string;
export declare const getCriteriasByType: (type: SchemeDbType) => CriteriaOperator[];
export declare const getNumericCriterias: () => CriteriaOperator[];
export declare const getStringCriterias: () => CriteriaOperator[];
export declare const getLogicalCriterias: () => CriteriaOperator[];
export declare const getDateTimeCriterias: () => CriteriaOperator[];
export declare const getIdentifiersCriterias: () => CriteriaOperator[];
export declare const getBinaryCriterias: () => CriteriaOperator[];
export declare const isOnlyDateType: (type: SchemeDbType) => boolean;
export declare const isOnlyTimeType: (type: SchemeDbType) => boolean;
export declare const isDateAndTimeType: (type: SchemeDbType) => boolean;
export declare const isDateTimeType: (type: SchemeDbType) => boolean;
export declare const isRefType: (type: SchemeDbType) => boolean;
