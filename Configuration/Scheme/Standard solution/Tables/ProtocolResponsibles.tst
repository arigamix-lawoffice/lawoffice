<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="34e972b7-fd6f-4417-99d1-f2578a82ab1d" Name="ProtocolResponsibles" Group="Common" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="34e972b7-fd6f-0017-2000-02578a82ab1d" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="34e972b7-fd6f-0117-4000-02578a82ab1d" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="34e972b7-fd6f-0017-3100-02578a82ab1d" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="0feeeeb2-ba1a-4c39-98ae-b9707a107990" Name="User" Type="Reference(Typified) Not Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<Description>Ответственный</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="0feeeeb2-ba1a-0039-4000-09707a107990" Name="UserID" Type="Guid Not Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="d0c1604b-d3df-4a5d-b4f9-32b0bd7dedaf" Name="UserName" Type="String(128) Not Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964">
			<Description>Отображаемое имя пользователя.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="0f345d8d-bef8-42de-8e19-19bbe7715509" Name="Parent" Type="Reference(Typified) Not Null" ReferencedTable="91c272de-462d-4076-8f64-592885a4abd4" IsReferenceToOwner="true">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="0f345d8d-bef8-00de-4000-09bbe7715509" Name="ParentRowID" Type="Guid Not Null" ReferencedColumn="91c272de-462d-0076-3100-092885a4abd4" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="34e972b7-fd6f-0017-5000-02578a82ab1d" Name="pk_ProtocolResponsibles">
		<SchemeIndexedColumn Column="34e972b7-fd6f-0017-3100-02578a82ab1d" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="34e972b7-fd6f-0017-7000-02578a82ab1d" Name="idx_ProtocolResponsibles_ID" IsClustered="true">
		<SchemeIndexedColumn Column="34e972b7-fd6f-0117-4000-02578a82ab1d" />
	</SchemeIndex>
	<SchemeIndex ID="dfe0cbcf-fe12-42cd-a6aa-0751fd5e7773" Name="ndx_ProtocolResponsibles_ParentRowID">
		<Description>Быстрое удаление решений по протоколу для FK.</Description>
		<SchemeIndexedColumn Column="0f345d8d-bef8-00de-4000-09bbe7715509" />
	</SchemeIndex>
</SchemeTable>