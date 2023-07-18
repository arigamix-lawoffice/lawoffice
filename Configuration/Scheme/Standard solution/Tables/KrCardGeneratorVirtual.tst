<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="1052a0bc-1a02-4fd4-9636-5dacd0acc436" Name="KrCardGeneratorVirtual" Group="Kr" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<Description>Параметры генерации тестовых карточек.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="1052a0bc-1a02-00d4-2000-0dacd0acc436" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="1052a0bc-1a02-01d4-4000-0dacd0acc436" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="5b16f272-54b3-4021-b8f0-4e4e9025b5f4" Name="UserCount" Type="Int16 Not Null">
		<Description>Количество создаваемый пользователей.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="7a3e6690-d4c6-42f9-af60-01ee0ff37098" Name="df_KrCardGeneratorVirtual_UserCount" Value="0" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="809aede1-85c9-4680-b39e-08d59587f3ee" Name="PartnerCount" Type="Int16 Not Null">
		<Description>Количество создаваемый контрагентов.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="1cff3599-d54b-4bd7-9d81-12c433a8ba91" Name="df_KrCardGeneratorVirtual_PartnerCount" Value="0" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="1052a0bc-1a02-00d4-5000-0dacd0acc436" Name="pk_KrCardGeneratorVirtual" IsClustered="true">
		<SchemeIndexedColumn Column="1052a0bc-1a02-01d4-4000-0dacd0acc436" />
	</SchemePrimaryKey>
</SchemeTable>