<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="5024c846-07bb-4932-bb8d-6c6f9c1e27f7" Name="KrPermissionStates" Group="Kr" InstanceType="Cards" ContentType="Collections">
	<Description>Состояния согласуемой карточки, к которым применяются права из карточки с правами.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="5024c846-07bb-0032-2000-0c6f9c1e27f7" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="5024c846-07bb-0132-4000-0c6f9c1e27f7" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="5024c846-07bb-0032-3100-0c6f9c1e27f7" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="9acfb365-f531-41e9-a30a-f64cbddfa3e1" Name="State" Type="Reference(Typified) Not Null" ReferencedTable="47107d7a-3a8c-47f0-b800-2a45da222ff4">
		<Description>Состояние согласуемой карточки, к которому применяются права из карточки с правами.</Description>
		<SchemeReferencingColumn IsPermanent="true" ID="c31031f0-64de-4152-9b8f-129c00a9d54b" Name="StateID" Type="Int16 Not Null" ReferencedColumn="502209b0-233f-4e1f-be01-35a50f53414c" />
		<SchemeReferencingColumn ID="2d2ff670-226f-4120-8100-b96b161ca28a" Name="StateName" Type="String(128) Not Null" ReferencedColumn="4c1a8dd7-72ed-4fc9-b559-b38ae30dccb9" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="5024c846-07bb-0032-5000-0c6f9c1e27f7" Name="pk_KrPermissionStates">
		<SchemeIndexedColumn Column="5024c846-07bb-0032-3100-0c6f9c1e27f7" />
	</SchemePrimaryKey>
	<SchemeUniqueKey ID="e707a846-2944-4fa7-9c70-b0f6a05c94fa" Name="ndx_KrPermissionStates_IDStateID">
		<Description>Для каждой карточки любое состояние указано не более одного раза</Description>
		<SchemeIndexedColumn Column="5024c846-07bb-0132-4000-0c6f9c1e27f7" />
		<SchemeIndexedColumn Column="c31031f0-64de-4152-9b8f-129c00a9d54b" />
	</SchemeUniqueKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="5024c846-07bb-0032-7000-0c6f9c1e27f7" Name="idx_KrPermissionStates_ID" IsClustered="true">
		<SchemeIndexedColumn Column="5024c846-07bb-0132-4000-0c6f9c1e27f7" />
	</SchemeIndex>
</SchemeTable>