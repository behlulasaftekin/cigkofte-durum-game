using UnityEngine;

public class HazirlikGorselYoneticisi : MonoBehaviour
{
    [Header("Tezgah Görselleri")]
    public GameObject LavasGorsel;
    public GameObject CigkofteGorsel;
    public GameObject MarulGorsel;
    public GameObject AcısosGorsel;
    public GameObject NareksisiGorsel;
    public GameObject LımonsosGorsel;
    public GameObject DoritosGorsel;
    public GameObject MisirGorsel;
    public GameObject MorLahanaGorsel;

    private void OnEnable()
    {
        // Backend'i dinlemeye başlıyoruz (Abone oluyoruz)
        HazirlikYoneticisi.OnMalzemeEklendi += GorseliAktifEt;
        HazirlikYoneticisi.OnTezgahTemizlendi += TumGorselleriKapat;
    }

    private void OnDisable()
    {
        // Abonelikten çıkıyoruz
        HazirlikYoneticisi.OnMalzemeEklendi -= GorseliAktifEt;
        HazirlikYoneticisi.OnTezgahTemizlendi -= TumGorselleriKapat;
    }

    private void GorseliAktifEt(MalzemeSO malzeme)
    {
        string ad = malzeme.ekrandaGozukenAd;

        if (ad == "Lavaş") LavasGorsel.SetActive(true);
        else if (ad == "Çiğköfte") CigkofteGorsel.SetActive(true);
        else if (ad == "Marul") MarulGorsel.SetActive(true);
        else if (ad == "Acı Sos") AcısosGorsel.SetActive(true);
        else if (ad == "Nar Ekşisi") NareksisiGorsel.SetActive(true);
        else if (ad == "Doritos") DoritosGorsel.SetActive(true);
        else if (ad == "Mısır") MisirGorsel.SetActive(true);
        else if (ad == "Mor Lahana") MorLahanaGorsel.SetActive(true);
        else if (ad == "Limon Sosu") LımonsosGorsel.SetActive(true);
    }

    private void TumGorselleriKapat()
    {
        LavasGorsel.SetActive(false);
        CigkofteGorsel.SetActive(false);
        MarulGorsel.SetActive(false);
        AcısosGorsel.SetActive(false);
        NareksisiGorsel.SetActive(false);
        DoritosGorsel.SetActive(false);
        MisirGorsel.SetActive(false);
        MorLahanaGorsel.SetActive(false);
        LımonsosGorsel.SetActive(false);
    }
}
