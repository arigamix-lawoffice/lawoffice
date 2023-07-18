/**
 * Метод определения кодировки контента файла.
 */
export interface TextEncodingResolver {
    (file: File | Blob): Promise<string | null>;
}
