
using UnityEngine;
using UnityEngine.UI;// sliderı kontrol etmek için      

public class Musteri : MonoBehaviour
{
    public Slider sabirSlider;
    public Image sabirFillImage; // Barın içindeki renkli kısmın resmi
    public float maxSabir; // Müşterinin toplam sabır süresi
    public float kalanSabir;
    [Header("Müşteri Bilgileri")]
    public SiparisVerisi siparisim;
    public MusteriProfiliSO profil;
                

    private bool siparisOnaylandiMi = false;


    public void Kurulum(MusteriProfiliSO atananProfil)
    {
        profil = atananProfil;
        maxSabir = profil.beklemeSuresi;
        kalanSabir = profil.beklemeSuresi;
    }

    public void KasayaSiraGeldi()
    {
        if(SiparisUretici.Sistem != null)
        {
            siparisim = SiparisUretici.Sistem.YeniSiparisUret(profil);

            SiparisKabulEdildi();
        }
    }

    void Update()
    {
        if (siparisOnaylandiMi)
        {
            kalanSabir -= Time.deltaTime;

            if(kalanSabir <= 0)
            {
                Debug.Log($"{profil.profilAdi} çok bekledi, sinirlenip gitti.");

                if (AdisyonUI.Sistem != null)
                {
                    AdisyonUI.Sistem.FisiTemizle();
                }
                if (OyunYoneticisi.Sistem != null)
                {
                    OyunYoneticisi.Sistem.MusteriGitti();
                }
                if (MusteriKuyrukYoneticisi.Sistem != null)
                {
                    MusteriKuyrukYoneticisi.Sistem.KuyruguIlerlet();
                }

                Destroy(gameObject);
            }
        }
        if (sabirSlider != null)
        {
            // Slider'ın değerini kalan sabır oranına eşitlemek için 
            sabirSlider.value = kalanSabir / maxSabir;
        }
        if (sabirFillImage != null)
        {
            //kırmızıdan yeşile geçiş
            sabirFillImage.color = Color.Lerp(Color.red, Color.green, kalanSabir / maxSabir);
        }
    }

    public void SiparisKabulEdildi()
    {
        siparisOnaylandiMi = true;
        Debug.Log($"Sıra {profil.profilAdi}'ne geldi. Siparişi: {siparisim.ToString()}");

        if(AdisyonUI.Sistem != null)
        {
            AdisyonUI.Sistem.FiseYaz(profil.profilAdi, siparisim.ToString());
        }
    }

    public void SiparisReddedildi()
    {
        Debug.Log($"{profil.profilAdi} adlı elemanı kovduk, dükkandan ayrılıyor.");
        if (AdisyonUI.Sistem != null)
        {
            AdisyonUI.Sistem.FisiTemizle();
        }
        if (OyunYoneticisi.Sistem != null)
        {
            OyunYoneticisi.Sistem.MusteriGitti();
        }
        if (MusteriKuyrukYoneticisi.Sistem != null)
        {
            MusteriKuyrukYoneticisi.Sistem.KuyruguIlerlet();
        }

        
        
        Destroy (gameObject);
    }

    public void SiparisTeslimAl(SiparisVerisi hazirlananSiparis)
    {
        SiparisDogrulayici.DogrulamaRaporu rapor = SiparisDogrulayici.Dogrula(siparisim, hazirlananSiparis);
        float odenecekTutar = 0f;

        if (rapor.sonuc == SiparisDogrulayici.DogrulamaSonucu.TamamenYanlis)
        {
            Debug.Log($"{profil.profilAdi}: 'Bu ne rezalet usta, ben bunu yemicem!' (Para ödemedi)");
        }

        else
        {
            odenecekTutar = siparisim.toplamFiyat;
            
            if(rapor.memnuniyetSkoru >= 0.7f)
            {
                if(Random.value <= profil.bahsisBirakmaIhtimali)
                {
                    float maxBahsis = siparisim.toplamFiyat * 0.2f;
                    float eklenecekBahsis = maxBahsis * rapor.memnuniyetSkoru * profil.bahsisCarpani;
                    odenecekTutar += eklenecekBahsis;

                    Debug.Log($"{profil.profilAdi} memnun kaldı ve {eklenecekBahsis:F2}TL bahşiş bıraktı.");
                    
                }

            
            }

            if(KasaYoneticisi.Sistem != null)
            {
                KasaYoneticisi.Sistem.SiparisGeliriEkle(odenecekTutar);
            }
        }
        if (AdisyonUI.Sistem != null)
        {
            AdisyonUI.Sistem.FisiTemizle();
        }
        if (OyunYoneticisi.Sistem != null)
        {
            OyunYoneticisi.Sistem.MusteriGitti();
        }

        if (MusteriKuyrukYoneticisi.Sistem != null)
        {
            MusteriKuyrukYoneticisi.Sistem.KuyruguIlerlet();
        }

        
        

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
}
