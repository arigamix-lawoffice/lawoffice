<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="be5a85fd-b2fb-4f60-a3b7-48e79e45249f" Name="ContextRoles" Group="Roles" InstanceType="Cards" ContentType="Entries">
	<Description>Контекстные роли.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="be5a85fd-b2fb-0060-2000-08e79e45249f" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="be5a85fd-b2fb-0160-4000-08e79e45249f" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="a9fc0b7b-fc09-487e-a346-191036a8bc86" Name="SqlText" Type="String(Max) Not Null">
		<Description>Текст SQL-запроса, возвращающего состав контекстной роли.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="28205f26-81b1-4e62-aed3-4b93fdf0cd04" Name="SqlTextForCard" Type="String(Max) Not Null">
		<Description>Текст SQL-запроса, возвращающего состав контекстной роли для заданной карточки.
Значение получается парсингом поля SqlText.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="3d68b1a3-a63e-4283-b2a0-ac73cd4f6818" Name="SqlTextForUser" Type="String(Max) Not Null">
		<Description>Текст SQL-запроса, возвращающего признак того, что заданный пользователь входит в состав контекстной роли для заданной карточки.
Значение получается парсингом поля SqlText.</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="be5a85fd-b2fb-0060-5000-08e79e45249f" Name="pk_ContextRoles" IsClustered="true">
		<SchemeIndexedColumn Column="be5a85fd-b2fb-0160-4000-08e79e45249f" />
	</SchemePrimaryKey>
</SchemeTable>