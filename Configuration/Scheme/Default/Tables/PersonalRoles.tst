<?xml version="1.0" encoding="utf-8"?>
<SchemeTable IsSystem="true" ID="6c977939-bbfc-456f-a133-f1c2244e3cc3" Name="PersonalRoles" Group="Roles" InstanceType="Cards" ContentType="Entries">
	<Description>Employees.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="6c977939-bbfc-006f-2000-01c2244e3cc3" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="6c977939-bbfc-016f-4000-01c2244e3cc3" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="1782f76a-4743-4aa4-920c-7edaee860964" Name="Name" Type="String(128) Not Null">
		<Description>Отображаемое имя пользователя.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="e89b6dc3-7932-4d74-a99f-91b402029536" Name="FullName" Type="String(256) Not Null">
		<Description>Полное имя пользователя. Составляется из полей LastName, FirstName и SecondName.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="13f4845d-0125-44cf-9ca3-dac793d881e5" Name="LastName" Type="String(64) Null">
		<Description>Фамилия.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="c969e755-cb4a-4414-bd35-d2541e656606" Name="FirstName" Type="String(64) Null">
		<Description>Имя.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="ea756075-b3ab-40df-89fc-a0e196b1c2a1" Name="MiddleName" Type="String(64) Null">
		<Description>Отчество.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="6ca55e0c-c14c-465f-8fdc-f585ae35df09" Name="Position" Type="String(256) Null">
		<Description>Должность.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="e88d2f12-ef36-4b7d-b588-673d9cfdd12a" Name="BirthDate" Type="Date Null">
		<Description>Дата рождения.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="e32de09d-a5dd-4de0-af63-5a9565cda555" Name="Email" Type="String(128) Null">
		<Description>Адрес электронной почты.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="ea4fe04c-048e-4655-83f0-9a154da8eb34" Name="Fax" Type="String(128) Null">
		<Description>Факс.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="4f4e4fe1-c9cf-4070-a4aa-f5a6d4aeca78" Name="Phone" Type="String(64) Null">
		<Description>Контактный телефон.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="808715d0-565a-46f7-8246-af4204c5bd29" Name="MobilePhone" Type="String(64) Null">
		<Description>Мобильный телефон.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="17fae6da-858d-4baa-ba6b-5d423b06f81b" Name="HomePhone" Type="String(64) Null">
		<Description>Домашний телефон.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="188210b1-93b0-4088-b9b7-1c5749c72f66" Name="IPPhone" Type="String(64) Null">
		<Description>IP-телефон.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="c05ae347-af13-468e-aeda-7c6b541c06b6" Name="Login" Type="String(256) Null">
		<Description>Логин пользователя или имя доменного аккаунта.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="27568b38-af0a-4071-97c6-c217ffb71396" Name="PasswordKey" Type="Binary(64) Null">
		<Description>Ключ, используемый для вычисления хеша от пароля пользователя для пользователя Tessa.
Для хеширования используется функция HMAC-SHA256, рекомендуемый размер ключа равен 64 байт.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="fd567104-cff3-4c99-a464-5aa02a43660d" Name="PasswordHash" Type="Binary(32) Null">
		<Description>Хеш от пароля для пользователя Tessa.
Для хеширования используется функция HMAC-SHA256, размер хеша в которой 256 бит или 32 байта.</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="fe3406b3-0aba-4d1d-a917-751af37fa17c" Name="AccessLevel" Type="Reference(Typified) Not Null" ReferencedTable="648381d6-8647-4ec6-87a4-3cbd6bae380c">
		<Description>Уровень доступа сотрудника.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="fe3406b3-0aba-001d-4000-051af37fa17c" Name="AccessLevelID" Type="Int16 Not Null" ReferencedColumn="5c20848a-0f1c-49ea-b6c1-454b0702295f">
			<SchemeDefaultConstraint IsPermanent="true" ID="5c924eec-a23a-4e97-8228-c4c2c72e43a9" Name="df_PersonalRoles_AccessLevelID" Value="0" />
		</SchemeReferencingColumn>
		<SchemeReferencingColumn ID="8ede559a-b9b5-4779-a515-f12968421709" Name="AccessLevelName" Type="String(128) Not Null" ReferencedColumn="6df97497-8f88-42ac-a445-d03ca67ed96b">
			<SchemeDefaultConstraint IsPermanent="true" ID="2e89a846-8450-4fde-9412-a3954ee7a9b2" Name="df_PersonalRoles_AccessLevelName" Value="$Enum_AccessLevels_Regular" />
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="668b47c4-f8a7-48d3-a9c7-03b3d0eb9bba" Name="LoginType" Type="Reference(Typified) Not Null" ReferencedTable="44a94501-a954-4ab1-a7f8-47eebb2f869b">
		<Description>Тип входа сотрудника в систему.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="668b47c4-f8a7-00d3-4000-03b3d0eb9bba" Name="LoginTypeID" Type="Int16 Not Null" ReferencedColumn="19e48b5c-b2fc-4f2a-b36d-90db3f3ae10e">
			<SchemeDefaultConstraint IsPermanent="true" ID="619b71a3-d7b2-4cf4-9209-a64e5e3645de" Name="df_PersonalRoles_LoginTypeID" Value="2" />
		</SchemeReferencingColumn>
		<SchemeReferencingColumn ID="c491273b-e2c8-4826-a3fb-57e400db6198" Name="LoginTypeName" Type="String(128) Not Null" ReferencedColumn="df05a434-d285-4608-b1c8-361ef4356773">
			<SchemeDefaultConstraint IsPermanent="true" ID="8a1bb535-ba3b-4615-8ec8-239c37b41081" Name="df_PersonalRoles_LoginTypeName" Value="$Enum_LoginTypes_Windows" />
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="4d1eac5b-1b41-4bff-bbe3-29765f0b9012" Name="Security" Type="BinaryJson Null">
		<Description>Объект UserSecurityObject, сериализованный в JSON, содержит информацию по предыдущим попыткам входа и по ранее заданным паролям.
Поле можно сбросить Null, чтобы очистить такую информацию, при следующем входе в систему поле будет заполнено.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="c0f21cf2-fee1-4f95-afdb-41442d3c6e34" Name="Blocked" Type="Boolean Not Null">
		<Description>Вход заблокирован. Если признак установлен, то пользователь не может войти в систему, даже если его поле "Тип входа" отлично от "Вход запрещён".
Снять флажок сможет только админ.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="ca685454-c154-4024-ae99-3d32251d1e63" Name="df_PersonalRoles_Blocked" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="8089d6be-77fb-4a84-93a4-fc74aefce9d6" Name="BlockedDueDate" Type="DateTime Null">
		<Description>Дата/время снятия блокировки. Имеет смысл только для установленного признака Blocked.
Если равно Null и установлен признак Blocked, то пользователь заблокирован бессрочно до тех пор, пока администратор его не разблокирует.
Иначе блокировка временная. При входе проверяется, что если дата/время уже настала, то блокировка снимается при очередном входе
(т.е. выполняется UPDATE Blocked=false, BlockedDueDate=NULL).</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="954e6a89-3bb4-44f5-a796-03e36cf450a2" Name="PasswordChanged" Type="DateTime Null">
		<Description>Дата/время изменения пароля для сотрудников с типом входа "Пользователь Tessa".
Если равно Null, то либо пользователь имеет другой тип входа, либо данные по заданию пароля были сброшены.
Дата/время актуальны при расчёте времени действия паролей, по наступлению которого пароль требуется сменить.</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="d0405a56-a74b-4cf2-88c6-710d6d9c7c90" Name="ApplicationArchitecture" Type="Reference(Typified) Not Null" ReferencedTable="27977834-b755-4a4a-9180-90748e71f361">
		<Description>Архитектура (разрядность) приложения, рекомендуемая для пользователя. Устанавливается администратором.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="d0405a56-a74b-00f2-4000-010d6d9c7c90" Name="ApplicationArchitectureID" Type="Int16 Not Null" ReferencedColumn="43f5ccb9-e6a2-4343-ac35-022f7b9b4971">
			<SchemeDefaultConstraint IsPermanent="true" ID="93fad539-dc41-448d-85f0-24970f97f711" Name="df_PersonalRoles_ApplicationArchitectureID" Value="0" />
		</SchemeReferencingColumn>
		<SchemeReferencingColumn ID="22f4f5e8-4b50-4196-888a-e02e867e2622" Name="ApplicationArchitectureName" Type="String(128) Not Null" ReferencedColumn="35e159bf-cdbf-45f2-a479-38c8c228565e">
			<SchemeDefaultConstraint IsPermanent="true" ID="02b55988-0c71-4ab8-844d-36f2589f5192" Name="df_PersonalRoles_ApplicationArchitectureName" Value="$Enum_ApplicationArchitectures_Auto" />
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="4aaece20-d957-4973-8912-2d854bec4c6c" Name="CipherInfo" Type="BinaryJson Null">
		<Description>Информация по шифрованию клиентских данных в папках пользователя. Сериализована в формате JSON.</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="6c977939-bbfc-006f-5000-01c2244e3cc3" Name="pk_PersonalRoles" IsClustered="true">
		<SchemeIndexedColumn Column="6c977939-bbfc-016f-4000-01c2244e3cc3" />
	</SchemePrimaryKey>
	<SchemeIndex ID="09e3d90b-bb2d-4ce6-8dc7-604ae884ed8d" Name="ndx_PersonalRoles_Login" IsUnique="true">
		<Description>Индекс использует фильтрацию по длине в байтах, чтобы не выполнять сравнение строк.</Description>
		<Predicate Dbms="SqlServer">[Login] IS NOT NULL AND [Login] &lt;&gt; N''</Predicate>
		<Predicate Dbms="PostgreSql">"Login" IS NOT NULL AND "Login" &lt;&gt; ''</Predicate>
		<SchemeIndexedColumn Column="c05ae347-af13-468e-aeda-7c6b541c06b6">
			<Expression Dbms="PostgreSql">lower("Login")</Expression>
		</SchemeIndexedColumn>
	</SchemeIndex>
	<SchemeIndex ID="ef6a60f2-1b26-4dff-aeb8-725a2fce2015" Name="ndx_PersonalRoles_Email">
		<SchemeIndexedColumn Column="e32de09d-a5dd-4de0-af63-5a9565cda555">
			<Expression Dbms="PostgreSql">lower("Email")</Expression>
		</SchemeIndexedColumn>
	</SchemeIndex>
	<SchemeIndex ID="76e8bea7-883e-4295-a52c-021112382462" Name="ndx_PersonalRoles_Email_76e8bea7" SupportsSqlServer="false" Type="GIN">
		<SchemeIndexedColumn Column="e32de09d-a5dd-4de0-af63-5a9565cda555">
			<Expression Dbms="PostgreSql">lower("Email") gin_trgm_ops</Expression>
		</SchemeIndexedColumn>
	</SchemeIndex>
	<SchemeIndex ID="6b20f55b-16a6-43ff-9ab8-1e6b035ef66b" Name="ndx_PersonalRoles_Login_6b20f55b" SupportsSqlServer="false" Type="GIN">
		<SchemeIndexedColumn Column="c05ae347-af13-468e-aeda-7c6b541c06b6">
			<Expression Dbms="PostgreSql">lower("Login") gin_trgm_ops</Expression>
		</SchemeIndexedColumn>
	</SchemeIndex>
	<SchemeIndex ID="4325a235-8ff9-4f82-929f-bbfd93b382c8" Name="ndx_PersonalRoles_PasswordChangedLoginTypeID">
		<SchemeIndexedColumn Column="954e6a89-3bb4-44f5-a796-03e36cf450a2" />
		<SchemeIndexedColumn Column="668b47c4-f8a7-00d3-4000-03b3d0eb9bba" />
		<SchemeIncludedColumn Column="e32de09d-a5dd-4de0-af63-5a9565cda555" />
	</SchemeIndex>
</SchemeTable>