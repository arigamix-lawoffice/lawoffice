<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="aa13a164-dc2e-47e2-a415-021b8b5666e9" Name="KrPermissionExtendedVisibilityRules" Group="Kr" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="aa13a164-dc2e-00e2-2000-021b8b5666e9" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="aa13a164-dc2e-01e2-4000-021b8b5666e9" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="aa13a164-dc2e-00e2-3100-021b8b5666e9" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="e509951d-8bda-48c8-a3ff-0604b0349c80" Name="Alias" Type="String(Max) Not Null">
		<Description>Имя объекта, который необходимо скрыть или показать.</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="e67fcf0a-990d-49ca-945a-d69d00aa4b99" Name="ControlType" Type="Reference(Typified) Not Null" ReferencedTable="18ad7847-b0f7-4d74-bc04-d96cbf18eecd">
		<Description>Тип контрола в UI. Может быть вкладкой, блоком или контролом.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="e67fcf0a-990d-00ca-4000-069d00aa4b99" Name="ControlTypeID" Type="Int32 Not Null" ReferencedColumn="42344e54-e11e-46e3-8965-b4cac26b5f23" />
		<SchemeReferencingColumn ID="533ca104-3fda-4de8-8297-5e4dee67c84a" Name="ControlTypeName" Type="String(128) Not Null" ReferencedColumn="a491d554-6ec8-4063-898c-70b983301c6d" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="03a38b6f-2266-4200-9fbc-bfb67e27a259" Name="IsHidden" Type="Boolean Not Null">
		<Description>Установка флага определяет, что элемент нужно скрыть. Если флаг снят, элемент нужно показать.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="942616cb-9d6c-4cc2-9ecb-c401942e1a41" Name="df_KrPermissionExtendedVisibilityRules_IsHidden" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="ec27ce3c-538d-4a97-a931-016718a164f9" Name="Order" Type="Int32 Not Null" />
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="aa13a164-dc2e-00e2-5000-021b8b5666e9" Name="pk_KrPermissionExtendedVisibilityRules">
		<SchemeIndexedColumn Column="aa13a164-dc2e-00e2-3100-021b8b5666e9" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="aa13a164-dc2e-00e2-7000-021b8b5666e9" Name="idx_KrPermissionExtendedVisibilityRules_ID" IsClustered="true">
		<SchemeIndexedColumn Column="aa13a164-dc2e-01e2-4000-021b8b5666e9" />
	</SchemeIndex>
</SchemeTable>