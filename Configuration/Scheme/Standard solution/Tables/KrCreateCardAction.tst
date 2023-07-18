<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="70e22440-564a-40a9-88a1-f695844a113b" Name="KrCreateCardAction" Group="KrWe" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<Description>Основная секция действия создания карточки по типу или шаблону</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="70e22440-564a-00a9-2000-0695844a113b" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="70e22440-564a-01a9-4000-0695844a113b" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="a1ef3380-9bf9-48c8-b556-419dc5ee60d7" Name="Type" Type="Reference(Typified) Null" ReferencedTable="b0538ece-8468-4d0b-8b4e-5a1d43e024db">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="a1ef3380-9bf9-00c8-4000-019dc5ee60d7" Name="TypeID" Type="Guid Null" ReferencedColumn="a628a864-c858-4200-a6b7-da78c8e6e1f4" />
		<SchemeReferencingColumn ID="0b6025c9-f7d7-4307-96c8-c0c0152d3879" Name="TypeCaption" Type="String(128) Null" ReferencedColumn="0a02451e-2e06-4001-9138-b4805e641afa" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="e8559402-f848-410f-87f5-de3449356e42" Name="Template" Type="Reference(Typified) Null" ReferencedTable="9f15aaf8-032c-4222-9c7c-2cfffeee89ed">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="e8559402-f848-000f-4000-0e3449356e42" Name="TemplateID" Type="Guid Null" ReferencedColumn="9f15aaf8-032c-0122-4000-0cfffeee89ed" />
		<SchemeReferencingColumn ID="cecbfa98-831e-43dd-891f-5430e6adaa19" Name="TemplateCaption" Type="String(128) Null" ReferencedColumn="5a28da2d-0d5f-48e1-a626-f9dc69278788" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="d2463d6a-6b7c-4dd4-a618-d794dc7c5232" Name="OpenCard" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="02ed8212-a873-4310-8905-45a2342d784f" Name="df_KrCreateCardAction_OpenCard" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="879804ee-c266-4a85-94ae-d5983d75ec22" Name="SetAsMainCard" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="2bebfb61-d364-41a7-b715-cc5290054029" Name="df_KrCreateCardAction_SetAsMainCard" Value="true" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="2e197651-9521-4c42-8a65-273b69806d9b" Name="Script" Type="String(Max) Null" />
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="70e22440-564a-00a9-5000-0695844a113b" Name="pk_KrCreateCardAction" IsClustered="true">
		<SchemeIndexedColumn Column="70e22440-564a-01a9-4000-0695844a113b" />
	</SchemePrimaryKey>
</SchemeTable>