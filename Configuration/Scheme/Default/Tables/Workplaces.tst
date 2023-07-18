<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="21cd7a4f-6930-4746-9a57-72481e951b02" Name="Workplaces" Group="System">
	<Description>Рабочие места.</Description>
	<SchemePhysicalColumn ID="39f02b29-1a58-4409-a8fa-11756a2870f4" Name="ID" Type="Guid Not Null" IsRowGuidColumn="true">
		<Description>ПК</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="60d1e6ca-06ac-4881-8048-8cc1c4459b95" Name="Name" Type="String(128) Not Null">
		<Description>Уникальное имя рабочего места.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="cbe2b8f2-d2f2-4084-9bc8-dc1482958c41" Name="Metadata" Type="BinaryJson Not Null">
		<Description>Метаданные рабочего места.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="123e6091-3967-49db-8544-788bb399c854" Name="Order" Type="Int32 Not Null">
		<Description>Порядок в списке рабочих мест.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="c37f6a3b-c1f9-4726-a9f6-d07de124ca13" Name="df_Workplaces_Order" Value="0" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey ID="980a2512-517b-45fc-bf98-febf808de30a" Name="pk_Workplaces">
		<SchemeIndexedColumn Column="39f02b29-1a58-4409-a8fa-11756a2870f4" />
	</SchemePrimaryKey>
	<SchemeIndex ID="d1580d55-26c1-431a-9b9a-839cf52da68f" Name="ndx_Workplaces_Name" IsUnique="true">
		<SchemeIndexedColumn Column="60d1e6ca-06ac-4881-8048-8cc1c4459b95">
			<Expression Dbms="PostgreSql">lower("Name")</Expression>
		</SchemeIndexedColumn>
	</SchemeIndex>
</SchemeTable>