<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="b75dc3d2-10c8-4ca9-a63c-2a8f54db5c42" Name="KrCommentsInfo" Group="KrStageTypes" InstanceType="Tasks" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="b75dc3d2-10c8-00a9-2000-0a8f54db5c42" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="5bfa9936-bb5a-4e8f-89a9-180bfd8f75f8">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="b75dc3d2-10c8-01a9-4000-0a8f54db5c42" Name="ID" Type="Guid Not Null" ReferencedColumn="5bfa9936-bb5a-008f-3100-080bfd8f75f8" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="b75dc3d2-10c8-00a9-3100-0a8f54db5c42" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="01ae8590-892a-4f78-97dd-f94b1620bc46" Name="Question" Type="String(Max) Null">
		<Description>Вопрос к комментатору</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="9b34b837-9b24-4afa-985d-ff4d77532230" Name="Answer" Type="String(Max) Null">
		<Description>Ответ комментатора на вопрос</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="450c7701-449f-41ca-971a-495256971b75" Name="Commentator" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b" WithForeignKey="false">
		<Description>Комментатор</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="450c7701-449f-00ca-4000-095256971b75" Name="CommentatorID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="6b6f1d03-a034-4b71-9e1b-c65f38893bf6" Name="CommentatorName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0">
			<Description>Отображаемое имя роли.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="b75dc3d2-10c8-00a9-5000-0a8f54db5c42" Name="pk_KrCommentsInfo">
		<SchemeIndexedColumn Column="b75dc3d2-10c8-00a9-3100-0a8f54db5c42" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="b75dc3d2-10c8-00a9-7000-0a8f54db5c42" Name="idx_KrCommentsInfo_ID" IsClustered="true">
		<SchemeIndexedColumn Column="b75dc3d2-10c8-01a9-4000-0a8f54db5c42" />
	</SchemeIndex>
</SchemeTable>