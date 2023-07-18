<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="eca29bb9-3085-4556-b19a-6015cbc8fb25" Name="SignatureProfiles" Group="System">
	<Description>Профили цифровой подписи</Description>
	<SchemePhysicalColumn ID="8c01d076-b862-4d75-852c-453efccfe590" Name="ID" Type="Int16 Not Null">
		<Description>Идентификатор профиля подписи</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="10a5a04e-258d-41d3-b450-9057a6f39ebd" Name="Name" Type="String(128) Not Null">
		<Description>Название профиля подписи</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey ID="fb49a779-24fb-47bb-ad8e-8a43ccf551a1" Name="pk_SignatureProfiles">
		<SchemeIndexedColumn Column="8c01d076-b862-4d75-852c-453efccfe590" />
	</SchemePrimaryKey>
	<SchemeRecord>
		<ID ID="8c01d076-b862-4d75-852c-453efccfe590">0</ID>
		<Name ID="10a5a04e-258d-41d3-b450-9057a6f39ebd">$Enum_Signature_Profiles_None</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="8c01d076-b862-4d75-852c-453efccfe590">1</ID>
		<Name ID="10a5a04e-258d-41d3-b450-9057a6f39ebd">$Enum_Signature_Profiles_BES</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="8c01d076-b862-4d75-852c-453efccfe590">3</ID>
		<Name ID="10a5a04e-258d-41d3-b450-9057a6f39ebd">$Enum_Signature_Profiles_T</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="8c01d076-b862-4d75-852c-453efccfe590">4</ID>
		<Name ID="10a5a04e-258d-41d3-b450-9057a6f39ebd">$Enum_Signature_Profiles_C</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="8c01d076-b862-4d75-852c-453efccfe590">5</ID>
		<Name ID="10a5a04e-258d-41d3-b450-9057a6f39ebd">$Enum_Signature_Profiles_XL</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="8c01d076-b862-4d75-852c-453efccfe590">6</ID>
		<Name ID="10a5a04e-258d-41d3-b450-9057a6f39ebd">$Enum_Signature_Profiles_X_Type1</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="8c01d076-b862-4d75-852c-453efccfe590">7</ID>
		<Name ID="10a5a04e-258d-41d3-b450-9057a6f39ebd">$Enum_Signature_Profiles_X_Type2</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="8c01d076-b862-4d75-852c-453efccfe590">8</ID>
		<Name ID="10a5a04e-258d-41d3-b450-9057a6f39ebd">$Enum_Signature_Profiles_XL_Type1</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="8c01d076-b862-4d75-852c-453efccfe590">9</ID>
		<Name ID="10a5a04e-258d-41d3-b450-9057a6f39ebd">$Enum_Signature_Profiles_XL_Type2</Name>
	</SchemeRecord>
</SchemeTable>