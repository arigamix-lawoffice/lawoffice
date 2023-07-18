<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="c9cf90e4-72e2-4cd2-a798-62b1d856cea5" Name="KrUsersCondition" Group="Kr" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Секция для условий для правил уведомлений, проверяющих принадлежность сотрудника</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="c9cf90e4-72e2-00d2-2000-02b1d856cea5" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="c9cf90e4-72e2-01d2-4000-02b1d856cea5" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="c9cf90e4-72e2-00d2-3100-02b1d856cea5" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="ff3884e0-c4f6-4cd7-b4da-97cbc9841e7f" Name="User" Type="Reference(Typified) Not Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="ff3884e0-c4f6-00d7-4000-07cbc9841e7f" Name="UserID" Type="Guid Not Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="39cf3cf0-0c0b-49f4-ba3d-b997e7c88302" Name="UserName" Type="String(128) Not Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="c9cf90e4-72e2-00d2-5000-02b1d856cea5" Name="pk_KrUsersCondition">
		<SchemeIndexedColumn Column="c9cf90e4-72e2-00d2-3100-02b1d856cea5" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="c9cf90e4-72e2-00d2-7000-02b1d856cea5" Name="idx_KrUsersCondition_ID" IsClustered="true">
		<SchemeIndexedColumn Column="c9cf90e4-72e2-01d2-4000-02b1d856cea5" />
	</SchemeIndex>
</SchemeTable>