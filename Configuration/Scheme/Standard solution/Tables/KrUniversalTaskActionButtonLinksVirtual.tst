<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="0938b2e9-485e-4f87-8622-706bbaf0efb7" Name="KrUniversalTaskActionButtonLinksVirtual" Group="KrWe" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Действие "Настраиваемое задание". Коллекционная секция объединяющая связи и вырианты завершения.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="0938b2e9-485e-0087-2000-006bbaf0efb7" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="0938b2e9-485e-0187-4000-006bbaf0efb7" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="0938b2e9-485e-0087-3100-006bbaf0efb7" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="a89b8be3-732b-4f50-b04a-179241593d8a" Name="Link" Type="Reference(Abstract) Not Null" WithForeignKey="false">
		<Description>Связь.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="a89b8be3-732b-0050-4000-079241593d8a" Name="LinkID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
		<SchemePhysicalColumn ID="6393ab13-6793-47c3-bf19-1d329d21716a" Name="LinkName" Type="String(Max) Not Null" />
		<SchemePhysicalColumn ID="a6474ddd-9792-43d8-bd5b-4ebfc5813482" Name="LinkCaption" Type="String(Max) Not Null" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="52460b80-dad5-4a33-9f3a-6b284063a945" Name="Button" Type="Reference(Typified) Not Null" ReferencedTable="e85631c4-0014-4842-86f4-9a6ba66166f3" IsReferenceToOwner="true">
		<Description>Параметры действия выполняемые при завершении задания с определённым вариантом завершения.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="52460b80-dad5-0033-4000-0b284063a945" Name="ButtonRowID" Type="Guid Not Null" ReferencedColumn="e85631c4-0014-0042-3100-0a6ba66166f3" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="0938b2e9-485e-0087-5000-006bbaf0efb7" Name="pk_KrUniversalTaskActionButtonLinksVirtual">
		<SchemeIndexedColumn Column="0938b2e9-485e-0087-3100-006bbaf0efb7" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="0938b2e9-485e-0087-7000-006bbaf0efb7" Name="idx_KrUniversalTaskActionButtonLinksVirtual_ID" IsClustered="true">
		<SchemeIndexedColumn Column="0938b2e9-485e-0187-4000-006bbaf0efb7" />
	</SchemeIndex>
</SchemeTable>