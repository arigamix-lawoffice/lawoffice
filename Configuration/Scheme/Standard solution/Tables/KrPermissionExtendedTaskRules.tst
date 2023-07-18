<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="536f27ed-f1d2-4850-ad9e-eab93f584f1a" Name="KrPermissionExtendedTaskRules" Group="Kr" InstanceType="Cards" ContentType="Collections">
	<Description>Секция с расширенными настройками доступа к заданиям</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="536f27ed-f1d2-0050-2000-0ab93f584f1a" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="536f27ed-f1d2-0150-4000-0ab93f584f1a" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="536f27ed-f1d2-0050-3100-0ab93f584f1a" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="fbef507e-0d1d-4721-a338-0f5b9ab407b3" Name="Section" Type="Reference(Abstract) Not Null" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="fbef507e-0d1d-0021-4000-0f5b9ab407b3" Name="SectionID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
		<SchemePhysicalColumn ID="12c4a912-c6ab-4de7-a5d3-eaadbb0ec81e" Name="SectionName" Type="String(Max) Not Null" />
		<SchemePhysicalColumn ID="ff22ce78-7984-43db-9a88-bc4ebc5e269b" Name="SectionTypeID" Type="Int32 Not Null" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="ac15b652-65c8-4b52-a05e-fbf550e0990d" Name="AccessSetting" Type="Reference(Typified) Null" ReferencedTable="4c274eda-ab9a-403f-9e5b-0b933283b5a3">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="ac15b652-65c8-0052-4000-0bf550e0990d" Name="AccessSettingID" Type="Int32 Null" ReferencedColumn="0f1e3ccd-ef3b-4c4a-b3be-dbf802f9278c" />
		<SchemeReferencingColumn ID="0caf7eb0-1888-4f40-a10f-978fdca9ec2d" Name="AccessSettingName" Type="String(128) Null" ReferencedColumn="daa3bd22-b7ad-469f-8ffb-41afa4fa2e58" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="cd813174-f132-447e-8502-0ff7136e5740" Name="Order" Type="Int32 Not Null" />
	<SchemePhysicalColumn ID="9b966a19-a9b6-44f7-8e7b-f335fb788f92" Name="IsHidden" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="ee322ce8-2c1e-4892-bdc9-50a1d07c7446" Name="df_KrPermissionExtendedTaskRules_IsHidden" Value="false" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="536f27ed-f1d2-0050-5000-0ab93f584f1a" Name="pk_KrPermissionExtendedTaskRules">
		<SchemeIndexedColumn Column="536f27ed-f1d2-0050-3100-0ab93f584f1a" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="536f27ed-f1d2-0050-7000-0ab93f584f1a" Name="idx_KrPermissionExtendedTaskRules_ID" IsClustered="true">
		<SchemeIndexedColumn Column="536f27ed-f1d2-0150-4000-0ab93f584f1a" />
	</SchemeIndex>
</SchemeTable>