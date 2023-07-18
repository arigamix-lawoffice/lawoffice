<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="dee05376-8267-42b9-8cc9-1ff5bb58bb06" Name="WeTaskGroupActionOptions" Group="WorkflowEngine" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Секция с настройками вариантов завершения заданий</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="dee05376-8267-00b9-2000-0ff5bb58bb06" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="dee05376-8267-01b9-4000-0ff5bb58bb06" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="dee05376-8267-00b9-3100-0ff5bb58bb06" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="d128404a-84b4-417d-a89c-612723c6b0a7" Name="Option" Type="Reference(Typified) Not Null" ReferencedTable="08cf782d-4130-4377-8a49-3e201a05d496" WithForeignKey="false">
		<Description>Вариант завершения</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="d128404a-84b4-007d-4000-012723c6b0a7" Name="OptionID" Type="Guid Not Null" ReferencedColumn="132dc5f5-ce87-4dd0-acce-b4a02acf7715" />
		<SchemeReferencingColumn ID="305b3272-6612-412c-9330-7dec2c6bd6b9" Name="OptionCaption" Type="String(128) Not Null" ReferencedColumn="6762309a-b0ff-4b2f-9cce-dd111116e554" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="eb5ed82c-78d9-4f24-8aea-8606b713d5fc" Name="Link" Type="Reference(Abstract) Not Null">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="eb5ed82c-78d9-0024-4000-0606b713d5fc" Name="LinkID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
		<SchemePhysicalColumn ID="bab26062-cc8e-4e06-969a-457ccd947a11" Name="LinkName" Type="String(128) Not Null" />
		<SchemePhysicalColumn ID="f7fd74eb-9fd3-4643-b72c-7007fc897cad" Name="LinkCaption" Type="String(128) Not Null" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="c5010117-8df4-461b-a717-7df7f60886f1" Name="Script" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="f7e270b8-bd30-4891-95fa-c7af797d12c7" Name="CancelGroup" Type="Boolean Not Null">
		<Description>Флаг определяет, отменят ли данный вариант завершения необходимость выполнения остальных заданий группы</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="63b31849-6901-4d85-b6c9-80736310f3c0" Name="df_WeTaskGroupActionOptions_CancelGroup" Value="false" />
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="df98d653-1850-4b2c-aebb-e7b7f49b660d" Name="OptionType" Type="Reference(Typified) Not Null" ReferencedTable="dc9eb404-c42d-40ab-a4c0-3b8b6089b926">
		<SchemeReferencingColumn ID="37cccdf0-350a-45e3-b756-4ecec976d3ed" Name="OptionTypeName" Type="String(128) Not Null" ReferencedColumn="1f9a9050-e03e-4971-a244-c069f6e0ca19" />
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="df98d653-1850-002c-4000-07b7f49b660d" Name="OptionTypeID" Type="Int32 Not Null" ReferencedColumn="42e8f9cb-9637-47a7-84d5-368f9834e248" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="13830fb1-c759-40d1-911e-f7dcf9ab04ef" Name="Order" Type="Int32 Not Null" />
	<SchemePhysicalColumn ID="326863e5-901a-4e8b-a553-efff24ad0bd3" Name="PauseGroup" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="b0a6624b-e100-4a6b-a70d-338c60cc482b" Name="df_WeTaskGroupActionOptions_PauseGroup" Value="false" />
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="a242a812-c920-4e04-8c4f-2971342d2c1e" Name="CancelOption" Type="Reference(Typified) Null" ReferencedTable="08cf782d-4130-4377-8a49-3e201a05d496" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="a242a812-c920-0004-4000-0971342d2c1e" Name="CancelOptionID" Type="Guid Null" ReferencedColumn="132dc5f5-ce87-4dd0-acce-b4a02acf7715" />
		<SchemeReferencingColumn ID="0c081c01-c188-45d5-a8fb-8dcc1be10235" Name="CancelOptionCaption" Type="String(128) Null" ReferencedColumn="6762309a-b0ff-4b2f-9cce-dd111116e554" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="ea0435ae-460d-4aaa-80e2-529df3e4daa5" Name="NewRole" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="ea0435ae-460d-00aa-4000-029df3e4daa5" Name="NewRoleID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="8939249a-b30a-4960-bb0f-4ab8bfc07d15" Name="NewRoleName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="ffbc30fe-b92b-40a0-b0c8-9a7991dc81ca" Name="UseAsNextRole" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="3f3d0915-8b59-4ed2-96aa-36ee191b7081" Name="df_WeTaskGroupActionOptions_UseAsNextRole" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="6045a251-98f8-495a-9056-b1f05e1c4e2b" Name="Result" Type="String(Max) Null" />
	<SchemeComplexColumn ID="a3493ed6-df80-4476-925e-3c93edf4ff4e" Name="Notification" Type="Reference(Typified) Not Null" ReferencedTable="18145bb5-fd4e-4795-aa1f-9e1cd9b4ee5a" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="a3493ed6-df80-0076-4000-0c93edf4ff4e" Name="NotificationID" Type="Guid Not Null" ReferencedColumn="18145bb5-fd4e-0195-4000-0e1cd9b4ee5a" />
		<SchemeReferencingColumn ID="334ef781-0c9c-4233-9ec8-adaafaf1221c" Name="NotificationName" Type="String(256) Not Null" ReferencedColumn="265d4336-6764-4db8-8874-0e5fa92cbd5d" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="6ac5dcc6-854e-42d9-8a6e-24f3af4dcffb" Name="SendToPerformer" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="cacb5aeb-414d-4977-bd47-926f8163966f" Name="df_WeTaskGroupActionOptions_SendToPerformer" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="26efbf7f-be97-4881-b31b-20f68480639b" Name="SendToAuthor" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="e728e12f-0bb7-4fa3-b9e1-9dd2c101fa3e" Name="df_WeTaskGroupActionOptions_SendToAuthor" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="709cb789-e667-4045-9a00-d18dc728323f" Name="ExcludeDeputies" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="94500372-769d-4050-957c-36d3e11f5916" Name="df_WeTaskGroupActionOptions_ExcludeDeputies" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="8a0ba646-9aec-40ad-97c8-133801c039da" Name="ExcludeSubscribers" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="7beea011-af9c-4177-9709-47bbc15b9819" Name="df_WeTaskGroupActionOptions_ExcludeSubscribers" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="587327df-e6e0-475c-88fe-377e58f84b8c" Name="NotificationScript" Type="String(Max) Not Null" />
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="dee05376-8267-00b9-5000-0ff5bb58bb06" Name="pk_WeTaskGroupActionOptions">
		<SchemeIndexedColumn Column="dee05376-8267-00b9-3100-0ff5bb58bb06" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="dee05376-8267-00b9-7000-0ff5bb58bb06" Name="idx_WeTaskGroupActionOptions_ID" IsClustered="true">
		<SchemeIndexedColumn Column="dee05376-8267-01b9-4000-0ff5bb58bb06" />
	</SchemeIndex>
</SchemeTable>