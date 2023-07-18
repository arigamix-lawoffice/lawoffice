<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="fe5739f6-d64b-45f5-a3a3-75e999f721dd" Name="KrApprovalCommonInfoVirtual" Group="Kr" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="fe5739f6-d64b-00f5-2000-05e999f721dd" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="fe5739f6-d64b-01f5-4000-05e999f721dd" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="856765a4-88a8-4e5a-a23a-4e8d3ecc907a" Name="MainCardID" Type="Guid Null" />
	<SchemeComplexColumn ID="2fd9593a-7a88-4b33-8b9d-89d95fe2c2ca" Name="State" Type="Reference(Typified) Not Null" ReferencedTable="47107d7a-3a8c-47f0-b800-2a45da222ff4">
		<SchemeReferencingColumn IsPermanent="true" ID="5687bd4c-0617-4b62-b211-50b1e9ad70fe" Name="StateID" Type="Int16 Not Null" ReferencedColumn="502209b0-233f-4e1f-be01-35a50f53414c">
			<SchemeDefaultConstraint IsPermanent="true" ID="8b191cf5-4887-4c1e-93e1-a56be3b8e4a1" Name="df_KrApprovalCommonInfoVirtual_StateID" Value="0" />
		</SchemeReferencingColumn>
		<SchemeReferencingColumn ID="b725bed3-9d22-4c4e-b638-3f90544ec3d8" Name="StateName" Type="String(128) Not Null" ReferencedColumn="4c1a8dd7-72ed-4fc9-b559-b38ae30dccb9">
			<SchemeDefaultConstraint IsPermanent="true" ID="784063b2-756c-42c9-b447-0db86de1ee68" Name="df_KrApprovalCommonInfoVirtual_StateName" Value="$KrStates_Doc_Draft" />
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="dceae040-f9f6-494f-91f4-3b633b5e3f21" Name="CurrentApprovalStage" Type="Reference(Typified) Null" ReferencedTable="89d78d5c-f8dd-48e7-868c-88bbafe74257">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="dceae040-f9f6-004f-4000-0b633b5e3f21" Name="CurrentApprovalStageRowID" Type="Guid Null" ReferencedColumn="89d78d5c-f8dd-00e7-3100-08bbafe74257" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="d1a59a3d-739d-4eae-b2e5-655f8f74cf34" Name="ApprovedBy" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="1e451b06-db0a-4c38-af57-f114b1a1e529" Name="DisapprovedBy" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="972b63c0-e78d-4869-901e-8d4d7acb2f10" Name="AuthorComment" Type="String(Max) Null" />
	<SchemeComplexColumn ID="b2aaa575-47df-4c85-b947-1a7dc8bb43a7" Name="Author" Type="Reference(Typified) Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="b2aaa575-47df-0085-4000-0a7dc8bb43a7" Name="AuthorID" Type="Guid Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="567a49c0-2a0b-4715-bdf5-018ec66beb3b" Name="AuthorName" Type="String(128) Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964">
			<Description>Отображаемое имя пользователя.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="936c6442-3dff-4d75-b767-17b1251f3719" Name="StateChangedDateTimeUTC" Type="DateTime Null">
		<Description>Дата + время последнего изменения состояния согласования, null если согласование еще не запускалось</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="2f105d82-695e-46d7-87d8-fca61e49fcaa" Name="ProcessOwner" Type="Reference(Typified) Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<Description>Владелец маршрута.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="2f105d82-695e-00d7-4000-0ca61e49fcaa" Name="ProcessOwnerID" Type="Guid Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="86bc3401-7853-45e9-aa7f-10b9c9c9cfea" Name="ProcessOwnerName" Type="String(128) Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="fe5739f6-d64b-00f5-5000-05e999f721dd" Name="pk_KrApprovalCommonInfoVirtual" IsClustered="true">
		<SchemeIndexedColumn Column="fe5739f6-d64b-01f5-4000-05e999f721dd" />
	</SchemePrimaryKey>
</SchemeTable>