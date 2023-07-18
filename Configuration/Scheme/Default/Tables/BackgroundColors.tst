<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="9f4fc1ce-af03-4009-8106-d9b861469ef1" Name="BackgroundColors" Group="System" InstanceType="Cards" ContentType="Entries">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="9f4fc1ce-af03-0009-2000-09b861469ef1" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="9f4fc1ce-af03-0109-4000-09b861469ef1" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="bf8c2dd7-ee67-4a1f-b0e7-331635a36255" Name="Color1" Type="Int32 Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="d4cc0ba1-efe8-44cc-95d5-f3c1962744c0" Name="df_BackgroundColors_Color1" Value="-6522" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="266f4a99-9851-4c47-acc0-4d1fe4ab31e7" Name="Color2" Type="Int32 Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="64614647-a49a-4bd5-ae40-a082fcc85abe" Name="df_BackgroundColors_Color2" Value="-2623599" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="935409ea-97a2-4fb2-8c5c-031cf739146d" Name="Color3" Type="Int32 Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="4436e41b-f0b1-4331-bcdf-90acb12f895c" Name="df_BackgroundColors_Color3" Value="-6105089" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="923ca2c8-1e48-45e9-ab6d-4eee07a11e01" Name="Color4" Type="Int32 Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="a0c19aa4-a1a5-41da-a728-d6e62af753c1" Name="df_BackgroundColors_Color4" Value="-24837" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="9297f57c-a07f-4cd9-8fce-fa43114783b9" Name="Color5" Type="Int32 Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="3fe65d06-4ed6-4e43-abdb-2b0dc0e14023" Name="df_BackgroundColors_Color5" Value="-41125" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="9f4fc1ce-af03-0009-5000-09b861469ef1" Name="pk_BackgroundColors" IsClustered="true">
		<SchemeIndexedColumn Column="9f4fc1ce-af03-0109-4000-09b861469ef1" />
	</SchemePrimaryKey>
</SchemeTable>