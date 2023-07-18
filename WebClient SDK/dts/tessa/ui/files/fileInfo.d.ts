import { IFile } from 'tessa/files';
/**
 * Информация о файле.
 */
export declare class FileInfo {
    constructor(content?: File | null, file?: IFile | null);
    /**
     * Содержимое файла или null, если содержимое файла определяется по объекту файла.
     */
    readonly content: File | null;
    /**
     * Объект файла или null, если информация о файле не относяится к файлу системы.
     */
    readonly file: IFile | null;
    /**
     * Имя файла.
     */
    get fileName(): string;
    /**
     * Размер файла.
     */
    get fileSize(): number;
    /**
     * Возвращает содержимое из content, если он задан, иначе возвращает содержимое объекта файла системы.
     * @returns Содержимое файла.
     */
    getFileContent(): Promise<File | null>;
}
