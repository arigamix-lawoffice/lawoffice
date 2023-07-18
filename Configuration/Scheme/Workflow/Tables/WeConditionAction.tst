<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="ad4abe1f-9f6b-4842-b8d5-bb34502c7dce" Name="WeConditionAction" Group="WorkflowEngine" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Основная секция действия Условия</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="ad4abe1f-9f6b-0042-2000-0b34502c7dce" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="ad4abe1f-9f6b-0142-4000-0b34502c7dce" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="ad4abe1f-9f6b-0042-3100-0b34502c7dce" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="1e2f26ba-e660-4a20-88ad-107a5d3b5b1a" Name="Condition" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="98e7786b-866d-46a8-840f-18e465842008" Name="IsElse" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="f6d9da28-0c45-41a8-8e11-57c2f33b7dfd" Name="df_WeConditionAction_IsElse" Value="false" />
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="39a42868-6ace-4285-90c5-7db6dab18efb" Name="Link" Type="Reference(Typified) Not Null" ReferencedTable="87f7e0c3-2d97-4e36-bb14-1aeec6e67a94">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="39a42868-6ace-0085-4000-0db6dab18efb" Name="LinkID" Type="Guid Not Null" ReferencedColumn="87f7e0c3-2d97-0136-4000-0aeec6e67a94" />
		<SchemeReferencingColumn ID="8c923976-2fab-4564-9284-5d8abea80ca2" Name="LinkName" Type="String(128) Not Null" ReferencedColumn="3113bddf-32cc-439a-8cde-4773ee2d35d8" />
		<SchemeReferencingColumn ID="8a9abb82-9eae-4488-88e0-5abf9b5d5418" Name="LinkCaption" Type="String(128) Not Null" ReferencedColumn="c58e9c8e-572d-4e79-8ab1-441bf2e6bdba" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="e170ab93-79a7-4227-9a59-a4fd1aa13f47" Name="Description" Type="String(Max) Not Null" />
	<SchemePhysicalColumn ID="cf7dc364-11b5-4249-9e9f-744a90fd317c" Name="TypeOfConditionCheck" Type="String(Max) Not Null" />
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="ad4abe1f-9f6b-0042-5000-0b34502c7dce" Name="pk_WeConditionAction">
		<SchemeIndexedColumn Column="ad4abe1f-9f6b-0042-3100-0b34502c7dce" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="ad4abe1f-9f6b-0042-7000-0b34502c7dce" Name="idx_WeConditionAction_ID" IsClustered="true">
		<SchemeIndexedColumn Column="ad4abe1f-9f6b-0142-4000-0b34502c7dce" />
	</SchemeIndex>
</SchemeTable>