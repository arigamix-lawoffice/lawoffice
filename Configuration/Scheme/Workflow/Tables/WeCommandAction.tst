<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="2dc38a34-3451-4ea9-9885-9afa15155612" Name="WeCommandAction" Group="WorkflowEngine" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<Description>Секция для действия Подписка на команду</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="2dc38a34-3451-00a9-2000-0afa15155612" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="2dc38a34-3451-01a9-4000-0afa15155612" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="2d9e306a-07f1-4f7b-83a9-583c2876db47" Name="ReusableSubscription" Type="Boolean Not Null">
		<Description>Флаг определяет, является ли подписка многоразовой</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="89f40f35-adbe-46b1-9bff-037eb0223d12" Name="Command" Type="Reference(Typified) Not Null" ReferencedTable="53dc8c0b-391a-4fbd-86c0-3da697abf065">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="89f40f35-adbe-00b1-4000-037eb0223d12" Name="CommandID" Type="Guid Not Null" ReferencedColumn="cabbc72d-b093-43be-a645-8503664980d6" />
		<SchemeReferencingColumn ID="37de289b-9ff8-4347-b1f5-10b48ff038c8" Name="CommandName" Type="String(128) Not Null" ReferencedColumn="2e7c413d-0de6-4900-ac97-68ce16e3da11" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="2dc38a34-3451-00a9-5000-0afa15155612" Name="pk_WeCommandAction" IsClustered="true">
		<SchemeIndexedColumn Column="2dc38a34-3451-01a9-4000-0afa15155612" />
	</SchemePrimaryKey>
</SchemeTable>