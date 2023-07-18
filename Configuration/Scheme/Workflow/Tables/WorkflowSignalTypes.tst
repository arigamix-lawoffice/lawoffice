<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="53dc8c0b-391a-4fbd-86c0-3da697abf065" Name="WorkflowSignalTypes" Group="WorkflowEngine">
	<Description>Список типов переходов, доступных для выбора в редакторе бизнес-процессов</Description>
	<SchemePhysicalColumn ID="cabbc72d-b093-43be-a645-8503664980d6" Name="ID" Type="Guid Not Null">
		<Description>Идентификатор типа</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="2e7c413d-0de6-4900-ac97-68ce16e3da11" Name="Name" Type="String(128) Not Null">
		<Description>Имя типа</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey ID="22193de1-6824-402c-9961-c1c09ebd3b64" Name="pk_WorkflowSignalTypes" IsClustered="true">
		<SchemeIndexedColumn Column="cabbc72d-b093-43be-a645-8503664980d6" />
	</SchemePrimaryKey>
	<SchemeRecord>
		<ID ID="cabbc72d-b093-43be-a645-8503664980d6">7af6f549-66fb-438f-a949-f70f4b8e0a15</ID>
		<Name ID="2e7c413d-0de6-4900-ac97-68ce16e3da11">Default</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="cabbc72d-b093-43be-a645-8503664980d6">f24d3d1c-6e83-4aa6-b76a-2fac7df1f492</ID>
		<Name ID="2e7c413d-0de6-4900-ac97-68ce16e3da11">Exit</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="cabbc72d-b093-43be-a645-8503664980d6">8bf6e5bb-274b-44b4-a1bd-ae9fd9015b17</ID>
		<Name ID="2e7c413d-0de6-4900-ac97-68ce16e3da11">CompleteTask</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="cabbc72d-b093-43be-a645-8503664980d6">38802cf7-c3df-415c-b682-cdae6feff6ce</ID>
		<Name ID="2e7c413d-0de6-4900-ac97-68ce16e3da11">DeleteTask</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="cabbc72d-b093-43be-a645-8503664980d6">48783b4c-4391-476c-b717-dae1a0cbf6b2</ID>
		<Name ID="2e7c413d-0de6-4900-ac97-68ce16e3da11">ReinstateTask</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="cabbc72d-b093-43be-a645-8503664980d6">507870ba-ae82-4be6-9f03-803608982629</ID>
		<Name ID="2e7c413d-0de6-4900-ac97-68ce16e3da11">ProgressTask</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="cabbc72d-b093-43be-a645-8503664980d6">27b9fd4d-b42c-4790-8fe2-0d5f3e9fbdfc</ID>
		<Name ID="2e7c413d-0de6-4900-ac97-68ce16e3da11">PostponeTask</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="cabbc72d-b093-43be-a645-8503664980d6">5c6dda50-0a03-4430-a328-3f7edda291ed</ID>
		<Name ID="2e7c413d-0de6-4900-ac97-68ce16e3da11">ReturnFromPostponeTask</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="cabbc72d-b093-43be-a645-8503664980d6">bb667fef-0885-4dc3-9360-6651705f0bac</ID>
		<Name ID="2e7c413d-0de6-4900-ac97-68ce16e3da11">UpdateTask</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="cabbc72d-b093-43be-a645-8503664980d6">12f172cd-7e80-45e3-a908-a58ca24c101c</ID>
		<Name ID="2e7c413d-0de6-4900-ac97-68ce16e3da11">SubprocessControl</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="cabbc72d-b093-43be-a645-8503664980d6">893427ba-1d2d-4369-b7fa-c28e53997846</ID>
		<Name ID="2e7c413d-0de6-4900-ac97-68ce16e3da11">Start</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="cabbc72d-b093-43be-a645-8503664980d6">2ee28367-0432-4c10-8571-a29a872e1ec5</ID>
		<Name ID="2e7c413d-0de6-4900-ac97-68ce16e3da11">UpdateTimer</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="cabbc72d-b093-43be-a645-8503664980d6">1b2eeb0c-5bda-495e-a6a0-c09f8f5bae49</ID>
		<Name ID="2e7c413d-0de6-4900-ac97-68ce16e3da11">StopTimer</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="cabbc72d-b093-43be-a645-8503664980d6">75cec30e-7b67-445f-9ba6-887e430b4cc6</ID>
		<Name ID="2e7c413d-0de6-4900-ac97-68ce16e3da11">TimerTick</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="cabbc72d-b093-43be-a645-8503664980d6">380b9b0c-a2c3-4e98-8d5a-b910d6bfcca2</ID>
		<Name ID="2e7c413d-0de6-4900-ac97-68ce16e3da11">SubprocessCompleted</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="cabbc72d-b093-43be-a645-8503664980d6">00cbaffe-5c4e-4cba-8f74-cbe796b737e9</ID>
		<Name ID="2e7c413d-0de6-4900-ac97-68ce16e3da11">TaskGroupControl</Name>
	</SchemeRecord>
</SchemeTable>