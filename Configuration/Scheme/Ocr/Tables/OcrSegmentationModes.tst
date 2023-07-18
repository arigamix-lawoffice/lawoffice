<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="a6d84d24-0461-4c65-aa5d-f41ee300f081" ID="5455dc3b-79f8-4c06-8cf0-64e63919ab4a" Name="OcrSegmentationModes" Group="Ocr">
	<Description>Image page segmentation modes for text recognition</Description>
	<SchemePhysicalColumn Partition="a6d84d24-0461-4c65-aa5d-f41ee300f081" ID="37507ad9-d1f0-4d39-8ae3-6e5a5936a730" Name="ID" Type="Int32 Not Null">
		<Description>Mode identifier</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="748bda68-c8b4-4e9e-ba52-14a8176332b2" Name="Name" Type="String(256) Not Null">
		<Description>Mode name</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="8697082e-837b-40aa-b4c9-36ed3ac1e6c9" Name="Hidden" Type="Boolean Not Null">
		<Description>Sign that mode is hidden</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="aca94c88-dfbb-45fe-9662-e0c27656f914" Name="df_OcrSegmentationModes_Hidden" Value="false" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey ID="5d6038f6-7fbc-4432-9f1d-90c6d565aaf1" Name="pk_OcrSegmentationModes">
		<SchemeIndexedColumn Column="37507ad9-d1f0-4d39-8ae3-6e5a5936a730" />
	</SchemePrimaryKey>
	<SchemeUniqueKey ID="e81df9a3-7907-4ee3-82ca-29094b9a984a" Name="ndx_OcrSegmentationModes_Name">
		<SchemeIndexedColumn Column="748bda68-c8b4-4e9e-ba52-14a8176332b2" />
	</SchemeUniqueKey>
	<SchemeRecord>
		<ID ID="37507ad9-d1f0-4d39-8ae3-6e5a5936a730">0</ID>
		<Name ID="748bda68-c8b4-4e9e-ba52-14a8176332b2">$Enum_OcrSegmentationModes_OsdOnly</Name>
		<Hidden ID="8697082e-837b-40aa-b4c9-36ed3ac1e6c9">true</Hidden>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="37507ad9-d1f0-4d39-8ae3-6e5a5936a730">1</ID>
		<Name ID="748bda68-c8b4-4e9e-ba52-14a8176332b2">$Enum_OcrSegmentationModes_AutoOsd</Name>
		<Hidden ID="8697082e-837b-40aa-b4c9-36ed3ac1e6c9">false</Hidden>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="37507ad9-d1f0-4d39-8ae3-6e5a5936a730">3</ID>
		<Name ID="748bda68-c8b4-4e9e-ba52-14a8176332b2">$Enum_OcrSegmentationModes_Auto</Name>
		<Hidden ID="8697082e-837b-40aa-b4c9-36ed3ac1e6c9">false</Hidden>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="37507ad9-d1f0-4d39-8ae3-6e5a5936a730">4</ID>
		<Name ID="748bda68-c8b4-4e9e-ba52-14a8176332b2">$Enum_OcrSegmentationModes_SingleColumn</Name>
		<Hidden ID="8697082e-837b-40aa-b4c9-36ed3ac1e6c9">false</Hidden>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="37507ad9-d1f0-4d39-8ae3-6e5a5936a730">5</ID>
		<Name ID="748bda68-c8b4-4e9e-ba52-14a8176332b2">$Enum_OcrSegmentationModes_SingleBlockVertText</Name>
		<Hidden ID="8697082e-837b-40aa-b4c9-36ed3ac1e6c9">true</Hidden>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="37507ad9-d1f0-4d39-8ae3-6e5a5936a730">6</ID>
		<Name ID="748bda68-c8b4-4e9e-ba52-14a8176332b2">$Enum_OcrSegmentationModes_SingleBlock</Name>
		<Hidden ID="8697082e-837b-40aa-b4c9-36ed3ac1e6c9">false</Hidden>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="37507ad9-d1f0-4d39-8ae3-6e5a5936a730">7</ID>
		<Name ID="748bda68-c8b4-4e9e-ba52-14a8176332b2">$Enum_OcrSegmentationModes_SingleLine</Name>
		<Hidden ID="8697082e-837b-40aa-b4c9-36ed3ac1e6c9">false</Hidden>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="37507ad9-d1f0-4d39-8ae3-6e5a5936a730">8</ID>
		<Name ID="748bda68-c8b4-4e9e-ba52-14a8176332b2">$Enum_OcrSegmentationModes_SingleWord</Name>
		<Hidden ID="8697082e-837b-40aa-b4c9-36ed3ac1e6c9">true</Hidden>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="37507ad9-d1f0-4d39-8ae3-6e5a5936a730">9</ID>
		<Name ID="748bda68-c8b4-4e9e-ba52-14a8176332b2">$Enum_OcrSegmentationModes_CircleWord</Name>
		<Hidden ID="8697082e-837b-40aa-b4c9-36ed3ac1e6c9">true</Hidden>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="37507ad9-d1f0-4d39-8ae3-6e5a5936a730">10</ID>
		<Name ID="748bda68-c8b4-4e9e-ba52-14a8176332b2">$Enum_OcrSegmentationModes_SingleChar</Name>
		<Hidden ID="8697082e-837b-40aa-b4c9-36ed3ac1e6c9">true</Hidden>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="37507ad9-d1f0-4d39-8ae3-6e5a5936a730">11</ID>
		<Name ID="748bda68-c8b4-4e9e-ba52-14a8176332b2">$Enum_OcrSegmentationModes_SparseText</Name>
		<Hidden ID="8697082e-837b-40aa-b4c9-36ed3ac1e6c9">false</Hidden>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="37507ad9-d1f0-4d39-8ae3-6e5a5936a730">12</ID>
		<Name ID="748bda68-c8b4-4e9e-ba52-14a8176332b2">$Enum_OcrSegmentationModes_SparseTextOsd</Name>
		<Hidden ID="8697082e-837b-40aa-b4c9-36ed3ac1e6c9">false</Hidden>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="37507ad9-d1f0-4d39-8ae3-6e5a5936a730">13</ID>
		<Name ID="748bda68-c8b4-4e9e-ba52-14a8176332b2">$Enum_OcrSegmentationModes_RawLine</Name>
		<Hidden ID="8697082e-837b-40aa-b4c9-36ed3ac1e6c9">true</Hidden>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="37507ad9-d1f0-4d39-8ae3-6e5a5936a730">2</ID>
		<Name ID="748bda68-c8b4-4e9e-ba52-14a8176332b2">$Enum_OcrSegmentationModes_AutoOnly</Name>
		<Hidden ID="8697082e-837b-40aa-b4c9-36ed3ac1e6c9">true</Hidden>
	</SchemeRecord>
</SchemeTable>