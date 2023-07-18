import { ArrayStorage } from './storage';
/**
 * Связываем storageItem и modelItem через id. Добавляем в объекты скрытое поле с одинаковыми id;
 * Id не очищается. Для нескольких разных arrayTransformer у storageItem будет один id.
 */
export declare function arrayTransformer<S, A>(storage: ArrayStorage<S>, array: Array<A>, transformFunc: (s: S) => A, cleanFunc?: ((a: A) => void) | null, sideEffectFunc?: ((array: Array<A>) => void) | null): Function;
