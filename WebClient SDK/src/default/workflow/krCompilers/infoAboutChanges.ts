
export enum InfoAboutChanges {
  None = 0x0,
  HasChangesToInfo = 0x1,
  ChangesListToInfo = 0x2,
  HasChangesToValidationResult = 0x4,
  ChangesListToValidationResult = 0x8,
  ChangesInHiddenStages = 0x16,
  ToInfo = HasChangesToInfo | ChangesListToInfo,
  ToInfoIncludingHiddenStages = ToInfo | ChangesInHiddenStages,
  ToValidationResult = HasChangesToValidationResult | ChangesListToValidationResult,
  ToValidationResultIncludingHiddenStages = ToValidationResult | ChangesInHiddenStages,
  Full = ToInfoIncludingHiddenStages | ToValidationResultIncludingHiddenStages,
}