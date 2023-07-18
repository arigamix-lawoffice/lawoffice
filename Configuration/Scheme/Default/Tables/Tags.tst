<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="0bf4050e-d7d4-4cda-ab55-4a4f0148dd7f" Name="Tags" Group="Tags" InstanceType="Cards" ContentType="Entries">
	<Description>Основная секция с настройками тегов.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="0bf4050e-d7d4-00da-2000-0a4f0148dd7f" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="0bf4050e-d7d4-01da-4000-0a4f0148dd7f" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="7ea91bae-9150-4593-a2eb-1971c0ba653f" Name="Name" Type="String(256) Not Null">
		<Description>Имя тега.</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="838d6c1d-e9c3-4e43-8ed4-613e04c395af" Name="Owner" Type="Reference(Typified) Not Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<Description>Владелец тега.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="838d6c1d-e9c3-0043-4000-013e04c395af" Name="OwnerID" Type="Guid Not Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="a90306df-6896-4307-8235-c9873fc08414" Name="OwnerName" Type="String(128) Not Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="b15bf752-7eb9-4fec-9d2a-d85d4fd3a60e" Name="Foreground" Type="Int32 Null">
		<Description>Цвет текста тега.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="8dd52521-044b-4000-b813-69bcb4a608fc" Name="Background" Type="Int32 Not Null">
		<Description>Цвет фона тега.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="bdd3f960-8caf-473f-ac87-0af58f5cdd45" Name="df_Tags_Background" Value="-16726239" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="04a52182-cbb8-4820-9fe2-7dde53fb7e78" Name="IsCommon" Type="Boolean Not Null">
		<Description>Тег является общим.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="a7c50eb0-e1af-4359-937c-52298603eccd" Name="df_Tags_IsCommon" Value="false" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="0bf4050e-d7d4-00da-5000-0a4f0148dd7f" Name="pk_Tags" IsClustered="true">
		<SchemeIndexedColumn Column="0bf4050e-d7d4-01da-4000-0a4f0148dd7f" />
	</SchemePrimaryKey>
	<SchemeIndex ID="aa5de624-4a7a-4476-b786-670a7f87281a" Name="ndx_Tags_OwnerID">
		<SchemeIndexedColumn Column="838d6c1d-e9c3-0043-4000-013e04c395af" />
		<SchemeIncludedColumn Column="7ea91bae-9150-4593-a2eb-1971c0ba653f" />
	</SchemeIndex>
</SchemeTable>