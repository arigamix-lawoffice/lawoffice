namespace Tessa.Extensions.Shared.Info
{// ReSharper disable InconsistentNaming
    #region IntVrstaRacuna

    /// <summary>
    ///     Alias: IntVrstaRacuna
    /// </summary>
    public sealed class IntVrstaRacunaExtSchemeInfo
    {
        private const string name = "IntVrstaRacuna";

        #region Columns

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string Id = nameof(Id);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Vrednost = nameof(Vrednost);

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string ZapSt = nameof(ZapSt);

        #endregion

        #region ToString

        public static implicit operator string(IntVrstaRacunaExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region Opomba

    /// <summary>
    ///     Alias: Opomba
    /// </summary>
    public sealed class OpombaExtSchemeInfo
    {
        private const string name = "Opomba";

        #region Columns

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string Uid = nameof(Uid);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string EntitetaUid = nameof(EntitetaUid);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string UporabnikUid = nameof(UporabnikUid);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Vsebina = nameof(Vsebina);

        /// <summary>
        ///     Type: datetime
        /// </summary>
        public readonly string DatumInCas = nameof(DatumInCas);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string VirOpombe = nameof(VirOpombe);

        #endregion

        #region ToString

        public static implicit operator string(OpombaExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region IntVrstaSubjekta

    /// <summary>
    ///     Alias: IntVrstaSubjekta
    /// </summary>
    public sealed class IntVrstaSubjektaExtSchemeInfo
    {
        private const string name = "IntVrstaSubjekta";

        #region Columns

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string Id = nameof(Id);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Vrednost = nameof(Vrednost);

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string ZapSt = nameof(ZapSt);

        #endregion

        #region ToString

        public static implicit operator string(IntVrstaSubjektaExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region ShrambaDatotek

    /// <summary>
    ///     Alias: ShrambaDatotek
    /// </summary>
    public sealed class ShrambaDatotekExtSchemeInfo
    {
        private const string name = "ShrambaDatotek";

        #region Columns

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string Uid = nameof(Uid);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Naziv = nameof(Naziv);

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string TipShrambeDatotekId = nameof(TipShrambeDatotekId);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Lokacija = nameof(Lokacija);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Racunalnik = nameof(Racunalnik);

        /// <summary>
        ///     Type: bit
        /// </summary>
        public readonly string Privzeta = nameof(Privzeta);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Opomba = nameof(Opomba);

        #endregion

        #region ToString

        public static implicit operator string(ShrambaDatotekExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region IntPoljeEntitete

    /// <summary>
    ///     Alias: IntPoljeEntitete
    /// </summary>
    public sealed class IntPoljeEntiteteExtSchemeInfo
    {
        private const string name = "IntPoljeEntitete";

        #region Columns

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string Id = nameof(Id);

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string TipEntiteteId = nameof(TipEntiteteId);

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string Ord = nameof(Ord);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string FieldName = nameof(FieldName);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Nominative = nameof(Nominative);

        /// <summary>
        ///     Type: bit
        /// </summary>
        public readonly string TrackChange = nameof(TrackChange);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Accusative = nameof(Accusative);

        #endregion

        #region ToString

        public static implicit operator string(IntPoljeEntiteteExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region TipDokumenta

    /// <summary>
    ///     Alias: TipDokumenta
    /// </summary>
    public sealed class TipDokumentaExtSchemeInfo
    {
        private const string name = "TipDokumenta";

        #region Columns

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string Uid = nameof(Uid);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Naziv = nameof(Naziv);

        #endregion

        #region ToString

        public static implicit operator string(TipDokumentaExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region NacinPosiljanjaPoste

    /// <summary>
    ///     Alias: NacinPosiljanjaPoste
    /// </summary>
    public sealed class NacinPosiljanjaPosteExtSchemeInfo
    {
        private const string name = "NacinPosiljanjaPoste";

        #region Columns

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string Uid = nameof(Uid);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Naziv = nameof(Naziv);

        /// <summary>
        ///     Type: bit
        /// </summary>
        public readonly string VkljuciVPostnoKnjigo = nameof(VkljuciVPostnoKnjigo);

        /// <summary>
        ///     Type: bit
        /// </summary>
        public readonly string Privzet = nameof(Privzet);

        #endregion

        #region ToString

        public static implicit operator string(NacinPosiljanjaPosteExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region Opravilo

    /// <summary>
    ///     Alias: Opravilo
    /// </summary>
    public sealed class OpraviloExtSchemeInfo
    {
        private const string name = "Opravilo";

        #region Columns

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string Uid = nameof(Uid);

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string AutoInc = nameof(AutoInc);

        /// <summary>
        ///     Type: datetime
        /// </summary>
        public readonly string Datum = nameof(Datum);

        /// <summary>
        ///     Type: datetime
        /// </summary>
        public readonly string Rok = nameof(Rok);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string UporabnikOdUid = nameof(UporabnikOdUid);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string UporabnikZaUid = nameof(UporabnikZaUid);

        /// <summary>
        ///     Type: bit
        /// </summary>
        public readonly string Opravljeno = nameof(Opravljeno);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Tema = nameof(Tema);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Opis = nameof(Opis);

        /// <summary>
        ///     Type: bit
        /// </summary>
        public readonly string PogledalOd = nameof(PogledalOd);

        /// <summary>
        ///     Type: bit
        /// </summary>
        public readonly string PogledalZa = nameof(PogledalZa);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string TipOpravilaUid = nameof(TipOpravilaUid);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string SpisUid = nameof(SpisUid);

        /// <summary>
        ///     Type: datetime
        /// </summary>
        public readonly string Modified = nameof(Modified);

        #endregion

        #region ToString

        public static implicit operator string(OpraviloExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region Datoteka

    /// <summary>
    ///     Alias: Datoteka
    /// </summary>
    public sealed class DatotekaExtSchemeInfo
    {
        private const string name = "Datoteka";

        #region Columns

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string Uid = nameof(Uid);

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string AutoInc = nameof(AutoInc);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string ShrambaUid = nameof(ShrambaUid);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Ime = nameof(Ime);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Pripona = nameof(Pripona);

        /// <summary>
        ///     Type: datetime
        /// </summary>
        public readonly string Dodana = nameof(Dodana);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string TipDatotekeUid = nameof(TipDatotekeUid);

        /// <summary>
        ///     Type: datetime
        /// </summary>
        public readonly string DateCreated = nameof(DateCreated);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Skupina = nameof(Skupina);

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string Verzija = nameof(Verzija);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string RezerviralUid = nameof(RezerviralUid);

        #endregion

        #region ToString

        public static implicit operator string(DatotekaExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region DokumentDatoteka

    /// <summary>
    ///     Alias: DokumentDatoteka
    /// </summary>
    public sealed class DokumentDatotekaExtSchemeInfo
    {
        private const string name = "DokumentDatoteka";

        #region Columns

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string DokumentUid = nameof(DokumentUid);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string DatotekaUid = nameof(DatotekaUid);

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string AvtoZapSt = nameof(AvtoZapSt);

        #endregion

        #region ToString

        public static implicit operator string(DokumentDatotekaExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region SpisSmsKontakt

    /// <summary>
    ///     Alias: SpisSmsKontakt
    /// </summary>
    public sealed class SpisSmsKontaktExtSchemeInfo
    {
        private const string name = "SpisSmsKontakt";

        #region Columns

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string Uid = nameof(Uid);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string SpisUid = nameof(SpisUid);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Ime = nameof(Ime);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Vrednost = nameof(Vrednost);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Opomba = nameof(Opomba);

        /// <summary>
        ///     Type: bit
        /// </summary>
        public readonly string Aktiven = nameof(Aktiven);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string OciscenaStevilka = nameof(OciscenaStevilka);

        #endregion

        #region ToString

        public static implicit operator string(SpisSmsKontaktExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region SpisNasprotnaStranka

    /// <summary>
    ///     Alias: SpisNasprotnaStranka
    /// </summary>
    public sealed class SpisNasprotnaStrankaExtSchemeInfo
    {
        private const string name = "SpisNasprotnaStranka";

        #region Columns

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string Uid = nameof(Uid);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string SpisUid = nameof(SpisUid);

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string ZapSt = nameof(ZapSt);

        /// <summary>
        ///     Type: bit
        /// </summary>
        public readonly string Zastopnik = nameof(Zastopnik);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string ImenikUid = nameof(ImenikUid);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string SubjektUid = nameof(SubjektUid);

        #endregion

        #region ToString

        public static implicit operator string(SpisNasprotnaStrankaExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region RokSms

    /// <summary>
    ///     Alias: RokSms
    /// </summary>
    public sealed class RokSmsExtSchemeInfo
    {
        private const string name = "RokSms";

        #region Columns

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string RokUid = nameof(RokUid);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string SpisSmsKontaktUid = nameof(SpisSmsKontaktUid);

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string Sms = nameof(Sms);

        #endregion

        #region ToString

        public static implicit operator string(RokSmsExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KontaktnaOseba

    /// <summary>
    ///     Alias: KontaktnaOseba
    /// </summary>
    public sealed class KontaktnaOsebaExtSchemeInfo
    {
        private const string name = "KontaktnaOseba";

        #region Columns

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string Uid = nameof(Uid);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string ImenikUid = nameof(ImenikUid);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Ime = nameof(Ime);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string DelovnoMesto = nameof(DelovnoMesto);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Opomba = nameof(Opomba);

        #endregion

        #region ToString

        public static implicit operator string(KontaktnaOsebaExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region Zgodovina

    /// <summary>
    ///     Alias: Zgodovina
    /// </summary>
    public sealed class ZgodovinaExtSchemeInfo
    {
        private const string name = "Zgodovina";

        #region Columns

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string Uid = nameof(Uid);

        /// <summary>
        ///     Type: datetime
        /// </summary>
        public readonly string DatumInCas = nameof(DatumInCas);

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string AkcijaId = nameof(AkcijaId);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string UporabnikUid = nameof(UporabnikUid);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string DelovnaPostajaUid = nameof(DelovnaPostajaUid);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string EntitetaUid = nameof(EntitetaUid);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Vsebina = nameof(Vsebina);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string ShranjenaEntiteta = nameof(ShranjenaEntiteta);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string ShranjeniUporabnik = nameof(ShranjeniUporabnik);

        #endregion

        #region ToString

        public static implicit operator string(ZgodovinaExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region ZgodovinaIzvornaEntiteta

    /// <summary>
    ///     Alias: ZgodovinaIzvornaEntiteta
    /// </summary>
    public sealed class ZgodovinaIzvornaEntitetaExtSchemeInfo
    {
        private const string name = "ZgodovinaIzvornaEntiteta";

        #region Columns

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string ZgodovinaUid = nameof(ZgodovinaUid);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string IzvornaEntitetaUid = nameof(IzvornaEntitetaUid);

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string PoljeEntiteteId = nameof(PoljeEntiteteId);

        #endregion

        #region ToString

        public static implicit operator string(ZgodovinaIzvornaEntitetaExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region ZgodovinaDetajl

    /// <summary>
    ///     Alias: ZgodovinaDetajl
    /// </summary>
    public sealed class ZgodovinaDetajlExtSchemeInfo
    {
        private const string name = "ZgodovinaDetajl";

        #region Columns

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string Id = nameof(Id);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string ZgodovinaUid = nameof(ZgodovinaUid);

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string PoljeEntiteteId = nameof(PoljeEntiteteId);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string StaraVrednost = nameof(StaraVrednost);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string NovaVrednost = nameof(NovaVrednost);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Opomba = nameof(Opomba);

        #endregion

        #region ToString

        public static implicit operator string(ZgodovinaDetajlExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region UporabnikPriljubljenaEntiteta

    /// <summary>
    ///     Alias: UporabnikPriljubljenaEntiteta
    /// </summary>
    public sealed class UporabnikPriljubljenaEntitetaExtSchemeInfo
    {
        private const string name = "UporabnikPriljubljenaEntiteta";

        #region Columns

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string UporabnikUid = nameof(UporabnikUid);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string EntitetaUid = nameof(EntitetaUid);

        /// <summary>
        ///     Type: datetime
        /// </summary>
        public readonly string Datum = nameof(Datum);

        #endregion

        #region ToString

        public static implicit operator string(UporabnikPriljubljenaEntitetaExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region Kontakt

    /// <summary>
    ///     Alias: Kontakt
    /// </summary>
    public sealed class KontaktExtSchemeInfo
    {
        private const string name = "Kontakt";

        #region Columns

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string Uid = nameof(Uid);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string KontaktnaOsebaUid = nameof(KontaktnaOsebaUid);

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string TipKontaktaId = nameof(TipKontaktaId);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Vrednost = nameof(Vrednost);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Opomba = nameof(Opomba);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string OciscenaStevilka = nameof(OciscenaStevilka);

        #endregion

        #region ToString

        public static implicit operator string(KontaktExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region NacinPlacila

    /// <summary>
    ///     Alias: NacinPlacila
    /// </summary>
    public sealed class NacinPlacilaExtSchemeInfo
    {
        private const string name = "NacinPlacila";

        #region Columns

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string Uid = nameof(Uid);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Naziv = nameof(Naziv);

        #endregion

        #region ToString

        public static implicit operator string(NacinPlacilaExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region Placilo

    /// <summary>
    ///     Alias: Placilo
    /// </summary>
    public sealed class PlaciloExtSchemeInfo
    {
        private const string name = "Placilo";

        #region Columns

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string Uid = nameof(Uid);

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string AutoInc = nameof(AutoInc);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string SpisUid = nameof(SpisUid);

        /// <summary>
        ///     Type: datetime
        /// </summary>
        public readonly string Datum = nameof(Datum);

        /// <summary>
        ///     Type: decimal
        /// </summary>
        public readonly string Kolicina = nameof(Kolicina);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string NacinPlacilaUid = nameof(NacinPlacilaUid);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Opis = nameof(Opis);

        #endregion

        #region ToString

        public static implicit operator string(PlaciloExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region IntTipEntitete

    /// <summary>
    ///     Alias: IntTipEntitete
    /// </summary>
    public sealed class IntTipEntiteteExtSchemeInfo
    {
        private const string name = "IntTipEntitete";

        #region Columns

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string Id = nameof(Id);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string EntitySet = nameof(EntitySet);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Nominative = nameof(Nominative);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Accusative = nameof(Accusative);

        /// <summary>
        ///     Type: bit
        /// </summary>
        public readonly string TrackAdd = nameof(TrackAdd);

        /// <summary>
        ///     Type: bit
        /// </summary>
        public readonly string TrackChange = nameof(TrackChange);

        /// <summary>
        ///     Type: bit
        /// </summary>
        public readonly string TrackDelete = nameof(TrackDelete);

        /// <summary>
        ///     Type: bit
        /// </summary>
        public readonly string TrackOpen = nameof(TrackOpen);

        #endregion

        #region ToString

        public static implicit operator string(IntTipEntiteteExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region ObrestnaMera

    /// <summary>
    ///     Alias: ObrestnaMera
    /// </summary>
    public sealed class ObrestnaMeraExtSchemeInfo
    {
        private const string name = "ObrestnaMera";

        #region Columns

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string Uid = nameof(Uid);

        /// <summary>
        ///     Type: datetime
        /// </summary>
        public readonly string VeljaOd = nameof(VeljaOd);

        /// <summary>
        ///     Type: decimal
        /// </summary>
        public readonly string Vrednost = nameof(Vrednost);

        #endregion

        #region ToString

        public static implicit operator string(ObrestnaMeraExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region Entiteta

    /// <summary>
    ///     Alias: Entiteta
    /// </summary>
    public sealed class EntitetaExtSchemeInfo
    {
        private const string name = "Entiteta";

        #region Columns

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string Uid = nameof(Uid);

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string TipEntiteteId = nameof(TipEntiteteId);

        #endregion

        #region ToString

        public static implicit operator string(EntitetaExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region EntityLock

    /// <summary>
    ///     Alias: EntityLock
    /// </summary>
    public sealed class EntityLockExtSchemeInfo
    {
        private const string name = "EntityLock";

        #region Columns

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string EntityUid = nameof(EntityUid);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string UporabnikUid = nameof(UporabnikUid);

        /// <summary>
        ///     Type: datetime
        /// </summary>
        public readonly string VeljaDo = nameof(VeljaDo);

        #endregion

        #region ToString

        public static implicit operator string(EntityLockExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region Uporabnik

    /// <summary>
    ///     Alias: Uporabnik
    /// </summary>
    public sealed class UporabnikExtSchemeInfo
    {
        private const string name = "Uporabnik";

        #region Columns

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Ime = nameof(Ime);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Priimek = nameof(Priimek);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string DelovnoMesto = nameof(DelovnoMesto);

        /// <summary>
        ///     Type: bit
        /// </summary>
        public readonly string WindowsLogin = nameof(WindowsLogin);

        /// <summary>
        ///     Type: bit
        /// </summary>
        public readonly string Aktiven = nameof(Aktiven);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string Uid = nameof(Uid);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string UporabniskoIme = nameof(UporabniskoIme);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Geslo = nameof(Geslo);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string EMail = nameof(EMail);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string IusUporabniskoIme = nameof(IusUporabniskoIme);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string IusGeslo = nameof(IusGeslo);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string ImeInPriimek = nameof(ImeInPriimek);

        #endregion

        #region ToString

        public static implicit operator string(UporabnikExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region TipRoka

    /// <summary>
    ///     Alias: TipRoka
    /// </summary>
    public sealed class TipRokaExtSchemeInfo
    {
        private const string name = "TipRoka";

        #region Columns

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string Uid = nameof(Uid);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Naziv = nameof(Naziv);

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string Barva = nameof(Barva);

        /// <summary>
        ///     Type: bit
        /// </summary>
        public readonly string Privzeto = nameof(Privzeto);

        /// <summary>
        ///     Type: bit
        /// </summary>
        public readonly string ObvescajPoSms = nameof(ObvescajPoSms);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string VzorecSms = nameof(VzorecSms);

        #endregion

        #region ToString

        public static implicit operator string(TipRokaExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region Naslov

    /// <summary>
    ///     Alias: Naslov
    /// </summary>
    public sealed class NaslovExtSchemeInfo
    {
        private const string name = "Naslov";

        #region Columns

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string Uid = nameof(Uid);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string UlicaInHisnaStevilka = nameof(UlicaInHisnaStevilka);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Posta = nameof(Posta);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Kraj = nameof(Kraj);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Drzava = nameof(Drzava);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string PostniPredal = nameof(PostniPredal);

        #endregion

        #region ToString

        public static implicit operator string(NaslovExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region IntTipSubjekta

    /// <summary>
    ///     Alias: IntTipSubjekta
    /// </summary>
    public sealed class IntTipSubjektaExtSchemeInfo
    {
        private const string name = "IntTipSubjekta";

        #region Columns

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string Id = nameof(Id);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Vrednost = nameof(Vrednost);

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string ZapSt = nameof(ZapSt);

        #endregion

        #region ToString

        public static implicit operator string(IntTipSubjektaExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region Imenik

    /// <summary>
    ///     Alias: Imenik
    /// </summary>
    public sealed class ImenikExtSchemeInfo
    {
        private const string name = "Imenik";

        #region Columns

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string Uid = nameof(Uid);

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string TipSubjektaId = nameof(TipSubjektaId);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Naziv = nameof(Naziv);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string DavcnaStevilka = nameof(DavcnaStevilka);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string MaticnaStevilka = nameof(MaticnaStevilka);

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string VrstaSubjektaId = nameof(VrstaSubjektaId);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string NaslovUid = nameof(NaslovUid);

        #endregion

        #region ToString

        public static implicit operator string(ImenikExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region PrivzetaMapa

    /// <summary>
    ///     Alias: PrivzetaMapa
    /// </summary>
    public sealed class PrivzetaMapaExtSchemeInfo
    {
        private const string name = "PrivzetaMapa";

        #region Columns

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string Uid = nameof(Uid);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Naziv = nameof(Naziv);

        #endregion

        #region ToString

        public static implicit operator string(PrivzetaMapaExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region MestoHranjenjaSpisa

    /// <summary>
    ///     Alias: MestoHranjenjaSpisa
    /// </summary>
    public sealed class MestoHranjenjaSpisaExtSchemeInfo
    {
        private const string name = "MestoHranjenjaSpisa";

        #region Columns

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string Uid = nameof(Uid);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Naziv = nameof(Naziv);

        /// <summary>
        ///     Type: bit
        /// </summary>
        public readonly string Privzeto = nameof(Privzeto);

        #endregion

        #region ToString

        public static implicit operator string(MestoHranjenjaSpisaExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region PrivzetaMapaDetail

    /// <summary>
    ///     Alias: PrivzetaMapaDetail
    /// </summary>
    public sealed class PrivzetaMapaDetailExtSchemeInfo
    {
        private const string name = "PrivzetaMapaDetail";

        #region Columns

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string Uid = nameof(Uid);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Naziv = nameof(Naziv);

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string VrstniRed = nameof(VrstniRed);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string PrivzetaMapaUid = nameof(PrivzetaMapaUid);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string PrivzetaMapaDetailId = nameof(PrivzetaMapaDetailId);

        #endregion

        #region ToString

        public static implicit operator string(PrivzetaMapaDetailExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region Sodnik

    /// <summary>
    ///     Alias: Sodnik
    /// </summary>
    public sealed class SodnikExtSchemeInfo
    {
        private const string name = "Sodnik";

        #region Columns

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string Uid = nameof(Uid);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Ime = nameof(Ime);

        #endregion

        #region ToString

        public static implicit operator string(SodnikExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region Kategorija

    /// <summary>
    ///     Alias: Kategorija
    /// </summary>
    public sealed class KategorijaExtSchemeInfo
    {
        private const string name = "Kategorija";

        #region Columns

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string Uid = nameof(Uid);

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string ZapSt = nameof(ZapSt);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Naziv = nameof(Naziv);

        /// <summary>
        ///     Type: varbinary
        /// </summary>
        public readonly string Ikona = nameof(Ikona);

        #endregion

        #region ToString

        public static implicit operator string(KategorijaExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region SpisZastopnikNasprotneStranke

    /// <summary>
    ///     Alias: SpisZastopnikNasprotneStranke
    /// </summary>
    public sealed class SpisZastopnikNasprotneStrankeExtSchemeInfo
    {
        private const string name = "SpisZastopnikNasprotneStranke";

        #region Columns

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string Uid = nameof(Uid);

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string ZapSt = nameof(ZapSt);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string SpisUid = nameof(SpisUid);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string ImenikUid = nameof(ImenikUid);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string SubjektUid = nameof(SubjektUid);

        #endregion

        #region ToString

        public static implicit operator string(SpisZastopnikNasprotneStrankeExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KlasifikacijskiZnak

    /// <summary>
    ///     Alias: KlasifikacijskiZnak
    /// </summary>
    public sealed class KlasifikacijskiZnakExtSchemeInfo
    {
        private const string name = "KlasifikacijskiZnak";

        #region Columns

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string Uid = nameof(Uid);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Znak = nameof(Znak);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Naziv = nameof(Naziv);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Opis = nameof(Opis);

        #endregion

        #region ToString

        public static implicit operator string(KlasifikacijskiZnakExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region GlobalnaNastavitev

    /// <summary>
    ///     Alias: GlobalnaNastavitev
    /// </summary>
    public sealed class GlobalnaNastavitevExtSchemeInfo
    {
        private const string name = "GlobalnaNastavitev";

        #region Columns

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Ident = nameof(Ident);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Opis = nameof(Opis);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Skupina = nameof(Skupina);

        /// <summary>
        ///     Type: bit
        /// </summary>
        public readonly string Visible = nameof(Visible);

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string Ord = nameof(Ord);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string EnumVrednosti = nameof(EnumVrednosti);

        /// <summary>
        ///     Type: bit
        /// </summary>
        public readonly string VrednostBoolean = nameof(VrednostBoolean);

        /// <summary>
        ///     Type: ntext
        /// </summary>
        public readonly string VrednostString = nameof(VrednostString);

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string VrednostInt = nameof(VrednostInt);

        /// <summary>
        ///     Type: float
        /// </summary>
        public readonly string VrednostDouble = nameof(VrednostDouble);

        #endregion

        #region ToString

        public static implicit operator string(GlobalnaNastavitevExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region Spis

    /// <summary>
    ///     Alias: Spis
    /// </summary>
    public sealed class SpisExtSchemeInfo
    {
        private const string name = "Spis";

        #region Columns

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string Uid = nameof(Uid);

        /// <summary>
        ///     Type: bit
        /// </summary>
        public readonly string Arhiviran = nameof(Arhiviran);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string KlasifikacijskiZnakUid = nameof(KlasifikacijskiZnakUid);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Stevilka = nameof(Stevilka);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string OpravilnaStevilka = nameof(OpravilnaStevilka);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string SodnaStevilka = nameof(SodnaStevilka);

        /// <summary>
        ///     Type: datetime
        /// </summary>
        public readonly string DatumZacetka = nameof(DatumZacetka);

        /// <summary>
        ///     Type: decimal
        /// </summary>
        public readonly string Pcto = nameof(Pcto);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Opis = nameof(Opis);

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string Ikona = nameof(Ikona);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string MestoHranjenjaSpisaUid = nameof(MestoHranjenjaSpisaUid);

        /// <summary>
        ///     Type: bit
        /// </summary>
        public readonly string OmejenDostop = nameof(OmejenDostop);

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string AutoInc = nameof(AutoInc);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string UpravniOrganUid = nameof(UpravniOrganUid);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string KategorijaUid = nameof(KategorijaUid);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string SodisceUid = nameof(SodisceUid);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string SodnikUid = nameof(SodnikUid);

        /// <summary>
        ///     Type: datetime
        /// </summary>
        public readonly string DatumResitve = nameof(DatumResitve);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string AutoStevilka = nameof(AutoStevilka);

        #endregion

        #region ToString

        public static implicit operator string(SpisExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region Rok

    /// <summary>
    ///     Alias: Rok
    /// </summary>
    public sealed class RokExtSchemeInfo
    {
        private const string name = "Rok";

        #region Columns

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string TipRokaUid = nameof(TipRokaUid);

        /// <summary>
        ///     Type: datetime
        /// </summary>
        public readonly string Zacetek = nameof(Zacetek);

        /// <summary>
        ///     Type: datetime
        /// </summary>
        public readonly string Konec = nameof(Konec);

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string OpominDni = nameof(OpominDni);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Opis = nameof(Opis);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Lokacija = nameof(Lokacija);

        /// <summary>
        ///     Type: bit
        /// </summary>
        public readonly string Opravljen = nameof(Opravljen);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string Uid = nameof(Uid);

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string AutoInc = nameof(AutoInc);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string SpisUid = nameof(SpisUid);

        /// <summary>
        ///     Type: datetime
        /// </summary>
        public readonly string Modified = nameof(Modified);

        #endregion

        #region ToString

        public static implicit operator string(RokExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region RokOutlookSync

    /// <summary>
    ///     Alias: RokOutlookSync
    /// </summary>
    public sealed class RokOutlookSyncExtSchemeInfo
    {
        private const string name = "RokOutlookSync";

        #region Columns

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string RokUid = nameof(RokUid);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string OutlookCalendarEntryId = nameof(OutlookCalendarEntryId);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string OutlookId = nameof(OutlookId);

        /// <summary>
        ///     Type: datetime
        /// </summary>
        public readonly string Synchronized = nameof(Synchronized);

        #endregion

        #region ToString

        public static implicit operator string(RokOutlookSyncExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region SpisNasaStranka

    /// <summary>
    ///     Alias: SpisNasaStranka
    /// </summary>
    public sealed class SpisNasaStrankaExtSchemeInfo
    {
        private const string name = "SpisNasaStranka";

        #region Columns

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string Uid = nameof(Uid);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string SpisUid = nameof(SpisUid);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string ImenikUid = nameof(ImenikUid);

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string ZapSt = nameof(ZapSt);

        /// <summary>
        ///     Type: bit
        /// </summary>
        public readonly string Narocnik = nameof(Narocnik);

        #endregion

        #region ToString

        public static implicit operator string(SpisNasaStrankaExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region Vplacilo

    /// <summary>
    ///     Alias: Vplacilo
    /// </summary>
    public sealed class VplaciloExtSchemeInfo
    {
        private const string name = "Vplacilo";

        #region Columns

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string RefStevilka = nameof(RefStevilka);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string ImenikUid = nameof(ImenikUid);

        /// <summary>
        ///     Type: bit
        /// </summary>
        public readonly string AvansnoVplacilo = nameof(AvansnoVplacilo);

        /// <summary>
        ///     Type: datetime
        /// </summary>
        public readonly string Datum = nameof(Datum);

        /// <summary>
        ///     Type: decimal
        /// </summary>
        public readonly string Znesek = nameof(Znesek);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Opis = nameof(Opis);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string Uid = nameof(Uid);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Stevilka = nameof(Stevilka);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string AutoStevilka = nameof(AutoStevilka);

        #endregion

        #region ToString

        public static implicit operator string(VplaciloExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region Valuta

    /// <summary>
    ///     Alias: Valuta
    /// </summary>
    public sealed class ValutaExtSchemeInfo
    {
        private const string name = "Valuta";

        #region Columns

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string Uid = nameof(Uid);

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string ZapSt = nameof(ZapSt);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Naziv = nameof(Naziv);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string KratekNaziv = nameof(KratekNaziv);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Oznaka = nameof(Oznaka);

        /// <summary>
        ///     Type: bit
        /// </summary>
        public readonly string DomacaValuta = nameof(DomacaValuta);

        #endregion

        #region ToString

        public static implicit operator string(ValutaExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region Racun

    /// <summary>
    ///     Alias: Racun
    /// </summary>
    public sealed class RacunExtSchemeInfo
    {
        private const string name = "Racun";

        #region Columns

        /// <summary>
        ///     Type: datetime
        /// </summary>
        public readonly string DatumIzdaje = nameof(DatumIzdaje);

        /// <summary>
        ///     Type: datetime
        /// </summary>
        public readonly string RokZaPlacilo = nameof(RokZaPlacilo);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Opis = nameof(Opis);

        /// <summary>
        ///     Type: bit
        /// </summary>
        public readonly string Arhiviran = nameof(Arhiviran);

        /// <summary>
        ///     Type: decimal
        /// </summary>
        public readonly string Suma = nameof(Suma);

        /// <summary>
        ///     Type: decimal
        /// </summary>
        public readonly string Ddv = nameof(Ddv);

        /// <summary>
        ///     Type: decimal
        /// </summary>
        public readonly string Znesek = nameof(Znesek);

        /// <summary>
        ///     Type: decimal
        /// </summary>
        public readonly string Preostanek = nameof(Preostanek);

        /// <summary>
        ///     Type: bit
        /// </summary>
        public readonly string Potrjen = nameof(Potrjen);

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string VrstaRacunaId = nameof(VrstaRacunaId);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string Uid = nameof(Uid);

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string AutoInc = nameof(AutoInc);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string ValutaUid = nameof(ValutaUid);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string UporabnikUid = nameof(UporabnikUid);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string StrankaUid = nameof(StrankaUid);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Stevilka = nameof(Stevilka);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string AutoStevilka = nameof(AutoStevilka);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string VezanRacunUid = nameof(VezanRacunUid);

        /// <summary>
        ///     Type: datetime
        /// </summary>
        public readonly string DatumOpravljeneStoritve = nameof(DatumOpravljeneStoritve);

        /// <summary>
        ///     Type: decimal
        /// </summary>
        public readonly string PoljubenZnesek = nameof(PoljubenZnesek);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string SpisUid = nameof(SpisUid);

        #endregion

        #region ToString

        public static implicit operator string(RacunExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region Subjekt

    /// <summary>
    ///     Alias: Subjekt
    /// </summary>
    public sealed class SubjektExtSchemeInfo
    {
        private const string name = "Subjekt";

        #region Columns

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string Uid = nameof(Uid);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Naziv = nameof(Naziv);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string DavcnaStevilka = nameof(DavcnaStevilka);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string MaticnaStevilka = nameof(MaticnaStevilka);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string KontaktnaOsebaIme = nameof(KontaktnaOsebaIme);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string NaslovUid = nameof(NaslovUid);

        #endregion

        #region ToString

        public static implicit operator string(SubjektExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region IntSmerDokumenta

    /// <summary>
    ///     Alias: IntSmerDokumenta
    /// </summary>
    public sealed class IntSmerDokumentaExtSchemeInfo
    {
        private const string name = "IntSmerDokumenta";

        #region Columns

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string Id = nameof(Id);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Vrednost = nameof(Vrednost);

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string ZapSt = nameof(ZapSt);

        #endregion

        #region ToString

        public static implicit operator string(IntSmerDokumentaExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region VrstaDokumenta

    /// <summary>
    ///     Alias: VrstaDokumenta
    /// </summary>
    public sealed class VrstaDokumentaExtSchemeInfo
    {
        private const string name = "VrstaDokumenta";

        #region Columns

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string Uid = nameof(Uid);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Naziv = nameof(Naziv);

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string SmerDokumentaId = nameof(SmerDokumentaId);

        /// <summary>
        ///     Type: bit
        /// </summary>
        public readonly string Privzeta = nameof(Privzeta);

        #endregion

        #region ToString

        public static implicit operator string(VrstaDokumentaExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region Dokument

    /// <summary>
    ///     Alias: Dokument
    /// </summary>
    public sealed class DokumentExtSchemeInfo
    {
        private const string name = "Dokument";

        #region Columns

        /// <summary>
        ///     Type: datetime
        /// </summary>
        public readonly string Datum = nameof(Datum);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string OdgovornaOsebaUid = nameof(OdgovornaOsebaUid);

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string SmerDokumentaId = nameof(SmerDokumentaId);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string VrstaDokumentaUid = nameof(VrstaDokumentaUid);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Stevilka = nameof(Stevilka);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string Uid = nameof(Uid);

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string AutoInc = nameof(AutoInc);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string RefStevilka = nameof(RefStevilka);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Naziv = nameof(Naziv);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string TipDokumentaUid = nameof(TipDokumentaUid);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string ParentDokumentUid = nameof(ParentDokumentUid);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string SubjektUid = nameof(SubjektUid);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string SpisUid = nameof(SpisUid);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string AutoStevilka = nameof(AutoStevilka);

        #endregion

        #region ToString

        public static implicit operator string(DokumentExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region OdhodnaPosta

    /// <summary>
    ///     Alias: OdhodnaPosta
    /// </summary>
    public sealed class OdhodnaPostaExtSchemeInfo
    {
        private const string name = "OdhodnaPosta";

        #region Columns

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string Uid = nameof(Uid);

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string AutoInc = nameof(AutoInc);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Stevilka = nameof(Stevilka);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string OdgovornaOsebaUid = nameof(OdgovornaOsebaUid);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string SubjektUid = nameof(SubjektUid);

        /// <summary>
        ///     Type: datetime
        /// </summary>
        public readonly string Datum = nameof(Datum);

        /// <summary>
        ///     Type: bit
        /// </summary>
        public readonly string Zakljuceno = nameof(Zakljuceno);

        /// <summary>
        ///     Type: decimal
        /// </summary>
        public readonly string Postnina = nameof(Postnina);

        /// <summary>
        ///     Type: decimal
        /// </summary>
        public readonly string Dopis = nameof(Dopis);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string NacinPosiljanjaPosteUid = nameof(NacinPosiljanjaPosteUid);

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string R = nameof(R);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string DokumentUid = nameof(DokumentUid);

        /// <summary>
        ///     Type: datetime
        /// </summary>
        public readonly string PotrjenPrejem = nameof(PotrjenPrejem);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string AutoStevilka = nameof(AutoStevilka);

        #endregion

        #region ToString

        public static implicit operator string(OdhodnaPostaExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region MerskaEnota

    /// <summary>
    ///     Alias: MerskaEnota
    /// </summary>
    public sealed class MerskaEnotaExtSchemeInfo
    {
        private const string name = "MerskaEnota";

        #region Columns

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string Uid = nameof(Uid);

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string ZapSt = nameof(ZapSt);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Naziv = nameof(Naziv);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string KratekNaziv = nameof(KratekNaziv);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Oznaka = nameof(Oznaka);

        /// <summary>
        ///     Type: float
        /// </summary>
        public readonly string Inkrement = nameof(Inkrement);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string ValutaUid = nameof(ValutaUid);

        #endregion

        #region ToString

        public static implicit operator string(MerskaEnotaExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region Cenik

    /// <summary>
    ///     Alias: Cenik
    /// </summary>
    public sealed class CenikExtSchemeInfo
    {
        private const string name = "Cenik";

        #region Columns

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string Uid = nameof(Uid);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Stevilka = nameof(Stevilka);

        /// <summary>
        ///     Type: datetime
        /// </summary>
        public readonly string VeljaOd = nameof(VeljaOd);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string ValutaUid = nameof(ValutaUid);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Naziv = nameof(Naziv);

        /// <summary>
        ///     Type: bit
        /// </summary>
        public readonly string Interni = nameof(Interni);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string ImenikUid = nameof(ImenikUid);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string SpisUid = nameof(SpisUid);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string AutoStevilka = nameof(AutoStevilka);

        #endregion

        #region ToString

        public static implicit operator string(CenikExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region CenikPostavka

    /// <summary>
    ///     Alias: CenikPostavka
    /// </summary>
    public sealed class CenikPostavkaExtSchemeInfo
    {
        private const string name = "CenikPostavka";

        #region Columns

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string Uid = nameof(Uid);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string CenikUid = nameof(CenikUid);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string MerskaEnotaUid = nameof(MerskaEnotaUid);

        /// <summary>
        ///     Type: decimal
        /// </summary>
        public readonly string CenaMerskeEnote = nameof(CenaMerskeEnote);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string UporabnikUid = nameof(UporabnikUid);

        #endregion

        #region ToString

        public static implicit operator string(CenikPostavkaExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region CenaMerskeEnoteLookup

    /// <summary>
    ///     Alias: CenaMerskeEnoteLookup
    /// </summary>
    public sealed class CenaMerskeEnoteLookupExtSchemeInfo
    {
        private const string name = "CenaMerskeEnoteLookup";

        #region Columns

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string EvaluationOrder = nameof(EvaluationOrder);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string ValutaUid = nameof(ValutaUid);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string MerskaEnotaUid = nameof(MerskaEnotaUid);

        /// <summary>
        ///     Type: decimal
        /// </summary>
        public readonly string CenaMerskeEnote = nameof(CenaMerskeEnote);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string CenikPostavkaUid = nameof(CenikPostavkaUid);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string SpisUid = nameof(SpisUid);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string ImenikUid = nameof(ImenikUid);

        /// <summary>
        ///     Type: datetime
        /// </summary>
        public readonly string VeljaOd = nameof(VeljaOd);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string UporabnikUid = nameof(UporabnikUid);

        #endregion

        #region ToString

        public static implicit operator string(CenaMerskeEnoteLookupExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region Stroskovnik

    /// <summary>
    ///     Alias: Stroskovnik
    /// </summary>
    public sealed class StroskovnikExtSchemeInfo
    {
        private const string name = "Stroskovnik";

        #region Columns

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string Uid = nameof(Uid);

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string AutoInc = nameof(AutoInc);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string SpisUid = nameof(SpisUid);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string UporabnikUid = nameof(UporabnikUid);

        /// <summary>
        ///     Type: datetime
        /// </summary>
        public readonly string Datum = nameof(Datum);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Opis = nameof(Opis);

        /// <summary>
        ///     Type: decimal
        /// </summary>
        public readonly string Kolicina = nameof(Kolicina);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string MerskaEnotaUid = nameof(MerskaEnotaUid);

        /// <summary>
        ///     Type: bit
        /// </summary>
        public readonly string BrezDdv = nameof(BrezDdv);

        /// <summary>
        ///     Type: bit
        /// </summary>
        public readonly string MaterialniStrosek = nameof(MaterialniStrosek);

        /// <summary>
        ///     Type: bit
        /// </summary>
        public readonly string BppExOffo = nameof(BppExOffo);

        /// <summary>
        ///     Type: bit
        /// </summary>
        public readonly string Potrjen = nameof(Potrjen);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string PovezavaUid = nameof(PovezavaUid);

        #endregion

        #region ToString

        public static implicit operator string(StroskovnikExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region DelovnaPostaja

    /// <summary>
    ///     Alias: DelovnaPostaja
    /// </summary>
    public sealed class DelovnaPostajaExtSchemeInfo
    {
        private const string name = "DelovnaPostaja";

        #region Columns

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string Uid = nameof(Uid);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string ComputerName = nameof(ComputerName);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Opis = nameof(Opis);

        /// <summary>
        ///     Type: datetime
        /// </summary>
        public readonly string ZadnjaPrijavaDatum = nameof(ZadnjaPrijavaDatum);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string ZadnjiUporabnik = nameof(ZadnjiUporabnik);

        #endregion

        #region ToString

        public static implicit operator string(DelovnaPostajaExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region RokUporabnik

    /// <summary>
    ///     Alias: RokUporabnik
    /// </summary>
    public sealed class RokUporabnikExtSchemeInfo
    {
        private const string name = "RokUporabnik";

        #region Columns

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string RokUid = nameof(RokUid);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string UporabnikUid = nameof(UporabnikUid);

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string AvtoZapSt = nameof(AvtoZapSt);

        #endregion

        #region ToString

        public static implicit operator string(RokUporabnikExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KljucnaBeseda

    /// <summary>
    ///     Alias: KljucnaBeseda
    /// </summary>
    public sealed class KljucnaBesedaExtSchemeInfo
    {
        private const string name = "KljucnaBeseda";

        #region Columns

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string Uid = nameof(Uid);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Beseda = nameof(Beseda);

        #endregion

        #region ToString

        public static implicit operator string(KljucnaBesedaExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region SpisKljucnaBeseda

    /// <summary>
    ///     Alias: SpisKljucnaBeseda
    /// </summary>
    public sealed class SpisKljucnaBesedaExtSchemeInfo
    {
        private const string name = "SpisKljucnaBeseda";

        #region Columns

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string Uid = nameof(Uid);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string SpisUid = nameof(SpisUid);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string KljucnaBesedaUid = nameof(KljucnaBesedaUid);

        #endregion

        #region ToString

        public static implicit operator string(SpisKljucnaBesedaExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region TipOpravila

    /// <summary>
    ///     Alias: TipOpravila
    /// </summary>
    public sealed class TipOpravilaExtSchemeInfo
    {
        private const string name = "TipOpravila";

        #region Columns

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string Uid = nameof(Uid);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Naziv = nameof(Naziv);

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string Barva = nameof(Barva);

        #endregion

        #region ToString

        public static implicit operator string(TipOpravilaExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region IntTipShrambeDatotek

    /// <summary>
    ///     Alias: IntTipShrambeDatotek
    /// </summary>
    public sealed class IntTipShrambeDatotekExtSchemeInfo
    {
        private const string name = "IntTipShrambeDatotek";

        #region Columns

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string Id = nameof(Id);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Vrednost = nameof(Vrednost);

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string ZapSt = nameof(ZapSt);

        #endregion

        #region ToString

        public static implicit operator string(IntTipShrambeDatotekExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region IntTipKontakta

    /// <summary>
    ///     Alias: IntTipKontakta
    /// </summary>
    public sealed class IntTipKontaktaExtSchemeInfo
    {
        private const string name = "IntTipKontakta";

        #region Columns

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string Id = nameof(Id);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Vrednost = nameof(Vrednost);

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string ZapSt = nameof(ZapSt);

        #endregion

        #region ToString

        public static implicit operator string(IntTipKontaktaExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region IntBlagajnaNamen

    /// <summary>
    ///     Alias: IntBlagajnaNamen
    /// </summary>
    public sealed class IntBlagajnaNamenExtSchemeInfo
    {
        private const string name = "IntBlagajnaNamen";

        #region Columns

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string Id = nameof(Id);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Vrednost = nameof(Vrednost);

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string ZapSt = nameof(ZapSt);

        /// <summary>
        ///     Type: bit
        /// </summary>
        public readonly string Prejemek = nameof(Prejemek);

        #endregion

        #region ToString

        public static implicit operator string(IntBlagajnaNamenExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region IntPravica

    /// <summary>
    ///     Alias: IntPravica
    /// </summary>
    public sealed class IntPravicaExtSchemeInfo
    {
        private const string name = "IntPravica";

        #region Columns

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string Id = nameof(Id);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Vrednost = nameof(Vrednost);

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string ZapSt = nameof(ZapSt);

        #endregion

        #region ToString

        public static implicit operator string(IntPravicaExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region ReportDefinition

    /// <summary>
    ///     Alias: ReportDefinition
    /// </summary>
    public sealed class ReportDefinitionExtSchemeInfo
    {
        private const string name = "ReportDefinition";

        #region Columns

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string ReportType = nameof(ReportType);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string ReportName = nameof(ReportName);

        /// <summary>
        ///     Type: varbinary
        /// </summary>
        public readonly string Stream = nameof(Stream);

        #endregion

        #region ToString

        public static implicit operator string(ReportDefinitionExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region Blagajna

    /// <summary>
    ///     Alias: Blagajna
    /// </summary>
    public sealed class BlagajnaExtSchemeInfo
    {
        private const string name = "Blagajna";

        #region Columns

        /// <summary>
        ///     Type: datetime
        /// </summary>
        public readonly string Datum = nameof(Datum);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string UporabnikUid = nameof(UporabnikUid);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string SubjektUid = nameof(SubjektUid);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string ValutaUid = nameof(ValutaUid);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string Uid = nameof(Uid);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Stevilka = nameof(Stevilka);

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string BlagajnaNamedId = nameof(BlagajnaNamedId);

        /// <summary>
        ///     Type: decimal
        /// </summary>
        public readonly string Znesek = nameof(Znesek);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string AutoStevilka = nameof(AutoStevilka);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Opis = nameof(Opis);

        #endregion

        #region ToString

        public static implicit operator string(BlagajnaExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region IntAkcijaZgodovine

    /// <summary>
    ///     Alias: IntAkcijaZgodovine
    /// </summary>
    public sealed class IntAkcijaZgodovineExtSchemeInfo
    {
        private const string name = "IntAkcijaZgodovine";

        #region Columns

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string Id = nameof(Id);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Vrednost = nameof(Vrednost);

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string ZapSt = nameof(ZapSt);

        #endregion

        #region ToString

        public static implicit operator string(IntAkcijaZgodovineExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region SpisUporabnik

    /// <summary>
    ///     Alias: SpisUporabnik
    /// </summary>
    public sealed class SpisUporabnikExtSchemeInfo
    {
        private const string name = "SpisUporabnik";

        #region Columns

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string SpisUid = nameof(SpisUid);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string UporabnikUid = nameof(UporabnikUid);

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string ZapSt = nameof(ZapSt);

        /// <summary>
        ///     Type: bit
        /// </summary>
        public readonly string Skrbnik = nameof(Skrbnik);

        #endregion

        #region ToString

        public static implicit operator string(SpisUporabnikExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region RacunPostavka

    /// <summary>
    ///     Alias: RacunPostavka
    /// </summary>
    public sealed class RacunPostavkaExtSchemeInfo
    {
        private const string name = "RacunPostavka";

        #region Columns

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string ZapSt = nameof(ZapSt);

        /// <summary>
        ///     Type: datetime
        /// </summary>
        public readonly string Datum = nameof(Datum);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string OdvetnikUid = nameof(OdvetnikUid);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Opis = nameof(Opis);

        /// <summary>
        ///     Type: decimal
        /// </summary>
        public readonly string Kolicina = nameof(Kolicina);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string MerskaEnotaUid = nameof(MerskaEnotaUid);

        /// <summary>
        ///     Type: decimal
        /// </summary>
        public readonly string CenaMerskeEnote = nameof(CenaMerskeEnote);

        /// <summary>
        ///     Type: decimal
        /// </summary>
        public readonly string Popust = nameof(Popust);

        /// <summary>
        ///     Type: decimal
        /// </summary>
        public readonly string DdvProcent = nameof(DdvProcent);

        /// <summary>
        ///     Type: decimal
        /// </summary>
        public readonly string DdvZnesek = nameof(DdvZnesek);

        /// <summary>
        ///     Type: decimal
        /// </summary>
        public readonly string Znesek = nameof(Znesek);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string Uid = nameof(Uid);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string RacunUid = nameof(RacunUid);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string StroskovnikUid = nameof(StroskovnikUid);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string VplaciloUid = nameof(VplaciloUid);

        #endregion

        #region ToString

        public static implicit operator string(RacunPostavkaExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region UporabniskaNastavitev

    /// <summary>
    ///     Alias: UporabniskaNastavitev
    /// </summary>
    public sealed class UporabniskaNastavitevExtSchemeInfo
    {
        private const string name = "UporabniskaNastavitev";

        #region Columns

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Ident = nameof(Ident);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string UporabnikUid = nameof(UporabnikUid);

        /// <summary>
        ///     Type: ntext
        /// </summary>
        public readonly string VrednostString = nameof(VrednostString);

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string VrednostInt = nameof(VrednostInt);

        /// <summary>
        ///     Type: float
        /// </summary>
        public readonly string VrednostDouble = nameof(VrednostDouble);

        /// <summary>
        ///     Type: bit
        /// </summary>
        public readonly string VrednostBoolean = nameof(VrednostBoolean);

        #endregion

        #region ToString

        public static implicit operator string(UporabniskaNastavitevExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region BlagajnaPostavka

    /// <summary>
    ///     Alias: BlagajnaPostavka
    /// </summary>
    public sealed class BlagajnaPostavkaExtSchemeInfo
    {
        private const string name = "BlagajnaPostavka";

        #region Columns

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string Uid = nameof(Uid);

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string BlagajnaUid = nameof(BlagajnaUid);

        /// <summary>
        ///     Type: decimal
        /// </summary>
        public readonly string Znesek = nameof(Znesek);

        /// <summary>
        ///     Type: nvarchar
        /// </summary>
        public readonly string Opis = nameof(Opis);

        #endregion

        #region ToString

        public static implicit operator string(BlagajnaPostavkaExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region UporabnikPravica

    /// <summary>
    ///     Alias: UporabnikPravica
    /// </summary>
    public sealed class UporabnikPravicaExtSchemeInfo
    {
        private const string name = "UporabnikPravica";

        #region Columns

        /// <summary>
        ///     Type: uniqueidentifier
        /// </summary>
        public readonly string UporabnikUid = nameof(UporabnikUid);

        /// <summary>
        ///     Type: int
        /// </summary>
        public readonly string PravicaId = nameof(PravicaId);

        #endregion

        #region ToString

        public static implicit operator string(UporabnikPravicaExtSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    public static class ExtSchemeInfo
    {
        public const string ConnectionString = "legacy";

        #region Tables

        public static readonly IntVrstaRacunaExtSchemeInfo IntVrstaRacuna = new();

        public static readonly OpombaExtSchemeInfo Opomba = new();

        public static readonly IntVrstaSubjektaExtSchemeInfo IntVrstaSubjekta = new();

        public static readonly ShrambaDatotekExtSchemeInfo ShrambaDatotek = new();

        public static readonly IntPoljeEntiteteExtSchemeInfo IntPoljeEntitete = new();

        public static readonly TipDokumentaExtSchemeInfo TipDokumenta = new();

        public static readonly NacinPosiljanjaPosteExtSchemeInfo NacinPosiljanjaPoste = new();

        public static readonly OpraviloExtSchemeInfo Opravilo = new();

        public static readonly DatotekaExtSchemeInfo Datoteka = new();

        public static readonly DokumentDatotekaExtSchemeInfo DokumentDatoteka = new();

        public static readonly SpisSmsKontaktExtSchemeInfo SpisSmsKontakt = new();

        public static readonly SpisNasprotnaStrankaExtSchemeInfo SpisNasprotnaStranka = new();

        public static readonly RokSmsExtSchemeInfo RokSms = new();

        public static readonly KontaktnaOsebaExtSchemeInfo KontaktnaOseba = new();

        public static readonly ZgodovinaExtSchemeInfo Zgodovina = new();

        public static readonly ZgodovinaIzvornaEntitetaExtSchemeInfo ZgodovinaIzvornaEntiteta = new();

        public static readonly ZgodovinaDetajlExtSchemeInfo ZgodovinaDetajl = new();

        public static readonly UporabnikPriljubljenaEntitetaExtSchemeInfo UporabnikPriljubljenaEntiteta = new();

        public static readonly KontaktExtSchemeInfo Kontakt = new();

        public static readonly NacinPlacilaExtSchemeInfo NacinPlacila = new();

        public static readonly PlaciloExtSchemeInfo Placilo = new();

        public static readonly IntTipEntiteteExtSchemeInfo IntTipEntitete = new();

        public static readonly ObrestnaMeraExtSchemeInfo ObrestnaMera = new();

        public static readonly EntitetaExtSchemeInfo Entiteta = new();

        public static readonly EntityLockExtSchemeInfo EntityLock = new();

        public static readonly UporabnikExtSchemeInfo Uporabnik = new();

        public static readonly TipRokaExtSchemeInfo TipRoka = new();

        public static readonly NaslovExtSchemeInfo Naslov = new();

        public static readonly IntTipSubjektaExtSchemeInfo IntTipSubjekta = new();

        public static readonly ImenikExtSchemeInfo Imenik = new();

        public static readonly PrivzetaMapaExtSchemeInfo PrivzetaMapa = new();

        public static readonly MestoHranjenjaSpisaExtSchemeInfo MestoHranjenjaSpisa = new();

        public static readonly PrivzetaMapaDetailExtSchemeInfo PrivzetaMapaDetail = new();

        public static readonly SodnikExtSchemeInfo Sodnik = new();

        public static readonly KategorijaExtSchemeInfo Kategorija = new();

        public static readonly SpisZastopnikNasprotneStrankeExtSchemeInfo SpisZastopnikNasprotneStranke = new();

        public static readonly KlasifikacijskiZnakExtSchemeInfo KlasifikacijskiZnak = new();

        public static readonly GlobalnaNastavitevExtSchemeInfo GlobalnaNastavitev = new();

        public static readonly SpisExtSchemeInfo Spis = new();

        public static readonly RokExtSchemeInfo Rok = new();

        public static readonly RokOutlookSyncExtSchemeInfo RokOutlookSync = new();

        public static readonly SpisNasaStrankaExtSchemeInfo SpisNasaStranka = new();

        public static readonly VplaciloExtSchemeInfo Vplacilo = new();

        public static readonly ValutaExtSchemeInfo Valuta = new();

        public static readonly RacunExtSchemeInfo Racun = new();

        public static readonly SubjektExtSchemeInfo Subjekt = new();

        public static readonly IntSmerDokumentaExtSchemeInfo IntSmerDokumenta = new();

        public static readonly VrstaDokumentaExtSchemeInfo VrstaDokumenta = new();

        public static readonly DokumentExtSchemeInfo Dokument = new();

        public static readonly OdhodnaPostaExtSchemeInfo OdhodnaPosta = new();

        public static readonly MerskaEnotaExtSchemeInfo MerskaEnota = new();

        public static readonly CenikExtSchemeInfo Cenik = new();

        public static readonly CenikPostavkaExtSchemeInfo CenikPostavka = new();

        public static readonly CenaMerskeEnoteLookupExtSchemeInfo CenaMerskeEnoteLookup = new();

        public static readonly StroskovnikExtSchemeInfo Stroskovnik = new();

        public static readonly DelovnaPostajaExtSchemeInfo DelovnaPostaja = new();

        public static readonly RokUporabnikExtSchemeInfo RokUporabnik = new();

        public static readonly KljucnaBesedaExtSchemeInfo KljucnaBeseda = new();

        public static readonly SpisKljucnaBesedaExtSchemeInfo SpisKljucnaBeseda = new();

        public static readonly TipOpravilaExtSchemeInfo TipOpravila = new();

        public static readonly IntTipShrambeDatotekExtSchemeInfo IntTipShrambeDatotek = new();

        public static readonly IntTipKontaktaExtSchemeInfo IntTipKontakta = new();

        public static readonly IntBlagajnaNamenExtSchemeInfo IntBlagajnaNamen = new();

        public static readonly IntPravicaExtSchemeInfo IntPravica = new();

        public static readonly ReportDefinitionExtSchemeInfo ReportDefinition = new();

        public static readonly BlagajnaExtSchemeInfo Blagajna = new();

        public static readonly IntAkcijaZgodovineExtSchemeInfo IntAkcijaZgodovine = new();

        public static readonly SpisUporabnikExtSchemeInfo SpisUporabnik = new();

        public static readonly RacunPostavkaExtSchemeInfo RacunPostavka = new();

        public static readonly UporabniskaNastavitevExtSchemeInfo UporabniskaNastavitev = new();

        public static readonly BlagajnaPostavkaExtSchemeInfo BlagajnaPostavka = new();

        public static readonly UporabnikPravicaExtSchemeInfo UporabnikPravica = new();

        #endregion
    }
}