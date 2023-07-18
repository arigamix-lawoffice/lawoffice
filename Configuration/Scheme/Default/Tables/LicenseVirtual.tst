<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="f81a7db5-a883-49e0-918c-59a5967828b5" Name="LicenseVirtual" Group="System" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<Description>Виртуальная секция для настроек лицензий.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="f81a7db5-a883-00e0-2000-09a5967828b5" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="f81a7db5-a883-01e0-4000-09a5967828b5" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="ce32af22-9929-4cfc-99e8-cf318bfd89bd" Name="ConcurrentCount" Type="Int32 Not Null">
		<Description>Текущее количество задействованных конкурентных лицензий.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="04e1369c-df04-45d1-ba45-bbb392021459" Name="ConcurrentLimit" Type="Int32 Not Null">
		<Description>Максимальное количество задействованных конкурентных лицензий.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="3987af6b-caa2-4fdc-a4ff-0705a7017640" Name="ConcurrentText" Type="String(128) Not Null">
		<Description>Информация по конкурентным лицензиям.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="205b77dd-2b07-49e9-8074-6b9455c08a27" Name="PersonalCount" Type="Int32 Not Null">
		<Description>Текущее количество задействованных персональных лицензий.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="1337ce44-6a0b-439e-babb-700221fd6d09" Name="PersonalLimit" Type="Int32 Not Null">
		<Description>Максимальное количество задействованных персональных лицензий.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="0456dc91-9cab-4a7a-a9d2-2254131ee77e" Name="PersonalText" Type="String(128) Not Null">
		<Description>Информация по персональным лицензиям.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="4cbddbd9-3e8a-4951-81b9-1d9e65b62d56" Name="MobileCount" Type="Int32 Not Null">
		<Description>Текущее количество задействованных лицензий мобильного согласования.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="16aecbb1-fd7e-42e8-848e-7386e5865c6b" Name="MobileLimit" Type="Int32 Not Null">
		<Description>Максимальное количество задействованных лицензий мобильного согласования.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="b475b9ba-de22-4f57-942b-f11347cec1ac" Name="MobileText" Type="String(128) Not Null">
		<Description>Информация по лицензиям мобильного согласования.</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="f81a7db5-a883-00e0-5000-09a5967828b5" Name="pk_LicenseVirtual" IsClustered="true">
		<SchemeIndexedColumn Column="f81a7db5-a883-01e0-4000-09a5967828b5" />
	</SchemePrimaryKey>
</SchemeTable>