<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="a6d84d24-0461-4c65-aa5d-f41ee300f081" ID="aefacd5d-1de1-42ac-86db-ddb98035f498" Name="OcrRequestsStates" Group="Ocr">
	<Description>Text recognition request states</Description>
	<SchemePhysicalColumn ID="029ae1ea-a6ec-4f77-bd30-e150282ced83" Name="ID" Type="Int32 Not Null">
		<Description>State identifier</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="ab9f15bf-f6d6-42b6-8eb6-40f8f302eae6" Name="Name" Type="String(64) Not Null">
		<Description>State name</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey ID="4c7e9e13-be59-42ce-af26-3269561a46b7" Name="pk_OcrRequestsStates">
		<SchemeIndexedColumn Column="029ae1ea-a6ec-4f77-bd30-e150282ced83" />
	</SchemePrimaryKey>
	<SchemeUniqueKey ID="d37d9f10-867c-4d3a-91cb-27cf35080bfe" Name="ndx_OcrRequestsStates_Name">
		<SchemeIndexedColumn Column="ab9f15bf-f6d6-42b6-8eb6-40f8f302eae6" />
	</SchemeUniqueKey>
	<SchemeRecord>
		<ID ID="029ae1ea-a6ec-4f77-bd30-e150282ced83">0</ID>
		<Name ID="ab9f15bf-f6d6-42b6-8eb6-40f8f302eae6">$Enum_OcrRequestStates_Created</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="029ae1ea-a6ec-4f77-bd30-e150282ced83">1</ID>
		<Name ID="ab9f15bf-f6d6-42b6-8eb6-40f8f302eae6">$Enum_OcrRequestStates_Active</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="029ae1ea-a6ec-4f77-bd30-e150282ced83">2</ID>
		<Name ID="ab9f15bf-f6d6-42b6-8eb6-40f8f302eae6">$Enum_OcrRequestStates_Completed</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="029ae1ea-a6ec-4f77-bd30-e150282ced83">3</ID>
		<Name ID="ab9f15bf-f6d6-42b6-8eb6-40f8f302eae6">$Enum_OcrRequestStates_Interrupted</Name>
	</SchemeRecord>
</SchemeTable>