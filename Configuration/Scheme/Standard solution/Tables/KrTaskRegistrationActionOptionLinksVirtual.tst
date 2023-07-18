<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="f6a8f11a-68c2-4743-a8ed-236fe459dfc9" Name="KrTaskRegistrationActionOptionLinksVirtual" Group="KrWe" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Действие "Задание регистрации". Коллекционная секция объединяющая связи и вырианты завершения.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="f6a8f11a-68c2-0043-2000-036fe459dfc9" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="f6a8f11a-68c2-0143-4000-036fe459dfc9" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="f6a8f11a-68c2-0043-3100-036fe459dfc9" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="9570e5b4-1281-4015-a78b-4cfa89982f4a" Name="Link" Type="Reference(Abstract) Not Null" WithForeignKey="false">
		<Description>Связь.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="9570e5b4-1281-0015-4000-0cfa89982f4a" Name="LinkID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
		<SchemePhysicalColumn ID="15dea6b2-c34b-4c06-b063-e0b194146a40" Name="LinkName" Type="String(Max) Not Null" />
		<SchemePhysicalColumn ID="23d809fc-660b-4363-a719-94bb4513b97b" Name="LinkCaption" Type="String(Max) Not Null" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="f0f045c3-dbfe-4bd0-b90e-419323bae0a6" Name="Option" Type="Reference(Typified) Not Null" ReferencedTable="2ba2b1a3-b8ad-4c47-a8fd-3a3fa421c7a9" IsReferenceToOwner="true">
		<Description>Параметры действия выполняемые при завершении задания с определённым вариантом завершения.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="f0f045c3-dbfe-00d0-4000-019323bae0a6" Name="OptionRowID" Type="Guid Not Null" ReferencedColumn="2ba2b1a3-b8ad-0047-3100-0a3fa421c7a9" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="f6a8f11a-68c2-0043-5000-036fe459dfc9" Name="pk_KrTaskRegistrationActionOptionLinksVirtual">
		<SchemeIndexedColumn Column="f6a8f11a-68c2-0043-3100-036fe459dfc9" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="f6a8f11a-68c2-0043-7000-036fe459dfc9" Name="idx_KrTaskRegistrationActionOptionLinksVirtual_ID" IsClustered="true">
		<SchemeIndexedColumn Column="f6a8f11a-68c2-0143-4000-036fe459dfc9" />
	</SchemeIndex>
</SchemeTable>