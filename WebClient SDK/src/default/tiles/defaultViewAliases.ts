
const viewsWithoutDelete = new Set<string>([
  'KrDocStateCards'
]);

export function canDeleteCardFromViewDefault(viewAlias: string) {
  return !viewsWithoutDelete.has(viewAlias);
}

const viewsWithoutExport = new Set<string>([
  'KrDocStateCards'
]);

export function canExportCardFromViewDefault(viewAlias: string) {
  return !viewsWithoutExport.has(viewAlias);
}

const viewsWithoutViewStorage = new Set<string>([
  'KrDocStateCards'
]);

export function canViewCardStorageFromViewDefault(viewAlias: string) {
  return !viewsWithoutViewStorage.has(viewAlias);
}