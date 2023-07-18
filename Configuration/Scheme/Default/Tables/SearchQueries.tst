<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="d0dde291-0d94-4e76-9f69-902809975216" Name="SearchQueries" Group="System">
	<SchemePhysicalColumn ID="9cdea9cd-69a4-4ffb-81d5-4b0c33f5244c" Name="ID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="215990d2-5514-41e3-a6bc-bab7a3a670d0" Name="Name" Type="String(128) Not Null">
		<Description>Название поискового запроса</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="11570c93-3ea7-4162-8178-17f220437072" Name="Metadata" Type="BinaryJson Not Null">
		<Description>Метаданные параметров</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="d121ef68-882d-47ee-a12c-f5aabefe53fb" Name="ViewAlias" Type="String(128) Not Null">
		<Description>Псевдоним представления</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="e5cefb6d-b687-470e-a99c-8ae115bf09fb" Name="IsPublic" Type="Boolean Not Null">
		<Description>Признак общедоступности запроса</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="81193c2a-7947-4d77-abb4-0f9d0b735824" Name="df_SearchQueries_IsPublic" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="90b22607-c612-4ab1-a42b-43d6769c5512" Name="LastModified" Type="DateTime Not Null">
		<Description>Дата и время последнего изменения</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="d1922aa9-031c-4a6e-bb09-e9b1f5bf45c4" Name="CreatedByUser" Type="Reference(Typified) Not Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="d1922aa9-031c-006e-4000-09b1f5bf45c4" Name="CreatedByUserID" Type="Guid Not Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="a93419d7-6158-444a-bb53-5fa2d8a181e9" Name="TemplateCompositionID" Type="Guid Null" />
	<SchemePrimaryKey ID="fddba95f-b6a8-44f6-abf8-a1699fb29af5" Name="pk_SearchQueries">
		<SchemeIndexedColumn Column="9cdea9cd-69a4-4ffb-81d5-4b0c33f5244c" />
	</SchemePrimaryKey>
</SchemeTable>