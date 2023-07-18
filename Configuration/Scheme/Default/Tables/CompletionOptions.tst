<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="08cf782d-4130-4377-8a49-3e201a05d496" Name="CompletionOptions" Group="System">
	<Description>Список возможных варианты завершения.</Description>
	<SchemePhysicalColumn ID="132dc5f5-ce87-4dd0-acce-b4a02acf7715" Name="ID" Type="Guid Not Null" IsRowGuidColumn="true">
		<Description>Идентификатор варианта завершения.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="aa6a7122-8384-4c81-9553-386f2c05e96c" Name="Name" Type="String(128) Not Null">
		<Description>Имя варианта завершения.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="6762309a-b0ff-4b2f-9cce-dd111116e554" Name="Caption" Type="String(128) Not Null">
		<Description>Отображаемое пользователю имя варианта завершения.</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey ID="1f42080e-2563-4a39-a5e2-c775ca6ff286" Name="pk_CompletionOptions" IsClustered="true">
		<SchemeIndexedColumn Column="132dc5f5-ce87-4dd0-acce-b4a02acf7715" />
	</SchemePrimaryKey>
	<SchemeIndex ID="5a5e4beb-cf6a-4770-af07-2c41ea7688e8" Name="ndx_CompletionOptions_Name" IsUnique="true">
		<SchemeIndexedColumn Column="aa6a7122-8384-4c81-9553-386f2c05e96c">
			<Expression Dbms="PostgreSql">lower("Name")</Expression>
		</SchemeIndexedColumn>
	</SchemeIndex>
	<SchemeRecord>
		<ID ID="132dc5f5-ce87-4dd0-acce-b4a02acf7715">78c2fd7d-d0fe-0ede-93a6-9de4f372e8e6</ID>
		<Name ID="aa6a7122-8384-4c81-9553-386f2c05e96c">AddComment</Name>
		<Caption ID="6762309a-b0ff-4b2f-9cce-dd111116e554">$UI_Tasks_CompletionOptions_AddComment</Caption>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="132dc5f5-ce87-4dd0-acce-b4a02acf7715">8cf5cf41-8347-05b4-b3b2-519e8e621225</ID>
		<Name ID="aa6a7122-8384-4c81-9553-386f2c05e96c">Approve</Name>
		<Caption ID="6762309a-b0ff-4b2f-9cce-dd111116e554">$UI_Tasks_CompletionOptions_Approve</Caption>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="132dc5f5-ce87-4dd0-acce-b4a02acf7715">2582b66f-375a-0d59-ae86-a149309c5785</ID>
		<Name ID="aa6a7122-8384-4c81-9553-386f2c05e96c">Cancel</Name>
		<Caption ID="6762309a-b0ff-4b2f-9cce-dd111116e554">$UI_Tasks_CompletionOptions_Cancel</Caption>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="132dc5f5-ce87-4dd0-acce-b4a02acf7715">6e244482-2e2f-46fd-8ec3-0de6daea2930</ID>
		<Name ID="aa6a7122-8384-4c81-9553-386f2c05e96c">CancelApprovalProcess</Name>
		<Caption ID="6762309a-b0ff-4b2f-9cce-dd111116e554">$UI_Tasks_CompletionOptions_CancelApprovalProcess</Caption>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="132dc5f5-ce87-4dd0-acce-b4a02acf7715">5b108223-92db-49b9-8085-336758ccabaa</ID>
		<Name ID="aa6a7122-8384-4c81-9553-386f2c05e96c">Complete</Name>
		<Caption ID="6762309a-b0ff-4b2f-9cce-dd111116e554">$UI_Tasks_CompletionOptions_Complete</Caption>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="132dc5f5-ce87-4dd0-acce-b4a02acf7715">9ba9f111-fa2f-4c8e-8236-c924280a4a07</ID>
		<Name ID="aa6a7122-8384-4c81-9553-386f2c05e96c">Continue</Name>
		<Caption ID="6762309a-b0ff-4b2f-9cce-dd111116e554">$UI_Tasks_CompletionOptions_Continue</Caption>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="132dc5f5-ce87-4dd0-acce-b4a02acf7715">793bbafa-7f62-4af8-a156-515887d4d066</ID>
		<Name ID="aa6a7122-8384-4c81-9553-386f2c05e96c">CreateChildResolution</Name>
		<Caption ID="6762309a-b0ff-4b2f-9cce-dd111116e554">$UI_Tasks_CompletionOptions_CreateChildResolution</Caption>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="132dc5f5-ce87-4dd0-acce-b4a02acf7715">b997a7f2-ad57-036f-8798-298c14309f46</ID>
		<Name ID="aa6a7122-8384-4c81-9553-386f2c05e96c">Delegate</Name>
		<Caption ID="6762309a-b0ff-4b2f-9cce-dd111116e554">$UI_Tasks_CompletionOptions_Delegate</Caption>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="132dc5f5-ce87-4dd0-acce-b4a02acf7715">66e0a7e1-484a-40a6-b123-06118ce3b160</ID>
		<Name ID="aa6a7122-8384-4c81-9553-386f2c05e96c">DeregisterDocument</Name>
		<Caption ID="6762309a-b0ff-4b2f-9cce-dd111116e554">$UI_Tasks_CompletionOptions_DeregisterDocument</Caption>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="132dc5f5-ce87-4dd0-acce-b4a02acf7715">811d41ef-5610-421e-a573-fcdfd821713e</ID>
		<Name ID="aa6a7122-8384-4c81-9553-386f2c05e96c">Disapprove</Name>
		<Caption ID="6762309a-b0ff-4b2f-9cce-dd111116e554">$UI_Tasks_CompletionOptions_Disapprove</Caption>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="132dc5f5-ce87-4dd0-acce-b4a02acf7715">89ada741-6829-4d9f-892b-72d76ecf4ee6</ID>
		<Name ID="aa6a7122-8384-4c81-9553-386f2c05e96c">ModifyAsAuthor</Name>
		<Caption ID="6762309a-b0ff-4b2f-9cce-dd111116e554">$UI_Tasks_CompletionOptions_ModifyAsAuthor</Caption>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="132dc5f5-ce87-4dd0-acce-b4a02acf7715">c0b704b3-3ac5-4a0d-bcb6-1210e9cdb0b3</ID>
		<Name ID="aa6a7122-8384-4c81-9553-386f2c05e96c">NewApprovalCycle</Name>
		<Caption ID="6762309a-b0ff-4b2f-9cce-dd111116e554">$UI_Tasks_CompletionOptions_NewApprovalCycle</Caption>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="132dc5f5-ce87-4dd0-acce-b4a02acf7715">d6fbbf34-d22d-4226-831d-f3f1f31b9954</ID>
		<Name ID="aa6a7122-8384-4c81-9553-386f2c05e96c">OptionA</Name>
		<Caption ID="6762309a-b0ff-4b2f-9cce-dd111116e554">$UI_Tasks_CompletionOptions_OptionA</Caption>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="132dc5f5-ce87-4dd0-acce-b4a02acf7715">679a8309-f251-4acf-8b2e-7c5277b04d63</ID>
		<Name ID="aa6a7122-8384-4c81-9553-386f2c05e96c">OptionB</Name>
		<Caption ID="6762309a-b0ff-4b2f-9cce-dd111116e554">$UI_Tasks_CompletionOptions_OptionB</Caption>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="132dc5f5-ce87-4dd0-acce-b4a02acf7715">174d3f96-c658-07b7-ba6a-d51a893390d8</ID>
		<Name ID="aa6a7122-8384-4c81-9553-386f2c05e96c">RebuildDocument</Name>
		<Caption ID="6762309a-b0ff-4b2f-9cce-dd111116e554">$UI_Tasks_CompletionOptions_Rebuild</Caption>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="132dc5f5-ce87-4dd0-acce-b4a02acf7715">48ae0fd4-8a0d-494a-b89d-ca8fc33efe7c</ID>
		<Name ID="aa6a7122-8384-4c81-9553-386f2c05e96c">RegisterDocument</Name>
		<Caption ID="6762309a-b0ff-4b2f-9cce-dd111116e554">$UI_Tasks_CompletionOptions_RegisterDocument</Caption>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="132dc5f5-ce87-4dd0-acce-b4a02acf7715">d97d75a9-96ae-00ca-83ad-baa5c6aa811b</ID>
		<Name ID="aa6a7122-8384-4c81-9553-386f2c05e96c">RejectApproval</Name>
		<Caption ID="6762309a-b0ff-4b2f-9cce-dd111116e554">$UI_Tasks_CompletionOptions_Reject</Caption>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="132dc5f5-ce87-4dd0-acce-b4a02acf7715">fffb3209-2b67-09f0-bd25-ba4ec94ca5e8</ID>
		<Name ID="aa6a7122-8384-4c81-9553-386f2c05e96c">RequestComments</Name>
		<Caption ID="6762309a-b0ff-4b2f-9cce-dd111116e554">$UI_Tasks_CompletionOptions_RequestComments</Caption>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="132dc5f5-ce87-4dd0-acce-b4a02acf7715">6472fea9-f818-4ab5-9f31-9ccdaea9b412</ID>
		<Name ID="aa6a7122-8384-4c81-9553-386f2c05e96c">Revoke</Name>
		<Caption ID="6762309a-b0ff-4b2f-9cce-dd111116e554">$UI_Tasks_CompletionOptions_Revoke</Caption>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="132dc5f5-ce87-4dd0-acce-b4a02acf7715">f4ebe563-14f6-4b20-a61f-0bac4c11c8ac</ID>
		<Name ID="aa6a7122-8384-4c81-9553-386f2c05e96c">SendToPerformer</Name>
		<Caption ID="6762309a-b0ff-4b2f-9cce-dd111116e554">$UI_Tasks_CompletionOptions_SendToPerformer</Caption>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="132dc5f5-ce87-4dd0-acce-b4a02acf7715">7000ea10-efd8-0479-a6d4-b5e37a27f30a</ID>
		<Name ID="aa6a7122-8384-4c81-9553-386f2c05e96c">Accept</Name>
		<Caption ID="6762309a-b0ff-4b2f-9cce-dd111116e554">$UI_Tasks_CompletionOptions_Accept</Caption>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="132dc5f5-ce87-4dd0-acce-b4a02acf7715">c726d8ba-73b9-4867-87fe-387d4c61a75a</ID>
		<Name ID="aa6a7122-8384-4c81-9553-386f2c05e96c">AdditionalApproval</Name>
		<Caption ID="6762309a-b0ff-4b2f-9cce-dd111116e554">$UI_Tasks_CompletionOptions_AdditionalApproval</Caption>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="132dc5f5-ce87-4dd0-acce-b4a02acf7715">45d6f756-d30b-4c98-9d72-6adf1a15d075</ID>
		<Name ID="aa6a7122-8384-4c81-9553-386f2c05e96c">Sign</Name>
		<Caption ID="6762309a-b0ff-4b2f-9cce-dd111116e554">$UI_Tasks_CompletionOptions_Sign</Caption>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="132dc5f5-ce87-4dd0-acce-b4a02acf7715">4de44ffd-c2ca-4fad-835b-631222b076e1</ID>
		<Name ID="aa6a7122-8384-4c81-9553-386f2c05e96c">Decline</Name>
		<Caption ID="6762309a-b0ff-4b2f-9cce-dd111116e554">$UI_Tasks_CompletionOptions_Decline</Caption>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="132dc5f5-ce87-4dd0-acce-b4a02acf7715">a9067834-1a01-468c-976b-0ec7a9939331</ID>
		<Name ID="aa6a7122-8384-4c81-9553-386f2c05e96c">ShowDialog</Name>
		<Caption ID="6762309a-b0ff-4b2f-9cce-dd111116e554">$UI_Tasks_CompletionOptions_ShowDialog</Caption>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="132dc5f5-ce87-4dd0-acce-b4a02acf7715">08cf782d-4130-4377-8a49-3e201a05d496</ID>
		<Name ID="aa6a7122-8384-4c81-9553-386f2c05e96c">TakeOver</Name>
		<Caption ID="6762309a-b0ff-4b2f-9cce-dd111116e554">$UI_Tasks_CompletionOptions_TakeOver</Caption>
	</SchemeRecord>
</SchemeTable>