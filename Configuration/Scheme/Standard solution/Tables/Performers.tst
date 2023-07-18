<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="d0f5547b-b2f5-4a08-8cd9-b34138d35125" Name="Performers" Group="Common" InstanceType="Cards" ContentType="Collections">
	<Description>Исполнители</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="d0f5547b-b2f5-0008-2000-034138d35125" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="d0f5547b-b2f5-0108-4000-034138d35125" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="d0f5547b-b2f5-0008-3100-034138d35125" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="01f82ec8-178a-426f-ae47-75aa0fcef5c6" Name="User" Type="Reference(Typified) Not Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="01f82ec8-178a-006f-4000-05aa0fcef5c6" Name="UserID" Type="Guid Not Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="00f28b0c-5fd9-4ac3-8559-25b0ed8b9420" Name="UserName" Type="String(128) Not Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964">
			<Description>Отображаемое имя пользователя.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="d0f5547b-b2f5-0008-5000-034138d35125" Name="pk_Performers">
		<SchemeIndexedColumn Column="d0f5547b-b2f5-0008-3100-034138d35125" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="d0f5547b-b2f5-0008-7000-034138d35125" Name="idx_Performers_ID" IsClustered="true">
		<SchemeIndexedColumn Column="d0f5547b-b2f5-0108-4000-034138d35125" />
	</SchemeIndex>
</SchemeTable>