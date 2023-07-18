<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="0d23c056-70cc-4b25-9c3b-d6e2a9e48509" Name="KrBuildGlobalOutputVirtual" Group="Kr" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Секция, используемая для вывода результатов сборки всех объектов подсистемы маршрутов.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="0d23c056-70cc-0025-2000-06e2a9e48509" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="0d23c056-70cc-0125-4000-06e2a9e48509" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="0d23c056-70cc-0025-3100-06e2a9e48509" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="1d1c6c50-f4fe-4aa3-aa1f-46544d82597c" Name="Object" Type="Reference(Abstract) Not Null" WithForeignKey="false">
		<Description>Объект.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="1d1c6c50-f4fe-00a3-4000-06544d82597c" Name="ObjectID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
		<SchemePhysicalColumn ID="9ae70bd2-a6cc-4f94-adda-4b3bfdb96969" Name="ObjectName" Type="String(Max) Not Null" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="60df0fb3-99f9-460d-a2ab-8fe5e1e3c2f2" Name="ObjectTypeCaption" Type="String(Max) Not Null">
		<Description>Отображаемое имя типа объекта.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="436bdbf0-564a-4ce7-87b5-20074d9acf88" Name="Output" Type="String(Max) Null">
		<Description>Результаты сборки.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="e237f94f-6c5d-4058-ad16-2467d056f8d7" Name="CompilationDateTime" Type="DateTime Null">
		<Description>Дата и время сборки.</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="eeabad85-d916-4aa2-83e3-bec09be9ba73" Name="State" Type="Reference(Typified) Not Null" ReferencedTable="e12af590-efd5-4890-b1c7-5a7ce83195dd">
		<Description>Состояние сборки</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="eeabad85-d916-00a2-4000-0ec09be9ba73" Name="StateID" Type="Int16 Not Null" ReferencedColumn="cc448df6-5d4f-4641-91ea-32dd7c455205" />
		<SchemeReferencingColumn ID="d3db86ee-f059-4fd8-bd98-0edc59b0ceb7" Name="StateName" Type="String(128) Not Null" ReferencedColumn="b0b81b41-7500-4290-8055-af8d5ee3d310" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="0d23c056-70cc-0025-5000-06e2a9e48509" Name="pk_KrBuildGlobalOutputVirtual">
		<SchemeIndexedColumn Column="0d23c056-70cc-0025-3100-06e2a9e48509" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="0d23c056-70cc-0025-7000-06e2a9e48509" Name="idx_KrBuildGlobalOutputVirtual_ID" IsClustered="true">
		<SchemeIndexedColumn Column="0d23c056-70cc-0125-4000-06e2a9e48509" />
	</SchemeIndex>
</SchemeTable>