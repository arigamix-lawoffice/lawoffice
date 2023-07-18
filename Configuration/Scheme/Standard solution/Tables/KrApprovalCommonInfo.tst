<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="410324bf-ce75-4024-a14c-5d78a8ad7588" Name="KrApprovalCommonInfo" Group="Kr" InstanceType="Cards" ContentType="Entries">
	<Description>Содержит информацию по основному процессу.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="410324bf-ce75-0024-2000-0d78a8ad7588" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="410324bf-ce75-0124-4000-0d78a8ad7588" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="c818fad3-4641-40ff-b1d5-058b6c152a24" Name="MainCardID" Type="Guid Null" />
	<SchemeComplexColumn ID="71399f2b-60cf-4b4c-bc21-06c4eec0a1dc" Name="State" Type="Reference(Typified) Not Null" ReferencedTable="47107d7a-3a8c-47f0-b800-2a45da222ff4">
		<SchemeReferencingColumn IsPermanent="true" ID="ccae9571-b9fa-4509-97c4-c9e36936766a" Name="StateID" Type="Int16 Not Null" ReferencedColumn="502209b0-233f-4e1f-be01-35a50f53414c">
			<SchemeDefaultConstraint IsPermanent="true" ID="e7eda1a0-242c-4e17-b0fa-ac8b83020fbb" Name="df_KrApprovalCommonInfo_StateID" Value="0" />
		</SchemeReferencingColumn>
		<SchemeReferencingColumn ID="e36cb975-6c89-4e23-a12d-ac7a44223b49" Name="StateName" Type="String(128) Not Null" ReferencedColumn="4c1a8dd7-72ed-4fc9-b559-b38ae30dccb9">
			<SchemeDefaultConstraint IsPermanent="true" ID="70fddc01-4e3c-40f2-8caf-4546ef35811e" Name="df_KrApprovalCommonInfo_StateName" Value="$KrStates_Doc_Draft" />
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="642e65d2-f2e4-4d64-8236-cc20dfad0ef7" Name="CurrentApprovalStage" Type="Reference(Typified) Null" ReferencedTable="92caadca-2409-40ff-b7d8-1d4fd302b1e9" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="642e65d2-f2e4-0064-4000-0c20dfad0ef7" Name="CurrentApprovalStageRowID" Type="Guid Null" ReferencedColumn="92caadca-2409-00ff-3100-0d4fd302b1e9" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="a1035de3-1eac-4862-8f36-f3453bd0b526" Name="ApprovedBy" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="2163d113-f5c3-4b6a-a4c0-ccb4c03b90aa" Name="DisapprovedBy" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="c83ae28f-d983-40d9-a25a-c506f2c2cb54" Name="AuthorComment" Type="String(Max) Null" />
	<SchemeComplexColumn ID="e5e585cf-f032-44d2-89f7-ddefbb93b6ee" Name="Author" Type="Reference(Typified) Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="e5e585cf-f032-00d2-4000-0defbb93b6ee" Name="AuthorID" Type="Guid Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="df06beed-7998-4129-bfbe-d5952e820ada" Name="AuthorName" Type="String(128) Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964">
			<Description>Отображаемое имя пользователя.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="ac07450d-47c8-49c6-b8d0-36d4bf99b285" Name="StateChangedDateTimeUTC" Type="DateTime Null">
		<Description>Дата + время последнего изменения состояния согласования, null если согласование еще не запускалось</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="6639caa4-2999-4b87-9243-a52e2dd7cda2" Name="Info" Type="BinaryJson Null" />
	<SchemePhysicalColumn ID="22d36bb9-a41d-4c9d-b6f4-315a85792aba" Name="CurrentHistoryGroup" Type="Guid Null" />
	<SchemePhysicalColumn ID="6fdee199-7dc4-4a53-b3dd-c5c32449839b" Name="NestedWorkflowProcesses" Type="BinaryJson Null" />
	<SchemeComplexColumn ID="123f506c-827c-482e-acf1-24d4b2e1407d" Name="ProcessOwner" Type="Reference(Typified) Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<Description>Владелец процесса.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="123f506c-827c-002e-4000-04d4b2e1407d" Name="ProcessOwnerID" Type="Guid Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="a8c88bf0-a98c-4f2f-a61a-3636b845dfb0" Name="ProcessOwnerName" Type="String(128) Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="410324bf-ce75-0024-5000-0d78a8ad7588" Name="pk_KrApprovalCommonInfo" IsClustered="true">
		<SchemeIndexedColumn Column="410324bf-ce75-0124-4000-0d78a8ad7588" />
	</SchemePrimaryKey>
	<SchemeIndex ID="b511ac43-422b-489f-9ace-b8aca40e7295" Name="ndx_KrApprovalCommonInfo_MainCardID" IsUnique="true">
		<SchemeIndexedColumn Column="c818fad3-4641-40ff-b1d5-058b6c152a24" />
		<SchemeIncludedColumn Column="ccae9571-b9fa-4509-97c4-c9e36936766a" />
		<SchemeIncludedColumn Column="642e65d2-f2e4-0064-4000-0c20dfad0ef7" />
	</SchemeIndex>
	<SchemeIndex ID="84c98108-82d5-4831-8623-9657b1099ea4" Name="ndx_KrApprovalCommonInfo_AuthorID">
		<SchemeIndexedColumn Column="e5e585cf-f032-00d2-4000-0defbb93b6ee" />
		<SchemeIncludedColumn Column="c818fad3-4641-40ff-b1d5-058b6c152a24" />
	</SchemeIndex>
	<SchemeIndex ID="407949d7-b2ba-47a6-b78e-f981743def2d" Name="ndx_KrApprovalCommonInfo_StateID">
		<Description>Индекс нужен, чтобы при удалении состояний не происходило полное сканирование таблицы для проверки нарушений FK.</Description>
		<SchemeIndexedColumn Column="ccae9571-b9fa-4509-97c4-c9e36936766a" />
	</SchemeIndex>
</SchemeTable>