<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="506e2fe6-397e-45c1-ae35-22cd7e85b14d" Name="SequencesReserved" Group="System" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="506e2fe6-397e-00c1-2000-02cd7e85b14d" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="506e2fe6-397e-01c1-4000-02cd7e85b14d" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="506e2fe6-397e-00c1-3100-02cd7e85b14d" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="52dccf04-434f-46a4-80bb-9365bfebf15f" Name="Number" Type="Int64 Not Null" />
	<SchemePhysicalColumn ID="dc74b250-05bc-4de2-a154-3576103f6d7e" Name="Date" Type="DateTime Not Null" />
	<SchemeComplexColumn ID="ca2639ff-cdcd-418e-9e0a-fbf32ebd8854" Name="User" Type="Reference(Typified) Not Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="ca2639ff-cdcd-008e-4000-0bf32ebd8854" Name="UserID" Type="Guid Not Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="3de0337a-79e2-4a28-b541-8ad2d3fbf4f4" Name="UserName" Type="String(128) Not Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964">
			<Description>Отображаемое имя пользователя.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="506e2fe6-397e-00c1-5000-02cd7e85b14d" Name="pk_SequencesReserved">
		<SchemeIndexedColumn Column="506e2fe6-397e-00c1-3100-02cd7e85b14d" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="506e2fe6-397e-00c1-7000-02cd7e85b14d" Name="idx_SequencesReserved_ID" IsClustered="true">
		<SchemeIndexedColumn Column="506e2fe6-397e-01c1-4000-02cd7e85b14d" />
	</SchemeIndex>
</SchemeTable>