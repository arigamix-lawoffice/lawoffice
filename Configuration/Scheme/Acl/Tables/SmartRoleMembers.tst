<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="bc3e1376-a8e1-4256-bf84-1bfc7a49c95f" ID="73cbcd25-8709-4c3d-9091-3db6ccba5055" Name="SmartRoleMembers" Group="Acl" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="73cbcd25-8709-003d-2000-0db6ccba5055" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="73cbcd25-8709-013d-4000-0db6ccba5055" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="73cbcd25-8709-003d-3100-0db6ccba5055" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="557762b2-0b86-4429-9365-c48974b67f55" Name="Role" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="557762b2-0b86-0029-4000-048974b67f55" Name="RoleID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="73cbcd25-8709-003d-5000-0db6ccba5055" Name="pk_SmartRoleMembers">
		<SchemeIndexedColumn Column="73cbcd25-8709-003d-3100-0db6ccba5055" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="73cbcd25-8709-003d-7000-0db6ccba5055" Name="idx_SmartRoleMembers_ID" IsClustered="true">
		<SchemeIndexedColumn Column="73cbcd25-8709-013d-4000-0db6ccba5055" />
	</SchemeIndex>
</SchemeTable>