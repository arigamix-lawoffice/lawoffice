<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="02a2a16e-7915-4a03-86b0-08b074b78c67" Name="WeTaskGroupControlAction" Group="WorkflowEngine" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<Description>Основная секция для действия управление группой заданий</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="02a2a16e-7915-0003-2000-08b074b78c67" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="02a2a16e-7915-0103-4000-08b074b78c67" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="c4e7fd72-2edc-419d-8c30-6c0b58608466" Name="ResumeGroup" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="4e9ca7ec-d3ab-4f02-b081-b1a5021c4a0c" Name="df_WeTaskGroupControlAction_ResumeGroup" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="c99a2e2b-5edb-4c00-9162-df511bd82932" Name="PauseGroup" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="a97a0fd1-6965-4421-b52c-af23e7a46efe" Name="df_WeTaskGroupControlAction_PauseGroup" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="c8faedc4-6e9b-46a3-97f8-2c94df42a7f0" Name="CancelGroup" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="5a321f34-3b80-4f6a-962a-3ba764bf8ef8" Name="df_WeTaskGroupControlAction_CancelGroup" Value="false" />
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="a5735835-fb7e-4037-a6ab-3d314a42d5f5" Name="CancelOption" Type="Reference(Typified) Null" ReferencedTable="08cf782d-4130-4377-8a49-3e201a05d496" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="a5735835-fb7e-0037-4000-0d314a42d5f5" Name="CancelOptionID" Type="Guid Null" ReferencedColumn="132dc5f5-ce87-4dd0-acce-b4a02acf7715" />
		<SchemeReferencingColumn ID="53d25fa5-c8d5-463c-b90a-3e78d386620a" Name="CancelOptionCaption" Type="String(128) Null" ReferencedColumn="6762309a-b0ff-4b2f-9cce-dd111116e554" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="a2af79d4-a85b-4bd2-b96e-993e4221a423" Name="UseAsNextRole" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="f658e21a-eff9-4bc5-bd53-88386286b098" Name="df_WeTaskGroupControlAction_UseAsNextRole" Value="false" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="02a2a16e-7915-0003-5000-08b074b78c67" Name="pk_WeTaskGroupControlAction" IsClustered="true">
		<SchemeIndexedColumn Column="02a2a16e-7915-0103-4000-08b074b78c67" />
	</SchemePrimaryKey>
</SchemeTable>