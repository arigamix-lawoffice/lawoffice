<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="bc3e1376-a8e1-4256-bf84-1bfc7a49c95f" ID="dd329f32-adf0-4336-bd9e-fa084c0fe494" Name="RoleDeputiesNestedManagement" Group="Acl" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="dd329f32-adf0-0036-2000-0a084c0fe494" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="dd329f32-adf0-0136-4000-0a084c0fe494" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="dd329f32-adf0-0036-3100-0a084c0fe494" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="7ad7917f-2a17-4d58-b98b-0476597bb7c8" Name="MinDate" Type="Date Not Null" />
	<SchemePhysicalColumn ID="bad61683-b0fa-4726-8df1-6a2a103f10cd" Name="MaxDate" Type="Date Not Null" />
	<SchemePhysicalColumn ID="a2188cb7-17d1-4da0-9a5d-59e589e65a7b" Name="IsActive" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="27180937-0b6f-4713-9060-2a7dfb66800b" Name="df_RoleDeputiesNestedManagement_IsActive" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="25f5c4c1-1989-4750-9866-74e0c6a4217f" Name="IsEnabled" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="c0822c89-8c56-487a-b116-e112c2d29579" Name="df_RoleDeputiesNestedManagement_IsEnabled" Value="true" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="dd329f32-adf0-0036-5000-0a084c0fe494" Name="pk_RoleDeputiesNestedManagement">
		<SchemeIndexedColumn Column="dd329f32-adf0-0036-3100-0a084c0fe494" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="dd329f32-adf0-0036-7000-0a084c0fe494" Name="idx_RoleDeputiesNestedManagement_ID" IsClustered="true">
		<SchemeIndexedColumn Column="dd329f32-adf0-0136-4000-0a084c0fe494" />
	</SchemeIndex>
</SchemeTable>