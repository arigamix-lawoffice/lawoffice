<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="64a54b5b-bbd2-49ae-a378-1e8daa88c070" Name="KrApprovalHistoryVirtual" Group="Kr" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Сопоставление истории заданий с историей согласования (с учетом циклов согласования)</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="64a54b5b-bbd2-00ae-2000-0e8daa88c070" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="64a54b5b-bbd2-01ae-4000-0e8daa88c070" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="64a54b5b-bbd2-00ae-3100-0e8daa88c070" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="6bcd6960-9b1d-4cee-870c-a0923b7f502e" Name="Cycle" Type="Int16 Not Null">
		<Description>Порядковый номер цикла согласования</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="94bc538b-d364-4448-a782-d176a02022f5" Name="HistoryRecord" Type="Guid Null">
		<Description>Ссылка на задание согласования в общей истории заданий</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="6a1262f2-5651-416c-becc-8e444b4442c3" Name="Advisory" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="29616dfe-11f8-4e98-ad1d-4245db018294" Name="df_KrApprovalHistoryVirtual_Advisory" Value="false" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="64a54b5b-bbd2-00ae-5000-0e8daa88c070" Name="pk_KrApprovalHistoryVirtual">
		<SchemeIndexedColumn Column="64a54b5b-bbd2-00ae-3100-0e8daa88c070" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="64a54b5b-bbd2-00ae-7000-0e8daa88c070" Name="idx_KrApprovalHistoryVirtual_ID" IsClustered="true">
		<SchemeIndexedColumn Column="64a54b5b-bbd2-01ae-4000-0e8daa88c070" />
	</SchemeIndex>
</SchemeTable>