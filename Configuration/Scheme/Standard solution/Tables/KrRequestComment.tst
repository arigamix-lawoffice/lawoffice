<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="db361bb6-d8d1-4645-8d9c-f296ce939c4b" Name="KrRequestComment" Group="Kr" InstanceType="Tasks" ContentType="Entries">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="db361bb6-d8d1-0045-2000-0296ce939c4b" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="5bfa9936-bb5a-4e8f-89a9-180bfd8f75f8">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="db361bb6-d8d1-0145-4000-0296ce939c4b" Name="ID" Type="Guid Not Null" ReferencedColumn="5bfa9936-bb5a-008f-3100-080bfd8f75f8" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="63f422ed-d643-4341-9787-e9ce07d180fb" Name="Comment" Type="String(Max) Null">
		<Description>Ответ комментирующего на вопрос согласанта</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="4e87ac1f-c086-405b-85b6-262fd368dc2f" Name="AuthorRole" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b" WithForeignKey="false">
		<Description>Первоначальная роль согласанта, который запрашивает комментарий. Требуется для восстановления задания согласования после получения запрошенного комментария.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="4e87ac1f-c086-005b-4000-062fd368dc2f" Name="AuthorRoleID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="b1963102-d5c0-4f9f-a9c0-3a404ee73a5b" Name="AuthorRoleName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0">
			<Description>Отображаемое имя роли.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="db361bb6-d8d1-0045-5000-0296ce939c4b" Name="pk_KrRequestComment" IsClustered="true">
		<SchemeIndexedColumn Column="db361bb6-d8d1-0145-4000-0296ce939c4b" />
	</SchemePrimaryKey>
</SchemeTable>