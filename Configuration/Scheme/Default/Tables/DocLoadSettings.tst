<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="0dbef4e9-7bf7-4b8f-aab0-fa908bc30e6f" Name="DocLoadSettings" Group="System" InstanceType="Cards" ContentType="Entries">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="0dbef4e9-7bf7-008f-2000-0a908bc30e6f" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="0dbef4e9-7bf7-018f-4000-0a908bc30e6f" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="9be2a9b2-fd31-42d0-8fae-6e5e37119038" Name="InputPath" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="83cb894e-a9f4-435d-9b09-7a3f583423a8" Name="OutputPath" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="19ed929c-1fe8-4d5b-bd5f-e6ac34cb277b" Name="ErrorPath" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="a5e3b3fb-0ce5-4d0b-9bd9-d2b7941123f8" Name="ExcludeBarcodePage" Type="Boolean Not Null" />
	<SchemeComplexColumn ID="d23f319e-3768-4c68-a42e-4476c74bdd33" Name="DefaultBarcodeTable" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="d23f319e-3768-0068-4000-0476c74bdd33" Name="DefaultBarcodeTableID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
		<SchemePhysicalColumn ID="f6f30167-edb1-4fb0-b1aa-1dcdcbf9ac97" Name="DefaultBarcodeTableName" Type="String(Max) Not Null" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="0ed49413-11ed-4bd9-9804-15633c0c9cde" Name="DefaultBarcodeField" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="0ed49413-11ed-00d9-4000-05633c0c9cde" Name="DefaultBarcodeFieldID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
		<SchemePhysicalColumn ID="40fc2db2-9094-4772-a464-2eea10b43fc3" Name="DefaultBarcodeFieldName" Type="String(Max) Not Null" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="e66dbb43-88d9-4f99-872a-43e690f60ef4" Name="BarcodeWrite" Type="Reference(Typified) Not Null" ReferencedTable="60ad88cc-f913-48ce-96e1-0abf417da790">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="e66dbb43-88d9-0099-4000-03e690f60ef4" Name="BarcodeWriteID" Type="Int32 Not Null" ReferencedColumn="eee124bb-83cc-496a-af59-cead1dfeaa0b" />
		<SchemeReferencingColumn ID="e19407e9-4de4-4c2d-9576-b20d84aaf5eb" Name="BarcodeWriteName" Type="String(128) Not Null" ReferencedColumn="872e6a17-18e8-4b20-886d-40730ce2be03" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="d1acfd9c-e143-448e-a289-e05253611053" Name="ForceRender" Type="Boolean Not Null" />
	<SchemePhysicalColumn ID="d7834fbe-5ae8-43e5-aef1-9de9504e571c" Name="BarcodeFormat" Type="String(Max) Not Null" />
	<SchemePhysicalColumn ID="2680972d-40c7-4feb-8567-8cedb9b5fadc" Name="BarcodeSequence" Type="String(Max) Not Null" />
	<SchemePhysicalColumn ID="7d44f20a-a004-496a-9ad8-30050fefa73c" Name="DocFormatName" Type="String(Max) Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="17d95984-2b32-495e-91f8-32df8afbc655" Name="df_DocLoadSettings_DocFormatName" Value="$CardTypes_TypesNames_DocLoad_Filename" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="2742a952-a6ca-43ef-b23e-f7d97b42b8c2" Name="BarcodeLabel" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="20a52fce-f430-4341-87ed-8fae1ab84c8b" Name="df_DocLoadSettings_BarcodeLabel" Value="true" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="56fb6370-e77f-4d5a-aaae-dc4e87f95fbc" Name="BarcodeWidth" Type="Double Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="c6d669ec-11eb-43f6-b0f5-94fd92721ecf" Name="df_DocLoadSettings_BarcodeWidth" Value="290" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="43f54d5c-fc58-4d33-88a8-9f346285af62" Name="BarcodeHeight" Type="Double Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="8df04671-cdfa-4153-abb8-ee7425a7d84b" Name="df_DocLoadSettings_BarcodeHeight" Value="120" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="9f2c10ca-6a4e-4a69-aa88-f85f3d7cee57" Name="IsEnabled" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="cdf7ed39-3137-40ab-8d66-119b0a60a575" Name="df_DocLoadSettings_IsEnabled" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="a9bd0d54-39ea-44a8-b5c0-c43b2149fa39" Name="ShowHeader" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="9b2c9de0-d192-40d6-9d4e-f518db2069ab" Name="df_DocLoadSettings_ShowHeader" Value="true" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="73aba5ed-dd32-4905-bc01-fef6e5671f90" Name="OffsetWidth" Type="Double Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="ef60b03e-b436-4f1a-9413-2c5a873e7f92" Name="df_DocLoadSettings_OffsetWidth" Value="0" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="e213ea20-4ee8-465d-9951-f95bcbf69b57" Name="OffsetHeight" Type="Double Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="37343313-8b43-4d6a-aa07-784a750cc965" Name="df_DocLoadSettings_OffsetHeight" Value="0" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="5ef73169-c287-4d3c-bb15-7427eb6bee20" Name="StartScale" Type="Double Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="10b97d3f-d487-4357-9165-347abd4b039e" Name="df_DocLoadSettings_StartScale" Value="0.5" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="2be42779-8609-40a7-9b60-524f6fc6a2a7" Name="StopScale" Type="Double Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="38fee841-85bc-4903-8b68-57926f244c10" Name="df_DocLoadSettings_StopScale" Value="1.5" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="cafbe1bc-fbaf-403c-8e7b-724b4330fa15" Name="IncrementScale" Type="Double Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="69812afc-3a84-4a4b-8500-fb7eb3a6d92e" Name="df_DocLoadSettings_IncrementScale" Value="0.5" />
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="9ecaf2fc-6401-45b5-8e2c-8a4a5ab56609" Name="SessionUser" Type="Reference(Typified) Not Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<Description>Пользователь с настройками локализации и форматирования</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="9ecaf2fc-6401-00b5-4000-0a4a5ab56609" Name="SessionUserID" Type="Guid Not Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3">
			<SchemeDefaultConstraint IsPermanent="true" ID="46aa0801-bb81-4a8f-8b2a-de8cb2e8e1ab" Name="df_DocLoadSettings_SessionUserID" Value="11111111-1111-1111-1111-111111111111" />
		</SchemeReferencingColumn>
		<SchemeReferencingColumn ID="39419bad-2ce6-42ce-8f2f-eef333b70246" Name="SessionUserName" Type="String(128) Not Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964">
			<SchemeDefaultConstraint IsPermanent="true" ID="ceea63f0-dc9d-4ec2-810e-b593fe743ca0" Name="df_DocLoadSettings_SessionUserName" Value="System" />
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="0dbef4e9-7bf7-008f-5000-0a908bc30e6f" Name="pk_DocLoadSettings" IsClustered="true">
		<SchemeIndexedColumn Column="0dbef4e9-7bf7-018f-4000-0a908bc30e6f" />
	</SchemePrimaryKey>
</SchemeTable>