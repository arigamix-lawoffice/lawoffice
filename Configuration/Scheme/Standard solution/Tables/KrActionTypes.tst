<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="b401e639-9167-4ada-9d46-4982bcd92488" Name="KrActionTypes" Group="Kr">
	<SchemePhysicalColumn ID="a17e5f4b-8838-4ac9-8b7b-2aaa66e102d0" Name="ID" Type="Int32 Not Null" />
	<SchemePhysicalColumn ID="c83a64ff-9ffd-49fc-8851-d65fcb138dc7" Name="Name" Type="String(256) Not Null" />
	<SchemePhysicalColumn ID="71d48834-f91e-48a0-9a00-4d1a891e04cf" Name="EventType" Type="String(256) Not Null" />
	<SchemePrimaryKey ID="fd94869c-c379-429a-8594-ac0059f956ae" Name="pk_KrActionTypes">
		<SchemeIndexedColumn Column="a17e5f4b-8838-4ac9-8b7b-2aaa66e102d0" />
	</SchemePrimaryKey>
	<SchemeRecord>
		<ID ID="a17e5f4b-8838-4ac9-8b7b-2aaa66e102d0">0</ID>
		<Name ID="c83a64ff-9ffd-49fc-8851-d65fcb138dc7">$KrAction_NewCard</Name>
		<EventType ID="71d48834-f91e-48a0-9a00-4d1a891e04cf">NewCard</EventType>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="a17e5f4b-8838-4ac9-8b7b-2aaa66e102d0">1</ID>
		<Name ID="c83a64ff-9ffd-49fc-8851-d65fcb138dc7">$KrAction_BeforeStoreCard</Name>
		<EventType ID="71d48834-f91e-48a0-9a00-4d1a891e04cf">BeforeStoreCard</EventType>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="a17e5f4b-8838-4ac9-8b7b-2aaa66e102d0">2</ID>
		<Name ID="c83a64ff-9ffd-49fc-8851-d65fcb138dc7">$KrAction_StoreCard</Name>
		<EventType ID="71d48834-f91e-48a0-9a00-4d1a891e04cf">StoreCard</EventType>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="a17e5f4b-8838-4ac9-8b7b-2aaa66e102d0">3</ID>
		<Name ID="c83a64ff-9ffd-49fc-8851-d65fcb138dc7">$KrAction_BeforeCompleteTask</Name>
		<EventType ID="71d48834-f91e-48a0-9a00-4d1a891e04cf">BeforeCompleteTask</EventType>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="a17e5f4b-8838-4ac9-8b7b-2aaa66e102d0">4</ID>
		<Name ID="c83a64ff-9ffd-49fc-8851-d65fcb138dc7">$KrAction_CompleteTask</Name>
		<EventType ID="71d48834-f91e-48a0-9a00-4d1a891e04cf">CompleteTask</EventType>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="a17e5f4b-8838-4ac9-8b7b-2aaa66e102d0">5</ID>
		<Name ID="c83a64ff-9ffd-49fc-8851-d65fcb138dc7">$KrAction_BeforeNewTask</Name>
		<EventType ID="71d48834-f91e-48a0-9a00-4d1a891e04cf">BeforeNewTask</EventType>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="a17e5f4b-8838-4ac9-8b7b-2aaa66e102d0">6</ID>
		<Name ID="c83a64ff-9ffd-49fc-8851-d65fcb138dc7">$KrAction_NewTask</Name>
		<EventType ID="71d48834-f91e-48a0-9a00-4d1a891e04cf">NewTask</EventType>
	</SchemeRecord>
</SchemeTable>