<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="cd241343-4eb1-425f-b534-f9ff4cfa597e" Name="WfSatelliteTaskHistory" Group="Wf" InstanceType="Cards" ContentType="Collections">
	<Description>Дополнительная информация по истории заданий в карточке-сателлите Workflow.
ID - идентификатор карточки-сателлита WfSatellite.
RowID - идентификатор задания (он же идентификатор записи в истории заданий TaskHistory после того, как задание завершено).</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="cd241343-4eb1-005f-2000-09ff4cfa597e" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="cd241343-4eb1-015f-4000-09ff4cfa597e" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="cd241343-4eb1-005f-3100-09ff4cfa597e" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="a3caf925-b4a8-4618-803d-77e13f19ec35" Name="Controller" Type="Reference(Typified) Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b" WithForeignKey="false">
		<Description>Роль, на которую осуществляется возврат на контроль для задания с идентификатором RowID, или Null, если возврат на контроль не осуществляется.
В поле записывается либо персональная роль автора задания, либо роль, указанная в поле "Контролёр", если задание было отправлено исполнителю с контролем. Если оно отправлено без контроля, то в поле всегда указано Null.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="a3caf925-b4a8-0018-4000-07e13f19ec35" Name="ControllerID" Type="Guid Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="92ee0e2e-65f6-4dd9-b67b-9880c31e0aa1" Name="ControllerName" Type="String(128) Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0">
			<Description>Отображаемое имя роли.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="d63d9e11-4d57-4213-9496-502c88de5b47" Name="Controlled" Type="Boolean Null">
		<Description>Признак того, что задание на контроль уже было выслано на роль Controller, или Null, если роль Controller равна Null и контроль не требуется.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="7a41e0a1-d3a1-4a8e-96fa-d3dd64df295d" Name="AliveSubtasks" Type="Int32 Null">
		<Description>Количество подзадач, отправленных без объединения исполнителей и ещё не завершённых с учётом подзадач, которые были созданы до того, как выполнена отправка. Указывается значение Null, если отправки без объединения исполнителей не было выполнено.</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="cd241343-4eb1-005f-5000-09ff4cfa597e" Name="pk_WfSatelliteTaskHistory">
		<SchemeIndexedColumn Column="cd241343-4eb1-005f-3100-09ff4cfa597e" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="cd241343-4eb1-005f-7000-09ff4cfa597e" Name="idx_WfSatelliteTaskHistory_ID" IsClustered="true">
		<SchemeIndexedColumn Column="cd241343-4eb1-015f-4000-09ff4cfa597e" />
	</SchemeIndex>
	<SchemeIndex ID="5b61c2cc-6457-4049-812b-bebc066bc766" Name="ndx_WfSatelliteTaskHistory_RowID">
		<SchemeIndexedColumn Column="cd241343-4eb1-005f-3100-09ff4cfa597e" />
		<SchemeIncludedColumn Column="d63d9e11-4d57-4213-9496-502c88de5b47" />
		<SchemeIncludedColumn Column="a3caf925-b4a8-0018-4000-07e13f19ec35" />
		<SchemeIncludedColumn Column="92ee0e2e-65f6-4dd9-b67b-9880c31e0aa1" />
	</SchemeIndex>
</SchemeTable>