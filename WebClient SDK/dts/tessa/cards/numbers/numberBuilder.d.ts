import { Card } from '../card';
export declare const NumberBuilderParameters: {
    /**
     * Возвращает <see cref="bool"/> - признак того, что поля номера можно редактировать вручную,
     * а также доступно выделение или освобождение посредством <see cref="CanReplaceNumber"/>.
     * Реализация по умолчанию возвращает значение в зависимости от разрешений в
     *  <see cref="CardPermissionInfo.CardPermissions"/>.
     */
    canEditNumber: string;
    /**
     * Возвращает <see cref="bool"/> - признак того, что номер можно выделить или освободить вручную.
     * Реализация по умолчанию всегда возвращает <c>true</c>.
     */
    canReplaceNumber: string;
};
export declare class NumberBuilder {
    getParameter(card: Card, method: string): boolean;
    private getTileNameFromParameters;
    private checkTileInInfo;
}
