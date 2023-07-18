<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="6c977939-bbfc-456f-a133-f1c2244e3cc3" Partition="29f90c69-c1ef-4cbf-b9d5-7fc91cd68c67">
	<SchemePhysicalColumn Partition="f3f630df-d649-43ce-9d5b-75048184a749" ID="8602ebe3-3a4f-4026-b519-b81ac3a5fddc" Name="ExternalUid" Type="Guid Null">
		<Description>Uid пользователя во внешней системе</Description>
	</SchemePhysicalColumn>
	<Predicate Dbms="SqlServer">[Login] IS NOT NULL AND [Login] &lt;&gt; N''</Predicate>
	<Predicate Dbms="PostgreSql">"Login" IS NOT NULL AND "Login" &lt;&gt; ''</Predicate>
</SchemeTable>