using TMPro;
using System;
using System.Collections.Generic;
using UnityEngine;

public class HazirlikYoneticisi : MonoBehaviour
{
    [Header("Frontend - Malzeme Objeleri")]
    public GameObject LavasGorsel;
    public GameObject CigkofteGorsel;
    public GameObject MarulGorsel;
    public GameObject AcısosGorsel;
    public GameObject NareksisiGorsel;
    public GameObject DoritosGorsel;

    public TextMeshProUGUI adisyonText;
    public static HazirlikYoneticisi Sistem { get; private set; }
    //[Header("Yayın Yapılacak Olaylar (Events)")]
    public static event Action<MalzemeSO> OnMalzemeEklendi;
    public static event Action OnTezgahTemizlendi;
    public static event Action<SiparisVerisi> OnSiparisTeslimEdildi;

    [Header("Tezgahın Anlık Durumu")]
    private bool dubleMi = false;
    private List<MalzemeSO> tezgahtakiMalzeler = new List<MalzemeSO>();

    private void Awake()
    {
        if(Sistem != null && Sistem != this)
        {
            Destroy(gameObject);
            return;
        }

        Sistem = this;
    }

    public void MalzemeEkle(MalzemeSO malzeme)
    {
        tezgahtakiMalzeler.Add(malzeme);
        OnMalzemeEklendi?.Invoke(malzeme);
        Debug.Log($"Tezgaha Eklendi: {malzeme.ekrandaGozukenAd}");
        GuncelDurumuYazdir();
        if (malzeme.ekrandaGozukenAd == "Lavas") LavasGorsel.SetActive(true);
        else if (malzeme.ekrandaGozukenAd == "Çiğköfte") CigkofteGorsel.SetActive(true);
        else if (malzeme.ekrandaGozukenAd == "Marul") MarulGorsel.SetActive(true);
        else if (malzeme.ekrandaGozukenAd == "Acı Sos") AcısosGorsel.SetActive(true);
        else if (malzeme.ekrandaGozukenAd == "Nar Ekşisi") NareksisiGorsel.SetActive(true);
        else if (malzeme.ekrandaGozukenAd == "Doritos") DoritosGorsel.SetActive(true);

    }

    public void DubleSecimi(bool secildiMi)
    {
        dubleMi = secildiMi;
        Debug.Log(dubleMi ? "Dürüm artık DUBLE!" : "Dürüm NORMAL porsiyona döndü.");
        GuncelDurumuYazdir();

    }
    private void TezgahiSifirla()
    {
        tezgahtakiMalzeler.Clear();
        dubleMi = false;
        OnTezgahTemizlendi?.Invoke();
        adisyonText.text = "Tezgah Boş...";
        //fronted kısmı
        LavasGorsel.SetActive(false);
        CigkofteGorsel.SetActive(false);
        MarulGorsel.SetActive(false);
        AcısosGorsel.SetActive(false);
        NareksisiGorsel.SetActive(false);
        DoritosGorsel.SetActive(false);
    }

    public void CopeAt()
    {
        TezgahiSifirla();
        Debug.Log("Tezgah temizlendi, Zarardayız.");

    }

    public void SiparisiTeslimEt()
    {
        SiparisVerisi hazirlananDurum = new SiparisVerisi(dubleMi, tezgahtakiMalzeler);
        OnSiparisTeslimEdildi?.Invoke(hazirlananDurum);
        Debug.Log("Dürüm sarıldı ve teslim edildi: "+ hazirlananDurum.ToString());
        TezgahiSifirla();
    }
    public void AdisyonuYazdir(string siparisIcerigi)
    {
        if (adisyonText != null)
        {
            adisyonText.text = "Sipariş: " + siparisIcerigi;
        }
    }
    private void GuncelDurumuYazdir()
    {
        if (adisyonText == null) return; 

        string icerik = dubleMi ? "[DUBLE] " : "[NORMAL] "; 
        List<string> isimler = new List<string>();

        foreach (var m in tezgahtakiMalzeler)
        {
            isimler.Add(m.ekrandaGozukenAd); 
        }

        string malzemeListesi = string.Join(", ", isimler);
        adisyonText.text = "Hazırlanan:\n" + icerik + malzemeListesi; 
    }

}
