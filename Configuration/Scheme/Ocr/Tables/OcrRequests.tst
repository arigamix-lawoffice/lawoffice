<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="a6d84d24-0461-4c65-aa5d-f41ee300f081" ID="d64806e9-ef31-4133-806b-670b178cc5bc" Name="OcrRequests" Group="Ocr" InstanceType="Cards" ContentType="Collections">
	<Description>Information on text recognition requests</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="d64806e9-ef31-0033-2000-070b178cc5bc" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<Description>Card identifier</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="d64806e9-ef31-0133-4000-070b178cc5bc" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747">
			<Description>Card identifier</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="d64806e9-ef31-0033-3100-070b178cc5bc" Name="RowID" Type="Guid Not Null">
		<Description>Unique record identifier, which is the same as text recognition operation identifier</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="23f3d28c-91f6-4d23-a48d-95996a873ec4" Name="Created" Type="DateTime Not Null">
		<Description>Date and time creation of request</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="7acdbb56-fed4-4535-a9dc-cdea3cada38c" Name="CreatedBy" Type="Reference(Typified) Not Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<Description>The user who created the request</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="7acdbb56-fed4-0035-4000-0dea3cada38c" Name="CreatedByID" Type="Guid Not Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3">
			<Description>User identifier</Description>
		</SchemeReferencingColumn>
		<SchemeReferencingColumn ID="d14da0fc-8471-4594-80bd-60e34b8b6d3b" Name="CreatedByName" Type="String(128) Not Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964">
			<Description>User name</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="bef558bf-e273-4cff-9f30-ce9768e9251a" Name="State" Type="Reference(Typified) Not Null" ReferencedTable="aefacd5d-1de1-42ac-86db-ddb98035f498">
		<Description>Request state</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="bef558bf-e273-00ff-4000-0e9768e9251a" Name="StateID" Type="Int32 Not Null" ReferencedColumn="029ae1ea-a6ec-4f77-bd30-e150282ced83">
			<Description>Request state identifier</Description>
			<SchemeDefaultConstraint IsPermanent="true" ID="983971b0-23ae-4a41-9fa5-1b55f82b9455" Name="df_OcrRequests_StateID" Value="0" />
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="7400b207-6f65-4327-929d-9eb8684ff056" Name="Confidence" Type="Decimal(4, 1) Not Null">
		<Description>Confidence threshold below which recognized text is considered unreliable</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="92f4e003-b45b-4690-b086-789d9e8e8c50" Name="df_OcrRequests_Confidence" Value="50" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="bde2cbfb-1a45-4e1a-a6e7-a7068aab46b3" Name="Preprocess" Type="Boolean Not Null">
		<Description>Perform image page preprocessing before recognition</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="241030d1-2ba8-4d88-95bb-0dcd479cc233" Name="df_OcrRequests_Preprocess" Value="false" />
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="e07ad515-201f-45fc-b90a-ca8ca4e49131" Name="SegmentationMode" Type="Reference(Typified) Not Null" ReferencedTable="5455dc3b-79f8-4c06-8cf0-64e63919ab4a">
		<Description>Image page segmentation mode in text recognition</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="e07ad515-201f-00fc-4000-0a8ca4e49131" Name="SegmentationModeID" Type="Int32 Not Null" ReferencedColumn="37507ad9-d1f0-4d39-8ae3-6e5a5936a730">
			<Description>Segmentation mode identifier</Description>
			<SchemeDefaultConstraint IsPermanent="true" ID="a826a222-08bf-4be7-a98b-164cc9d08d4f" Name="df_OcrRequests_SegmentationModeID" Value="1" />
		</SchemeReferencingColumn>
		<SchemeReferencingColumn ID="beecf999-870a-43dc-8f05-2edc0f16951f" Name="SegmentationModeName" Type="String(256) Not Null" ReferencedColumn="748bda68-c8b4-4e9e-ba52-14a8176332b2">
			<Description>Segmentation mode name</Description>
			<SchemeDefaultConstraint IsPermanent="true" ID="93f4b620-9ba7-4a01-b713-5486274643e6" Name="df_OcrRequests_SegmentationModeName" Value="$Enum_OcrSegmentationModes_AutoOsd" />
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="5b668109-75de-499f-ad47-957b3fe069c7" Name="DetectLanguages" Type="Boolean Not Null">
		<Description>Determine the set of possible languages ​​during the recognition process</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="58c28f0b-bc5c-4d7e-a996-282d8e8af293" Name="df_OcrRequests_DetectLanguages" Value="true" />
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="3b8c14eb-52f6-4476-903a-3da5f7f1e4c3" Name="ContentFile" Type="Reference(Typified) Null" ReferencedTable="dd716146-b177-4920-bc90-b1196b16347c" WithForeignKey="false">
		<Description>Reference to recognized file with content</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="3b8c14eb-52f6-0076-4000-0da5f7f1e4c3" Name="ContentFileID" Type="Guid Null" ReferencedColumn="dd716146-b177-0020-3100-01196b16347c">
			<Description>File identifier</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="5427cad0-570e-4825-91ad-3abd62122ceb" Name="MetadataFile" Type="Reference(Typified) Null" ReferencedTable="dd716146-b177-4920-bc90-b1196b16347c" WithForeignKey="false">
		<Description>Reference to recognized file with metadata</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="5427cad0-570e-0025-4000-0abd62122ceb" Name="MetadataFileID" Type="Guid Null" ReferencedColumn="dd716146-b177-0020-3100-01196b16347c">
			<Description>File identifier</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="e8b0c26a-a38e-4458-bfec-3fee0e124502" Name="IsMain" Type="Boolean Not Null">
		<Description>Sign that the request is main (basic)</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="2815a15e-5058-492c-9e81-6e45cab7b870" Name="df_OcrRequests_IsMain" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="afea8b96-cc8a-46b6-9215-1603ef162577" Name="Overwrite" Type="Boolean Not Null">
		<Description>Create a new file from a set of recognized image pages</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="f4b762fd-77da-4347-ac43-d59909b721d3" Name="df_OcrRequests_Overwrite" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="8175661c-a714-41da-bd04-a6411c7e1576" Name="DetectRotation" Type="Boolean Not Null">
		<Description>Detect the scew and rotation of an image page before recognition</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="3787cb12-f074-4b39-bd83-96f9ba9449cb" Name="df_OcrRequests_DetectRotation" Value="true" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="e675d6ef-becd-4cbd-a13b-b63e39226b5e" Name="DetectTables" Type="Boolean Not Null">
		<Description>Detect tables on image page on recognition</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="a9de510a-562f-4ac2-98db-d49b7d184317" Name="df_OcrRequests_DetectTables" Value="false" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="d64806e9-ef31-0033-5000-070b178cc5bc" Name="pk_OcrRequests">
		<SchemeIndexedColumn Column="d64806e9-ef31-0033-3100-070b178cc5bc" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="d64806e9-ef31-0033-7000-070b178cc5bc" Name="idx_OcrRequests_ID" IsClustered="true">
		<SchemeIndexedColumn Column="d64806e9-ef31-0133-4000-070b178cc5bc" />
	</SchemeIndex>
</SchemeTable>