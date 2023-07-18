<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="03d962fa-c020-481a-90bd-932cbbd4368d" Name="WorkflowOutLinks" Group="WorkflowEngine" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Список исходящих из узла связей</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="03d962fa-c020-001a-2000-032cbbd4368d" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="03d962fa-c020-011a-4000-032cbbd4368d" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="03d962fa-c020-001a-3100-032cbbd4368d" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="bd1ed5f2-0477-4391-9e1b-cae1d0fc4424" Name="Name" Type="String(128) Not Null">
		<Description>Имя перехода</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="73ca920d-7ad8-4047-ba48-157939d3e7bd" Name="Caption" Type="String(128) Not Null">
		<Description>Заголовок перехода</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="370ee7fc-28df-40f5-9f0f-19135412920d" Name="Script" Type="String(Max) Not Null">
		<Description>Скрипт-условие для осуществления перехода</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="49e379bc-96b3-4955-9346-fd402e199096" Name="HasCondition" Type="Boolean Not Null">
		<Description>Флаг, определяющий, задано ли условие перехода</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="532126cf-1645-4c72-b778-df279d1cf8a7" Name="Description" Type="String(Max) Not Null">
		<Description>Описание</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="03d962fa-c020-001a-5000-032cbbd4368d" Name="pk_WorkflowOutLinks">
		<SchemeIndexedColumn Column="03d962fa-c020-001a-3100-032cbbd4368d" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="03d962fa-c020-001a-7000-032cbbd4368d" Name="idx_WorkflowOutLinks_ID" IsClustered="true">
		<SchemeIndexedColumn Column="03d962fa-c020-011a-4000-032cbbd4368d" />
	</SchemeIndex>
</SchemeTable>