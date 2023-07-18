<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="f4947518-a710-4693-8c8b-ab2acc42bc5a" Name="TagsUserSettingsVirtual" Group="Tags" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<Description>Виртуальная таблица для формы с пользовательскими настройками тегов</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="f4947518-a710-0093-2000-0b2acc42bc5a" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="f4947518-a710-0193-4000-0b2acc42bc5a" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="9a24ae55-9c57-494f-956d-ea6d5dac6fd5" Name="MaxTagsDisplayed" Type="Int16 Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="a4e1d9c7-f863-4caa-98a9-4c916a57114c" Name="df_TagsUserSettingsVirtual_MaxTagsDisplayed" Value="5" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="f4947518-a710-0093-5000-0b2acc42bc5a" Name="pk_TagsUserSettingsVirtual" IsClustered="true">
		<SchemeIndexedColumn Column="f4947518-a710-0193-4000-0b2acc42bc5a" />
	</SchemePrimaryKey>
</SchemeTable>