<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="87619627-44a5-4f67-af9a-8f5736538f51" Name="KrRouteSettings" Group="Kr" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="87619627-44a5-0067-2000-0f5736538f51" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="87619627-44a5-0167-4000-0f5736538f51" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="b2157a1a-76b3-4f6c-b2ae-05c320f895b9" Name="AllowedRegistration" Type="Boolean Not Null">
		<Description>Признак, по которому проверяется используется ли регистрация.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="3ebe6f6c-205e-421a-85e6-10e7973936f7" Name="df_KrRouteSettings_AllowedRegistration" Value="false" />
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="a7222cd9-acdf-476a-8d88-8ed5bb76ec83" Name="RouteMode" Type="Reference(Typified) Not Null" ReferencedTable="01c6933a-204d-490e-a6db-fc69345c7e32">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="a7222cd9-acdf-006a-4000-0ed5bb76ec83" Name="RouteModeID" Type="Int16 Not Null" ReferencedColumn="287cc66f-012e-48f2-b7cf-f2d890a4997c" />
		<SchemeReferencingColumn ID="14ae9b32-a05c-43e9-ae12-0e8e045d68ac" Name="RouteModeName" Type="String(128) Not Null" ReferencedColumn="3dc935a0-3529-4960-8285-d9954bb5f9e2" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="87619627-44a5-0067-5000-0f5736538f51" Name="pk_KrRouteSettings" IsClustered="true">
		<SchemeIndexedColumn Column="87619627-44a5-0167-4000-0f5736538f51" />
	</SchemePrimaryKey>
</SchemeTable>