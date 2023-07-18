<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="cae44467-e2c1-4638-8444-857575455f80" Name="KrRegistrationStageSettingsVirtual" Group="KrStageTypes" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="cae44467-e2c1-0038-2000-057575455f80" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="cae44467-e2c1-0138-4000-057575455f80" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="16a2d83d-d730-4a9e-8f9b-af75772e6fd0" Name="Comment" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="049972c0-0ad2-4f50-997f-7cda3691a2eb" Name="CanEditCard" Type="Boolean Not Null">
		<Description>Возможность редактировать файлы при наличии задания регистрации</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="01869e0e-eaec-4ceb-8d89-1afca3ed9ff8" Name="df_KrRegistrationStageSettingsVirtual_CanEditCard" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="e395a508-58d8-4d41-8ae8-287f7afe93f1" Name="CanEditFiles" Type="Boolean Not Null">
		<Description>Возможность редактировать файлы при наличии задания регистрации</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="72d6e053-78fe-4768-a664-ed1cc4e45ac6" Name="df_KrRegistrationStageSettingsVirtual_CanEditFiles" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="05bd5ce6-6fea-4b42-b4c5-6e62552c8a7b" Name="WithoutTask" Type="Boolean Null" />
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="cae44467-e2c1-0038-5000-057575455f80" Name="pk_KrRegistrationStageSettingsVirtual" IsClustered="true">
		<SchemeIndexedColumn Column="cae44467-e2c1-0138-4000-057575455f80" />
	</SchemePrimaryKey>
</SchemeTable>