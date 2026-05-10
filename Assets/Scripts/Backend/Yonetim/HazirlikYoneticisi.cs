using TMPro;
using System;
using System.Collections.Generic;
using UnityEngine;

public class HazirlikYoneticisi : MonoBehaviour
{
    
    public TextMeshProUGUI adisyonText;
    public static HazirlikYoneticisi Sistem { get; private set; }
    //[Header("Yayın Yapılacak Olaylar (Events)")]
    public static event Action<MalzemeSO> OnMalzemeEklendi;
    public static event Action OnTezgahTemizlendi;
    public static event Action<SiparisVerisi> OnSiparisTeslimEdildi;

    [Header("Tezgahın Anlık Durumu")]
    
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

    }

   
    private void TezgahiSifirla()
    {
        tezgahtakiMalzeler.Clear();
        OnTezgahTemizlendi?.Invoke();
        adisyonText.text = "Tezgah Boş...";
    }

    public void CopeAt()
    {
        TezgahiSifirla();
        Debug.Log("Tezgah temizlendi, Zarardayız.");

    }

    public void SiparisiTeslimEt()
    {
        int cigkofteSayisi = 0;
        int lavasSayisi = 0;
        List<MalzemeSO> dogrulanacakMalzemeler = new List<MalzemeSO>();

        foreach (var m in tezgahtakiMalzeler)
        {
            if (m.ekrandaGozukenAd == "Çiğköfte")
            {
                cigkofteSayisi++;
                
                if (cigkofteSayisi == 1) dogrulanacakMalzemeler.Add(m);
            }
            else if (m.ekrandaGozukenAd == "Lavaş")
            {
                lavasSayisi++;
                
                if (lavasSayisi == 1) dogrulanacakMalzemeler.Add(m);
            }
            else
            {
                dogrulanacakMalzemeler.Add(m);
            }
        }

        bool otomatikDuble = cigkofteSayisi >= 2;
        bool otomatikCiftLavas = lavasSayisi >= 2;
    
        SiparisVerisi hazirlananDurum = new SiparisVerisi(otomatikDuble, otomatikCiftLavas, dogrulanacakMalzemeler);
        TezgahiSifirla();
        OnSiparisTeslimEdildi?.Invoke(hazirlananDurum);
        Debug.Log("Dürüm sarıldı ve teslim edildi: " + hazirlananDurum.ToString());

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

        int cigkofteSayisi = 0;
        int lavasSayisi = 0;
        List<string> isimler = new List<string>();

        foreach (var m in tezgahtakiMalzeler)
        {
            if (m.ekrandaGozukenAd == "Çiğköfte") cigkofteSayisi++;
            else if (m.ekrandaGozukenAd == "Lavaş") lavasSayisi++; 

            else isimler.Add(m.ekrandaGozukenAd);
        }

        string porsiyonYazisi = (cigkofteSayisi >= 2) ? "[DUBLE] " : "[NORMAL] ";
        string lavasYazisi = (lavasSayisi >= 2) ? "[ÇİFT LAVAŞ] " : "[TEK LAVAŞ] ";
        string malzemeListesi = string.Join(", ", isimler);

        adisyonText.text = "Hazırlanan:\n" + porsiyonYazisi + lavasYazisi + malzemeListesi;
    }

}
