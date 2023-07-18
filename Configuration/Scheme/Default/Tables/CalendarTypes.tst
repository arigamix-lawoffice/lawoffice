<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="c411ab46-1df7-4a76-97b5-d0d39fff656b" Name="CalendarTypes" Group="System" InstanceType="Cards" ContentType="Entries">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="c411ab46-1df7-0076-2000-00d39fff656b" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="c411ab46-1df7-0176-4000-00d39fff656b" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="bd4f4c54-1921-4890-b040-3a5e54a17d7d" Name="Caption" Type="String(255) Not Null" />
	<SchemePhysicalColumn ID="c9a2fd2d-d3cb-40f5-b7cc-66729586c094" Name="Description" Type="String(4000) Null" />
	<SchemeComplexColumn ID="61f17a5f-5394-41ab-84ce-af40bb0fc329" Name="CalcMethod" Type="Reference(Typified) Null" ReferencedTable="011f3246-c0f2-4d91-aaee-5129c6b83e15">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="61f17a5f-5394-00ab-4000-0f40bb0fc329" Name="CalcMethodID" Type="Guid Null" ReferencedColumn="011f3246-c0f2-0191-4000-0129c6b83e15" />
		<SchemeReferencingColumn ID="a7a4126c-baca-4d57-8425-f2d740706fe2" Name="CalcMethodName" Type="String(255) Null" ReferencedColumn="bd906dc7-dee2-49f7-99ab-301888285796" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="060a60dc-fdcd-46a9-99ad-e221189851dd" Name="HoursInDay" Type="Double Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="0eef081d-3df8-4db7-93c6-d81cef04502a" Name="df_CalendarTypes_HoursInDay" Value="0" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="f0063ef3-3cca-4bb8-98b1-a70f23e8e14d" Name="WorkDaysInWeek" Type="Int16 Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="be4a6442-7b7c-4f3d-be3b-4cb48c565091" Name="df_CalendarTypes_WorkDaysInWeek" Value="0" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="c411ab46-1df7-0076-5000-00d39fff656b" Name="pk_CalendarTypes" IsClustered="true">
		<SchemeIndexedColumn Column="c411ab46-1df7-0176-4000-00d39fff656b" />
	</SchemePrimaryKey>
</SchemeTable>