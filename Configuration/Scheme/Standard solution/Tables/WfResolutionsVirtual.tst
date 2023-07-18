<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="1f805af5-f412-4878-9d70-af989b905fb5" Name="WfResolutionsVirtual" Group="Wf" IsVirtual="true" InstanceType="Tasks" ContentType="Entries">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="1f805af5-f412-0078-2000-0f989b905fb5" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="5bfa9936-bb5a-4e8f-89a9-180bfd8f75f8">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="1f805af5-f412-0178-4000-0f989b905fb5" Name="ID" Type="Guid Not Null" ReferencedColumn="5bfa9936-bb5a-008f-3100-080bfd8f75f8" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="d3722c05-74e9-44f0-a97f-de21afda8a29" Name="Planned" Type="DateTime Null">
		<Description>Дата запланированного завершения задания, которую изменяет автор задачи.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="349f8f49-f29a-4e6f-827d-bb333f86bb1d" Name="Digest" Type="String(Max) Null">
		<Description>Digest, который изменяет автор задачи.</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="1f805af5-f412-0078-5000-0f989b905fb5" Name="pk_WfResolutionsVirtual" IsClustered="true">
		<SchemeIndexedColumn Column="1f805af5-f412-0178-4000-0f989b905fb5" />
	</SchemePrimaryKey>
</SchemeTable>