import { Result } from 'tessa/platform';
import { ParticipantModel } from 'tessa/forums';
import { IStorage } from 'tessa/platform/storage';
import { ValidationResult } from 'tessa/platform/validation';
export interface IForumPermissionsProvider {
    checkHasPermissionAddTopic(cardId: guid, permissionsToken?: IStorage): Promise<Result<boolean>>;
    checkHasPermissionIsSuperModerator(cardId: guid, permissionsToken: IStorage | null): Promise<Result<boolean>>;
    resolveUserPermissionsAsync(topicId: guid, checkSuperModeratorMode: boolean, permissionsToken: IStorage | null): any;
    checkEditMessagesPermissionAsync(topicId: guid, isMyMessage: boolean, permissionsToken: IStorage | null): any;
}
export declare class ForumPermissionsProvider implements IForumPermissionsProvider {
    checkHasPermissionAddTopic(cardId: guid, permissionsToken?: IStorage | null): Promise<Result<boolean>>;
    checkHasPermissionIsSuperModerator(cardId: guid, permissionsToken?: IStorage | null): Promise<Result<boolean>>;
    resolveUserPermissionsAsync(topicId: guid, checkSuperModeratorMode?: boolean, permissionsToken?: IStorage | null): Promise<[ParticipantModel, ValidationResult]>;
    checkEditMessagesPermissionAsync(topicId: guid, isMyMessage?: boolean, permissionsToken?: IStorage | null): Promise<Result<boolean>>;
}
