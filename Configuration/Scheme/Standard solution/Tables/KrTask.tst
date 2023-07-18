<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="51936147-e0ff-4e19-a7d1-0ea7d462ceec" Name="KrTask" Group="KrStageTypes" InstanceType="Tasks" ContentType="Entries">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="51936147-e0ff-0019-2000-0ea7d462ceec" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="5bfa9936-bb5a-4e8f-89a9-180bfd8f75f8">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="51936147-e0ff-0119-4000-0ea7d462ceec" Name="ID" Type="Guid Not Null" ReferencedColumn="5bfa9936-bb5a-008f-3100-080bfd8f75f8" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="08dfcb43-dbe5-4c99-812b-75a0b491ab10" Name="Comment" Type="String(Max) Null" />
	<SchemeComplexColumn ID="113f93ef-27bc-4917-a7be-54928f6e9108" Name="Delegate" Type="Reference(Typified) Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="113f93ef-27bc-0017-4000-04928f6e9108" Name="DelegateID" Type="Guid Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="a2fbc5e3-7185-4454-93fd-e371e63da662" Name="DelegateName" Type="String(128) Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0">
			<Description>Отображаемое имя роли.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="51936147-e0ff-0019-5000-0ea7d462ceec" Name="pk_KrTask" IsClustered="true">
		<SchemeIndexedColumn Column="51936147-e0ff-0119-4000-0ea7d462ceec" />
	</SchemePrimaryKey>
</SchemeTable>