<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="e490c196-41b3-489b-8425-ee36a0119f64" Name="KrCommentsInfoVirtual" Group="KrStageTypes" IsVirtual="true" InstanceType="Tasks" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="e490c196-41b3-009b-2000-0e36a0119f64" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="5bfa9936-bb5a-4e8f-89a9-180bfd8f75f8">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="e490c196-41b3-019b-4000-0e36a0119f64" Name="ID" Type="Guid Not Null" ReferencedColumn="5bfa9936-bb5a-008f-3100-080bfd8f75f8" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="e490c196-41b3-009b-3100-0e36a0119f64" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="2c8eaf8a-21b2-443c-ba5d-042f7d35f584" Name="QuestionShort" Type="String(32) Null" />
	<SchemePhysicalColumn ID="27a24263-917c-488c-9eb8-30b999d6375d" Name="AnswerShort" Type="String(32) Null" />
	<SchemePhysicalColumn ID="7685401b-8e05-4af6-bdb4-0a97516c8968" Name="CommentatorNameShort" Type="String(32) Null" />
	<SchemePhysicalColumn ID="b1d8947e-1af5-4180-b9f7-4f9912121781" Name="QuestionFull" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="560ff9ae-e3e9-4bf9-b1a8-d12c462c0a68" Name="AnswerFull" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="9ff30481-4558-4f7b-b15d-5f4a9bf73fdc" Name="CommentatorNameFull" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="e093ee47-429f-4d1b-ac7e-5d5eb5504195" Name="Completed" Type="DateTime Null">
		<Description>Время завершения задания комментирования</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="e490c196-41b3-009b-5000-0e36a0119f64" Name="pk_KrCommentsInfoVirtual">
		<SchemeIndexedColumn Column="e490c196-41b3-009b-3100-0e36a0119f64" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="e490c196-41b3-009b-7000-0e36a0119f64" Name="idx_KrCommentsInfoVirtual_ID" IsClustered="true">
		<SchemeIndexedColumn Column="e490c196-41b3-019b-4000-0e36a0119f64" />
	</SchemeIndex>
</SchemeTable>