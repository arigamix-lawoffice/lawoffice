<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="caac66aa-0cbb-4e2b-83fd-7c368e814d64" Name="KrSecondaryProcesses" Group="Kr" InstanceType="Cards" ContentType="Entries">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="caac66aa-0cbb-002b-2000-0c368e814d64" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="caac66aa-0cbb-012b-4000-0c368e814d64" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="444b8925-572a-449b-901e-8660ddeb3b6c" Name="Name" Type="String(255) Not Null" />
	<SchemePhysicalColumn ID="907a2c94-13d2-4aaf-a406-8d9714decce5" Name="Description" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="cc2a6151-4aa7-4807-b456-5c7739e70244" Name="TileGroup" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="dd2c063a-8e33-4910-92e8-114217a497a6" Name="IsGlobal" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="e3c95797-fc01-4fad-8499-809023816789" Name="df_KrSecondaryProcesses_IsGlobal" Value="true" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="9dc03d20-25d1-4324-8238-133394bd76e8" Name="Async" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="defb5e76-cb84-47e9-8376-74be41937346" Name="df_KrSecondaryProcesses_Async" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="45af3f83-bf09-4c1b-bc04-9943c54d689d" Name="RefreshAndNotify" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="25b9edb7-56d2-4770-a0cb-cc6b2c44b873" Name="df_KrSecondaryProcesses_RefreshAndNotify" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="08ade495-696b-4b96-8cb0-aaa6b39e364c" Name="Caption" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="1fc4f088-b600-43f2-90c4-89e503def818" Name="Tooltip" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="3c68f974-2e54-46db-abd4-c61b819f4c11" Name="Icon" Type="String(Max) Null" />
	<SchemeComplexColumn ID="22593e34-ef06-41ca-87e5-60c9203ef04d" Name="TileSize" Type="Reference(Typified) Not Null" ReferencedTable="9d1fb4ee-fa51-4926-8abb-c464ca91e450">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="22593e34-ef06-00ca-4000-00c9203ef04d" Name="TileSizeID" Type="Int16 Not Null" ReferencedColumn="47a9e2f0-0216-4b02-b8d4-e65b166d689f">
			<SchemeDefaultConstraint IsPermanent="true" ID="0de5a8a4-845e-48b5-af2b-f2de83d12a0c" Name="df_KrSecondaryProcesses_TileSizeID" Value="0" />
		</SchemeReferencingColumn>
		<SchemeReferencingColumn ID="f405c201-da12-45f7-952e-06cf352f352c" Name="TileSizeName" Type="String(128) Not Null" ReferencedColumn="7594adc2-eeb7-4d62-a6bd-2ae7040013fb">
			<SchemeDefaultConstraint IsPermanent="true" ID="034d12b8-d832-4d8a-9885-0f65e088ecde" Name="df_KrSecondaryProcesses_TileSizeName" Value="$Enum_TileSize_Full" />
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="c50ea095-4acc-425a-88b5-367cf03a5318" Name="AskConfirmation" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="3f195b55-8cee-4489-8db3-cfafd9b5b58f" Name="df_KrSecondaryProcesses_AskConfirmation" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="277e1728-d576-47c0-b0ee-a57dc853ba64" Name="ConfirmationMessage" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="c54a38a3-e652-45ad-86f2-3a29158f713e" Name="ActionGrouping" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="bf8d27dd-b495-4ac5-95eb-dd3c2c3f6c53" Name="df_KrSecondaryProcesses_ActionGrouping" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="bfa31176-ffa1-4608-b0d8-6ad3a6ce8fca" Name="VisibilitySqlCondition" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="4cf1e57f-f681-47ab-b0d1-aa3ab506eaaf" Name="ExecutionSqlCondition" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="8c401245-65a9-42af-81dd-44bc8398d5a5" Name="VisibilitySourceCondition" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="932bb41c-f6fd-4f8d-a494-6a1304a2dbb9" Name="ExecutionSourceCondition" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="41c04021-1ec0-41ea-bb18-fca4eebd2a58" Name="ExecutionAccessDeniedMessage" Type="String(Max) Null" />
	<SchemeComplexColumn ID="5403fecd-5db4-4a85-8e43-07c7837c6e4f" Name="Mode" Type="Reference(Typified) Not Null" ReferencedTable="a8a8e7df-0237-4fda-824f-030df82a1030">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="5403fecd-5db4-0085-4000-07c7837c6e4f" Name="ModeID" Type="Int32 Not Null" ReferencedColumn="d415b7f3-4c09-48b6-b4ef-d455a9340483">
			<SchemeDefaultConstraint IsPermanent="true" ID="ceb58ff3-dc1d-4f6b-995f-aa5f59a001c0" Name="df_KrSecondaryProcesses_ModeID" Value="1" />
		</SchemeReferencingColumn>
		<SchemeReferencingColumn ID="6e454cd4-657b-431f-8ed0-36d1c3bd9907" Name="ModeName" Type="String(128) Not Null" ReferencedColumn="c1c9be67-b394-4aa1-bce9-0bfaa59b6c56">
			<SchemeDefaultConstraint IsPermanent="true" ID="0cf14ccf-544d-4be2-9f75-c3934a7f5b84" Name="df_KrSecondaryProcesses_ModeName" Value="$KrSecondaryProcess_Mode_Button" />
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="90ddeed2-c653-4c5c-8847-c9f8be88ae48" Name="Action" Type="Reference(Typified) Null" ReferencedTable="b401e639-9167-4ada-9d46-4982bcd92488">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="90ddeed2-c653-005c-4000-09f8be88ae48" Name="ActionID" Type="Int32 Null" ReferencedColumn="a17e5f4b-8838-4ac9-8b7b-2aaa66e102d0" />
		<SchemeReferencingColumn ID="5e5cd85e-a3ce-41a7-bf45-bc7dedb2233d" Name="ActionName" Type="String(256) Null" ReferencedColumn="c83a64ff-9ffd-49fc-8851-d65fcb138dc7" />
		<SchemeReferencingColumn ID="076cbdf4-e21a-4673-9ef7-4b44f45f8777" Name="ActionEventType" Type="String(256) Null" ReferencedColumn="71d48834-f91e-48a0-9a00-4d1a891e04cf" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="13389cd5-b1e1-470b-98e3-165e49b03239" Name="AllowClientSideLaunch" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="7fc29efa-637d-414d-9792-5026635afa56" Name="df_KrSecondaryProcesses_AllowClientSideLaunch" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="4bd85d15-147d-4316-9c48-5f8b28f482d5" Name="CheckRecalcRestrictions" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="421cc136-a4ec-44b8-a59b-514fa5ffce67" Name="df_KrSecondaryProcesses_CheckRecalcRestrictions" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="3c00c7df-7d51-47b0-91a0-fae34a87606e" Name="RunOnce" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="382c810e-662a-4144-b4d2-19cc2b8af82b" Name="df_KrSecondaryProcesses_RunOnce" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="4049d391-95df-4c79-a040-ebff04236da1" Name="ButtonHotkey" Type="String(256) Null" />
	<SchemePhysicalColumn ID="37b58131-4172-489f-86a9-18c0eeb7f877" Name="Conditions" Type="BinaryJson Null">
		<Description>Сериализованные данные с условиями</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="bcb8db5f-ec4b-4109-8e1a-0c21250b148c" Name="Order" Type="Int32 Not Null">
		<Description>Порядковый номер тайла</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="89738f43-a437-4d18-b839-0298cf8d2b2d" Name="df_KrSecondaryProcesses_Order" Value="0" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="d74acf37-1881-4b87-b09f-3d393db04210" Name="NotMessageHasNoActiveStages" Type="Boolean Not Null">
		<Description>Не отображать сообщение при отсутствии этапов доступных для выполнения</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="dabcedcb-52af-4427-927c-fd12db0f8124" Name="df_KrSecondaryProcesses_NotMessageHasNoActiveStages" Value="false" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="caac66aa-0cbb-002b-5000-0c368e814d64" Name="pk_KrSecondaryProcesses" IsClustered="true">
		<SchemeIndexedColumn Column="caac66aa-0cbb-012b-4000-0c368e814d64" />
	</SchemePrimaryKey>
	<SchemeIndex ID="2a7b3f42-47db-41fd-bd38-668f7bf20e5d" Name="ndx_KrSecondaryProcesses_Name" IsUnique="true">
		<SchemeIndexedColumn Column="444b8925-572a-449b-901e-8660ddeb3b6c" />
	</SchemeIndex>
</SchemeTable>