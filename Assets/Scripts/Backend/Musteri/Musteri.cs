using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Musteri : MonoBehaviour
{
    public Slider sabirSlider;
    public Image sabirFillImage;
    public float maxSabir;
    public float kalanSabir;
    private bool terkEdiyor = false;

    [Header("Konuşma Balonu")]
    public GameObject konusmaBalonuObjesi;
    public TextMeshProUGUI konusmaBalonuText;

    [Header("Müşteri Bilgileri")]
    public SiparisVerisi siparisim;
    public MusteriProfiliSO profil;

    [Header("Görsel Bağlantıları")]
    public Image karakterGoruntusu;

    private bool siparisOnaylandiMi = false;

    public void Kurulum(MusteriProfiliSO atananProfil)
    {
        profil = atananProfil;
        maxSabir = profil.beklemeSuresi;
        kalanSabir = profil.beklemeSuresi;

        if (karakterGoruntusu != null && profil.musteriResmi != null)
        {
            karakterGoruntusu.sprite = profil.musteriResmi;
            karakterGoruntusu.color = Color.white;
        }
    }

    public void KasayaSiraGeldi()
    {
        if (SiparisUretici.Sistem != null)
        {
            siparisim = SiparisUretici.Sistem.YeniSiparisUret(profil);
            SiparisKabulEdildi();
        }
    }

    
    void Update()
    {
        if (siparisOnaylandiMi && !terkEdiyor)
        {
            kalanSabir -= Time.deltaTime;

            if (kalanSabir <= 0)
            {
                StartCoroutine(SinirliCikisSureci());
            }
        }

        if (sabirSlider != null)
        {
            sabirSlider.value = kalanSabir / maxSabir;
        }
        if (sabirFillImage != null)
        {
            sabirFillImage.color = Color.Lerp(Color.red, Color.green, kalanSabir / maxSabir);
        }
    }

    public void SiparisKabulEdildi()
    {
        siparisOnaylandiMi = true;
        Debug.Log($"Sıra {profil.profilAdi}'ne geldi.");

        if (konusmaBalonuObjesi != null && konusmaBalonuText != null)
        {
            konusmaBalonuObjesi.SetActive(true);
            konusmaBalonuText.text = siparisim.MusteriKonusmaFormati();
        }

        if (AdisyonUI.Sistem != null)
        {
            AdisyonUI.Sistem.FiseYaz(profil.profilAdi, siparisim.AdisyonFisiFormati());
        }
    }

    public void SiparisReddedildi()
    {
        Debug.Log($"{profil.profilAdi} adlı elemanı kovduk, dükkandan ayrılıyor.");
        if (AdisyonUI.Sistem != null) AdisyonUI.Sistem.FisiTemizle();
        if (OyunYoneticisi.Sistem != null)
        {
            
            OyunYoneticisi.Sistem.MusteriKacti();
        }
        if (MusteriKuyrukYoneticisi.Sistem != null) MusteriKuyrukYoneticisi.Sistem.KuyruguIlerlet();

        Destroy(gameObject);
    }

    public void SiparisTeslimAl(SiparisVerisi hazirlananSiparis)
    {
        SiparisDogrulayici.DogrulamaRaporu rapor = SiparisDogrulayici.Dogrula(siparisim, hazirlananSiparis);
        float odenecekTutar = 0f;

        if (rapor.sonuc == SiparisDogrulayici.DogrulamaSonucu.TamamenYanlis)
        {
            Debug.Log($"{profil.profilAdi}: 'Bu ne rezalet usta, ben bunu yemicem!' (Para ödemedi)");
        }
        else if (rapor.sonuc == SiparisDogrulayici.DogrulamaSonucu.Kusursuz)
        {
            odenecekTutar = siparisim.toplamFiyat;

            if (Random.value <= profil.bahsisBirakmaIhtimali)
            {
                float maxBahsis = siparisim.toplamFiyat * 0.2f;
                float eklenecekBahsis = maxBahsis * rapor.memnuniyetSkoru * profil.bahsisCarpani;
                odenecekTutar += eklenecekBahsis;

                Debug.Log($"{profil.profilAdi} kusursuz siparişe bayıldı ve {eklenecekBahsis:F2} TL bahşiş ateşledi.");
            }
        }
        else
        {
            odenecekTutar = siparisim.toplamFiyat * Random.Range(0.4f, 0.6f);
        }

        if (odenecekTutar > 0f && KasaYoneticisi.Sistem != null)
        {
            KasaYoneticisi.Sistem.SiparisGeliriEkle(odenecekTutar);
        }

        if (AdisyonUI.Sistem != null) AdisyonUI.Sistem.FisiTemizle();

        
        if (OyunYoneticisi.Sistem != null)
        {
            if (rapor.sonuc == SiparisDogrulayici.DogrulamaSonucu.TamamenYanlis)
            {
                OyunYoneticisi.Sistem.MusteriKacti(); 
            }
            else
            {
                OyunYoneticisi.Sistem.SiparisBasarili(); 
            }
        }
       

        if (MusteriKuyrukYoneticisi.Sistem != null) MusteriKuyrukYoneticisi.Sistem.KuyruguIlerlet();

        Destroy(gameObject);
    }

    public float SabirOraniVer()
    {
        if (profil == null || profil.beklemeSuresi == 0) return 0f;
        return Mathf.Clamp01(kalanSabir / profil.beklemeSuresi);
    }

    public int EkrandaGosterilecekSabirDegeri()
    {
        float oran = SabirOraniVer();
        float hesaplananDeger = Mathf.Lerp(20f, 100f, oran);
        return Mathf.RoundToInt(hesaplananDeger);
    }

    
    private System.Collections.IEnumerator SinirliCikisSureci()
    {
        terkEdiyor = true;

        if (konusmaBalonuText != null)
        {
            konusmaBalonuText.text = "<color=red><b>ÇOK YAVAŞSIN!</b>\nGidiyorum!</color>";
        }

        if (karakterGoruntusu != null)
        {
            karakterGoruntusu.color = Color.red;
        }

        yield return new WaitForSeconds(1.5f);

        if (AdisyonUI.Sistem != null) AdisyonUI.Sistem.FisiTemizle();
        if (OyunYoneticisi.Sistem != null)
        {
            OyunYoneticisi.Sistem.MusteriKacti();
        }
        if (MusteriKuyrukYoneticisi.Sistem != null) MusteriKuyrukYoneticisi.Sistem.KuyruguIlerlet();

        Destroy(gameObject);
    }
}