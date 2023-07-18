<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="b965332c-296b-48e3-b16f-21a0cd8a6a25" Name="KrDocNumberRegistrationAutoAssignment" Group="Kr">
	<Description>Перечисление вариантов автоматического выделения номера при регистрации документа</Description>
	<SchemePhysicalColumn ID="dd4b2d82-5ed5-4765-9f07-37ae3ab7eb3f" Name="ID" Type="Int32 Not Null" />
	<SchemePhysicalColumn ID="4ef823f7-c84d-42dd-8689-ed3571b19c3c" Name="Description" Type="String(128) Not Null" />
	<SchemePrimaryKey ID="df811f76-0a94-4c7b-8fac-daf7810c4067" Name="pk_KrDocNumberRegistrationAutoAssignment">
		<SchemeIndexedColumn Column="dd4b2d82-5ed5-4765-9f07-37ae3ab7eb3f" />
	</SchemePrimaryKey>
	<SchemeRecord>
		<ID ID="dd4b2d82-5ed5-4765-9f07-37ae3ab7eb3f">0</ID>
		<Description ID="4ef823f7-c84d-42dd-8689-ed3571b19c3c">$Views_KrAutoAssigment_NotToAssign</Description>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="dd4b2d82-5ed5-4765-9f07-37ae3ab7eb3f">1</ID>
		<Description ID="4ef823f7-c84d-42dd-8689-ed3571b19c3c">$Views_KrAutoAssigment_Assign</Description>
	</SchemeRecord>
</SchemeTable>