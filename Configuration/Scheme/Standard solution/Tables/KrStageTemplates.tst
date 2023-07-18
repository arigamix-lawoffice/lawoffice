<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="5a33ac72-f6f5-4e5a-8d8c-4a94ed7bf324" Name="KrStageTemplates" Group="Kr" InstanceType="Cards" ContentType="Entries">
	<Description>Таблица с информацией по шаблонам этапов. Для карточки KrStageTemplate.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="5a33ac72-f6f5-005a-2000-0a94ed7bf324" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="5a33ac72-f6f5-015a-4000-0a94ed7bf324" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="65776ea1-97aa-48db-a170-8cb5d4eed2bc" Name="Name" Type="String(255) Not Null" />
	<SchemePhysicalColumn ID="c8c0153c-78cf-416c-8ce6-50fbcfbfc08d" Name="Order" Type="Int32 Not Null">
		<Description>Порядок этапа.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="d3dda225-7bfd-4658-b5f9-f44a95798ec6" Name="df_KrStageTemplates_Order" Value="0" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="fdf8d1d2-2e82-40d3-94b3-1da9ea31c635" Name="Description" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="e790c4ac-c470-4b27-a9d9-93c4d4ed572f" Name="CanChangeOrder" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="6ad66b42-4e2c-4ff3-8770-6e9a1160c391" Name="df_KrStageTemplates_CanChangeOrder" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="76067e88-b032-4a77-b2a4-8e6c4c1462a1" Name="IsStagesReadonly" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="b5bfa3bc-d8ff-4497-8ec4-68aed41c0e09" Name="df_KrStageTemplates_IsStagesReadonly" Value="true" />
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="b91d59a3-f6b5-4ad3-be57-b41ecdf52d4a" Name="GroupPosition" Type="Reference(Typified) Not Null" ReferencedTable="496c30f2-79d0-408a-8085-95b43d67a22b">
		<SchemeReferencingColumn IsPermanent="true" ID="271dd937-38e8-40f7-a0bd-22dee8ba81b6" Name="GroupPositionID" Type="Int32 Not Null" ReferencedColumn="2357fdc4-35e8-4200-b626-a21027473232" />
		<SchemeReferencingColumn ID="366c53e7-3f76-4647-a4ce-77f6304bd353" Name="GroupPositionName" Type="String(50) Not Null" ReferencedColumn="bf262a11-5e78-4752-9dc7-795ae179eca7" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="8a2c2598-5dd0-4735-816a-5f26ba584014" Name="SqlCondition" Type="String(Max) Null">
		<Description>sql запрос, который должен вернуть 0/1</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="dda43c9c-28a6-48b2-9ddf-eefc28cf63e4" Name="SourceCondition" Type="String(Max) Null">
		<Description>C# участок кода, который должен возвращать true/false</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="8e9750b9-0c51-434b-86d3-a7b472fe59cb" Name="SourceBefore" Type="String(Max) Null">
		<Description>C# код, выполняемый до condition</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="a176ee64-e7b4-4cd3-871c-c00e4a3f147e" Name="SourceAfter" Type="String(Max) Null">
		<Description>c# код, выполняемый после condition и подстановки этапов</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="8300c88d-475c-48ca-8171-3141fe89c905" Name="StageGroup" Type="Reference(Typified) Not Null" ReferencedTable="fde6b6e3-f7b6-467f-96e1-e2df41a22f05" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="8300c88d-475c-00ca-4000-0141fe89c905" Name="StageGroupID" Type="Guid Not Null" ReferencedColumn="fde6b6e3-f7b6-017f-4000-02df41a22f05">
			<SchemeDefaultConstraint IsPermanent="true" ID="3a09385f-f69d-4825-9b31-3b4a60738299" Name="df_KrStageTemplates_StageGroupID" Value="498cb3c3-23b5-469d-a9a3-05a62a098c92" />
		</SchemeReferencingColumn>
		<SchemeReferencingColumn ID="07029fb0-383e-45a2-9c14-5307907f491e" Name="StageGroupName" Type="String(255) Not Null" ReferencedColumn="fc8faabd-cc86-44b3-8430-1a0e816cea27">
			<SchemeDefaultConstraint IsPermanent="true" ID="06f11a7e-a3f3-4f31-88df-d3be2d57b6bd" Name="df_KrStageTemplates_StageGroupName" Value="$KrStageGroups_DefaultApprovalGroup" />
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="5a33ac72-f6f5-005a-5000-0a94ed7bf324" Name="pk_KrStageTemplates" IsClustered="true">
		<SchemeIndexedColumn Column="5a33ac72-f6f5-015a-4000-0a94ed7bf324" />
	</SchemePrimaryKey>
	<SchemeIndex ID="619c9bdf-2714-4eaf-bcc9-2eb276efb386" Name="ndx_KrStageTemplates_Name_619c9bdf" SupportsSqlServer="false" Type="GIN">
		<SchemeIndexedColumn Column="65776ea1-97aa-48db-a170-8cb5d4eed2bc" SortOrder="Descending">
			<Expression Dbms="PostgreSql">lower("Name") gin_trgm_ops</Expression>
		</SchemeIndexedColumn>
	</SchemeIndex>
	<SchemeIndex ID="d6613df2-f374-4b50-9bbe-5c80275f1c50" Name="ndx_KrStageTemplates_Name_d6613df2">
		<SchemeIndexedColumn Column="65776ea1-97aa-48db-a170-8cb5d4eed2bc" />
	</SchemeIndex>
	<SchemeIndex ID="1a52a586-7790-452b-82b9-7078076f12f9" Name="ndx_KrStageTemplates_Name" IsUnique="true">
		<SchemeIndexedColumn Column="65776ea1-97aa-48db-a170-8cb5d4eed2bc">
			<Expression Dbms="PostgreSql">lower("Name")</Expression>
		</SchemeIndexedColumn>
	</SchemeIndex>
	<SchemeIndex ID="cce415ad-fe22-4117-99f7-be2ff722f80a" Name="ndx_KrStageTemplates_StageGroupID">
		<SchemeIndexedColumn Column="8300c88d-475c-00ca-4000-0141fe89c905" />
		<SchemeIncludedColumn Column="5a33ac72-f6f5-015a-4000-0a94ed7bf324" />
	</SchemeIndex>
</SchemeTable>