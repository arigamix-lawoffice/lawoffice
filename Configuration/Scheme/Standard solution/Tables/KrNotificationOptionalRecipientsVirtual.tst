<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="2bd36c6d-c035-4407-a270-d329fae7ec76" Name="KrNotificationOptionalRecipientsVirtual" Group="KrStageTypes" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Секция необязательных получателей этапа Уведомление</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="2bd36c6d-c035-0007-2000-0329fae7ec76" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="2bd36c6d-c035-0107-4000-0329fae7ec76" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="2bd36c6d-c035-0007-3100-0329fae7ec76" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="c658a6e8-6838-4dac-af66-49cb38ad6ba9" Name="Role" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="c658a6e8-6838-00ac-4000-09cb38ad6ba9" Name="RoleID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="b2db2507-9f86-4d47-99f2-9395ee73109c" Name="RoleName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="2bd36c6d-c035-0007-5000-0329fae7ec76" Name="pk_KrNotificationOptionalRecipientsVirtual">
		<SchemeIndexedColumn Column="2bd36c6d-c035-0007-3100-0329fae7ec76" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="2bd36c6d-c035-0007-7000-0329fae7ec76" Name="idx_KrNotificationOptionalRecipientsVirtual_ID" IsClustered="true">
		<SchemeIndexedColumn Column="2bd36c6d-c035-0107-4000-0329fae7ec76" />
	</SchemeIndex>
</SchemeTable>