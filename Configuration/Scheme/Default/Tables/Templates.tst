<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="9f15aaf8-032c-4222-9c7c-2cfffeee89ed" Name="Templates" Group="System" InstanceType="Cards" ContentType="Entries">
	<Description>Шаблоны карточек.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="9f15aaf8-032c-0022-2000-0cfffeee89ed" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="9f15aaf8-032c-0122-4000-0cfffeee89ed" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="df69c246-39f5-48ce-b915-dceb39827e45" Name="Digest" Type="String(128) Null">
		<Description>Краткое описание карточки в момент создания шаблона.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="b17f0fef-9477-44a8-8cf7-a2e128d31222" Name="Description" Type="String(Max) Null">
		<Description>Текстовое описание карточки шаблона, задаваемое пользователем.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="c15da5e8-f21f-44ca-ba31-5a821aa02ba4" Name="Definition" Type="Xml Null">
		<Description>Дополнительная информация, предоставленная расширениями по созданию шаблона, для фильтрации в представлениях.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="23642926-17f7-490b-86c9-902c710ffb8a" Name="Card" Type="BinaryJson Not Null">
		<Description>Карточка, для которой создан шаблон, в сериализованном виде.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="c71cd247-ea5a-41bf-a22d-86103947de24" Name="df_Templates_Card" Value="{}" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="48355f7c-a9be-47ee-bcd0-df4c4ebf8e0e" Name="CardID" Type="Guid Not Null">
		<Description>Идентификатор исходной карточки, которая использовалась для создания шаблона.</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="c4fa4541-0d47-484c-9b48-f7359652af65" Name="Type" Type="Reference(Typified) Not Null" ReferencedTable="b0538ece-8468-4d0b-8b4e-5a1d43e024db">
		<Description>Тип карточки, для которой создан шаблон.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="c4fa4541-0d47-004c-4000-07359652af65" Name="TypeID" Type="Guid Not Null" ReferencedColumn="a628a864-c858-4200-a6b7-da78c8e6e1f4">
			<Description>ID of a type.</Description>
		</SchemeReferencingColumn>
		<SchemeReferencingColumn ID="39576090-9635-45be-bfd0-63d0c5b97dbb" Name="TypeCaption" Type="String(128) Not Null" ReferencedColumn="0a02451e-2e06-4001-9138-b4805e641afa">
			<Description>Caption of a type.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="22bbbda5-f5f6-43f4-bee8-9c3eae594a95" Name="Version" Type="Int32 Not Null">
		<Description>Версия карточки на момент создания шаблона.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="5a28da2d-0d5f-48e1-a626-f9dc69278788" Name="Caption" Type="String(128) Not Null">
		<Description>Название шаблона. По умолчанию равно полю Digest, но пользователь может его изменить.</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="9f15aaf8-032c-0022-5000-0cfffeee89ed" Name="pk_Templates" IsClustered="true">
		<SchemeIndexedColumn Column="9f15aaf8-032c-0122-4000-0cfffeee89ed" />
	</SchemePrimaryKey>
</SchemeTable>