/*
 * Скрипт на удаление типовых задач по идентификаторам.
 * Работает, только если у задачи нет ни одной активной подзадачи.
 */

begin tran

declare @Tasks table (ID uniqueidentifier not null primary key)
insert into @Tasks values
-- Список идентификаторов для удаляемых заданий
 ('00000000-0000-0000-0000-000000000000')
,('00000000-0000-0000-0000-000000000001')



-- таблицы задания
delete t
from WfResolutionChildren t
inner join @Tasks tt on tt.ID = t.ID

delete t
from WfResolutionPerformers t
inner join @Tasks tt on tt.ID = t.ID

delete t
from WfResolutions t
inner join @Tasks tt on tt.ID = t.ID

delete t
from TaskCommonInfo t
inner join @Tasks tt on tt.ID = t.ID

delete t
from Tasks t
inner join @Tasks tt on tt.ID = t.RowID



-- таблицы истории заданий
delete t
from TaskHistory t
inner join @Tasks tt on tt.ID = t.RowID

delete t
from WfSatelliteTaskHistory t
inner join @Tasks tt on tt.ID = t.RowID



-- таблицы процессов Workflow
declare @Processes table (ID uniqueidentifier not null primary key)

insert into @Processes (ID)
select p.RowID
from WorkflowProcesses p with(nolock)
inner join WorkflowTasks t with(nolock) on t.ProcessRowID = p.RowID
inner join @Tasks tt on tt.ID = t.RowID

delete t
from WorkflowTasks t
inner join @Tasks tt on tt.ID = t.RowID

-- удаляем подпроцесс Workflow, если в нём было удалённое задание и других заданий в нём не осталось

delete p
from WorkflowProcesses p
inner join @Processes pp on pp.ID = p.RowID
where not exists (
	select 1
	from WorkflowTasks t with(nolock)
	where t.ProcessRowID = p.RowID
)

commit
