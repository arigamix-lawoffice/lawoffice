import { ITileLocalExtensionContext, TileExtension } from 'tessa/ui/tiles';
/**
 * Расширение на модификацию тайлов для типа карточки "Сотрудник".
 * Скрывает тайлы "Копировать карточку" и "Создать шаблон", если пользователь не администратор и не используется кастомная система прав доступа к карточке "Сотрудник".
 */
export declare class PersonalRoleTileExtension extends TileExtension {
    initializingLocal(context: ITileLocalExtensionContext): void;
    private roleTypeUseCustomPermissions;
}
