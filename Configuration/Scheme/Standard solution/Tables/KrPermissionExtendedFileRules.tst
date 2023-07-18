<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="7ca15c10-9fd1-46e9-8769-b0acc0efe118" Name="KrPermissionExtendedFileRules" Group="Kr" InstanceType="Cards" ContentType="Collections">
	<Description>Секция с расширенными настройками доступа к файлам</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="7ca15c10-9fd1-00e9-2000-00acc0efe118" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="7ca15c10-9fd1-01e9-4000-00acc0efe118" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="7ca15c10-9fd1-00e9-3100-00acc0efe118" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="3dc7aaf0-eea0-4304-a229-ca72a011f637" Name="Extensions" Type="String(Max) Null" />
	<SchemeComplexColumn ID="d869fbf1-4153-496d-964e-2eeb701af579" Name="ReadAccessSetting" Type="Reference(Typified) Null" ReferencedTable="95a74318-2e98-46bd-bced-1890dd1cd017">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="d869fbf1-4153-006d-4000-0eeb701af579" Name="ReadAccessSettingID" Type="Int32 Null" ReferencedColumn="7e9d555d-de8f-4b5c-9ad2-bc80ed062c12" />
		<SchemeReferencingColumn ID="72702106-5db1-4c24-aa8d-9491a0587c5f" Name="ReadAccessSettingName" Type="String(128) Null" ReferencedColumn="433c1052-0a35-47e3-b24a-44d08200cffb" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="18a0235d-dc17-4b79-8f3a-2640761e2762" Name="Order" Type="Int32 Not Null" />
	<SchemeComplexColumn ID="4fdd43e3-741c-435c-b1d1-304e11d4224b" Name="EditAccessSetting" Type="Reference(Typified) Null" ReferencedTable="9247ed2e-109d-4543-b888-1fe9da9479aa">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="4fdd43e3-741c-005c-4000-004e11d4224b" Name="EditAccessSettingID" Type="Int32 Null" ReferencedColumn="0fdf8f67-3ebf-4d07-a559-5a02f370f0eb" />
		<SchemeReferencingColumn ID="7d719d55-a477-4b0b-9f94-2f87a05b5f88" Name="EditAccessSettingName" Type="String(128) Null" ReferencedColumn="867b9598-88c4-4fb5-b6f7-c9672320f9bd" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="de5abf36-97e4-495a-a1bf-6c90f0b94c6a" Name="DeleteAccessSetting" Type="Reference(Typified) Null" ReferencedTable="9247ed2e-109d-4543-b888-1fe9da9479aa">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="de5abf36-97e4-005a-4000-0c90f0b94c6a" Name="DeleteAccessSettingID" Type="Int32 Null" ReferencedColumn="0fdf8f67-3ebf-4d07-a559-5a02f370f0eb" />
		<SchemeReferencingColumn ID="043a73f0-d8ee-474f-bff4-533333a42eec" Name="DeleteAccessSettingName" Type="String(128) Null" ReferencedColumn="867b9598-88c4-4fb5-b6f7-c9672320f9bd" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="a0acf9a2-74b7-4e2c-ae02-8c78aa9580e0" Name="SignAccessSetting" Type="Reference(Typified) Null" ReferencedTable="9247ed2e-109d-4543-b888-1fe9da9479aa">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="a0acf9a2-74b7-002c-4000-0c78aa9580e0" Name="SignAccessSettingID" Type="Int32 Null" ReferencedColumn="0fdf8f67-3ebf-4d07-a559-5a02f370f0eb" />
		<SchemeReferencingColumn ID="9ad42bf4-e46b-48a6-a4a7-709ab68d04e3" Name="SignAccessSettingName" Type="String(128) Null" ReferencedColumn="867b9598-88c4-4fb5-b6f7-c9672320f9bd" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="ed1b7375-e70a-4d6e-8b20-015c684cc7c4" Name="FileCheckRule" Type="Reference(Typified) Not Null" ReferencedTable="2baf9cf1-a8d5-4e82-bccd-769d7c70e10a">
		<Description>Правило выбора файла</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="ed1b7375-e70a-006e-4000-015c684cc7c4" Name="FileCheckRuleID" Type="Int32 Not Null" ReferencedColumn="ed8d4653-0c62-477b-bc81-ada61ddbc5c5">
			<SchemeDefaultConstraint IsPermanent="true" ID="62a986fd-7a16-45e1-addb-91a6667a78d6" Name="df_KrPermissionExtendedFileRules_FileCheckRuleID" Value="0" />
		</SchemeReferencingColumn>
		<SchemeReferencingColumn ID="b2aa9c58-0e98-4fa9-9859-d20b99256290" Name="FileCheckRuleName" Type="String(128) Not Null" ReferencedColumn="9fa664e7-5718-4a33-966e-593915e5aac2">
			<SchemeDefaultConstraint IsPermanent="true" ID="9801acc4-499b-42d6-9432-39c018cfdc38" Name="df_KrPermissionExtendedFileRules_FileCheckRuleName" Value="$KrPermissions_FileCheckRules_AllFiles" />
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="bc04dddb-79d9-4c90-8679-af787c9af472" Name="AddAccessSetting" Type="Reference(Typified) Null" ReferencedTable="9247ed2e-109d-4543-b888-1fe9da9479aa">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="bc04dddb-79d9-0090-4000-0f787c9af472" Name="AddAccessSettingID" Type="Int32 Null" ReferencedColumn="0fdf8f67-3ebf-4d07-a559-5a02f370f0eb" />
		<SchemeReferencingColumn ID="cadd013f-8078-4afe-bd30-3967d33f7c90" Name="AddAccessSettingName" Type="String(128) Null" ReferencedColumn="867b9598-88c4-4fb5-b6f7-c9672320f9bd" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="9a36833a-e3a2-41a9-a127-19086000b271" Name="FileSizeLimit" Type="Int32 Null">
		<Description>Ограничение на размер добавляемого файла при включенной настройке доступа на добавление файла.</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="7ca15c10-9fd1-00e9-5000-00acc0efe118" Name="pk_KrPermissionExtendedFileRules">
		<SchemeIndexedColumn Column="7ca15c10-9fd1-00e9-3100-00acc0efe118" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="7ca15c10-9fd1-00e9-7000-00acc0efe118" Name="idx_KrPermissionExtendedFileRules_ID" IsClustered="true">
		<SchemeIndexedColumn Column="7ca15c10-9fd1-01e9-4000-00acc0efe118" />
	</SchemeIndex>
</SchemeTable>