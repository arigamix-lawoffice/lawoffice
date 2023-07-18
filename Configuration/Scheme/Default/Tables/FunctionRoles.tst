<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="a59078ce-8acf-4c45-a49a-503fa88a0580" Name="FunctionRoles" Group="System">
	<Description>Функциональные роли заданий, такие как "автор", "исполнитель", "контролёр" и др.</Description>
	<SchemePhysicalColumn ID="bd4fdcea-8042-488a-94c9-770b49357cfe" Name="ID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="8e682682-9729-4231-8152-046c69337615" Name="Name" Type="String(128) Not Null">
		<Description>Уникальное имя функциональной роли.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="f8b3afc6-cea7-4a98-b907-e716e0a426c6" Name="Caption" Type="String(128) Not Null">
		<Description>Отображаемое название функциональной роли. Может быть строкой локализации $Alias</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="196f68f6-d645-4aab-a016-33078a421fcd" Name="CanBeDeputy" Type="Boolean Not Null">
		<Description>Признак того, что пользователь может входить в функциональную роль как заместитель. В противном случае проверяется только явное включение в роль (RoleUsers.IsDeputy = false).</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="368bd3db-960d-4c66-acc5-4900b53d5518" Name="df_FunctionRoles_CanBeDeputy" Value="true" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="232bc5f4-b628-4a3b-a6e5-053e2fca59f6" Name="CanTakeInProgress" Type="Boolean Not Null">
		<Description>Разрешает брать задание в работу</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="383e0984-ee0f-4a76-b2fa-905eda2f0b56" Name="df_FunctionRoles_CanTakeInProgress" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="b685759e-b32a-4cad-9eec-7d71f9af48d4" Name="HideTaskByDefault" Type="Boolean Not Null">
		<Description>Скрывать задание по умолчанию</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="41f0b78d-dfda-4f5b-9a99-f2a1032ec035" Name="df_FunctionRoles_HideTaskByDefault" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="430d64f0-0dcc-4bc7-8ebc-210a9593e01d" Name="CanChangeTaskInfo" Type="Boolean Not Null">
		<Description>Разрешает изменять дайджест и плановую дату</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="25196e7a-3011-448c-a551-b3743857b5db" Name="df_FunctionRoles_CanChangeTaskInfo" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="477aaa9f-5792-436a-abd3-6817aabe5325" Name="CanChangeTaskRoles" Type="Boolean Not Null">
		<Description>Разрешает изменять список ролей своего задания</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="61985f61-e44d-4231-a494-406400bfe442" Name="df_FunctionRoles_CanChangeTaskRoles" Value="false" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey ID="31b5332d-8c51-4a4b-bc67-e86a6aecb411" Name="pk_FunctionRoles" IsClustered="true">
		<SchemeIndexedColumn Column="bd4fdcea-8042-488a-94c9-770b49357cfe" />
	</SchemePrimaryKey>
	<SchemeIndex ID="78b1e4b7-c323-4fda-951c-4d2c2e8a3970" Name="ndx_FunctionRoles_Name" IsUnique="true">
		<SchemeIndexedColumn Column="8e682682-9729-4231-8152-046c69337615" />
	</SchemeIndex>
	<SchemeRecord>
		<ID ID="bd4fdcea-8042-488a-94c9-770b49357cfe">6bc228a0-e5a2-4f15-bf6d-c8e744533241</ID>
		<Name ID="8e682682-9729-4231-8152-046c69337615">Author</Name>
		<Caption ID="f8b3afc6-cea7-4a98-b907-e716e0a426c6">$Enum_FunctionRoles_Author</Caption>
		<CanBeDeputy ID="196f68f6-d645-4aab-a016-33078a421fcd">true</CanBeDeputy>
		<CanTakeInProgress ID="232bc5f4-b628-4a3b-a6e5-053e2fca59f6">false</CanTakeInProgress>
		<HideTaskByDefault ID="b685759e-b32a-4cad-9eec-7d71f9af48d4">true</HideTaskByDefault>
		<CanChangeTaskInfo ID="430d64f0-0dcc-4bc7-8ebc-210a9593e01d">true</CanChangeTaskInfo>
		<CanChangeTaskRoles ID="477aaa9f-5792-436a-abd3-6817aabe5325">false</CanChangeTaskRoles>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="bd4fdcea-8042-488a-94c9-770b49357cfe">f726ab6c-a279-4d79-863a-47253e55ccc1</ID>
		<Name ID="8e682682-9729-4231-8152-046c69337615">Performer</Name>
		<Caption ID="f8b3afc6-cea7-4a98-b907-e716e0a426c6">$Enum_FunctionRoles_Performer</Caption>
		<CanBeDeputy ID="196f68f6-d645-4aab-a016-33078a421fcd">true</CanBeDeputy>
		<CanTakeInProgress ID="232bc5f4-b628-4a3b-a6e5-053e2fca59f6">true</CanTakeInProgress>
		<HideTaskByDefault ID="b685759e-b32a-4cad-9eec-7d71f9af48d4">false</HideTaskByDefault>
		<CanChangeTaskInfo ID="430d64f0-0dcc-4bc7-8ebc-210a9593e01d">false</CanChangeTaskInfo>
		<CanChangeTaskRoles ID="477aaa9f-5792-436a-abd3-6817aabe5325">false</CanChangeTaskRoles>
	</SchemeRecord>
	<SchemeRecord Partition="29f90c69-c1ef-4cbf-b9d5-7fc91cd68c67">
		<ID ID="bd4fdcea-8042-488a-94c9-770b49357cfe">d75c4fb4-50b9-4f9e-8651-eb6c9de8a847</ID>
		<Name ID="8e682682-9729-4231-8152-046c69337615">Sender</Name>
		<Caption ID="f8b3afc6-cea7-4a98-b907-e716e0a426c6">$Enum_FunctionRoles_Sender</Caption>
		<CanBeDeputy ID="196f68f6-d645-4aab-a016-33078a421fcd">true</CanBeDeputy>
		<CanTakeInProgress ID="232bc5f4-b628-4a3b-a6e5-053e2fca59f6">false</CanTakeInProgress>
		<HideTaskByDefault ID="b685759e-b32a-4cad-9eec-7d71f9af48d4">true</HideTaskByDefault>
		<CanChangeTaskInfo ID="430d64f0-0dcc-4bc7-8ebc-210a9593e01d">true</CanChangeTaskInfo>
		<CanChangeTaskRoles ID="477aaa9f-5792-436a-abd3-6817aabe5325">false</CanChangeTaskRoles>
	</SchemeRecord>
</SchemeTable>