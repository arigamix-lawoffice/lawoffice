<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="420a67fd-2ea0-4ccd-9c3f-6378c2fda2cc" Name="ActionTypes" Group="System">
	<Description>Типы действий с карточкой.</Description>
	<SchemePhysicalColumn ID="e017b3c3-aa04-4a5a-8bab-d66a0496b1f6" Name="ID" Type="Int16 Not Null">
		<Description>Идентификатор типа действия с карточкой.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="c452b453-1c95-498a-a03d-8566b504a96e" Name="Name" Type="String(64) Not Null">
		<Description>Название типа действия с карточкой.</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey ID="5f0201b2-95d5-4350-a8d7-fc0b9bdef54e" Name="pk_ActionTypes">
		<SchemeIndexedColumn Column="e017b3c3-aa04-4a5a-8bab-d66a0496b1f6" />
	</SchemePrimaryKey>
	<SchemeRecord>
		<ID ID="e017b3c3-aa04-4a5a-8bab-d66a0496b1f6">1</ID>
		<Name ID="c452b453-1c95-498a-a03d-8566b504a96e">$ActionHistory_Action_Creating</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="e017b3c3-aa04-4a5a-8bab-d66a0496b1f6">2</ID>
		<Name ID="c452b453-1c95-498a-a03d-8566b504a96e">$ActionHistory_Action_Opening</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="e017b3c3-aa04-4a5a-8bab-d66a0496b1f6">3</ID>
		<Name ID="c452b453-1c95-498a-a03d-8566b504a96e">$ActionHistory_Action_Editing</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="e017b3c3-aa04-4a5a-8bab-d66a0496b1f6">4</ID>
		<Name ID="c452b453-1c95-498a-a03d-8566b504a96e">$ActionHistory_Action_Deleting</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="e017b3c3-aa04-4a5a-8bab-d66a0496b1f6">5</ID>
		<Name ID="c452b453-1c95-498a-a03d-8566b504a96e">$ActionHistory_Action_FileOpening</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="e017b3c3-aa04-4a5a-8bab-d66a0496b1f6">6</ID>
		<Name ID="c452b453-1c95-498a-a03d-8566b504a96e">$ActionHistory_Action_Restoring</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="e017b3c3-aa04-4a5a-8bab-d66a0496b1f6">7</ID>
		<Name ID="c452b453-1c95-498a-a03d-8566b504a96e">$ActionHistory_Action_Export</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="e017b3c3-aa04-4a5a-8bab-d66a0496b1f6">8</ID>
		<Name ID="c452b453-1c95-498a-a03d-8566b504a96e">$ActionHistory_Action_Import</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="e017b3c3-aa04-4a5a-8bab-d66a0496b1f6">9</ID>
		<Name ID="c452b453-1c95-498a-a03d-8566b504a96e">$ActionHistory_Action_FinalDeleting</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="e017b3c3-aa04-4a5a-8bab-d66a0496b1f6">10</ID>
		<Name ID="c452b453-1c95-498a-a03d-8566b504a96e">$ActionHistory_Action_Login</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="e017b3c3-aa04-4a5a-8bab-d66a0496b1f6">11</ID>
		<Name ID="c452b453-1c95-498a-a03d-8566b504a96e">$ActionHistory_Action_Logout</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="e017b3c3-aa04-4a5a-8bab-d66a0496b1f6">12</ID>
		<Name ID="c452b453-1c95-498a-a03d-8566b504a96e">$ActionHistory_Action_ReserveNumber</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="e017b3c3-aa04-4a5a-8bab-d66a0496b1f6">13</ID>
		<Name ID="c452b453-1c95-498a-a03d-8566b504a96e">$ActionHistory_Action_AcquireNumber</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="e017b3c3-aa04-4a5a-8bab-d66a0496b1f6">14</ID>
		<Name ID="c452b453-1c95-498a-a03d-8566b504a96e">$ActionHistory_Action_AcquireReservedNumber</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="e017b3c3-aa04-4a5a-8bab-d66a0496b1f6">15</ID>
		<Name ID="c452b453-1c95-498a-a03d-8566b504a96e">$ActionHistory_Action_AcquireUnreservedNumber</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="e017b3c3-aa04-4a5a-8bab-d66a0496b1f6">16</ID>
		<Name ID="c452b453-1c95-498a-a03d-8566b504a96e">$ActionHistory_Action_ReleaseNumber</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="e017b3c3-aa04-4a5a-8bab-d66a0496b1f6">17</ID>
		<Name ID="c452b453-1c95-498a-a03d-8566b504a96e">$ActionHistory_Action_DereserveNumber</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="e017b3c3-aa04-4a5a-8bab-d66a0496b1f6">18</ID>
		<Name ID="c452b453-1c95-498a-a03d-8566b504a96e">$ActionHistory_Action_SessionClosedByAdmin</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="e017b3c3-aa04-4a5a-8bab-d66a0496b1f6">19</ID>
		<Name ID="c452b453-1c95-498a-a03d-8566b504a96e">$ActionHistory_Action_LoginFailed</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="e017b3c3-aa04-4a5a-8bab-d66a0496b1f6">20</ID>
		<Name ID="c452b453-1c95-498a-a03d-8566b504a96e">$ActionHistory_Action_Error</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="e017b3c3-aa04-4a5a-8bab-d66a0496b1f6">21</ID>
		<Name ID="c452b453-1c95-498a-a03d-8566b504a96e">$ActionHistory_Action_AddCardType</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="e017b3c3-aa04-4a5a-8bab-d66a0496b1f6">22</ID>
		<Name ID="c452b453-1c95-498a-a03d-8566b504a96e">$ActionHistory_Action_ModifyCardType</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="e017b3c3-aa04-4a5a-8bab-d66a0496b1f6">23</ID>
		<Name ID="c452b453-1c95-498a-a03d-8566b504a96e">$ActionHistory_Action_DeleteCardType</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="e017b3c3-aa04-4a5a-8bab-d66a0496b1f6">24</ID>
		<Name ID="c452b453-1c95-498a-a03d-8566b504a96e">$ActionHistory_Action_AddView</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="e017b3c3-aa04-4a5a-8bab-d66a0496b1f6">25</ID>
		<Name ID="c452b453-1c95-498a-a03d-8566b504a96e">$ActionHistory_Action_ModifyView</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="e017b3c3-aa04-4a5a-8bab-d66a0496b1f6">26</ID>
		<Name ID="c452b453-1c95-498a-a03d-8566b504a96e">$ActionHistory_Action_DeleteView</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="e017b3c3-aa04-4a5a-8bab-d66a0496b1f6">27</ID>
		<Name ID="c452b453-1c95-498a-a03d-8566b504a96e">$ActionHistory_Action_ImportView</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="e017b3c3-aa04-4a5a-8bab-d66a0496b1f6">28</ID>
		<Name ID="c452b453-1c95-498a-a03d-8566b504a96e">$ActionHistory_Action_AddWorkplace</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="e017b3c3-aa04-4a5a-8bab-d66a0496b1f6">29</ID>
		<Name ID="c452b453-1c95-498a-a03d-8566b504a96e">$ActionHistory_Action_ModifyWorkplace</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="e017b3c3-aa04-4a5a-8bab-d66a0496b1f6">30</ID>
		<Name ID="c452b453-1c95-498a-a03d-8566b504a96e">$ActionHistory_Action_DeleteWorkplace</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="e017b3c3-aa04-4a5a-8bab-d66a0496b1f6">31</ID>
		<Name ID="c452b453-1c95-498a-a03d-8566b504a96e">$ActionHistory_Action_ImportWorkplace</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="e017b3c3-aa04-4a5a-8bab-d66a0496b1f6">32</ID>
		<Name ID="c452b453-1c95-498a-a03d-8566b504a96e">$ActionHistory_Action_ModifyTable</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="e017b3c3-aa04-4a5a-8bab-d66a0496b1f6">33</ID>
		<Name ID="c452b453-1c95-498a-a03d-8566b504a96e">$ActionHistory_Action_DeleteTable</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="e017b3c3-aa04-4a5a-8bab-d66a0496b1f6">34</ID>
		<Name ID="c452b453-1c95-498a-a03d-8566b504a96e">$ActionHistory_Action_ModifyProcedure</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="e017b3c3-aa04-4a5a-8bab-d66a0496b1f6">35</ID>
		<Name ID="c452b453-1c95-498a-a03d-8566b504a96e">$ActionHistory_Action_DeleteProcedure</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="e017b3c3-aa04-4a5a-8bab-d66a0496b1f6">36</ID>
		<Name ID="c452b453-1c95-498a-a03d-8566b504a96e">$ActionHistory_Action_ModifyFunction</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="e017b3c3-aa04-4a5a-8bab-d66a0496b1f6">37</ID>
		<Name ID="c452b453-1c95-498a-a03d-8566b504a96e">$ActionHistory_Action_DeleteFunction</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="e017b3c3-aa04-4a5a-8bab-d66a0496b1f6">38</ID>
		<Name ID="c452b453-1c95-498a-a03d-8566b504a96e">$ActionHistory_Action_ModifyMigration</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="e017b3c3-aa04-4a5a-8bab-d66a0496b1f6">39</ID>
		<Name ID="c452b453-1c95-498a-a03d-8566b504a96e">$ActionHistory_Action_DeleteMigration</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="e017b3c3-aa04-4a5a-8bab-d66a0496b1f6">40</ID>
		<Name ID="c452b453-1c95-498a-a03d-8566b504a96e">$ActionHistory_Action_ModifyPartition</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="e017b3c3-aa04-4a5a-8bab-d66a0496b1f6">41</ID>
		<Name ID="c452b453-1c95-498a-a03d-8566b504a96e">$ActionHistory_Action_DeletePartition</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="e017b3c3-aa04-4a5a-8bab-d66a0496b1f6">42</ID>
		<Name ID="c452b453-1c95-498a-a03d-8566b504a96e">$ActionHistory_Action_ModifyLocalizationLibrary</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="e017b3c3-aa04-4a5a-8bab-d66a0496b1f6">43</ID>
		<Name ID="c452b453-1c95-498a-a03d-8566b504a96e">$ActionHistory_Action_DeleteLocalizationLibrary</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="e017b3c3-aa04-4a5a-8bab-d66a0496b1f6">44</ID>
		<Name ID="c452b453-1c95-498a-a03d-8566b504a96e">$ActionHistory_Action_ReserveAcquiredNumber</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="e017b3c3-aa04-4a5a-8bab-d66a0496b1f6">45</ID>
		<Name ID="c452b453-1c95-498a-a03d-8566b504a96e">$ActionHistory_Action_StoreTag</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="e017b3c3-aa04-4a5a-8bab-d66a0496b1f6">46</ID>
		<Name ID="c452b453-1c95-498a-a03d-8566b504a96e">$ActionHistory_Action_DeleteTag</Name>
	</SchemeRecord>
</SchemeTable>