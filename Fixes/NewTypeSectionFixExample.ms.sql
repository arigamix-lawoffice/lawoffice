﻿/*
 * Пример скрипта, который надо выполнять при добавлении новой СТРОКОВОЙ секции для типа карточки,
 * если в базе данных уже есть карточки данного типа. При добавлении коллекционной секции или новых полей
 * в уже существующую секцию это не требуется.
 *
 * Данный пример во все карточки входящего документа добавит строку в секции NewSection, если этой строки там нет.
 */

insert into NewSection(ID) -- в примере мы задаем только Id, все остальные поля секции получают значения по умолчанию. При необходимости, укажите все желаемые колонки и в выборке ниже выберите для них значения или задайте константами.
select
	i.ID
	-- если в секции есть обязательные поля без значений по умолчанию, здесь дать им значения
from Instances i with (nolock)
where i.TypeID = '001F99FD-5BF3-0679-9B6F-455767AF72B5' -- Для примера тип карточки "Входящий". Посмотреть идентификатор типа можно в свойствах типа в TessaAdmin.
and not exists (
	select *
	from NewSection n with (nolock)
	where n.ID = i.ID)