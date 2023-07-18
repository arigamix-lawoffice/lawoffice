<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="a396cc70-75bb-427b-ac42-4978fb5575ac" Name="SessionActivity" Group="System">
	<Description>Таблица для хранения признаков активности и последней активности сессии.
Дополняет таблицу Sessions.</Description>
	<SchemeComplexColumn ID="f297d7fe-9e54-4d54-b64b-3d73e31ad21b" Name="Session" Type="Reference(Typified) Not Null" ReferencedTable="bbd3d574-a33e-49fb-867d-db3c6811365e" WithForeignKey="false">
		<Description>Ссылка на сессию в таблице Sessions.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="f297d7fe-9e54-0054-4000-0d73e31ad21b" Name="SessionID" Type="Guid Not Null" ReferencedColumn="5100aae0-3958-4b1a-b135-57b6640ced19" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="b8c4be3d-3e47-418f-934c-0aba3e30d507" Name="IsActive" Type="Boolean Not Null">
		<Description>Признак того, что сессия является активной, т.е. обращения к ней не приведут к ошибкам.

Сессия может стать неактивной и перестать использовать лицензию, если лицензия конкурентная, сессия длительное время не использовалась и при этом не была закрыта. В этом случае при открытии новой сессии текущая сессия становится неактивной, а при обращении к текущей сессии будет запрошен возврат лицензии. Если лицензию не получилось вернуть (т.к. количество конкурентных лицензий недостаточно), то сессия закрывается.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="7568494d-91b1-425a-99ab-a93f218f5ad2" Name="LastActivity" Type="DateTime Not Null">
		<Description>Дата и время последнего запроса к серверу, определяющего активность клиента.</Description>
	</SchemePhysicalColumn>
</SchemeTable>