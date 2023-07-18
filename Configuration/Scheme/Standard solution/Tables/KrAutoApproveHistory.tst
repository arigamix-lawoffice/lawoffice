<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="dee2be6f-5d24-443f-b468-f6e03a6742b5" Name="KrAutoApproveHistory" Group="System">
	<SchemePhysicalColumn ID="42c6ff9e-df9e-4703-a50b-ba433a8b8fd8" Name="CardDigest" Type="String(255) Null" />
	<SchemePhysicalColumn ID="57d93542-6e71-4168-a9a5-20dac61aba99" Name="Date" Type="DateTime Not Null" />
	<SchemePhysicalColumn ID="60c46ee6-1e9b-4bef-a8ad-e4d43f4e75e0" Name="CardID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="8ed805f9-3a80-4606-bb83-44547db67a57" Name="CardTypeID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="8069cb48-3d8e-4dbe-b1d0-896c6c5b5c96" Name="CardTypeCaption" Type="String(255) Not Null" />
	<SchemePhysicalColumn ID="0abd8df6-c723-4802-8621-ef1e7ca0941a" Name="ID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="1018ace4-4b14-4e23-a56f-07f9fd35dbb5" Name="User" Type="Reference(Typified) Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="1018ace4-4b14-0023-4000-07f9fd35dbb5" Name="UserID" Type="Guid Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="03714e0c-1316-433d-b3ab-e5233292d948" Name="Comment" Type="String(Max) Null">
		<Description>Комментарий, с которым было завершено задание.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="15f49b4f-4ee7-4c45-93a5-fc9d735648dc" Name="RowNumber" Type="Int64 Not Null" IsIdentity="true" />
	<SchemePrimaryKey ID="cb225f5d-f49d-462b-82be-7efc2167a2b1" Name="pk_KrAutoApproveHistory">
		<SchemeIndexedColumn Column="15f49b4f-4ee7-4c45-93a5-fc9d735648dc" />
	</SchemePrimaryKey>
	<SchemeIndex ID="9808616a-8c91-4e12-a2d0-3f33afb5c417" Name="ndx_KrAutoApproveHistory_IDUserID">
		<SchemeIndexedColumn Column="0abd8df6-c723-4802-8621-ef1e7ca0941a" />
		<SchemeIndexedColumn Column="1018ace4-4b14-0023-4000-07f9fd35dbb5" />
	</SchemeIndex>
</SchemeTable>