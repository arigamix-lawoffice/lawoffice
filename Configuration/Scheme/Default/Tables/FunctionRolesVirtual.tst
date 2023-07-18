<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="ef4bbb91-4d48-4c68-9e05-34ab4d5c2b36" Name="FunctionRolesVirtual" Group="System" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<Description>Виртуальная карточка для функциональной роли.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="ef4bbb91-4d48-0068-2000-04ab4d5c2b36" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="ef4bbb91-4d48-0168-4000-04ab4d5c2b36" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="5357657e-2a03-4cda-9558-dd6cfb7bb99a" Name="FunctionRoleID" Type="Guid Not Null">
		<Description>Идентификатор функциональной роли.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="3667ef0b-5efb-4dfc-b9d2-bb7cc9d20c64" Name="Name" Type="String(128) Not Null">
		<Description>Имя функциональной роли.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="cdf0fc4e-f5bf-45a1-8cb0-acd12ea0aaf2" Name="Caption" Type="String(128) Not Null">
		<Description>Отображаемое пользователю имя функциональной роли.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="a69fb995-d1f2-4d68-852e-192b8b322882" Name="CanBeDeputy" Type="Boolean Not Null">
		<Description>Признак того, что при проверке вхождения в функциональную роль также проверяются заместители.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="00d08eb2-928f-4410-a684-2f6badf6399d" Name="df_FunctionRolesVirtual_CanBeDeputy" Value="true" />
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="7110af74-feb7-4c08-8899-170db4ce0568" Name="Partition" Type="Reference(Typified) Not Null" ReferencedTable="5ca00fac-d04e-4b82-8139-9778518e00bf">
		<Description>Библиотека схемы, в которую включается функциональная роль.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="7110af74-feb7-0008-4000-070db4ce0568" Name="PartitionID" Type="Guid Not Null" ReferencedColumn="fc636796-f583-4306-ad69-30fb2a070f9a" />
		<SchemeReferencingColumn ID="d9d6ebfc-37e8-47a2-b256-c61e430666e1" Name="PartitionName" Type="String(128) Not Null" ReferencedColumn="6af8d64d-cff0-4ece-9db3-b1f38e73814d" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="e05a7b13-3eb1-413b-8940-dd4b2f8e6ad5" Name="CanTakeInProgress" Type="Boolean Not Null">
		<Description>Разрешает брать задание в работу</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="86c6adf2-bb36-4795-a1e0-70c0d8aa7b64" Name="df_FunctionRolesVirtual_CanTakeInProgress" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="7152d990-55c5-40ff-90e0-43346e42ea58" Name="HideTaskByDefault" Type="Boolean Not Null">
		<Description>Скрывать задание по умолчанию</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="c1d0f164-6d22-49a0-8a30-fea39d73585a" Name="df_FunctionRolesVirtual_HideTaskByDefault" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="cd2def33-c381-456f-ac5b-801a4d18019b" Name="CanChangeTaskInfo" Type="Boolean Not Null">
		<Description>Разрешает изменять дайджест и плановую дату</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="3ac6c8ff-484b-46ed-add8-70a391098e05" Name="df_FunctionRolesVirtual_CanChangeTaskInfo" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="a2fedd9d-6cd3-45e2-b3e2-12f6d08285ec" Name="CanChangeTaskRoles" Type="Boolean Not Null">
		<Description>Разрешает изменять список ролей задания</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="a77ad432-87e0-46ed-8e53-e5686c9933b0" Name="df_FunctionRolesVirtual_CanChangeTaskRoles" Value="false" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="ef4bbb91-4d48-0068-5000-04ab4d5c2b36" Name="pk_FunctionRolesVirtual" IsClustered="true">
		<SchemeIndexedColumn Column="ef4bbb91-4d48-0168-4000-04ab4d5c2b36" />
	</SchemePrimaryKey>
</SchemeTable>