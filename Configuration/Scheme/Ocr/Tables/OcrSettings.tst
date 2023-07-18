<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="a6d84d24-0461-4c65-aa5d-f41ee300f081" ID="4463ae11-e603-4daa-8b93-2e4323abef37" Name="OcrSettings" Group="Ocr" InstanceType="Cards" ContentType="Entries">
	<Description>Text recognition settings</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="4463ae11-e603-00aa-2000-0e4323abef37" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<Description>Card identifier</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="4463ae11-e603-01aa-4000-0e4323abef37" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747">
			<Description>Card identifier</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="0de7ffc9-759a-4180-9b6d-ee4cc1d30546" Name="IsEnabled" Type="Boolean Not Null">
		<Description>Text recognition in files enabled</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="aea985aa-251a-4331-ac25-df37a2f3a773" Name="df_OcrSettings_IsEnabled" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="d6951361-a7e3-403b-ac78-37679617d38d" Name="BaseAddress" Type="String(128) Not Null">
		<Description>Document web service base address</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="4463ae11-e603-00aa-5000-0e4323abef37" Name="pk_OcrSettings" IsClustered="true">
		<SchemeIndexedColumn Column="4463ae11-e603-01aa-4000-0e4323abef37" />
	</SchemePrimaryKey>
</SchemeTable>