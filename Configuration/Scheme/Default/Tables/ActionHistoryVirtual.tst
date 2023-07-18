<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="d1ab792c-2758-4778-a3cf-d91191b3ec52" Name="ActionHistoryVirtual" Group="System" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<Description>История действий с карточкой для отображения в UI.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="d1ab792c-2758-0078-2000-091191b3ec52" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="d1ab792c-2758-0178-4000-091191b3ec52" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="4c5a66d6-e044-4a28-8615-fbde90bcc382" Name="RowID" Type="Guid Not Null">
		<Description>Идентификатор записи об истории карточки.</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="284873ea-e3d2-4975-b10b-d8f6ea0ed5af" Name="Action" Type="Reference(Typified) Not Null" ReferencedTable="420a67fd-2ea0-4ccd-9c3f-6378c2fda2cc" WithForeignKey="false">
		<Description>Тип действия.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="284873ea-e3d2-0075-4000-08f6ea0ed5af" Name="ActionID" Type="Int16 Not Null" ReferencedColumn="e017b3c3-aa04-4a5a-8bab-d66a0496b1f6" />
		<SchemeReferencingColumn ID="530be055-197a-4c30-bc88-d0ee647428db" Name="ActionName" Type="String(64) Not Null" ReferencedColumn="c452b453-1c95-498a-a03d-8566b504a96e" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="89e01ab3-5abe-4288-9017-4152fc2eb7b5" Name="Type" Type="Reference(Typified) Not Null" ReferencedTable="b0538ece-8468-4d0b-8b4e-5a1d43e024db" WithForeignKey="false">
		<Description>Тип карточки.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="89e01ab3-5abe-0088-4000-0152fc2eb7b5" Name="TypeID" Type="Guid Not Null" ReferencedColumn="a628a864-c858-4200-a6b7-da78c8e6e1f4" />
		<SchemeReferencingColumn ID="c6bb698c-38a4-418e-a1a1-2fc061f8bf65" Name="TypeCaption" Type="String(128) Not Null" ReferencedColumn="0a02451e-2e06-4001-9138-b4805e641afa" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="88bf6b48-1e7f-4feb-8210-6c5cd083165d" Name="Card" Type="Reference(Abstract) Not Null" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="88bf6b48-1e7f-00eb-4000-0c5cd083165d" Name="CardID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
		<SchemePhysicalColumn ID="92daea65-7eb6-41b1-a1a4-6aa3dddc99b7" Name="CardDigest" Type="String(128) Null">
			<Description>Краткое описание карточки или действия с карточкой.</Description>
		</SchemePhysicalColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="e2c5a6b6-e496-4621-8dcb-9470a8da0a05" Name="Request" Type="String(Max) Not Null">
		<Description>Текстовое представление запроса на действие с карточкой.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="0f648e95-3545-4156-9300-2c7289cfdb05" Name="RequestJson" Type="BinaryJson Not Null">
		<Description>Сериализованный запрос на выполнение действий с карточкой.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="0299aac6-4b1d-4745-ad89-d17fa65a0a28" Name="Description" Type="String(Max) Not Null">
		<Description>Текстовое описание изменений с карточкой.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="e40fc426-9347-4658-99de-ddbc05f88a00" Name="Category" Type="String(Max) Null">
		<Description>Категория ошибки, обычно строка-алиас. Равно Null или пустой строке, если категория не задана.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="093e2f48-02db-40a0-b1e3-e993fdcca3a7" Name="Text" Type="String(Max) Null">
		<Description>Дополнительное текстовое описание ошибки.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="9289a3be-466b-4b8b-86e7-84a8478ed775" Name="Modified" Type="DateTime Not Null">
		<Description>Дата и время действия с карточкой.</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="de8eb4b9-475b-4738-abf0-535521a1725a" Name="ModifiedBy" Type="Reference(Typified) Not Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3" WithForeignKey="false">
		<Description>Пользователь, который произвёл действие с карточкой.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="de8eb4b9-475b-0038-4000-035521a1725a" Name="ModifiedByID" Type="Guid Not Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="5fefcbfe-1ede-4cf8-9b6d-ec669e631732" Name="ModifiedByName" Type="String(128) Not Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="370c3b17-16a2-4587-9273-184596e4f040" Name="HasDetailsCard" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="5dfe571b-2067-4b34-a1f0-5258a2c70dee" Name="df_ActionHistoryVirtual_HasDetailsCard" Value="false" />
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="30a877d0-2c1d-45c3-906c-9eb2506cc2b8" Name="Session" Type="Reference(Typified) Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e" WithForeignKey="false">
		<Description>Сессия, в рамках которой выполнялось действие, или Null, если действие было выполнено вне пределов сессии
или в старых сборках платформы, не поддерживавших сессию в истории действий.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="30a877d0-2c1d-00c3-4000-0eb2506cc2b8" Name="SessionID" Type="Guid Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="d1ab792c-2758-0078-5000-091191b3ec52" Name="pk_ActionHistoryVirtual" IsClustered="true">
		<SchemeIndexedColumn Column="d1ab792c-2758-0178-4000-091191b3ec52" />
	</SchemePrimaryKey>
</SchemeTable>