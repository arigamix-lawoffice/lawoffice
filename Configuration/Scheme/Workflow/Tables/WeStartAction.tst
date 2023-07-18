<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="fff6d6ad-c17e-4692-863d-07032f4b95fd" Name="WeStartAction" Group="WorkflowEngine" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<Description>Секция для действия Старта процесса</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="fff6d6ad-c17e-0092-2000-07032f4b95fd" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="fff6d6ad-c17e-0192-4000-07032f4b95fd" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="33769221-a9ae-4f1a-aeed-9209356b7323" Name="StartSignal" Type="Reference(Typified) Not Null" ReferencedTable="53dc8c0b-391a-4fbd-86c0-3da697abf065">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="33769221-a9ae-001a-4000-0209356b7323" Name="StartSignalID" Type="Guid Not Null" ReferencedColumn="cabbc72d-b093-43be-a645-8503664980d6">
			<SchemeDefaultConstraint IsPermanent="true" ID="b661b477-46f1-4f4d-babc-e7d3624240ea" Name="df_WeStartAction_StartSignalID" Value="893427ba-1d2d-4369-b7fa-c28e53997846" />
		</SchemeReferencingColumn>
		<SchemeReferencingColumn ID="973ce574-8422-4bf3-9d6b-1b3c27209477" Name="StartSignalName" Type="String(128) Not Null" ReferencedColumn="2e7c413d-0de6-4900-ac97-68ce16e3da11">
			<SchemeDefaultConstraint IsPermanent="true" ID="2e3dd43d-68db-4df5-af37-0444f61ebca1" Name="df_WeStartAction_StartSignalName" Value="Start" />
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="a2870611-6b15-42ab-8068-2f4252224cea" Name="IsNotPersistent" Type="Boolean Not Null">
		<Description>Определяет, что процесс должен быть запущен как неперсистентный.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="e434c8d3-4420-44f0-a9e1-539939814656" Name="df_WeStartAction_IsNotPersistent" Value="false" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="fff6d6ad-c17e-0092-5000-07032f4b95fd" Name="pk_WeStartAction" IsClustered="true">
		<SchemeIndexedColumn Column="fff6d6ad-c17e-0192-4000-07032f4b95fd" />
	</SchemePrimaryKey>
</SchemeTable>