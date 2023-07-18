import { KrPermissionFlagDescriptor } from './krPermissionFlagDescriptor';
/**
 * Создание карточки.
 */
export declare const CreateCard: KrPermissionFlagDescriptor;
/**
 * Создание шаблона и копирование.
 */
export declare const CreateTemplateAndCopy: KrPermissionFlagDescriptor;
/**
 * Чтение карточки.
 */
export declare const ReadCard: KrPermissionFlagDescriptor;
/**
 * Редактирование данных карточки.
 */
export declare const EditCard: KrPermissionFlagDescriptor;
/**
 * Редактирование маршрута согласования.
 */
export declare const EditRoute: KrPermissionFlagDescriptor;
/**
 * Возможность редактирования/выделения/освобождения номера.
 */
export declare const EditNumber: KrPermissionFlagDescriptor;
/**
 * Возможность подписывать файлы.
 */
export declare const SignFiles: KrPermissionFlagDescriptor;
/**
 * Добавление файлов в карточку.
 */
export declare const AddFiles: KrPermissionFlagDescriptor;
/**
 * Можно редактировать свои файлы.
 */
export declare const EditOwnFiles: KrPermissionFlagDescriptor;
/**
 * Редактирование файлов карточки.
 */
export declare const EditFiles: KrPermissionFlagDescriptor;
/**
 * Возможность удалять собственные файлы.
 */
export declare const DeleteOwnFiles: KrPermissionFlagDescriptor;
/**
 * Возможность удалять файлы.
 */
export declare const DeleteFiles: KrPermissionFlagDescriptor;
/**
 * Удаление карточки.
 */
export declare const DeleteCard: KrPermissionFlagDescriptor;
/**
 * Возможность создания резолюций.
 */
export declare const CreateResolutions: KrPermissionFlagDescriptor;
/**
 * Возможность создания топика.
 */
export declare const AddTopics: KrPermissionFlagDescriptor;
/**
 * Возможность перейти врежим супермодератора.
 */
export declare const SuperModeratorMode: KrPermissionFlagDescriptor;
/**
 * Возможность подписываться на уведомления карточки.
 */
export declare const SubscribeForNotifications: KrPermissionFlagDescriptor;
/**
 * Возможность полного пересчёта маршрута.
 */
export declare const CanFullRecalcRoute: KrPermissionFlagDescriptor;
/**
 * Возможность пропускать этапы маршрута.
 */
export declare const CanSkipStages: KrPermissionFlagDescriptor;
/**
 * Полные права на редактирование карточки (читать, редактировать карточку,
 * добавлять и редактировать файлы, редактировать маршрут).
 * Даются, напр, при наличии задания редактирования.
 */
export declare const FullCardPermissionsGroup: KrPermissionFlagDescriptor;
export declare const getAllDescriptors: () => KrPermissionFlagDescriptor;
