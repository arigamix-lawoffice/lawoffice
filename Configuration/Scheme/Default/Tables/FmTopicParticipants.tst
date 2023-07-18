<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="b8150fdd-b439-4eaa-9665-9a8b9ee774f0" Name="FmTopicParticipants" Group="Fm" InstanceType="Cards" ContentType="Collections">
	<Description>Участники топика</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="b8150fdd-b439-00aa-2000-0a8b9ee774f0" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="b8150fdd-b439-01aa-4000-0a8b9ee774f0" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="b8150fdd-b439-00aa-3100-0a8b9ee774f0" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="96226f50-ccfb-41b1-bb41-9171bd771f99" Name="Topic" Type="Reference(Typified) Not Null" ReferencedTable="35b11a3c-f9ec-4fac-a3f1-def11bba44ae">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="96226f50-ccfb-00b1-4000-0171bd771f99" Name="TopicRowID" Type="Guid Not Null" ReferencedColumn="35b11a3c-f9ec-00ac-3100-0ef11bba44ae" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="7d6a173d-4565-4f51-9460-71623d86d428" Name="User" Type="Reference(Typified) Not Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="7d6a173d-4565-0051-4000-01623d86d428" Name="UserID" Type="Guid Not Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="9b802008-a3af-4edf-9c34-ff8afb4eb29d" Name="UserName" Type="String(128) Not Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="5a12a666-5710-4220-b1cf-36fd16346715" Name="ReadOnly" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="ec0e3cdd-a48e-48df-a1f4-34a6890f0f26" Name="df_FmTopicParticipants_ReadOnly" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="3fdc17ca-89d0-4710-802e-5bbddf6c4cbb" Name="Subscribed" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="c29385fa-a7ae-4a14-9a90-c780182a2872" Name="df_FmTopicParticipants_Subscribed" Value="true" />
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="f5cd0e85-3290-43f1-9ab9-79b46f6b7d5c" Name="Type" Type="Reference(Typified) Not Null" ReferencedTable="2b2a8e44-eecd-4afe-b017-20f8a00846ff">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="f5cd0e85-3290-00f1-4000-09b46f6b7d5c" Name="TypeID" Type="Int32 Not Null" ReferencedColumn="9a779c0c-c9b5-4a10-96f6-134a10783c05" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="40006242-a69b-47f8-96f1-d57b037ac10d" Name="InvitingUser" Type="Reference(Typified) Not Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<Description>Пользователь который добавил в топик роль</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="40006242-a69b-00f8-4000-057b037ac10d" Name="InvitingUserID" Type="Guid Not Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="f29ddae8-3b2e-4f36-b756-c69ea720c07f" Name="InvitingUserName" Type="String(128) Not Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="b8150fdd-b439-00aa-5000-0a8b9ee774f0" Name="pk_FmTopicParticipants">
		<SchemeIndexedColumn Column="b8150fdd-b439-00aa-3100-0a8b9ee774f0" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="b8150fdd-b439-00aa-7000-0a8b9ee774f0" Name="idx_FmTopicParticipants_ID" IsClustered="true">
		<SchemeIndexedColumn Column="b8150fdd-b439-01aa-4000-0a8b9ee774f0" />
	</SchemeIndex>
	<SchemeIndex ID="d79caba8-63e2-48f7-8819-7cf204a72140" Name="ndx_FmTopicParticipants_TopicRowID">
		<SchemeIndexedColumn Column="96226f50-ccfb-00b1-4000-0171bd771f99" />
	</SchemeIndex>
</SchemeTable>