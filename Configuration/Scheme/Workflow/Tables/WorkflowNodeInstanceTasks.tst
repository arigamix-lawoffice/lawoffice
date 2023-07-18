<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="6ba32f52-56a3-4319-968b-90f1651cc5a7" Name="WorkflowNodeInstanceTasks" Group="WorkflowEngine" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Секция с отображением заданий, привязанных к экземпляру узла</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="6ba32f52-56a3-0019-2000-00f1651cc5a7" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="6ba32f52-56a3-0119-4000-00f1651cc5a7" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="6ba32f52-56a3-0019-3100-00f1651cc5a7" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="040a1e25-8315-4ea0-8bfe-e0e9691f0330" Name="Role" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="040a1e25-8315-00a0-4000-00e9691f0330" Name="RoleID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="6fad1a97-2c04-439b-96dd-73902eacd173" Name="RoleName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="b43e72ec-5bb2-4fea-87b5-0e123e1afb50" Name="User" Type="Reference(Typified) Not Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="b43e72ec-5bb2-00ea-4000-0e123e1afb50" Name="UserID" Type="Guid Not Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="cb369113-ed9c-49bf-beb6-60e3630835de" Name="UserName" Type="String(128) Not Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="564bb44a-0fb4-4184-ae01-253f17e4abcf" Name="Planned" Type="DateTime Not Null" />
	<SchemePhysicalColumn ID="4ecb4026-bdfc-4901-87a1-152609d9b924" Name="InProgress" Type="DateTime Null" />
	<SchemeComplexColumn ID="de9d862e-3a62-441d-9a94-25932a7088ea" Name="Type" Type="Reference(Typified) Not Null" ReferencedTable="b0538ece-8468-4d0b-8b4e-5a1d43e024db">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="de9d862e-3a62-001d-4000-05932a7088ea" Name="TypeID" Type="Guid Not Null" ReferencedColumn="a628a864-c858-4200-a6b7-da78c8e6e1f4" />
		<SchemeReferencingColumn ID="3df69ac1-ccb6-404e-bcfa-73e3398144d2" Name="TypeCaption" Type="String(128) Not Null" ReferencedColumn="0a02451e-2e06-4001-9138-b4805e641afa" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="848171dd-03e9-471f-a8ad-83297eeb289e" Name="Digest" Type="String(Max) Not Null" />
	<SchemeComplexColumn ID="95a236c3-7a62-4f0b-8452-14d570e7bf30" Name="Author" Type="Reference(Typified) Not Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="95a236c3-7a62-000b-4000-04d570e7bf30" Name="AuthorID" Type="Guid Not Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="201e3c08-058f-4e9e-a6d1-5cfa9d30ac22" Name="AuthorName" Type="String(128) Not Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="6a8a7eae-2cff-41e1-a414-ff7cb87ef205" Name="Postponed" Type="DateTime Null" />
	<SchemePhysicalColumn ID="86095cd6-2569-4f0a-8817-45ffb05114dc" Name="PostponedTo" Type="DateTime Null" />
	<SchemePhysicalColumn ID="f24aedbc-2edb-4305-b439-e968c2881568" Name="Created" Type="DateTime Not Null" />
	<SchemeComplexColumn ID="276bacc6-6ba9-4cb4-b67a-06996fdb7bb4" Name="State" Type="Reference(Typified) Not Null" ReferencedTable="057a85c8-c20f-430b-bd3b-6ea9f9fb82ee">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="276bacc6-6ba9-00b4-4000-06996fdb7bb4" Name="StateID" Type="Int16 Not Null" ReferencedColumn="413df3de-fc7a-476d-a604-77ee5135e7bc" />
		<SchemeReferencingColumn ID="973b405c-59f2-4a18-95ec-5c071cb9e154" Name="StateName" Type="String(128) Not Null" ReferencedColumn="e715302d-7604-416a-b7f6-8c8d99b48a17" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="6ba32f52-56a3-0019-5000-00f1651cc5a7" Name="pk_WorkflowNodeInstanceTasks">
		<SchemeIndexedColumn Column="6ba32f52-56a3-0019-3100-00f1651cc5a7" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="6ba32f52-56a3-0019-7000-00f1651cc5a7" Name="idx_WorkflowNodeInstanceTasks_ID" IsClustered="true">
		<SchemeIndexedColumn Column="6ba32f52-56a3-0119-4000-00f1651cc5a7" />
	</SchemeIndex>
</SchemeTable>