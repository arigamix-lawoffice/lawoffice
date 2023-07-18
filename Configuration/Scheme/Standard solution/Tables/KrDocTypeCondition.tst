<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="7a74559a-0729-4dd8-9040-3367367ac673" Name="KrDocTypeCondition" Group="Kr" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Секция для условия для правил уведомлений, првоеряющая тип документа/карточки.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="7a74559a-0729-00d8-2000-0367367ac673" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="7a74559a-0729-01d8-4000-0367367ac673" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="7a74559a-0729-00d8-3100-0367367ac673" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="b9702192-edf7-4fc3-b398-2db262ed22d3" Name="DocType" Type="Reference(Typified) Not Null" ReferencedTable="a90baecf-c9ce-4cba-8bb0-150a13666266">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="b9702192-edf7-00c3-4000-0db262ed22d3" Name="DocTypeID" Type="Guid Not Null" ReferencedColumn="a90baecf-c9ce-01ba-4000-050a13666266" />
		<SchemeReferencingColumn ID="74571440-9cb3-456b-87ec-0de4e232aae6" Name="DocTypeCaption" Type="String(128) Not Null" ReferencedColumn="447f7cb1-76ae-4703-b3bb-16a57d4e7ab1" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="7a74559a-0729-00d8-5000-0367367ac673" Name="pk_KrDocTypeCondition">
		<SchemeIndexedColumn Column="7a74559a-0729-00d8-3100-0367367ac673" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="7a74559a-0729-00d8-7000-0367367ac673" Name="idx_KrDocTypeCondition_ID" IsClustered="true">
		<SchemeIndexedColumn Column="7a74559a-0729-01d8-4000-0367367ac673" />
	</SchemeIndex>
</SchemeTable>