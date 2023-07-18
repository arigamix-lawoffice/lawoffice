<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="c55a7921-d82d-4f8b-b801-f1c693c4c2e3" Name="RoleDeputiesManagementDeputizedVirtual" Group="Roles" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Таблица, в которой перечислены замещаемые сотрудники и параметры замещения.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="c55a7921-d82d-008b-2000-01c693c4c2e3" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="c55a7921-d82d-018b-4000-01c693c4c2e3" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="c55a7921-d82d-008b-3100-01c693c4c2e3" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="29063885-2127-42c0-86d5-50a94773078c" Name="Deputized" Type="Reference(Typified) Not Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<Description>Замещаемый сотрудник.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="29063885-2127-00c0-4000-00a94773078c" Name="DeputizedID" Type="Guid Not Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="0a74107a-c6e8-40e9-af5d-5ede9280ea19" Name="DeputizedName" Type="String(128) Not Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964">
			<Description>Отображаемое имя пользователя.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="154d9547-6522-451a-9670-78244ac36c64" Name="MinDate" Type="Date Not Null">
		<Description>Начальная дата временного замещения или минимальное значение, если замещение постоянное.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="fe93e377-859a-4930-aa1e-8c0bfd670d02" Name="MaxDate" Type="Date Not Null">
		<Description>Конечная дата временного замещения или максимальное значение, если замещение постоянное.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="5bda4e28-63ef-44ca-9e86-648c92113995" Name="IsActive" Type="Boolean Not Null">
		<Description>Признак того, что замещение активно.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="afcb3525-f9e5-4742-bdc8-1f6a7de21cfd" Name="df_RoleDeputiesManagementDeputizedVirtual_IsActive" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="07020625-b544-4132-9ae6-52b8d8a96324" Name="IsEnabled" Type="Boolean Not Null">
		<Description>Признак того, что замещение доступно, т.е. может стать активным.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="9121a0c7-1f69-413a-8112-2d61ee693851" Name="df_RoleDeputiesManagementDeputizedVirtual_IsEnabled" Value="true" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="9da3ac60-9b9c-4fd1-a1d3-0c4142477cac" Name="IsPermanent" Type="Boolean Not Null">
		<Description>Признак постоянного замещения.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="d874280c-ca19-4727-9496-a49626c929c1" Name="df_RoleDeputiesManagementDeputizedVirtual_IsPermanent" Value="false" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="c55a7921-d82d-008b-5000-01c693c4c2e3" Name="pk_RoleDeputiesManagementDeputizedVirtual">
		<SchemeIndexedColumn Column="c55a7921-d82d-008b-3100-01c693c4c2e3" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="c55a7921-d82d-008b-7000-01c693c4c2e3" Name="idx_RoleDeputiesManagementDeputizedVirtual_ID" IsClustered="true">
		<SchemeIndexedColumn Column="c55a7921-d82d-018b-4000-01c693c4c2e3" />
	</SchemeIndex>
</SchemeTable>