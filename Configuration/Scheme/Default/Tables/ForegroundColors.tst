<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="f22e70d5-17da-4e6a-8d41-17796e5f75d0" Name="ForegroundColors" Group="System" InstanceType="Cards" ContentType="Entries">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="f22e70d5-17da-006a-2000-07796e5f75d0" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="f22e70d5-17da-016a-4000-07796e5f75d0" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="c4cc039e-44fb-4654-a701-3410f28c44c1" Name="Color1" Type="Int32 Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="55a68429-d888-41fb-b867-768b37971eca" Name="df_ForegroundColors_Color1" Value="-23552" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="8c618188-b37d-4491-8409-3c4fcc98cc28" Name="Color2" Type="Int32 Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="fa6be3a9-2d49-41cc-8947-ba8eb62f27c7" Name="df_ForegroundColors_Color2" Value="-13668096" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="72a5755b-a545-4cbd-8831-5d7f81016eb3" Name="Color3" Type="Int32 Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="3e4ab17f-7370-4655-941c-64a95156d83e" Name="df_ForegroundColors_Color3" Value="-15899741" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="5f58f3bd-54db-4487-b5ce-69610d3c94df" Name="Color4" Type="Int32 Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="275d0977-e15d-4223-8a74-d4d311028348" Name="df_ForegroundColors_Color4" Value="-7536499" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="2c3aa21b-1d4a-4c24-ba34-37f229fe97be" Name="Color5" Type="Int32 Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="88885872-d840-492f-924c-091d12be76e5" Name="df_ForegroundColors_Color5" Value="-4456448" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="f22e70d5-17da-006a-5000-07796e5f75d0" Name="pk_ForegroundColors" IsClustered="true">
		<SchemeIndexedColumn Column="f22e70d5-17da-016a-4000-07796e5f75d0" />
	</SchemePrimaryKey>
</SchemeTable>