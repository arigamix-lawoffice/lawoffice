<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="51c5a6be-fe5d-411c-95ba-21d503ced67a" Name="KrPermissionTypes" Group="Kr" InstanceType="Cards" ContentType="Collections">
	<Description>Типы карточек, к которым применяются разрешения из карточки с правами.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="51c5a6be-fe5d-001c-2000-01d503ced67a" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="51c5a6be-fe5d-011c-4000-01d503ced67a" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="51c5a6be-fe5d-001c-3100-01d503ced67a" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="09b8e21f-0469-44b8-8675-4c0037b61b6a" Name="Type" Type="Reference(Typified) Not Null" ReferencedTable="a90baecf-c9ce-4cba-8bb0-150a13666266" WithForeignKey="false">
		<Description>Тип карточки, для которого применимы разрешения из карточки с правами.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="09b8e21f-0469-00b8-4000-0c0037b61b6a" Name="TypeID" Type="Guid Not Null" ReferencedColumn="a90baecf-c9ce-01ba-4000-050a13666266" />
		<SchemeReferencingColumn ID="eee42c51-36c5-4be9-9c37-2ba4a03a77b8" Name="TypeCaption" Type="String(128) Not Null" ReferencedColumn="447f7cb1-76ae-4703-b3bb-16a57d4e7ab1" />
		<SchemePhysicalColumn ID="7a61a90f-6f7e-475e-8dd1-81226a8843c9" Name="TypeIsDocType" Type="Boolean Not Null">
			<Description>Признак того, что указанный тип - это тип документа (а не карточки).</Description>
			<SchemeDefaultConstraint IsPermanent="true" ID="cc4eda5b-577b-4fe8-b48f-a85b20c3225f" Name="df_KrPermissionTypes_TypeIsDocType" Value="false" />
		</SchemePhysicalColumn>
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="51c5a6be-fe5d-001c-5000-01d503ced67a" Name="pk_KrPermissionTypes">
		<SchemeIndexedColumn Column="51c5a6be-fe5d-001c-3100-01d503ced67a" />
	</SchemePrimaryKey>
	<SchemeUniqueKey ID="0fc4d78c-8183-4eee-81ff-75f177f57664" Name="ndx_KrPermissionTypes_TypeIDID">
		<Description>Для каждой карточки любой тип указан не более 1го раза</Description>
		<SchemeIndexedColumn Column="09b8e21f-0469-00b8-4000-0c0037b61b6a" />
		<SchemeIndexedColumn Column="51c5a6be-fe5d-011c-4000-01d503ced67a" />
	</SchemeUniqueKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="51c5a6be-fe5d-001c-7000-01d503ced67a" Name="idx_KrPermissionTypes_ID" IsClustered="true">
		<SchemeIndexedColumn Column="51c5a6be-fe5d-011c-4000-01d503ced67a" />
	</SchemeIndex>
</SchemeTable>