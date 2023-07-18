<?xml version="1.0" encoding="utf-8"?>
<SchemeTable IsSystem="true" ID="b0538ece-8468-4d0b-8b4e-5a1d43e024db" Name="Types" Group="System">
	<Description>Contains metadata that describes types which used by Tessa.</Description>
	<SchemePhysicalColumn IsPermanent="true" IsSealed="true" ID="a628a864-c858-4200-a6b7-da78c8e6e1f4" Name="ID" Type="Guid Not Null" IsRowGuidColumn="true">
		<Description>ID of a type.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn IsPermanent="true" IsSealed="true" ID="71181642-0d62-45f9-8ad8-ccec4bd4ce22" Name="Name" Type="String(128) Not Null">
		<Description>Name of a type.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="0a02451e-2e06-4001-9138-b4805e641afa" Name="Caption" Type="String(128) Not Null">
		<Description>Caption of a type.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="b7ad3de6-6cad-447b-bd8c-ab1dd4039cde" Name="Group" Type="String(128) Null">
		<Description>Group of a type.</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="59c8b49e-0d0e-439c-84bb-b0ab61d3379a" Name="InstanceType" Type="Reference(Typified) Not Null" ReferencedTable="2a567cee-1489-4a90-acf5-4f6d2c5bd67e">
		<Description>Instance type of a type.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="59c8b49e-0d0e-009c-4000-00ab61d3379a" Name="InstanceTypeID" Type="Int32 Not Null" ReferencedColumn="e1a61f56-f8ba-4ffd-ab6f-48c20e5f018a">
			<SchemeDefaultConstraint IsPermanent="true" ID="978ede03-3658-4631-a2b3-c46944cd51fd" Name="df_Types_InstanceTypeID" Value="0" />
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="7c46ba62-ef29-426a-a445-1ca772e9125a" Name="Flags" Type="Int64 Not Null">
		<Description>Bit flags for a type.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="c0a5f7c5-2abf-4727-b213-7d69d8f5db0b" Name="df_Types_Flags" Value="0" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="5d8facb8-8ced-4f37-a6b4-e89c40cf6529" Name="Metadata" Type="BinaryJson Not Null">
		<Description>Metadata for a type.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="845f78b0-5fef-49e8-a094-8c6f262a1a44" Name="df_Types_Metadata" Value="{}" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsPermanent="true" IsSealed="true" ID="c2789dd3-b51f-4adb-a1c2-8635695e9bed" Name="pk_Types" IsClustered="true">
		<SchemeIndexedColumn Column="a628a864-c858-4200-a6b7-da78c8e6e1f4" />
	</SchemePrimaryKey>
	<SchemeUniqueKey IsPermanent="true" IsSealed="true" ID="cb1555cd-79c0-49d8-ac5f-1ac0df4d045d" Name="ndx_Types_Name">
		<SchemeIndexedColumn Column="71181642-0d62-45f9-8ad8-ccec4bd4ce22" />
	</SchemeUniqueKey>
	<SchemeIndex ID="3bbc3896-37e5-401b-b94a-91a35301ace7" Name="ndx_Types_InstanceTypeIDFlags">
		<SchemeIndexedColumn Column="59c8b49e-0d0e-009c-4000-00ab61d3379a" />
		<SchemeIndexedColumn Column="7c46ba62-ef29-426a-a445-1ca772e9125a" />
		<SchemeIncludedColumn Column="71181642-0d62-45f9-8ad8-ccec4bd4ce22" />
		<SchemeIncludedColumn Column="0a02451e-2e06-4001-9138-b4805e641afa" />
		<SchemeIncludedColumn Column="b7ad3de6-6cad-447b-bd8c-ab1dd4039cde" />
	</SchemeIndex>
</SchemeTable>