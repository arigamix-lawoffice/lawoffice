<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="8d6cb6a6-c3f5-4c92-88d7-0cc6b8e8d09d" Name="RoleTypes" Group="Roles">
	<Description>Типы ролей.</Description>
	<SchemePhysicalColumn ID="c9e1fce6-f27f-4fce-83a0-fadbff72f848" Name="ID" Type="Int16 Not Null">
		<Description>Идентификатор типа роли.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="11ee4290-ae9f-43c5-a27e-69295aa976f6" Name="Name" Type="String(128) Not Null">
		<Description>Отображаемое имя типа роли.</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey ID="63759471-f514-4b83-ac2a-d76f9922655b" Name="pk_RoleTypes" IsClustered="true">
		<SchemeIndexedColumn Column="c9e1fce6-f27f-4fce-83a0-fadbff72f848" />
	</SchemePrimaryKey>
	<SchemeRecord>
		<ID ID="c9e1fce6-f27f-4fce-83a0-fadbff72f848">0</ID>
		<Name ID="11ee4290-ae9f-43c5-a27e-69295aa976f6">$CardTypes_TypesNames_StaticRole</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="c9e1fce6-f27f-4fce-83a0-fadbff72f848">1</ID>
		<Name ID="11ee4290-ae9f-43c5-a27e-69295aa976f6">$CardTypes_TypesNames_PersonalRole</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="c9e1fce6-f27f-4fce-83a0-fadbff72f848">2</ID>
		<Name ID="11ee4290-ae9f-43c5-a27e-69295aa976f6">$CardTypes_TypesNames_DepartmentRole</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="c9e1fce6-f27f-4fce-83a0-fadbff72f848">3</ID>
		<Name ID="11ee4290-ae9f-43c5-a27e-69295aa976f6">$CardTypes_TypesNames_DynamicRole</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="c9e1fce6-f27f-4fce-83a0-fadbff72f848">4</ID>
		<Name ID="11ee4290-ae9f-43c5-a27e-69295aa976f6">$CardTypes_TypesNames_ContextRole</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="c9e1fce6-f27f-4fce-83a0-fadbff72f848">5</ID>
		<Name ID="11ee4290-ae9f-43c5-a27e-69295aa976f6">$CardTypes_TypesNames_Metarole</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="c9e1fce6-f27f-4fce-83a0-fadbff72f848">6</ID>
		<Name ID="11ee4290-ae9f-43c5-a27e-69295aa976f6">$CardTypes_TypesNames_TaskRole</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="c9e1fce6-f27f-4fce-83a0-fadbff72f848">7</ID>
		<Name ID="11ee4290-ae9f-43c5-a27e-69295aa976f6">$CardTypes_TypesNames_SmartRole</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="c9e1fce6-f27f-4fce-83a0-fadbff72f848">8</ID>
		<Name ID="11ee4290-ae9f-43c5-a27e-69295aa976f6">$CardTypes_TypesNames_NestedRole</Name>
	</SchemeRecord>
</SchemeTable>