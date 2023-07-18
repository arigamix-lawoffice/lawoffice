<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="07d45e20-a501-4e3b-a246-e548a74d0730" Name="KrApprovalHistory" Group="Kr" InstanceType="Cards" ContentType="Collections">
	<Description>Сопоставление истории заданий с историей согласования (с учетом циклов согласования)</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="07d45e20-a501-003b-2000-0548a74d0730" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="07d45e20-a501-013b-4000-0548a74d0730" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="07d45e20-a501-003b-3100-0548a74d0730" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="3b63d1e8-03d0-4779-b093-426bbb631046" Name="Cycle" Type="Int16 Not Null">
		<Description>Порядковый номер цикла согласования</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="4c0f47b7-0b20-4731-8246-fa8bd68120b8" Name="HistoryRecord" Type="Guid Null">
		<Description>Ссылка на задание согласования в общей истории заданий</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="94e7a942-80b7-4cf5-8574-f75650ccf66f" Name="Advisory" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="3afae405-8c31-47a0-ac9f-115328ef9434" Name="df_KrApprovalHistory_Advisory" Value="false" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="07d45e20-a501-003b-5000-0548a74d0730" Name="pk_KrApprovalHistory">
		<SchemeIndexedColumn Column="07d45e20-a501-003b-3100-0548a74d0730" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="07d45e20-a501-003b-7000-0548a74d0730" Name="idx_KrApprovalHistory_ID" IsClustered="true">
		<SchemeIndexedColumn Column="07d45e20-a501-013b-4000-0548a74d0730" />
	</SchemeIndex>
	<SchemeIndex ID="ee5d52bf-3324-4c7b-a0e8-d4676f52eb2b" Name="ndx_KrApprovalHistory_HistoryRecord">
		<SchemeIndexedColumn Column="4c0f47b7-0b20-4731-8246-fa8bd68120b8" />
		<SchemeIncludedColumn Column="3b63d1e8-03d0-4779-b093-426bbb631046" />
	</SchemeIndex>
</SchemeTable>