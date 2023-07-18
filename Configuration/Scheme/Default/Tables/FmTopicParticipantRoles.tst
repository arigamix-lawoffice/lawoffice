<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="ecd6e90e-3bbe-4c24-975b-6644b20efe7f" Name="FmTopicParticipantRoles" Group="Fm" InstanceType="Cards" ContentType="Collections">
	<Description>Таблица с ролями - учасниками </Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="ecd6e90e-3bbe-0024-2000-0644b20efe7f" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="ecd6e90e-3bbe-0124-4000-0644b20efe7f" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="ecd6e90e-3bbe-0024-3100-0644b20efe7f" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="e573d068-cca2-4c79-830b-1ff14ec27de0" Name="Topic" Type="Reference(Typified) Not Null" ReferencedTable="35b11a3c-f9ec-4fac-a3f1-def11bba44ae">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="e573d068-cca2-0079-4000-0ff14ec27de0" Name="TopicRowID" Type="Guid Not Null" ReferencedColumn="35b11a3c-f9ec-00ac-3100-0ef11bba44ae" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="11563969-1920-4d9b-be44-4df030b19102" Name="Role" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="11563969-1920-009b-4000-0df030b19102" Name="RoleID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="dde580df-4edc-4f73-927b-d6f08fab5a93" Name="RoleName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="652cacce-06cc-436d-b118-f8917d68660a" Name="ReadOnly" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="a49a19cc-9425-4374-8ae3-320568a93b9b" Name="df_FmTopicParticipantRoles_ReadOnly" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="356e7361-31c0-45d3-9b38-3b9951bc7909" Name="Subscribed" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="58746407-253c-47d5-b39c-cd3ffe1d8250" Name="df_FmTopicParticipantRoles_Subscribed" Value="true" />
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="e565f480-a5e1-44af-a4f1-08d92666efa1" Name="InvitingUser" Type="Reference(Typified) Not Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<Description>Пользователь который добавил в топик роль</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="e565f480-a5e1-00af-4000-08d92666efa1" Name="InvitingUserID" Type="Guid Not Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="4261f59d-15e7-48dc-9211-f13257337bfb" Name="InvitingUserName" Type="String(128) Not Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="ecd6e90e-3bbe-0024-5000-0644b20efe7f" Name="pk_FmTopicParticipantRoles">
		<SchemeIndexedColumn Column="ecd6e90e-3bbe-0024-3100-0644b20efe7f" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="ecd6e90e-3bbe-0024-7000-0644b20efe7f" Name="idx_FmTopicParticipantRoles_ID" IsClustered="true">
		<SchemeIndexedColumn Column="ecd6e90e-3bbe-0124-4000-0644b20efe7f" />
	</SchemeIndex>
	<SchemeIndex ID="984000b7-b11a-4ac3-9ff1-372cd6862c44" Name="ndx_FmTopicParticipantRoles_TopicRowID">
		<SchemeIndexedColumn Column="e573d068-cca2-0079-4000-0ff14ec27de0" />
	</SchemeIndex>
</SchemeTable>