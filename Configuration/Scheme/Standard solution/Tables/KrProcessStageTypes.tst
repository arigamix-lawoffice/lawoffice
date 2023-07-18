<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="7454f645-850f-4e9b-8c80-1f129c5cb1c4" Name="KrProcessStageTypes" Group="KrStageTypes">
	<SchemePhysicalColumn ID="faee08f2-f030-4f6d-83ad-d7fc2feff82f" Name="ID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="a4fe24fc-2b3f-408a-8d2b-0440bd205eb8" Name="Caption" Type="String(300) Not Null" />
	<SchemePhysicalColumn ID="e5452027-b65d-4345-ad9f-3f7ec7a16752" Name="DefaultStageName" Type="String(300) Null" />
	<SchemePrimaryKey ID="8e14fcf7-e007-442a-8966-7ebb968a99b4" Name="pk_KrProcessStageTypes">
		<SchemeIndexedColumn Column="faee08f2-f030-4f6d-83ad-d7fc2feff82f" />
	</SchemePrimaryKey>
	<SchemeRecord>
		<ID ID="faee08f2-f030-4f6d-83ad-d7fc2feff82f">185610e1-6ab0-064e-9429-4c529804dfe4</ID>
		<Caption ID="a4fe24fc-2b3f-408a-8d2b-0440bd205eb8">$KrStages_Approval</Caption>
		<DefaultStageName ID="e5452027-b65d-4345-ad9f-3f7ec7a16752" xsi:nil="true" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" />
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="faee08f2-f030-4f6d-83ad-d7fc2feff82f">4bf667bf-1a82-4e3f-9ef0-44b3b56fb98d</ID>
		<Caption ID="a4fe24fc-2b3f-408a-8d2b-0440bd205eb8">$KrStages_Edit</Caption>
		<DefaultStageName ID="e5452027-b65d-4345-ad9f-3f7ec7a16752">$KrStages_Edit</DefaultStageName>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="faee08f2-f030-4f6d-83ad-d7fc2feff82f">c8a9a721-ba8e-45cd-a049-c24d4bdf76cb</ID>
		<Caption ID="a4fe24fc-2b3f-408a-8d2b-0440bd205eb8">$KrStages_ChangeState</Caption>
		<DefaultStageName ID="e5452027-b65d-4345-ad9f-3f7ec7a16752">$KrStages_ChangeState</DefaultStageName>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="faee08f2-f030-4f6d-83ad-d7fc2feff82f">42cef425-1180-4ccc-88d9-50fdc1ea3982</ID>
		<Caption ID="a4fe24fc-2b3f-408a-8d2b-0440bd205eb8">$KrStages_PartialRecalc</Caption>
		<DefaultStageName ID="e5452027-b65d-4345-ad9f-3f7ec7a16752">$KrStages_PartialRecalc</DefaultStageName>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="faee08f2-f030-4f6d-83ad-d7fc2feff82f">c7bc176c-8779-46bd-9604-ec847140bd52</ID>
		<Caption ID="a4fe24fc-2b3f-408a-8d2b-0440bd205eb8">$KrStages_ProcessManagement</Caption>
		<DefaultStageName ID="e5452027-b65d-4345-ad9f-3f7ec7a16752">$KrStages_ProcessManagement</DefaultStageName>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="faee08f2-f030-4f6d-83ad-d7fc2feff82f">6e6f6b28-97af-4ffe-b6f1-b1d8371cb3fa</ID>
		<Caption ID="a4fe24fc-2b3f-408a-8d2b-0440bd205eb8">$KrStages_Resolution</Caption>
		<DefaultStageName ID="e5452027-b65d-4345-ad9f-3f7ec7a16752">$KrStages_Resolution</DefaultStageName>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="faee08f2-f030-4f6d-83ad-d7fc2feff82f">9e85f310-226c-4273-804c-52c95b3bac8e</ID>
		<Caption ID="a4fe24fc-2b3f-408a-8d2b-0440bd205eb8">$KrStages_CreateCard</Caption>
		<DefaultStageName ID="e5452027-b65d-4345-ad9f-3f7ec7a16752">$KrStages_CreateCard</DefaultStageName>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="faee08f2-f030-4f6d-83ad-d7fc2feff82f">b468700e-6535-440d-a107-8945ed927429</ID>
		<Caption ID="a4fe24fc-2b3f-408a-8d2b-0440bd205eb8">$KrStages_Registration</Caption>
		<DefaultStageName ID="e5452027-b65d-4345-ad9f-3f7ec7a16752">$KrStages_Registration</DefaultStageName>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="faee08f2-f030-4f6d-83ad-d7fc2feff82f">9e6eee69-fbee-4be6-b0e2-9a1b5f8f63eb</ID>
		<Caption ID="a4fe24fc-2b3f-408a-8d2b-0440bd205eb8">$KrStages_Deregistration</Caption>
		<DefaultStageName ID="e5452027-b65d-4345-ad9f-3f7ec7a16752">$KrStages_Deregistration</DefaultStageName>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="faee08f2-f030-4f6d-83ad-d7fc2feff82f">d4670257-6028-4bbc-9cd6-ce163f36ea35</ID>
		<Caption ID="a4fe24fc-2b3f-408a-8d2b-0440bd205eb8">$KrStages_Signing</Caption>
		<DefaultStageName ID="e5452027-b65d-4345-ad9f-3f7ec7a16752" xsi:nil="true" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" />
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="faee08f2-f030-4f6d-83ad-d7fc2feff82f">c3acbff6-707f-477c-99c9-d15fc241fc78</ID>
		<Caption ID="a4fe24fc-2b3f-408a-8d2b-0440bd205eb8">$KrStages_UniversalTask</Caption>
		<DefaultStageName ID="e5452027-b65d-4345-ad9f-3f7ec7a16752">$KrStages_UniversalTask</DefaultStageName>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="faee08f2-f030-4f6d-83ad-d7fc2feff82f">c02d9a43-ad2a-475a-9188-8fc600b64ee8</ID>
		<Caption ID="a4fe24fc-2b3f-408a-8d2b-0440bd205eb8">$KrStages_Script</Caption>
		<DefaultStageName ID="e5452027-b65d-4345-ad9f-3f7ec7a16752">$KrStages_Script</DefaultStageName>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="faee08f2-f030-4f6d-83ad-d7fc2feff82f">19c7a9b3-6ae7-4072-b9ac-1753245ec0ac</ID>
		<Caption ID="a4fe24fc-2b3f-408a-8d2b-0440bd205eb8">$KrStages_Notification</Caption>
		<DefaultStageName ID="e5452027-b65d-4345-ad9f-3f7ec7a16752">$KrStages_Notification</DefaultStageName>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="faee08f2-f030-4f6d-83ad-d7fc2feff82f">c2e0e75a-de77-42cd-9ff8-e872b9899362</ID>
		<Caption ID="a4fe24fc-2b3f-408a-8d2b-0440bd205eb8">$KrStages_Acquaintance</Caption>
		<DefaultStageName ID="e5452027-b65d-4345-ad9f-3f7ec7a16752">$KrStages_Acquaintance</DefaultStageName>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="faee08f2-f030-4f6d-83ad-d7fc2feff82f">371937b5-38c6-436a-959b-42fd0ee01611</ID>
		<Caption ID="a4fe24fc-2b3f-408a-8d2b-0440bd205eb8">$KrStages_HistoryManagement</Caption>
		<DefaultStageName ID="e5452027-b65d-4345-ad9f-3f7ec7a16752">$KrStages_HistoryManagement</DefaultStageName>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="faee08f2-f030-4f6d-83ad-d7fc2feff82f">2246da18-bcf9-4a0c-a2b8-f61fbe9bfddb</ID>
		<Caption ID="a4fe24fc-2b3f-408a-8d2b-0440bd205eb8">$KrStages_Fork</Caption>
		<DefaultStageName ID="e5452027-b65d-4345-ad9f-3f7ec7a16752">$KrStages_Fork</DefaultStageName>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="faee08f2-f030-4f6d-83ad-d7fc2feff82f">e1f86f2d-c8d5-4482-ad9f-a023eda4bc48</ID>
		<Caption ID="a4fe24fc-2b3f-408a-8d2b-0440bd205eb8">$KrStages_ForkManagement</Caption>
		<DefaultStageName ID="e5452027-b65d-4345-ad9f-3f7ec7a16752">$KrStages_ForkManagement</DefaultStageName>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="faee08f2-f030-4f6d-83ad-d7fc2feff82f">ac7fcf5b-57d9-4a53-9c30-50e74cd3b68d</ID>
		<Caption ID="a4fe24fc-2b3f-408a-8d2b-0440bd205eb8">$KrStages_TypedTask</Caption>
		<DefaultStageName ID="e5452027-b65d-4345-ad9f-3f7ec7a16752">$KrStages_TypedTask</DefaultStageName>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="faee08f2-f030-4f6d-83ad-d7fc2feff82f">c80839e2-1766-4e02-b85c-279ea6fd600d</ID>
		<Caption ID="a4fe24fc-2b3f-408a-8d2b-0440bd205eb8">$KrStages_AddFromTemplate</Caption>
		<DefaultStageName ID="e5452027-b65d-4345-ad9f-3f7ec7a16752">$KrStages_AddFromTemplate</DefaultStageName>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="faee08f2-f030-4f6d-83ad-d7fc2feff82f">be14045d-f10e-4fc3-9b6e-8961ccc43c49</ID>
		<Caption ID="a4fe24fc-2b3f-408a-8d2b-0440bd205eb8">$KrStages_Dialog</Caption>
		<DefaultStageName ID="e5452027-b65d-4345-ad9f-3f7ec7a16752">$KrStages_Dialog</DefaultStageName>
	</SchemeRecord>
</SchemeTable>