<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="83b4c03f-fdb8-4e11-bca4-02177dd4b3dc" Name="KrDocNumberRegularAutoAssignment" Group="Kr">
	<Description>Перечисление вариантов автоматического выделения номера для документа</Description>
	<SchemePhysicalColumn ID="7ef0f81c-6121-447c-9a2c-21bbdcaf3707" Name="ID" Type="Int32 Not Null" />
	<SchemePhysicalColumn ID="a769e235-b237-4f3a-be39-f1e7602fe9da" Name="Description" Type="String(128) Not Null" />
	<SchemePrimaryKey ID="6c1f3273-23a9-43c1-bfd1-564c0a1e7181" Name="pk_KrDocNumberRegularAutoAssignment">
		<SchemeIndexedColumn Column="7ef0f81c-6121-447c-9a2c-21bbdcaf3707" />
	</SchemePrimaryKey>
	<SchemeRecord>
		<ID ID="7ef0f81c-6121-447c-9a2c-21bbdcaf3707">0</ID>
		<Description ID="a769e235-b237-4f3a-be39-f1e7602fe9da">$Views_KrAutoAssigment_NotToAssign</Description>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="7ef0f81c-6121-447c-9a2c-21bbdcaf3707">1</ID>
		<Description ID="a769e235-b237-4f3a-be39-f1e7602fe9da">$Views_KrAutoAssigment_WhenCreating</Description>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="7ef0f81c-6121-447c-9a2c-21bbdcaf3707">2</ID>
		<Description ID="a769e235-b237-4f3a-be39-f1e7602fe9da">$Views_KrAutoAssigment_WhenSaving</Description>
	</SchemeRecord>
</SchemeTable>