<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="4801bc15-cfaf-455c-aba6-cf77dd72484d" Name="KrSettingsCycleGrouping" Group="Kr" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="4801bc15-cfaf-005c-2000-0f77dd72484d" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="4801bc15-cfaf-015c-4000-0f77dd72484d" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="4801bc15-cfaf-005c-3100-0f77dd72484d" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="d025808d-aad1-4967-9920-4c811475ae7c" Name="Type" Type="Reference(Typified) Not Null" ReferencedTable="a90baecf-c9ce-4cba-8bb0-150a13666266" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="d025808d-aad1-0067-4000-0c811475ae7c" Name="TypeID" Type="Guid Not Null" ReferencedColumn="a90baecf-c9ce-01ba-4000-050a13666266" />
		<SchemeReferencingColumn ID="ba4886fe-b393-44b6-96e5-937e67d564f1" Name="TypeCaption" Type="String(128) Not Null" ReferencedColumn="447f7cb1-76ae-4703-b3bb-16a57d4e7ab1" />
		<SchemePhysicalColumn ID="fdecbe75-d543-4797-a148-605ac987a359" Name="TypeIsDocType" Type="Boolean Not Null">
			<Description>Признак того, что указанный тип - это тип документа (а не карточки).</Description>
		</SchemePhysicalColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="03de3b36-ffef-493c-b674-10677869ebc2" Name="Types" Type="Reference(Typified) Not Null" ReferencedTable="4012de1a-efd8-442d-a25c-8fe78008e38d" IsReferenceToOwner="true">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="03de3b36-ffef-003c-4000-00677869ebc2" Name="TypesRowID" Type="Guid Not Null" ReferencedColumn="4012de1a-efd8-002d-3100-0fe78008e38d" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="4801bc15-cfaf-005c-5000-0f77dd72484d" Name="pk_KrSettingsCycleGrouping">
		<SchemeIndexedColumn Column="4801bc15-cfaf-005c-3100-0f77dd72484d" />
	</SchemePrimaryKey>
	<SchemeUniqueKey ID="5e317ed3-50e1-4f1b-9674-a51b53827415" Name="ndx_KrSettingsCycleGrouping" />
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="4801bc15-cfaf-005c-7000-0f77dd72484d" Name="idx_KrSettingsCycleGrouping_ID" IsClustered="true">
		<SchemeIndexedColumn Column="4801bc15-cfaf-015c-4000-0f77dd72484d" />
	</SchemeIndex>
</SchemeTable>