<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="900bdbcd-1e87-451c-8b4b-082d8f7efd48" Name="RoleDeputies" Group="Roles" InstanceType="Cards" ContentType="Collections">
	<Description>Заместители.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="900bdbcd-1e87-001c-2000-082d8f7efd48" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="900bdbcd-1e87-011c-4000-082d8f7efd48" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="900bdbcd-1e87-001c-3100-082d8f7efd48" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="6a8e6bab-e51f-4c17-884b-666fb22ead61" Name="Type" Type="Reference(Typified) Not Null" ReferencedTable="8d6cb6a6-c3f5-4c92-88d7-0cc6b8e8d09d">
		<Description>Тип роли.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="6a8e6bab-e51f-0017-4000-066fb22ead61" Name="TypeID" Type="Int16 Not Null" ReferencedColumn="c9e1fce6-f27f-4fce-83a0-fadbff72f848">
			<Description>Идентификатор типа роли.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="bfd2c3de-3d3c-4b69-ad05-c77a474ea876" Name="Deputy" Type="Reference(Typified) Not Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<Description>Персональная роль заместителя.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="bfd2c3de-3d3c-0069-4000-077a474ea876" Name="DeputyID" Type="Guid Not Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="55b5f822-946b-45b9-ac68-e1b8b05f5c0f" Name="DeputyName" Type="String(128) Not Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964">
			<Description>Отображаемое имя пользователя.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="9b6562a1-d679-41c0-8204-485b2968500b" Name="Deputized" Type="Reference(Typified) Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<Description>Персональная роль пользователя, которого замещает пользователь Deputy в этой роли.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="9b6562a1-d679-00c0-4000-085b2968500b" Name="DeputizedID" Type="Guid Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="4b8841aa-44d0-4a7f-b341-cc6ee0918d92" Name="DeputizedName" Type="String(128) Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964">
			<Description>Отображаемое имя пользователя.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="a0cbd36b-f4e4-4ccd-9412-0112290e835d" Name="MinDate" Type="Date Not Null">
		<Description>Начальная дата временного замещения или минимальное значение, если замещение постоянное.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="32a7cbe2-5745-413c-b58f-cc3c8f8c8f7c" Name="MaxDate" Type="Date Not Null">
		<Description>Конечная дата временного замещения или максимальное значение, если замещение постоянное.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="f1409939-4142-4dee-a35a-93b91b483407" Name="IsActive" Type="Boolean Not Null">
		<Description>Признак того, что замещение активно.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="cdba0fc1-a1b6-409c-92b5-b217cdd7f7f7" Name="df_RoleDeputies_IsActive" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="2c50d350-e091-43fb-98db-71a485cda01d" Name="IsEnabled" Type="Boolean Not Null">
		<Description>Признак того, что замещение доступно, т.е. может стать активным.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="267cecad-0310-4c05-80c5-07bdbb13ebe1" Name="df_RoleDeputies_IsEnabled" Value="true" />
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="93485556-351e-4d06-879a-0cc3c01ee6cc" Name="Management" Type="Reference(Typified) Null" ReferencedTable="91acf9b9-8476-4dc8-a239-ac6b8f250077" WithForeignKey="false">
		<Description>Ссылка на RowID в секции ролей в диалоге "Мои замещения"</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="93485556-351e-0006-4000-0cc3c01ee6cc" Name="ManagementRowID" Type="Guid Null" ReferencedColumn="91acf9b9-8476-00c8-3100-0c6b8f250077" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="f570743f-7e8f-4bdc-b9c5-b1a13e8b6c93" Name="Parent" Type="Reference(Typified) Null" ReferencedTable="900bdbcd-1e87-451c-8b4b-082d8f7efd48" WithForeignKey="false">
		<Description>Ссылка на строку замещения, заместителем которой является текущая строка. Null для заместителей первого уровня.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="f570743f-7e8f-00dc-4000-01a13e8b6c93" Name="ParentRowID" Type="Guid Null" ReferencedColumn="900bdbcd-1e87-001c-3100-082d8f7efd48" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="39d1962e-90d4-46fd-9ac7-45b375a78119" Name="Level" Type="Int32 Not Null">
		<Description>Уровень вложенности замещения. 0 - если сотрудник является заместителем сотрудника непосредственно роли, 1 - если является заместителем сотрудника-заместителя уровня 0, и т.д.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="0fb0a535-d1fd-4948-b899-b8138061dd1d" Name="df_RoleDeputies_Level" Value="0" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="900bdbcd-1e87-001c-5000-082d8f7efd48" Name="pk_RoleDeputies">
		<SchemeIndexedColumn Column="900bdbcd-1e87-001c-3100-082d8f7efd48" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="900bdbcd-1e87-001c-7000-082d8f7efd48" Name="idx_RoleDeputies_ID" IsClustered="true">
		<SchemeIndexedColumn Column="900bdbcd-1e87-011c-4000-082d8f7efd48" />
	</SchemeIndex>
	<SchemeIndex ID="3ec1ae68-4e30-4fa6-a1d3-641128802c19" Name="ndx_RoleDeputies_IDDeputyIDDeputizedIDIsActive">
		<SchemeIndexedColumn Column="900bdbcd-1e87-011c-4000-082d8f7efd48" />
		<SchemeIndexedColumn Column="bfd2c3de-3d3c-0069-4000-077a474ea876" />
		<SchemeIndexedColumn Column="9b6562a1-d679-00c0-4000-085b2968500b" />
		<SchemeIndexedColumn Column="f1409939-4142-4dee-a35a-93b91b483407" />
	</SchemeIndex>
	<SchemeIndex ID="76efbe40-eac2-4c4a-91a5-8afd86b4aeb5" Name="ndx_RoleDeputies_ManagementRowIDParentRowIDRowIDLevel">
		<SchemeIndexedColumn Column="93485556-351e-0006-4000-0cc3c01ee6cc" />
		<SchemeIndexedColumn Column="f570743f-7e8f-00dc-4000-01a13e8b6c93" />
		<SchemeIndexedColumn Column="900bdbcd-1e87-001c-3100-082d8f7efd48" />
		<SchemeIndexedColumn Column="39d1962e-90d4-46fd-9ac7-45b375a78119" />
	</SchemeIndex>
	<SchemeIndex ID="47436fe0-b36b-4146-8421-6fe0ef00d24e" Name="ndx_RoleDeputies_IDDeputyIDManagementRowIDLevel">
		<Description>Индекс для перерасчёта заместителей роли</Description>
		<SchemeIndexedColumn Column="900bdbcd-1e87-011c-4000-082d8f7efd48" />
		<SchemeIndexedColumn Column="bfd2c3de-3d3c-0069-4000-077a474ea876" />
		<SchemeIndexedColumn Column="93485556-351e-0006-4000-0cc3c01ee6cc" />
		<SchemeIndexedColumn Column="39d1962e-90d4-46fd-9ac7-45b375a78119" />
	</SchemeIndex>
	<SchemeIndex ID="fe5792cc-bb41-498d-8df0-2e1a1548400e" Name="ndx_RoleDeputies_LevelDeputyIDRowIDIDTypeID">
		<Description>Индекс для быстрой вставки новых элементов по изменениям настроек замещения у сотрудника.</Description>
		<SchemeIndexedColumn Column="39d1962e-90d4-46fd-9ac7-45b375a78119" />
		<SchemeIndexedColumn Column="bfd2c3de-3d3c-0069-4000-077a474ea876" />
		<SchemeIndexedColumn Column="900bdbcd-1e87-001c-3100-082d8f7efd48" />
		<SchemeIndexedColumn Column="900bdbcd-1e87-011c-4000-082d8f7efd48" />
		<SchemeIndexedColumn Column="6a8e6bab-e51f-0017-4000-066fb22ead61" />
	</SchemeIndex>
	<SchemeIndex ID="ae6a8eac-5a00-4077-88a8-7b5864081849" Name="ndx_RoleDeputies_RowIDLevel">
		<SchemeIndexedColumn Column="900bdbcd-1e87-001c-3100-082d8f7efd48" />
		<SchemeIndexedColumn Column="39d1962e-90d4-46fd-9ac7-45b375a78119" />
		<SchemeIncludedColumn Column="900bdbcd-1e87-011c-4000-082d8f7efd48" />
		<SchemeIncludedColumn Column="f1409939-4142-4dee-a35a-93b91b483407" />
	</SchemeIndex>
	<SchemeIndex ID="9497845a-4192-46d6-84e9-7fe590b2837d" Name="ndx_RoleDeputies_DeputyID">
		<SchemeIndexedColumn Column="bfd2c3de-3d3c-0069-4000-077a474ea876" />
	</SchemeIndex>
	<SchemeIndex ID="119bb5f4-0827-47f4-b19c-9635ccc4ea31" Name="ndx_RoleDeputies_DeputizedID">
		<SchemeIndexedColumn Column="9b6562a1-d679-00c0-4000-085b2968500b" />
	</SchemeIndex>
</SchemeTable>