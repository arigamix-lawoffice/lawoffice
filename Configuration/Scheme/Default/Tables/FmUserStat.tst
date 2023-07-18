<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="d10d18eb-d803-4151-8a60-8bfd262d2800" Name="FmUserStat" Group="Fm" InstanceType="Cards" ContentType="Collections">
	<Description>Таблица, в который храним дату посещения пользователем топика</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="d10d18eb-d803-0051-2000-0bfd262d2800" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="d10d18eb-d803-0151-4000-0bfd262d2800" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="d10d18eb-d803-0051-3100-0bfd262d2800" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="c5ae3770-9d5c-4cf2-be45-b13dfb034594" Name="Topic" Type="Reference(Typified) Not Null" ReferencedTable="35b11a3c-f9ec-4fac-a3f1-def11bba44ae" IsReferenceToOwner="true">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="c5ae3770-9d5c-00f2-4000-013dfb034594" Name="TopicRowID" Type="Guid Not Null" ReferencedColumn="35b11a3c-f9ec-00ac-3100-0ef11bba44ae" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="322ccb71-3753-45fc-be4d-b1c7da8c66d2" Name="User" Type="Reference(Typified) Not Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="322ccb71-3753-00fc-4000-01c7da8c66d2" Name="UserID" Type="Guid Not Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="1951f9f5-4db0-4e47-9f01-fa6c1aef7c99" Name="LastReadMessageTime" Type="DateTime2 Null">
		<Description>Дата последнего прочитанного сообщения</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="d10d18eb-d803-0051-5000-0bfd262d2800" Name="pk_FmUserStat">
		<SchemeIndexedColumn Column="d10d18eb-d803-0051-3100-0bfd262d2800" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="d10d18eb-d803-0051-7000-0bfd262d2800" Name="idx_FmUserStat_ID" IsClustered="true">
		<SchemeIndexedColumn Column="d10d18eb-d803-0151-4000-0bfd262d2800" />
	</SchemeIndex>
	<SchemeIndex ID="28e9852d-927c-449c-a03d-59c2a5bf240e" Name="ndx_FmUserStat_UserIDTopicRowID" IsUnique="true">
		<SchemeIndexedColumn Column="322ccb71-3753-00fc-4000-01c7da8c66d2" />
		<SchemeIndexedColumn Column="c5ae3770-9d5c-00f2-4000-013dfb034594" />
	</SchemeIndex>
	<SchemeIndex ID="38fd9b47-836e-4223-8e57-8229466534b2" Name="ndx_FmUserStat_TopicRowID">
		<Description>Быстрое удаление топиков для FK.</Description>
		<SchemeIndexedColumn Column="c5ae3770-9d5c-00f2-4000-013dfb034594" />
	</SchemeIndex>
</SchemeTable>