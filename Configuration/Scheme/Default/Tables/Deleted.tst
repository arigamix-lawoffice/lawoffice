<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="a49102cc-6bb4-425b-95ad-75ff0b3edf0d" Name="Deleted" Group="System" InstanceType="Cards" ContentType="Entries">
	<Description>Информация об удалённой карточке.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="a49102cc-6bb4-005b-2000-05ff0b3edf0d" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="a49102cc-6bb4-015b-4000-05ff0b3edf0d" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="8ec16f22-aef1-4f39-8f88-68589e9c017a" Name="Digest" Type="String(128) Null">
		<Description>Краткое описание карточки в момент удаления.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="5509ca1e-1527-48a3-9bdc-aff6f47c177d" Name="Card" Type="BinaryJson Not Null">
		<Description>Удалённая карточка в сериализованном виде.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="3627013e-57d1-4ee9-b32f-6c8641db6093" Name="df_Deleted_Card" Value="{}" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="6a80b8a6-bd38-4b81-a747-8c39a9e68dd1" Name="CardID" Type="Guid Not Null">
		<Description>Идентификатор удалённой карточки.</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="3bea0d27-bfa9-475c-b771-c4fcb69be8eb" Name="Type" Type="Reference(Typified) Not Null" ReferencedTable="b0538ece-8468-4d0b-8b4e-5a1d43e024db">
		<Description>Тип удалённой карточки.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="3bea0d27-bfa9-005c-4000-04fcb69be8eb" Name="TypeID" Type="Guid Not Null" ReferencedColumn="a628a864-c858-4200-a6b7-da78c8e6e1f4">
			<Description>ID of a type.</Description>
		</SchemeReferencingColumn>
		<SchemeReferencingColumn ID="55b69fb9-72b3-4829-8298-076258924c41" Name="TypeCaption" Type="String(128) Not Null" ReferencedColumn="0a02451e-2e06-4001-9138-b4805e641afa">
			<Description>Caption of a type.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="a5f2fe3b-998f-4c8c-8384-e78aaa31829d" Name="Created" Type="DateTime Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="8eddda58-7f1d-4917-bd1b-2cf1098d2a7c" Name="df_Deleted_Created" Value="2014-08-21T00:00:00Z" />
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="119a0608-122e-425a-aa79-f7d0204f4725" Name="CreatedBy" Type="Reference(Typified) Not Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="119a0608-122e-005a-4000-07d0204f4725" Name="CreatedByID" Type="Guid Not Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3">
			<SchemeDefaultConstraint IsPermanent="true" ID="12b4e679-eeb6-4565-b12a-e1e5543a4382" Name="df_Deleted_CreatedByID" Value="11111111-1111-1111-1111-111111111111" />
		</SchemeReferencingColumn>
		<SchemeReferencingColumn ID="897c1432-4062-4de2-853f-6a0de0fd0b78" Name="CreatedByName" Type="String(128) Not Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964">
			<SchemeDefaultConstraint IsPermanent="true" ID="3cb405d4-0d7a-4697-ae1f-4cbf16b2c19e" Name="df_Deleted_CreatedByName" Value="System" />
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="a49102cc-6bb4-005b-5000-05ff0b3edf0d" Name="pk_Deleted" IsClustered="true">
		<SchemeIndexedColumn Column="a49102cc-6bb4-015b-4000-05ff0b3edf0d" />
	</SchemePrimaryKey>
	<SchemeIndex ID="ec3e010f-d04b-4f9d-9f8e-d906841c0b0c" Name="ndx_Deleted_CardID">
		<SchemeIndexedColumn Column="6a80b8a6-bd38-4b81-a747-8c39a9e68dd1" />
	</SchemeIndex>
	<SchemeIndex ID="873e45da-f011-4662-baac-b5bb26691b21" Name="ndx_Deleted_Created">
		<SchemeIndexedColumn Column="a5f2fe3b-998f-4c8c-8384-e78aaa31829d" />
	</SchemeIndex>
</SchemeTable>