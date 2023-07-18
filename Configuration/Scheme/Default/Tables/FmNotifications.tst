<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="fe822963-6091-4f70-9fbe-167aba72b4a2" Name="FmNotifications" Group="Fm">
	<Description>Таблица для хранения уведомления</Description>
	<SchemeComplexColumn ID="14f79568-03b6-41b6-a3f2-bb028d61612d" Name="User" Type="Reference(Typified) Not Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<Description>Пользователь</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="14f79568-03b6-00b6-4000-0b028d61612d" Name="UserID" Type="Guid Not Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="81e2b424-346b-4b3b-b84f-e946b0479620" Name="Batch0" Type="String(Max) Null">
		<Description>Первый батч с лентой уведомлений </Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="e2e1c28b-a005-4ae3-b21b-f5dcc60d5f0e" Name="Batch1" Type="String(Max) Null">
		<Description>Второй батч с лентой уведомлений </Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="5c8333f1-db71-4bd8-8d2a-050871c5ebe1" Name="Count0" Type="Int32 Null">
		<Description>Кол-во сообщений в первом батче</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="84769476-9c84-454c-8897-56cb6dffbdc3" Name="Count1" Type="Int32 Null">
		<Description>Кол-во сообщений во втором батче</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="d12559ea-ecf6-477a-9a90-707fa185b90c" Name="ReadMessages0" Type="String(Max) Null">
		<Description>Список прочитанных сообщений в первом батче</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="e2a499c8-6653-4a61-b60b-20b6ad7ba6eb" Name="ReadMessages1" Type="String(Max) Null">
		<Description>Список прочитанных сообщений в втором батче</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="4694ec5d-743c-48ae-9fe2-0b5713e9aa8f" Name="ActiveBatch" Type="Int32 Not Null">
		<Description>Номер активного батча</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey ID="641d5f88-d835-43c1-81de-dab29831823b" Name="pk_FmNotifications">
		<SchemeIndexedColumn Column="14f79568-03b6-00b6-4000-0b028d61612d" />
	</SchemePrimaryKey>
</SchemeTable>