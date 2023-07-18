<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="5ad723d6-21d8-48c2-8799-a1ba9fb1c758" Name="KrVirtualFileCardTypes" Group="Kr" InstanceType="Cards" ContentType="Collections">
	<Description>Типы карточек для карточки виртуального файла</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="5ad723d6-21d8-00c2-2000-01ba9fb1c758" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="5ad723d6-21d8-01c2-4000-01ba9fb1c758" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="5ad723d6-21d8-00c2-3100-01ba9fb1c758" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="59b96e45-ba11-4206-a2cf-34c51381f1f9" Name="Type" Type="Reference(Typified) Not Null" ReferencedTable="b0538ece-8468-4d0b-8b4e-5a1d43e024db">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="59b96e45-ba11-0006-4000-04c51381f1f9" Name="TypeID" Type="Guid Not Null" ReferencedColumn="a628a864-c858-4200-a6b7-da78c8e6e1f4" />
		<SchemeReferencingColumn ID="e515de68-2184-4b73-aba3-ee19ee1fd0a0" Name="TypeCaption" Type="String(128) Not Null" ReferencedColumn="0a02451e-2e06-4001-9138-b4805e641afa" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="5ad723d6-21d8-00c2-5000-01ba9fb1c758" Name="pk_KrVirtualFileCardTypes">
		<SchemeIndexedColumn Column="5ad723d6-21d8-00c2-3100-01ba9fb1c758" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="5ad723d6-21d8-00c2-7000-01ba9fb1c758" Name="idx_KrVirtualFileCardTypes_ID" IsClustered="true">
		<SchemeIndexedColumn Column="5ad723d6-21d8-01c2-4000-01ba9fb1c758" />
	</SchemeIndex>
	<SchemeIndex ID="99abb2f9-e359-44d6-b44e-5256590224ea" Name="ndx_KrVirtualFileCardTypes_TypeIDID">
		<SchemeIndexedColumn Column="59b96e45-ba11-0006-4000-04c51381f1f9" />
		<SchemeIndexedColumn Column="5ad723d6-21d8-01c2-4000-01ba9fb1c758" />
	</SchemeIndex>
</SchemeTable>