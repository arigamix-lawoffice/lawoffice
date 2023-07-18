<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="ffcaed62-a85f-43b0-b029-ed50bc562ef1" Name="WeTaskAction" Group="WorkflowEngine" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<Description>Секция для действия Задание</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="ffcaed62-a85f-00b0-2000-0d50bc562ef1" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="ffcaed62-a85f-01b0-4000-0d50bc562ef1" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="741508fa-2bb5-44bc-bce5-405db48af6e0" Name="Digest" Type="String(Max) Not Null">
		<Description>Описание задания</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="720202cc-e39d-44f7-b97e-4e61dc4e4908" Name="TaskType" Type="Reference(Typified) Not Null" ReferencedTable="b0538ece-8468-4d0b-8b4e-5a1d43e024db">
		<Description>Тип задания</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="720202cc-e39d-00f7-4000-0e61dc4e4908" Name="TaskTypeID" Type="Guid Not Null" ReferencedColumn="a628a864-c858-4200-a6b7-da78c8e6e1f4" />
		<SchemeReferencingColumn ID="fbe9df2f-625f-4f0a-a3bd-5db925b40e1f" Name="TaskTypeCaption" Type="String(128) Not Null" ReferencedColumn="0a02451e-2e06-4001-9138-b4805e641afa" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="fa19e58b-5430-4100-8969-cfe86fa4d6ac" Name="Role" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<Description>Роль, на которую будет отправлено задание</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="fa19e58b-5430-0000-4000-0fe86fa4d6ac" Name="RoleID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="df395f43-dc61-4d8c-aa7e-95474652e154" Name="RoleName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="d399bd01-49e8-4632-b4a5-4091cfeb4f5b" Name="Period" Type="Double Not Null">
		<Description>Количество дней на выполнение задания</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="97f2099f-f60e-4f0a-b3e0-f222e846313f" Name="Planned" Type="DateTime Null" />
	<SchemePhysicalColumn ID="f1fd1334-7a1b-4c8f-9c9c-762381924acc" Name="Result" Type="String(Max) Not Null">
		<Description>Результат в истории заданий</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="518b56c4-a1f8-48be-be2c-562a7e1e9131" Name="Author" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="518b56c4-a1f8-00be-4000-062a7e1e9131" Name="AuthorID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="94874af7-179f-402e-9cdf-3d7817a0a4d2" Name="AuthorName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="e16acccf-c879-4979-88d4-9fb791299bd9" Name="InitTaskScript" Type="String(Max) Null">
		<Description>Скрипт, вызываемый при создании задания</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="38c8db41-559b-42de-968b-2de17690b038" Name="Notification" Type="Reference(Typified) Not Null" ReferencedTable="18145bb5-fd4e-4795-aa1f-9e1cd9b4ee5a" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="38c8db41-559b-00de-4000-0de17690b038" Name="NotificationID" Type="Guid Not Null" ReferencedColumn="18145bb5-fd4e-0195-4000-0e1cd9b4ee5a" />
		<SchemeReferencingColumn ID="45753382-48f6-4c53-903d-ca1fbac0479d" Name="NotificationName" Type="String(256) Not Null" ReferencedColumn="265d4336-6764-4db8-8874-0e5fa92cbd5d" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="37c366ac-ed0e-4fc8-9db5-f74841978480" Name="ExcludeDeputies" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="183b4966-7e24-4f49-a96c-92db1ea68efe" Name="df_WeTaskAction_ExcludeDeputies" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="a573a030-b5d5-4533-88f6-ac464ab960d2" Name="ExcludeSubscribers" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="190e3df8-6a2a-4a69-9cd5-1741d95334fe" Name="df_WeTaskAction_ExcludeSubscribers" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="083a15f7-4f0a-4065-9c05-b0f0ba0c6a2d" Name="NotificationScript" Type="String(Max) Not Null" />
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="ffcaed62-a85f-00b0-5000-0d50bc562ef1" Name="pk_WeTaskAction" IsClustered="true">
		<SchemeIndexedColumn Column="ffcaed62-a85f-01b0-4000-0d50bc562ef1" />
	</SchemePrimaryKey>
</SchemeTable>