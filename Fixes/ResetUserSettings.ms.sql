/*
 * Запрос выполняется только на Microsoft SQL Server.
 *
 * Скрипт для сброса личных настроек пользователя, таких как выбранный язык интерфейса,
 * добавленные в дерево поисковые запросы (сами запросы сохраняются, их можно будет добавить повторно),
 * а также настройки из диалога "Мои настройки".
 *
 * Используйте этот скрипт только в том случае, если параметры нельзя сбросить из интерфейса приложения,
 * например, из диалога "Мои настройки" или из карточки сотрудника.
 *
 * В параметре @UserID указывается идентификатор пользователя, для которого настройки сбрасываются.
 * Если убрать из запроса WHERE, то настройки будут сброшены для всех пользователей.
 */
 
DECLARE @UserID uniqueidentifier = '11111111-1111-1111-1111-111111111111';

UPDATE [PersonalRoleSatellite]
SET
    [Settings] = NULL,
    [LanguageID] = NULL,
    [LanguageCode] = NULL,
    [LanguageCaption] = NULL,
    [FilePreviewPosition] = 0,
    [FilePreviewIsHidden] = 0,
    [FilePreviewWidthRatio] = 0.5,
    [TaskAreaWidth] = 450.0,
    [WebTheme] = NULL,
    [WebWallpaper] = NULL,
    [WorkplaceExtensions] = NULL
WHERE [MainCardID] = @UserID;
