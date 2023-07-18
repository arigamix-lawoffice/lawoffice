<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="f383bf09-2ec9-4fe5-aa50-f3b14898c976" Name="DialogCardStoreModes" Group="System">
	<SchemePhysicalColumn ID="c3ebd27e-4fd3-40d9-9bed-13716ba05342" Name="ID" Type="Int32 Not Null" />
	<SchemePhysicalColumn ID="a0c0f93e-a43c-4949-9216-e3b1f8de1b3a" Name="Name" Type="String(Max) Not Null" />
	<SchemePrimaryKey ID="99d57ee8-b3fc-4c00-85e2-e463ad9a4a30" Name="pk_DialogCardStoreModes">
		<SchemeIndexedColumn Column="c3ebd27e-4fd3-40d9-9bed-13716ba05342" />
	</SchemePrimaryKey>
	<SchemeRecord>
		<ID ID="c3ebd27e-4fd3-40d9-9bed-13716ba05342">0</ID>
		<Name ID="a0c0f93e-a43c-4949-9216-e3b1f8de1b3a">$DialogCardStoreModes_StoreIntoInfo</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="c3ebd27e-4fd3-40d9-9bed-13716ba05342">1</ID>
		<Name ID="a0c0f93e-a43c-4949-9216-e3b1f8de1b3a">$DialogCardStoreModes_StoreIntoSettings</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="c3ebd27e-4fd3-40d9-9bed-13716ba05342">2</ID>
		<Name ID="a0c0f93e-a43c-4949-9216-e3b1f8de1b3a">$DialogCardStoreModes_StoreAsCard</Name>
	</SchemeRecord>
</SchemeTable>