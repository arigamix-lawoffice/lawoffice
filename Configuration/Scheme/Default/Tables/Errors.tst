<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="754008b7-831b-44f9-9c58-99fa0334e62f" Name="Errors" Group="System" InstanceType="Cards" ContentType="Entries">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="754008b7-831b-00f9-2000-09fa0334e62f" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="754008b7-831b-01f9-4000-09fa0334e62f" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="cd409666-7a43-4e22-97dd-2334a880b3da" Name="Action" Type="Reference(Typified) Not Null" ReferencedTable="420a67fd-2ea0-4ccd-9c3f-6378c2fda2cc" WithForeignKey="false">
		<Description>Тип действия.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="cd409666-7a43-0022-4000-0334a880b3da" Name="ActionID" Type="Int16 Not Null" ReferencedColumn="e017b3c3-aa04-4a5a-8bab-d66a0496b1f6" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="f95a3b4f-223e-4c0e-983f-48adf3d5c55d" Name="Type" Type="Reference(Typified) Not Null" ReferencedTable="b0538ece-8468-4d0b-8b4e-5a1d43e024db" WithForeignKey="false">
		<Description>Тип карточки.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="f95a3b4f-223e-000e-4000-08adf3d5c55d" Name="TypeID" Type="Guid Not Null" ReferencedColumn="a628a864-c858-4200-a6b7-da78c8e6e1f4" />
		<SchemeReferencingColumn ID="afb59cd6-e6d3-4a58-a66f-c58d17b21a26" Name="TypeCaption" Type="String(128) Not Null" ReferencedColumn="0a02451e-2e06-4001-9138-b4805e641afa" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="ad3f30d7-6a39-4299-bdbe-2e36f2b5b72e" Name="Card" Type="Reference(Abstract) Not Null" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="ad3f30d7-6a39-0099-4000-0e36f2b5b72e" Name="CardID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
		<SchemePhysicalColumn ID="f20acab6-a09a-4ad1-b340-2a3bb6ca6f8a" Name="CardDigest" Type="String(128) Null">
			<Description>Краткое описание карточки или действия с карточкой.</Description>
		</SchemePhysicalColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="0f7a0917-68ab-4d46-b023-a365c5e7a92d" Name="Request" Type="BinaryJson Not Null">
		<Description>Сериализованный запрос на выполнение действий с карточкой.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="ef51ee7e-5ade-4752-912c-cc83d8171d7c" Name="df_Errors_Request" Value="{}" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="08d78055-71d5-40f6-9307-09c0f41f7d7a" Name="Category" Type="String(128) Null">
		<Description>Категория ошибки, обычно строка-алиас. Равно Null или пустой строке, если категория не задана.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="64502236-5372-45f4-9bf2-3c6e6b84510f" Name="Text" Type="String(440) Null">
		<Description>Дополнительное текстовое описание ошибки.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="7f9f1aa8-9255-4b7a-b7c2-dc41002b1cad" Name="Modified" Type="DateTime Not Null">
		<Description>Дата и время действия с карточкой.</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="9c7ddc4e-b310-40b8-b6b1-04161d68b5b4" Name="ModifiedBy" Type="Reference(Typified) Not Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3" WithForeignKey="false">
		<Description>Пользователь, который произвёл действие с карточкой.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="9c7ddc4e-b310-00b8-4000-04161d68b5b4" Name="ModifiedByID" Type="Guid Not Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="080e67a0-617b-4b71-92f8-0fee2462bbee" Name="ModifiedByName" Type="String(128) Not Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="754008b7-831b-00f9-5000-09fa0334e62f" Name="pk_Errors" IsClustered="true">
		<SchemeIndexedColumn Column="754008b7-831b-01f9-4000-09fa0334e62f" />
	</SchemePrimaryKey>
	<SchemeIndex ID="55365668-7872-4743-a1f3-06a29513882a" Name="ndx_Errors_ModifiedCategory" SupportsPostgreSql="false">
		<SchemeIndexedColumn Column="7f9f1aa8-9255-4b7a-b7c2-dc41002b1cad" />
		<SchemeIndexedColumn Column="08d78055-71d5-40f6-9307-09c0f41f7d7a" />
	</SchemeIndex>
	<SchemeIndex ID="40d0864f-629d-4a85-abc4-516bfca7d266" Name="ndx_Errors_ModifiedText" SupportsPostgreSql="false">
		<SchemeIndexedColumn Column="7f9f1aa8-9255-4b7a-b7c2-dc41002b1cad" />
		<SchemeIndexedColumn Column="64502236-5372-45f4-9bf2-3c6e6b84510f" />
	</SchemeIndex>
	<SchemeIndex ID="72647da2-66e9-4168-9fa8-442c97dc2cbf" Name="ndx_Errors_Modified" SupportsSqlServer="false">
		<SchemeIndexedColumn Column="7f9f1aa8-9255-4b7a-b7c2-dc41002b1cad" />
	</SchemeIndex>
	<SchemeIndex ID="fdf0d157-9c39-4f13-b3bf-7294bb7761a0" Name="ndx_Errors_Text" SupportsSqlServer="false" Type="GIN">
		<SchemeIndexedColumn Column="64502236-5372-45f4-9bf2-3c6e6b84510f">
			<Expression Dbms="PostgreSql">lower("Text") gin_trgm_ops</Expression>
		</SchemeIndexedColumn>
	</SchemeIndex>
	<SchemeIndex ID="f2b635ca-6312-4b6e-af33-68e28f251fe9" Name="ndx_Errors_Category" SupportsSqlServer="false" Type="GIN">
		<SchemeIndexedColumn Column="08d78055-71d5-40f6-9307-09c0f41f7d7a">
			<Expression Dbms="PostgreSql">"Category" gin_trgm_ops</Expression>
		</SchemeIndexedColumn>
	</SchemeIndex>
</SchemeTable>