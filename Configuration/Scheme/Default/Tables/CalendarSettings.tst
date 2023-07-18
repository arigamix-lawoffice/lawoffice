<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="67b1fd42-0106-4b31-a368-ea3e4d38ac5c" Name="CalendarSettings" Group="System" InstanceType="Cards" ContentType="Entries">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="67b1fd42-0106-0031-2000-0a3e4d38ac5c" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="67b1fd42-0106-0131-4000-0a3e4d38ac5c" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="64f07fbf-6091-488d-a808-a9ad42b530b1" Name="CalendarStart" Type="DateTime Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="1cfd3375-b585-495d-a28b-6cfa71925a18" Name="df_CalendarSettings_CalendarStart" Value="2013-12-31T20:00:00Z" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="336e47fb-f3c4-42a5-8b63-6f43f3392fab" Name="CalendarEnd" Type="DateTime Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="d2027c8e-f2b2-40c5-8fb4-90fd65553839" Name="df_CalendarSettings_CalendarEnd" Value="2014-12-31T19:59:59Z" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="26552f22-2599-43ad-86e4-1e6c0e3a52db" Name="Description" Type="String(4000) Null">
		<Description>Описание (многострочный текст)</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="aeea4bac-ef15-49e9-997f-18d7a830309a" Name="CalendarID" Type="Int32 Not Null">
		<Description>Числовой идентификатор</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="a4d26aae-c25b-4231-a61d-39b828ad37c9" Name="df_CalendarSettings_CalendarID" Value="0" />
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="aeee918a-8aee-4314-b04c-57ab7830b2dd" Name="CalendarType" Type="Reference(Typified) Null" ReferencedTable="c411ab46-1df7-4a76-97b5-d0d39fff656b">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="aeee918a-8aee-0014-4000-07ab7830b2dd" Name="CalendarTypeID" Type="Guid Null" ReferencedColumn="c411ab46-1df7-0176-4000-00d39fff656b" />
		<SchemeReferencingColumn ID="6d030520-ad8d-4cce-af5d-3f8aac3ffbb5" Name="CalendarTypeCaption" Type="String(255) Null" ReferencedColumn="bd4f4c54-1921-4890-b040-3a5e54a17d7d" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="a593a14d-d146-4071-aaa7-215307755c58" Name="Name" Type="String(255) Null" />
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="67b1fd42-0106-0031-5000-0a3e4d38ac5c" Name="pk_CalendarSettings" IsClustered="true">
		<SchemeIndexedColumn Column="67b1fd42-0106-0131-4000-0a3e4d38ac5c" />
	</SchemePrimaryKey>
	<SchemeIndex ID="a3d5162b-a2cc-4cd7-adea-e75814d21b78" Name="ndx_CalendarSettings_CalendarID" IsUnique="true">
		<SchemeIndexedColumn Column="aeea4bac-ef15-49e9-997f-18d7a830309a" />
	</SchemeIndex>
</SchemeTable>