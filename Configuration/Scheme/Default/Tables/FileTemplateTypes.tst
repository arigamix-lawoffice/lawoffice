<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="628e0e44-564c-4107-b943-0ec1e378bae7" Name="FileTemplateTypes" Group="System" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="628e0e44-564c-0007-2000-0ec1e378bae7" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="628e0e44-564c-0107-4000-0ec1e378bae7" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="628e0e44-564c-0007-3100-0ec1e378bae7" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="a74dd2f0-ce76-4f7c-b2db-b35d16d441ee" Name="Type" Type="Reference(Typified) Null" ReferencedTable="b0538ece-8468-4d0b-8b4e-5a1d43e024db" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="a74dd2f0-ce76-007c-4000-035d16d441ee" Name="TypeID" Type="Guid Null" ReferencedColumn="a628a864-c858-4200-a6b7-da78c8e6e1f4">
			<Description>ID of a type.</Description>
		</SchemeReferencingColumn>
		<SchemeReferencingColumn ID="5399fcae-93fd-4194-8ee2-8bc65a2118cd" Name="TypeCaption" Type="String(128) Null" ReferencedColumn="0a02451e-2e06-4001-9138-b4805e641afa">
			<Description>Caption of a type.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="628e0e44-564c-0007-5000-0ec1e378bae7" Name="pk_FileTemplateTypes">
		<SchemeIndexedColumn Column="628e0e44-564c-0007-3100-0ec1e378bae7" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="628e0e44-564c-0007-7000-0ec1e378bae7" Name="idx_FileTemplateTypes_ID" IsClustered="true">
		<SchemeIndexedColumn Column="628e0e44-564c-0107-4000-0ec1e378bae7" />
	</SchemeIndex>
</SchemeTable>