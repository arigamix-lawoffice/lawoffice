<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="60ad88cc-f913-48ce-96e1-0abf417da790" Name="BarcodeTypes" Group="System">
	<Description>Типы штрих-кодов</Description>
	<SchemePhysicalColumn ID="eee124bb-83cc-496a-af59-cead1dfeaa0b" Name="ID" Type="Int32 Not Null" />
	<SchemePhysicalColumn ID="872e6a17-18e8-4b20-886d-40730ce2be03" Name="Name" Type="String(128) Not Null" />
	<SchemePhysicalColumn ID="027eb375-cf7f-401d-b3b6-b6b251fd8c98" Name="CanPrint" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="02ec28b7-1fa0-424e-b1ae-8b6b53ef9199" Name="df_BarcodeTypes_CanPrint" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="b6ad77be-b59d-4746-8f38-ad94e0271170" Name="CanScan" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="1aaec513-26f6-4cdd-b99a-22c0bf47010f" Name="df_BarcodeTypes_CanScan" Value="true" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey ID="ac46a388-98ce-4dd6-a8a2-1af73b225b66" Name="pk_BarcodeTypes">
		<SchemeIndexedColumn Column="eee124bb-83cc-496a-af59-cead1dfeaa0b" />
	</SchemePrimaryKey>
	<SchemeRecord>
		<ID ID="eee124bb-83cc-496a-af59-cead1dfeaa0b">1</ID>
		<Name ID="872e6a17-18e8-4b20-886d-40730ce2be03">AZTEC</Name>
		<CanPrint ID="027eb375-cf7f-401d-b3b6-b6b251fd8c98">false</CanPrint>
		<CanScan ID="b6ad77be-b59d-4746-8f38-ad94e0271170">true</CanScan>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="eee124bb-83cc-496a-af59-cead1dfeaa0b">2</ID>
		<Name ID="872e6a17-18e8-4b20-886d-40730ce2be03">CODABAR</Name>
		<CanPrint ID="027eb375-cf7f-401d-b3b6-b6b251fd8c98">true</CanPrint>
		<CanScan ID="b6ad77be-b59d-4746-8f38-ad94e0271170">true</CanScan>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="eee124bb-83cc-496a-af59-cead1dfeaa0b">4</ID>
		<Name ID="872e6a17-18e8-4b20-886d-40730ce2be03">CODE_93</Name>
		<CanPrint ID="027eb375-cf7f-401d-b3b6-b6b251fd8c98">true</CanPrint>
		<CanScan ID="b6ad77be-b59d-4746-8f38-ad94e0271170">true</CanScan>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="eee124bb-83cc-496a-af59-cead1dfeaa0b">8</ID>
		<Name ID="872e6a17-18e8-4b20-886d-40730ce2be03">EAN_13</Name>
		<CanPrint ID="027eb375-cf7f-401d-b3b6-b6b251fd8c98">true</CanPrint>
		<CanScan ID="b6ad77be-b59d-4746-8f38-ad94e0271170">true</CanScan>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="eee124bb-83cc-496a-af59-cead1dfeaa0b">16</ID>
		<Name ID="872e6a17-18e8-4b20-886d-40730ce2be03">UPC_E</Name>
		<CanPrint ID="027eb375-cf7f-401d-b3b6-b6b251fd8c98">true</CanPrint>
		<CanScan ID="b6ad77be-b59d-4746-8f38-ad94e0271170">true</CanScan>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="eee124bb-83cc-496a-af59-cead1dfeaa0b">3</ID>
		<Name ID="872e6a17-18e8-4b20-886d-40730ce2be03">CODE_39</Name>
		<CanPrint ID="027eb375-cf7f-401d-b3b6-b6b251fd8c98">true</CanPrint>
		<CanScan ID="b6ad77be-b59d-4746-8f38-ad94e0271170">true</CanScan>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="eee124bb-83cc-496a-af59-cead1dfeaa0b">5</ID>
		<Name ID="872e6a17-18e8-4b20-886d-40730ce2be03">CODE_128</Name>
		<CanPrint ID="027eb375-cf7f-401d-b3b6-b6b251fd8c98">true</CanPrint>
		<CanScan ID="b6ad77be-b59d-4746-8f38-ad94e0271170">true</CanScan>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="eee124bb-83cc-496a-af59-cead1dfeaa0b">6</ID>
		<Name ID="872e6a17-18e8-4b20-886d-40730ce2be03">DATA_MATRIX</Name>
		<CanPrint ID="027eb375-cf7f-401d-b3b6-b6b251fd8c98">false</CanPrint>
		<CanScan ID="b6ad77be-b59d-4746-8f38-ad94e0271170">true</CanScan>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="eee124bb-83cc-496a-af59-cead1dfeaa0b">7</ID>
		<Name ID="872e6a17-18e8-4b20-886d-40730ce2be03">EAN_8</Name>
		<CanPrint ID="027eb375-cf7f-401d-b3b6-b6b251fd8c98">true</CanPrint>
		<CanScan ID="b6ad77be-b59d-4746-8f38-ad94e0271170">true</CanScan>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="eee124bb-83cc-496a-af59-cead1dfeaa0b">9</ID>
		<Name ID="872e6a17-18e8-4b20-886d-40730ce2be03">ITF</Name>
		<CanPrint ID="027eb375-cf7f-401d-b3b6-b6b251fd8c98">true</CanPrint>
		<CanScan ID="b6ad77be-b59d-4746-8f38-ad94e0271170">true</CanScan>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="eee124bb-83cc-496a-af59-cead1dfeaa0b">10</ID>
		<Name ID="872e6a17-18e8-4b20-886d-40730ce2be03">MAXICODE</Name>
		<CanPrint ID="027eb375-cf7f-401d-b3b6-b6b251fd8c98">false</CanPrint>
		<CanScan ID="b6ad77be-b59d-4746-8f38-ad94e0271170">true</CanScan>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="eee124bb-83cc-496a-af59-cead1dfeaa0b">11</ID>
		<Name ID="872e6a17-18e8-4b20-886d-40730ce2be03">PDF_417</Name>
		<CanPrint ID="027eb375-cf7f-401d-b3b6-b6b251fd8c98">false</CanPrint>
		<CanScan ID="b6ad77be-b59d-4746-8f38-ad94e0271170">true</CanScan>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="eee124bb-83cc-496a-af59-cead1dfeaa0b">12</ID>
		<Name ID="872e6a17-18e8-4b20-886d-40730ce2be03">QR_CODE</Name>
		<CanPrint ID="027eb375-cf7f-401d-b3b6-b6b251fd8c98">false</CanPrint>
		<CanScan ID="b6ad77be-b59d-4746-8f38-ad94e0271170">true</CanScan>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="eee124bb-83cc-496a-af59-cead1dfeaa0b">13</ID>
		<Name ID="872e6a17-18e8-4b20-886d-40730ce2be03">RSS_14</Name>
		<CanPrint ID="027eb375-cf7f-401d-b3b6-b6b251fd8c98">false</CanPrint>
		<CanScan ID="b6ad77be-b59d-4746-8f38-ad94e0271170">true</CanScan>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="eee124bb-83cc-496a-af59-cead1dfeaa0b">14</ID>
		<Name ID="872e6a17-18e8-4b20-886d-40730ce2be03">RSS_EXPANDED</Name>
		<CanPrint ID="027eb375-cf7f-401d-b3b6-b6b251fd8c98">false</CanPrint>
		<CanScan ID="b6ad77be-b59d-4746-8f38-ad94e0271170">true</CanScan>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="eee124bb-83cc-496a-af59-cead1dfeaa0b">15</ID>
		<Name ID="872e6a17-18e8-4b20-886d-40730ce2be03">UPC_A</Name>
		<CanPrint ID="027eb375-cf7f-401d-b3b6-b6b251fd8c98">true</CanPrint>
		<CanScan ID="b6ad77be-b59d-4746-8f38-ad94e0271170">true</CanScan>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="eee124bb-83cc-496a-af59-cead1dfeaa0b">17</ID>
		<Name ID="872e6a17-18e8-4b20-886d-40730ce2be03">All_1D</Name>
		<CanPrint ID="027eb375-cf7f-401d-b3b6-b6b251fd8c98">false</CanPrint>
		<CanScan ID="b6ad77be-b59d-4746-8f38-ad94e0271170">true</CanScan>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="eee124bb-83cc-496a-af59-cead1dfeaa0b">18</ID>
		<Name ID="872e6a17-18e8-4b20-886d-40730ce2be03">UPC_EAN_EXTENSION</Name>
		<CanPrint ID="027eb375-cf7f-401d-b3b6-b6b251fd8c98">false</CanPrint>
		<CanScan ID="b6ad77be-b59d-4746-8f38-ad94e0271170">true</CanScan>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="eee124bb-83cc-496a-af59-cead1dfeaa0b">19</ID>
		<Name ID="872e6a17-18e8-4b20-886d-40730ce2be03">MSI</Name>
		<CanPrint ID="027eb375-cf7f-401d-b3b6-b6b251fd8c98">true</CanPrint>
		<CanScan ID="b6ad77be-b59d-4746-8f38-ad94e0271170">true</CanScan>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="eee124bb-83cc-496a-af59-cead1dfeaa0b">20</ID>
		<Name ID="872e6a17-18e8-4b20-886d-40730ce2be03">PLESSEY</Name>
		<CanPrint ID="027eb375-cf7f-401d-b3b6-b6b251fd8c98">true</CanPrint>
		<CanScan ID="b6ad77be-b59d-4746-8f38-ad94e0271170">true</CanScan>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="eee124bb-83cc-496a-af59-cead1dfeaa0b">21</ID>
		<Name ID="872e6a17-18e8-4b20-886d-40730ce2be03">IMB</Name>
		<CanPrint ID="027eb375-cf7f-401d-b3b6-b6b251fd8c98">false</CanPrint>
		<CanScan ID="b6ad77be-b59d-4746-8f38-ad94e0271170">true</CanScan>
	</SchemeRecord>
</SchemeTable>