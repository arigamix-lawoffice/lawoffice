<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="6a0f5914-6a44-4e7d-b400-6b82ec1e2209" Name="WfResolutions" Group="Wf" InstanceType="Tasks" ContentType="Entries">
	<Description>Задание резолюции, построенное на Workflow.
Содержит как информацию по заданию, так и информацию по тому, какие поля будут заполняться для действий с резолюцией.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="6a0f5914-6a44-007d-2000-0b82ec1e2209" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="5bfa9936-bb5a-4e8f-89a9-180bfd8f75f8">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="6a0f5914-6a44-017d-4000-0b82ec1e2209" Name="ID" Type="Guid Not Null" ReferencedColumn="5bfa9936-bb5a-008f-3100-080bfd8f75f8" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="5f044513-2ca7-4630-b298-a6f327c28730" Name="Kind" Type="Reference(Typified) Null" ReferencedTable="856068b1-0e78-4aa8-8e7a-4f53d91a7298">
		<Description>Вид резолюции, которая будет отправлена как дочернее или в результате делегирования.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="5f044513-2ca7-0030-4000-06f327c28730" Name="KindID" Type="Guid Null" ReferencedColumn="856068b1-0e78-01a8-4000-0f53d91a7298" />
		<SchemeReferencingColumn ID="461ebb67-8912-4481-9391-cc83e1a4992d" Name="KindCaption" Type="String(128) Null" ReferencedColumn="63d9110b-7628-4bf9-9dae-750c3035e48d">
			<Description>Название вида заданий.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="ba6bbfd4-28aa-4db7-b853-1dc4f43a5d20" Name="Author" Type="Reference(Typified) Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<Description>Автор выполняемого с резолюцией действия, от имени которого делегируется или создаётся дочерняя резолюция.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="ba6bbfd4-28aa-00b7-4000-0dc4f43a5d20" Name="AuthorID" Type="Guid Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="3e80ec6c-768a-42cf-9dcc-3778f695545a" Name="AuthorName" Type="String(128) Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964">
			<Description>Отображаемое имя пользователя.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="3aa9c728-209f-499e-9fc9-c90e4aa83667" Name="Controller" Type="Reference(Typified) Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b" WithForeignKey="false">
		<Description>Роль для контролёра выполняемого с резолюцией действия или Null, если контроль для действия не требуется, не осуществляется или выполняется для пользователя Author, от имени которого была создана резолюция.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="3aa9c728-209f-009e-4000-090e4aa83667" Name="ControllerID" Type="Guid Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="b95a6563-1fea-472e-a438-784277a4d1ea" Name="ControllerName" Type="String(128) Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0">
			<Description>Отображаемое имя роли.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="c20c8363-1c6d-4097-9dd6-e577e1e41fdf" Name="Comment" Type="String(Max) Null">
		<Description>Комментарий к действию, выполняемому с резолюцией.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="b451788f-5dac-454e-9ea4-9ab56e4d5eb9" Name="Planned" Type="DateTime Null">
		<Description>Дата и время выполнения действия с резолюцией или Null, или вместо этого указана длительность или для действия не задаётся такая дата.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="a268eeda-bfc4-45a3-aa1f-8269a325aa53" Name="DurationInDays" Type="Double Null">
		<Description>Длительность выполнения действия с заданием в календарных днях (может быть дробным) или Null, если вместо этого указаны дата и время завершения задания или для действия не задаётся такая длительность.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="a9fbf363-b4b1-4dbb-aca2-21eb15be1ebe" Name="df_WfResolutions_DurationInDays" Value="3" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="9cb12cf3-8029-466f-989e-9e4677c73bf3" Name="RevokeChildren" Type="Boolean Not Null">
		<Description>Признак того, что дочерние резолюции отзываются при завершении созданной или делегированной резолюции.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="b3a7fae5-9934-437b-a310-3cbcc79de1ae" Name="df_WfResolutions_RevokeChildren" Value="true" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="43d388e4-a9ef-42dc-80d2-e6ca2e0c83ce" Name="WithControl" Type="Boolean Not Null">
		<Description>Признак того, что создаваемая резолюция создаётся с контролем, т.е. при завершении такой резолюции возвращается резолюция типа "Контроль исполнения".</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="ec5c2e30-0841-400f-b417-3d62b856c426" Name="df_WfResolutions_WithControl" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="77a55c13-18f1-4733-b3b4-baf9894272da" Name="ShowAdditional" Type="Boolean Not Null">
		<Description>Признак того, что в задании должны отображаться дополнительные поля, которые скрыты в обычном случае.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="c6b19f36-6e7f-4f5a-b9ac-86f88ef833ec" Name="df_WfResolutions_ShowAdditional" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="f9286d8d-15a3-451c-bb27-e016bdf5d771" Name="MassCreation" Type="Boolean Not Null">
		<Description>Признак того, что при создании дочерней резолюции производится массовое создание резолюций каждому исполнителю в списке. В противном случае создаётся одна резолюция на роль, содержащую всех сотрудников, входящих в список исполнителей.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="75aac0f4-f653-4e1e-b688-8fcdfb18260a" Name="df_WfResolutions_MassCreation" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="5fe72169-4115-4ca8-b304-0fb6e22f1f92" Name="ParentComment" Type="String(Max) Null">
		<Description>Комментарий к действию, выполняемому с резолюцией, который был указан в родительской резолюции при отправке этой (дочерней) резолюции.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="d19fd693-9fe2-4975-89b8-db5dbc36431e" Name="MajorPerformer" Type="Boolean Not Null">
		<Description>Флажок "Первый исполнитель - ответственный" при отправке резолюции на несколько исполнителей без объединения.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="8a94dfc1-1658-4ed5-af02-c02138216ef2" Name="df_WfResolutions_MajorPerformer" Value="false" />
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="90baf14c-81d4-47d4-b247-7e52f4b35581" Name="Sender" Type="Reference(Typified) Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="90baf14c-81d4-00d4-4000-0e52f4b35581" Name="SenderID" Type="Guid Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="58e7ed00-9c71-4ad0-ba03-5c72d05077d5" Name="SenderName" Type="String(128) Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="6a0f5914-6a44-007d-5000-0b82ec1e2209" Name="pk_WfResolutions" IsClustered="true">
		<SchemeIndexedColumn Column="6a0f5914-6a44-017d-4000-0b82ec1e2209" />
	</SchemePrimaryKey>
	<SchemeIndex ID="6adb2a8e-becb-4a28-afe1-f70c2bd6df04" Name="ndx_WfResolutions" />
</SchemeTable>