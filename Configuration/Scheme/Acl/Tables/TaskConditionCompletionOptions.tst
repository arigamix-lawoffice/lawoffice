<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="bc3e1376-a8e1-4256-bf84-1bfc7a49c95f" ID="059e1354-8b89-4e86-8fa1-29395e952926" Name="TaskConditionCompletionOptions" Group="Acl" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Варианты завершения для условий проверки заданий.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="059e1354-8b89-0086-2000-09395e952926" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="059e1354-8b89-0186-4000-09395e952926" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="059e1354-8b89-0086-3100-09395e952926" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="bac75541-dfd8-4a25-8183-f8df7c96713a" Name="CompletionOption" Type="Reference(Typified) Not Null" ReferencedTable="08cf782d-4130-4377-8a49-3e201a05d496">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="bac75541-dfd8-0025-4000-08df7c96713a" Name="CompletionOptionID" Type="Guid Not Null" ReferencedColumn="132dc5f5-ce87-4dd0-acce-b4a02acf7715" />
		<SchemeReferencingColumn ID="15bac4ba-22af-46d2-8fd4-465191ff62d0" Name="CompletionOptionCaption" Type="String(128) Not Null" ReferencedColumn="6762309a-b0ff-4b2f-9cce-dd111116e554" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="059e1354-8b89-0086-5000-09395e952926" Name="pk_TaskConditionCompletionOptions">
		<SchemeIndexedColumn Column="059e1354-8b89-0086-3100-09395e952926" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="059e1354-8b89-0086-7000-09395e952926" Name="idx_TaskConditionCompletionOptions_ID" IsClustered="true">
		<SchemeIndexedColumn Column="059e1354-8b89-0186-4000-09395e952926" />
	</SchemeIndex>
</SchemeTable>