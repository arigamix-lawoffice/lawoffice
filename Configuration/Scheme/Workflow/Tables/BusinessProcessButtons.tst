<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="59bf0d0b-f7fc-41d3-92da-56c673f1e0b3" Name="BusinessProcessButtons" Group="WorkflowEngine" InstanceType="Cards" ContentType="Collections">
	<Description>Секция с описанием кнопок бизнес-процесса (как запускающих сам процесс, так и отправляющих команду для процесса).</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="59bf0d0b-f7fc-00d3-2000-06c673f1e0b3" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="59bf0d0b-f7fc-01d3-4000-06c673f1e0b3" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="59bf0d0b-f7fc-00d3-3100-06c673f1e0b3" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="2f850678-5490-4533-9987-8c5d4e34e3b6" Name="Caption" Type="String(256) Not Null" />
	<SchemePhysicalColumn ID="4df108af-e65c-4ad8-ac7d-fadd35af5c55" Name="StartProcess" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="2a9f1525-a2c2-4b67-b94a-c95858d4954e" Name="df_BusinessProcessButtons_StartProcess" Value="true" />
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="fb8dd2f0-77d4-40bb-b1b9-bc899375c8d0" Name="Signal" Type="Reference(Typified) Null" ReferencedTable="53dc8c0b-391a-4fbd-86c0-3da697abf065" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="fb8dd2f0-77d4-00bb-4000-0c899375c8d0" Name="SignalID" Type="Guid Null" ReferencedColumn="cabbc72d-b093-43be-a645-8503664980d6" />
		<SchemeReferencingColumn ID="edcef3a1-3770-4dbe-b71f-900c838e210e" Name="SignalName" Type="String(128) Null" ReferencedColumn="2e7c413d-0de6-4900-ac97-68ce16e3da11" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="7ac4ebfa-44aa-49b8-bfe9-7e74209c3a07" Name="Group" Type="String(256) Null">
		<Description>Определяет группу, к которой относится тайл</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="eca7abad-ff0f-438e-8192-536b392ccede" Name="Icon" Type="String(128) Null">
		<Description>Имя ресурса иконки тайла</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="6d347d18-0fc0-44b3-a754-e13d1f6cdb73" Name="Description" Type="String(Max) Null">
		<Description>Описание тайла, которое будет показано при наведении на него</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="013b6942-82e9-4b8f-b935-e400ec990cc5" Name="Condition" Type="String(Max) Null">
		<Description>Скрипт условия для проверки доступа к тайлу</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="d887964f-d10b-4470-bc6a-638b7234c595" Name="TileSize" Type="Reference(Typified) Not Null" ReferencedTable="9d1fb4ee-fa51-4926-8abb-c464ca91e450">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="d887964f-d10b-0070-4000-038b7234c595" Name="TileSizeID" Type="Int16 Not Null" ReferencedColumn="47a9e2f0-0216-4b02-b8d4-e65b166d689f">
			<SchemeDefaultConstraint IsPermanent="true" ID="61d12c21-2824-45a7-b030-e0fd5fb851e7" Name="df_BusinessProcessButtons_TileSizeID" Value="0" />
		</SchemeReferencingColumn>
		<SchemeReferencingColumn ID="1bfb66be-135d-4861-92eb-fa6090462fe3" Name="TileSizeName" Type="String(128) Not Null" ReferencedColumn="7594adc2-eeb7-4d62-a6bd-2ae7040013fb">
			<SchemeDefaultConstraint IsPermanent="true" ID="98747a67-449f-43d1-82e8-fdb4891e6072" Name="df_BusinessProcessButtons_TileSizeName" Value="$Enum_TileSize_Full" />
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="bbc48b66-1ba6-4d81-a1dc-1a8fc0033134" Name="Tooltip" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="0259f0fb-7336-4ec2-86e6-2a3aa9682bad" Name="AskConfirmation" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="edaaee89-a398-49b6-80fa-b3cfbb22c4ee" Name="df_BusinessProcessButtons_AskConfirmation" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="b902066a-098d-49ef-afe7-cb3b5b151dc5" Name="ConfirmationMessage" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="0bb7c819-2a5a-4d0a-867f-4645a0752b00" Name="ActionGrouping" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="01b7b1f4-6079-4cba-b6a8-fc8175e836b4" Name="df_BusinessProcessButtons_ActionGrouping" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="cfed5494-905a-4008-a9ba-2fb8052334e7" Name="DisplaySettings" Type="String(Max) Null">
		<Description>Отображаемые настройки кнопки. Заполняются расширениями</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="81ba50e3-ccaa-4f86-983e-a49646545bfa" Name="ButtonHotkey" Type="String(256) Null" />
	<SchemePhysicalColumn ID="3149026c-2adb-4d8a-acf0-aa6785ba41ed" Name="AccessDeniedMessage" Type="String(Max) Null">
		<Description>Сообщение об отсутствии доступа при нажатии кнопки.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="2c563fee-1122-4d2c-8eef-d41190a46c4d" Name="Order" Type="Int32 Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="04a0b7a8-6ddd-4cd1-b5ee-ee464487ef69" Name="df_BusinessProcessButtons_Order" Value="0" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="59bf0d0b-f7fc-00d3-5000-06c673f1e0b3" Name="pk_BusinessProcessButtons">
		<SchemeIndexedColumn Column="59bf0d0b-f7fc-00d3-3100-06c673f1e0b3" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="59bf0d0b-f7fc-00d3-7000-06c673f1e0b3" Name="idx_BusinessProcessButtons_ID" IsClustered="true">
		<SchemeIndexedColumn Column="59bf0d0b-f7fc-01d3-4000-06c673f1e0b3" />
	</SchemeIndex>
</SchemeTable>