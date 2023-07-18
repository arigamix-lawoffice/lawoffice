<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="a6d84d24-0461-4c65-aa5d-f41ee300f081" ID="b9a84cff-8153-4a81-b9f3-832d59461596" Name="OcrRecognitionModes" Group="Ocr">
	<Description>Text recognition modes</Description>
	<SchemePhysicalColumn ID="64fa7d34-fdcf-4990-b5ad-2295ce153869" Name="ID" Type="Int32 Not Null">
		<Description>Mode identifier</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="3a709eb4-ee3e-4646-99c6-e4bbbd945cd8" Name="Name" Type="String(128) Not Null">
		<Description>Mode name</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="34963691-9b99-44ca-89ac-c3bb28e42c06" Name="Description" Type="String(256) Not Null">
		<Description>Mode description</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey ID="6d0f3493-a959-43fe-9c04-45ea0c5074bc" Name="pk_OcrRecognitionModes">
		<SchemeIndexedColumn Column="64fa7d34-fdcf-4990-b5ad-2295ce153869" />
	</SchemePrimaryKey>
	<SchemeUniqueKey ID="7deafcda-e624-4289-9b9d-4d9cae725c9d" Name="ndx_OcrRecognitionModes_Name">
		<SchemeIndexedColumn Column="3a709eb4-ee3e-4646-99c6-e4bbbd945cd8" />
	</SchemeUniqueKey>
	<SchemeRecord>
		<ID ID="64fa7d34-fdcf-4990-b5ad-2295ce153869">0</ID>
		<Name ID="3a709eb4-ee3e-4646-99c6-e4bbbd945cd8">$Enum_OcrRecognitionModes_OpticalRecognition</Name>
		<Description ID="34963691-9b99-44ca-89ac-c3bb28e42c06">$Enum_OcrRecognitionModes_OpticalRecognition_Description</Description>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="64fa7d34-fdcf-4990-b5ad-2295ce153869">1</ID>
		<Name ID="3a709eb4-ee3e-4646-99c6-e4bbbd945cd8">$Enum_OcrRecognitionModes_NeuralNetwork</Name>
		<Description ID="34963691-9b99-44ca-89ac-c3bb28e42c06">$Enum_OcrRecognitionModes_NeuralNetwork_Description</Description>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="64fa7d34-fdcf-4990-b5ad-2295ce153869">2</ID>
		<Name ID="3a709eb4-ee3e-4646-99c6-e4bbbd945cd8">$Enum_OcrRecognitionModes_OpticalRecognitionAndNeuralNetwork</Name>
		<Description ID="34963691-9b99-44ca-89ac-c3bb28e42c06">$Enum_OcrRecognitionModes_OpticalRecognitionAndNeuralNetwork_Description</Description>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="64fa7d34-fdcf-4990-b5ad-2295ce153869">3</ID>
		<Name ID="3a709eb4-ee3e-4646-99c6-e4bbbd945cd8">$Enum_OcrRecognitionModes_Default</Name>
		<Description ID="34963691-9b99-44ca-89ac-c3bb28e42c06">$Enum_OcrRecognitionModes_Default_Description</Description>
	</SchemeRecord>
</SchemeTable>