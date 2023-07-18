<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="5e584567-9e11-4741-ab3a-d96af0b6e0c9" Name="KrResolutionSettingsVirtual" Group="KrStageTypes" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="5e584567-9e11-0041-2000-096af0b6e0c9" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="5e584567-9e11-0141-4000-096af0b6e0c9" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="f8d673d0-6d9a-4760-990a-521055bd1f44" Name="Kind" Type="Reference(Typified) Null" ReferencedTable="856068b1-0e78-4aa8-8e7a-4f53d91a7298">
		<Description>Вид резолюции, которая будет отправлена как дочернее или в результате делегирования.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="f8d673d0-6d9a-0060-4000-021055bd1f44" Name="KindID" Type="Guid Null" ReferencedColumn="856068b1-0e78-01a8-4000-0f53d91a7298" />
		<SchemeReferencingColumn ID="333b8457-ccb4-457c-8792-537d803f1785" Name="KindCaption" Type="String(128) Null" ReferencedColumn="63d9110b-7628-4bf9-9dae-750c3035e48d" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="de72b856-72ff-4610-977e-f703e96ad3d0" Name="Controller" Type="Reference(Typified) Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<Description>Роль для контролёра выполняемого с резолюцией действия или Null, если контроль для действия не требуется, не осуществляется или выполняется для пользователя Author, от имени которого была создана резолюция.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="de72b856-72ff-0010-4000-0703e96ad3d0" Name="ControllerID" Type="Guid Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="55b60316-7f24-4789-a63c-d3f533bb1984" Name="ControllerName" Type="String(128) Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="55882a45-efce-4afa-858c-b9e9ec02d9cd" Name="Comment" Type="String(Max) Null">
		<Description>Комментарий к действию, выполняемому с резолюцией.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="73766f10-6645-43d8-a14f-7fca0863da8f" Name="Planned" Type="DateTime Null">
		<Description>Дата и время выполнения действия с резолюцией или Null, или вместо этого указана длительность или для действия не задаётся такая дата.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="b7af51c9-e7e8-4edf-8a0f-8f8e49f9443a" Name="DurationInDays" Type="Double Null">
		<Description>Длительность выполнения действия с заданием в календарных днях (может быть дробным) или Null, если вместо этого указаны дата и время завершения задания или для действия не задаётся такая длительность.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="c1ad96eb-b24e-4446-9e18-12807b74da95" Name="df_KrResolutionSettingsVirtual_DurationInDays" Value="1" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="3026cbf2-8ff6-47dc-b24f-afcfb4037017" Name="WithControl" Type="Boolean Not Null">
		<Description>Признак того, что создаваемая резолюция создаётся с контролем, т.е. при завершении такой резолюции возвращается резолюция типа "Контроль исполнения".</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="77401815-404f-4b85-ae8d-193f6a5f281f" Name="df_KrResolutionSettingsVirtual_WithControl" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="23fe6d6d-67ef-44fa-8909-02bc1582a3b1" Name="MassCreation" Type="Boolean Not Null">
		<Description>Признак того, что при создании дочерней резолюции производится массовое создание резолюций каждому исполнителю в списке. В противном случае создаётся одна резолюция на роль, содержащую всех сотрудников, входящих в список исполнителей.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="f982a273-e843-4bde-97b4-0c42bb9f9301" Name="df_KrResolutionSettingsVirtual_MassCreation" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="1bd2bb44-591e-4a26-8808-cb0ac7b83c9e" Name="MajorPerformer" Type="Boolean Not Null">
		<Description>Флажок "Первый исполнитель - ответственный" при отправке резолюции на несколько исполнителей без объединения.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="8f2589f8-2109-4eab-92ca-6a5ecef4c46e" Name="df_KrResolutionSettingsVirtual_MajorPerformer" Value="false" />
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="48b3b368-5026-4887-a320-242b3aa1c6d3" Name="Sender" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<Description>Отправитель - тот, кто отправил задание.&lt;</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="48b3b368-5026-0087-4000-042b3aa1c6d3" Name="SenderID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="a0b00876-c6dc-45bc-adfc-5df402b7eda8" Name="SenderName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="5e584567-9e11-0041-5000-096af0b6e0c9" Name="pk_KrResolutionSettingsVirtual" IsClustered="true">
		<SchemeIndexedColumn Column="5e584567-9e11-0141-4000-096af0b6e0c9" />
	</SchemePrimaryKey>
</SchemeTable>