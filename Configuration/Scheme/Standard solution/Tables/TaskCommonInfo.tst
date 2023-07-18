<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="005962e7-65f1-4763-a0ef-b76751d26de3" Name="TaskCommonInfo" Group="Common" InstanceType="Tasks" ContentType="Entries">
	<Description>Общая информация для заданий.
Во всех случаях, когда в задании надо вывести некое описание текста задания, нужно использовать эту секцию. 
Также используется в представлении "Мои задания", чтобы выводить некий  текст - описание задания в табличку со списком заданий.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="005962e7-65f1-0063-2000-076751d26de3" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="5bfa9936-bb5a-4e8f-89a9-180bfd8f75f8">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="005962e7-65f1-0163-4000-076751d26de3" Name="ID" Type="Guid Not Null" ReferencedColumn="5bfa9936-bb5a-008f-3100-080bfd8f75f8" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="225b3890-5782-4afc-80a5-4824798cda15" Name="Info" Type="String(Max) Null">
		<Description>Текстовая информация о задании. Обычно выводится на форме задания и в представлении "Мои задания". Используется различными типами заданий.</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="3e920b6a-94aa-4691-9014-207d7ed01b27" Name="Kind" Type="Reference(Typified) Null" ReferencedTable="856068b1-0e78-4aa8-8e7a-4f53d91a7298" WithForeignKey="false">
		<Description>Вид задания или Null, если вид задания не задан или не доступен для текущего типа задания.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="3e920b6a-94aa-0091-4000-007d7ed01b27" Name="KindID" Type="Guid Null" ReferencedColumn="856068b1-0e78-01a8-4000-0f53d91a7298" />
		<SchemeReferencingColumn ID="029546e3-cc9c-4ca0-b23c-7f2943c0ce87" Name="KindCaption" Type="String(128) Null" ReferencedColumn="63d9110b-7628-4bf9-9dae-750c3035e48d">
			<Description>Название вида заданий.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="005962e7-65f1-0063-5000-076751d26de3" Name="pk_TaskCommonInfo" IsClustered="true">
		<SchemeIndexedColumn Column="005962e7-65f1-0163-4000-076751d26de3" />
	</SchemePrimaryKey>
</SchemeTable>