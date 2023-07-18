<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="971d8661-d445-42fb-84d0-b0b71aa978a2" Name="KrStageTypes" Group="Kr" InstanceType="Cards" ContentType="Collections">
	<Description>Список типов документов для шаблона этапа и группы этапов</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="971d8661-d445-00fb-2000-00b71aa978a2" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="971d8661-d445-01fb-4000-00b71aa978a2" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="971d8661-d445-00fb-3100-00b71aa978a2" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="f42734d0-d8e4-49d3-b4ed-6d74e8ad9e9f" Name="Type" Type="Reference(Typified) Not Null" ReferencedTable="a90baecf-c9ce-4cba-8bb0-150a13666266" WithForeignKey="false">
		<Description>Тип карточки, для которого применимы вычисления шаблона</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="f42734d0-d8e4-00d3-4000-0d74e8ad9e9f" Name="TypeID" Type="Guid Not Null" ReferencedColumn="a90baecf-c9ce-01ba-4000-050a13666266" />
		<SchemeReferencingColumn ID="9acc7f1e-d5ed-4b42-907a-79d8bed43894" Name="TypeCaption" Type="String(128) Not Null" ReferencedColumn="447f7cb1-76ae-4703-b3bb-16a57d4e7ab1" />
		<SchemePhysicalColumn ID="df33fde1-d10f-438a-b224-dabd671dbfd0" Name="TypeIsDocType" Type="Boolean Not Null">
			<Description>Признак того, что указанный тип - это тип документа (а не карточки).</Description>
			<SchemeDefaultConstraint IsPermanent="true" ID="04867c14-e5d7-483a-ace1-d6b2855ddd77" Name="df_KrStageTypes_TypeIsDocType" Value="false" />
		</SchemePhysicalColumn>
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="971d8661-d445-00fb-5000-00b71aa978a2" Name="pk_KrStageTypes">
		<SchemeIndexedColumn Column="971d8661-d445-00fb-3100-00b71aa978a2" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="971d8661-d445-00fb-7000-00b71aa978a2" Name="idx_KrStageTypes_ID" IsClustered="true">
		<SchemeIndexedColumn Column="971d8661-d445-01fb-4000-00b71aa978a2" />
	</SchemeIndex>
	<SchemeIndex ID="05a48b1c-769b-4ddd-bec5-231800d2c369" Name="ndx_KrStageTypes_TypeIDID" IsUnique="true">
		<SchemeIndexedColumn Column="f42734d0-d8e4-00d3-4000-0d74e8ad9e9f" />
		<SchemeIndexedColumn Column="971d8661-d445-01fb-4000-00b71aa978a2" />
	</SchemeIndex>
</SchemeTable>