<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="8874a392-0fd9-47dd-a6b5-bc3c02ede681" Name="AcquaintanceRows" Group="Common">
	<Description>Строки по данным для отправки на массовое ознакомление. По одной строке для каждого сотрудника, которому была отправлена карточка на ознакомление.</Description>
	<SchemePhysicalColumn ID="afbbc9fd-21de-4ecd-8fd3-6ebb876f14df" Name="ID" Type="Guid Not Null">
		<Description>Идентификатор записи</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="e0ebf86c-a58a-4d14-81bc-b7e078c1afe6" Name="CardID" Type="Guid Not Null">
		<Description>Идентификатор карточки</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="4185e966-6059-45fc-9f0a-2a641c67884a" Name="Sender" Type="Reference(Typified) Not Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="4185e966-6059-00fc-4000-0a641c67884a" Name="SenderID" Type="Guid Not Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="a6935066-2f70-4e34-a868-950456c74578" Name="SenderName" Type="String(128) Not Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964">
			<Description>Отображаемое имя пользователя.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="ac204cb7-9274-4f22-9dc5-70391ad8f702" Name="User" Type="Reference(Typified) Not Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="ac204cb7-9274-0022-4000-00391ad8f702" Name="UserID" Type="Guid Not Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="42462b71-c5d5-49a3-86f5-48b8abf98122" Name="UserName" Type="String(128) Not Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964">
			<Description>Отображаемое имя пользователя.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="20c6ad34-e0fd-4632-b882-8eb347236001" Name="IsReceived" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="25a93c08-5885-4007-8790-4d2d183e4c12" Name="df_AcquaintanceRows_IsReceived" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="d5ceb20c-dfaa-4398-b5cf-77914826865f" Name="Sent" Type="DateTime Not Null">
		<Description>Дата отправки на ознакомление</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="6e29ef97-a268-4b52-89b3-888d060f8276" Name="Received" Type="DateTime Null">
		<Description>Дата ознакомления</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="b48adebd-4273-497e-b2af-a301857cb559" Name="Comment" Type="Reference(Typified) Null" ReferencedTable="ae4e68f0-ff8e-4055-9386-f601f1f3c664" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="b48adebd-4273-007e-4000-0301857cb559" Name="CommentID" Type="Guid Null" ReferencedColumn="ab53d587-0dc1-4695-822a-ffc50cc472a2" />
	</SchemeComplexColumn>
	<SchemePrimaryKey ID="975e82ec-46ca-42e1-86cf-a89ef61db7f8" Name="pk_AcquaintanceRows" IsClustered="true">
		<SchemeIndexedColumn Column="afbbc9fd-21de-4ecd-8fd3-6ebb876f14df" />
	</SchemePrimaryKey>
	<SchemeIndex ID="b32ea939-0b5d-4fe2-bd26-bede1bb98683" Name="ndx_AcquaintanceRows_CardIDInformedDate">
		<SchemeIndexedColumn Column="e0ebf86c-a58a-4d14-81bc-b7e078c1afe6" />
		<SchemeIndexedColumn Column="6e29ef97-a268-4b52-89b3-888d060f8276" />
		<SchemeIncludedColumn Column="b48adebd-4273-007e-4000-0301857cb559" />
	</SchemeIndex>
	<SchemeIndex ID="9348d8b2-f652-47e3-bf73-1c6734d8bf43" Name="ndx_AcquaintanceRows_SenderID">
		<SchemeIndexedColumn Column="4185e966-6059-00fc-4000-0a641c67884a" />
	</SchemeIndex>
	<SchemeIndex ID="1940d0c0-ece9-490e-a8bb-905d2d351c6f" Name="ndx_AcquaintanceRows_CardIDSenderName">
		<SchemeIndexedColumn Column="e0ebf86c-a58a-4d14-81bc-b7e078c1afe6" />
		<SchemeIndexedColumn Column="a6935066-2f70-4e34-a868-950456c74578" />
		<SchemeIncludedColumn Column="b48adebd-4273-007e-4000-0301857cb559" />
	</SchemeIndex>
	<SchemeIndex ID="1f13feb5-6fe8-4572-9475-14fba5351089" Name="ndx_AcquaintanceRows_CardIDIsInformed">
		<SchemeIndexedColumn Column="e0ebf86c-a58a-4d14-81bc-b7e078c1afe6" />
		<SchemeIndexedColumn Column="20c6ad34-e0fd-4632-b882-8eb347236001" />
		<SchemeIncludedColumn Column="b48adebd-4273-007e-4000-0301857cb559" />
	</SchemeIndex>
	<SchemeIndex ID="1bea9f8e-a44a-4491-a6c9-7f122ebc153e" Name="ndx_AcquaintanceRows_CardIDUserName">
		<SchemeIndexedColumn Column="e0ebf86c-a58a-4d14-81bc-b7e078c1afe6" />
		<SchemeIndexedColumn Column="42462b71-c5d5-49a3-86f5-48b8abf98122" />
		<SchemeIncludedColumn Column="b48adebd-4273-007e-4000-0301857cb559" />
	</SchemeIndex>
	<SchemeIndex ID="5c2ce774-e729-4f02-bb01-15fbeeb9cd9f" Name="ndx_AcquaintanceRows_CardIDSendDate">
		<SchemeIndexedColumn Column="e0ebf86c-a58a-4d14-81bc-b7e078c1afe6" />
		<SchemeIndexedColumn Column="d5ceb20c-dfaa-4398-b5cf-77914826865f" />
		<SchemeIncludedColumn Column="b48adebd-4273-007e-4000-0301857cb559" />
	</SchemeIndex>
	<SchemeIndex ID="9abbf65c-e3ed-4982-a371-10916fc3fb4a" Name="ndx_AcquaintanceRows_CardIDCommentID">
		<SchemeIndexedColumn Column="e0ebf86c-a58a-4d14-81bc-b7e078c1afe6" />
		<SchemeIndexedColumn Column="b48adebd-4273-007e-4000-0301857cb559" />
		<SchemeIncludedColumn Column="a6935066-2f70-4e34-a868-950456c74578" />
		<SchemeIncludedColumn Column="42462b71-c5d5-49a3-86f5-48b8abf98122" />
		<SchemeIncludedColumn Column="d5ceb20c-dfaa-4398-b5cf-77914826865f" />
		<SchemeIncludedColumn Column="20c6ad34-e0fd-4632-b882-8eb347236001" />
		<SchemeIncludedColumn Column="6e29ef97-a268-4b52-89b3-888d060f8276" />
	</SchemeIndex>
	<SchemeIndex ID="39978247-a7dd-4d97-b5df-5f744d9bb66e" Name="ndx_AcquaintanceRows_UserIDIsInformedSendDate">
		<SchemeIndexedColumn Column="ac204cb7-9274-0022-4000-00391ad8f702" />
		<SchemeIndexedColumn Column="20c6ad34-e0fd-4632-b882-8eb347236001" />
		<SchemeIndexedColumn Column="d5ceb20c-dfaa-4398-b5cf-77914826865f" />
		<SchemeIncludedColumn Column="b48adebd-4273-007e-4000-0301857cb559" />
	</SchemeIndex>
	<SchemeIndex ID="8a244453-3bc5-43ea-88fe-8728fb3d0700" Name="ndx_AcquaintanceRows_UserIDSendDate">
		<SchemeIndexedColumn Column="ac204cb7-9274-0022-4000-00391ad8f702" />
		<SchemeIndexedColumn Column="d5ceb20c-dfaa-4398-b5cf-77914826865f" />
		<SchemeIncludedColumn Column="b48adebd-4273-007e-4000-0301857cb559" />
	</SchemeIndex>
	<SchemeIndex ID="54cd3df2-1766-4252-a86c-fa401ee61327" Name="ndx_AcquaintanceRows_UserIDSenderName">
		<SchemeIndexedColumn Column="ac204cb7-9274-0022-4000-00391ad8f702" />
		<SchemeIndexedColumn Column="a6935066-2f70-4e34-a868-950456c74578" />
		<SchemeIncludedColumn Column="b48adebd-4273-007e-4000-0301857cb559" />
	</SchemeIndex>
</SchemeTable>