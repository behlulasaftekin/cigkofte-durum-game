using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class OyunYoneticisi : MonoBehaviour
{
    public static OyunYoneticisi Sistem { get; private set; }
    [Header("Gün Ayarları")]
    public int mevcutGun = 1;
    public int maxGun = 3;
    public int gunlukMusteriHedefi = 5;
    private int bugunHizmetEdilen = 0;

    [Header("Günlük İstatistikler")]
    public float gunlukGelir = 0;
    public float gunlukGider = 0;
    public int basariliSiparis = 0;
    public int kacanMusteri = 0;

    [Header("İflas Ayarları")]
    public float iflasSiniri = -50f;

    [Header("UI - Gün Sonu Paneli")]
    public GameObject gunSonuPaneli;
    public TextMeshProUGUI gunBaslikText;
    public TextMeshProUGUI kazancText;
    public TextMeshProUGUI giderText;
    public TextMeshProUGUI mutluMusteriText;
    public TextMeshProUGUI kacanMusteriText;
    public TextMeshProUGUI netKarText;

    [Header("UI - Yeni Gün Animasyonu")]
    public GameObject yeniGunObje;
    public TextMeshProUGUI yeniGunText;

    private void Awake()
    {
        if (Sistem != null && Sistem != this) { Destroy(gameObject); return; }
        Sistem = this;
    }

    private void Start()
    {
        if(gunSonuPaneli != null) gunSonuPaneli.SetActive(false);
        YeniGunYazisiniGoster();
    }

    private void YeniGunYazisiniGoster()
    {
        if(yeniGunObje != null && yeniGunText != null)
        {
            yeniGunObje.SetActive(true);
            yeniGunText.text = $"{mevcutGun}. GÜN BAŞLIYOR!";

            Invoke("YeniGunYazisiniKapat", 3f);

        }
    }
    
    private void YeniGunYazisiniKapat()
    {
        if (yeniGunObje != null) yeniGunObje.SetActive(false);
    }

   
    private void Update()
    {
        IflasKontrolu();
    }



    private void IflasKontrolu()
    {
        if (KasaYoneticisi.Sistem != null && KasaYoneticisi.Sistem.kasaBakiyesi <= iflasSiniri)
        {
            Debug.LogError($"İFLAS ETTİN! Kasa {iflasSiniri} TL'yi gördü. Dükkan kapandı.");
            Time.timeScale = 0;
            this.enabled = false;
        }
    }

    public void MusteriGitti()
    {
        bugunHizmetEdilen++;
        if (bugunHizmetEdilen >= gunlukMusteriHedefi)
        {
            GunBitti();
        }
    }

    private void GunBitti()
    {
        if (MusteriKuyrukYoneticisi.Sistem != null)
            MusteriKuyrukYoneticisi.Sistem.dukkanAcikMi = false;
        MusteriKuyrukYoneticisi.Sistem.DukkaniBosalt();
        HazirlikYoneticisi.Sistem.TezgahiSifirla();
        if (gunSonuPaneli != null)
        {
            gunSonuPaneli.SetActive(true);
            if (gunBaslikText != null) gunBaslikText.text = $"{mevcutGun}. GÜN SONU";
            if (kazancText != null) kazancText.text = $"{gunlukGelir:F2}";
            if (giderText != null) giderText.text = $"{gunlukGider:F2}";
            if (mutluMusteriText != null) mutluMusteriText.text = $"{basariliSiparis}";
            if (kacanMusteriText != null) kacanMusteriText.text = $"{kacanMusteri}";

            float netKar = gunlukGelir - gunlukGider;
            if(netKar != null)
            {
                netKarText.text = $"{netKar:F2}";
                netKarText.color = netKar >= 0 ? Color.green : Color.red;

            }
        }

        if(mevcutGun  >= maxGun)
        {
            Debug.Log("<color=gold>🏆 TEBRİKLER! TÜM GÜNLERİ BAŞARIYLA ATLATIP ZENGİN BİR ESNAF OLDUN!</color>");
        }
    }

    public void YeniGuneBasla()
    {
        if (mevcutGun >= maxGun) return;

        mevcutGun++;
        bugunHizmetEdilen = 0;
        gunlukGelir = 0;
        gunlukGider = 0;
        basariliSiparis = 0;
        kacanMusteri = 0;

        if(gunSonuPaneli != null) gunSonuPaneli.SetActive(false);

        YeniGunYazisiniGoster();

        if (MusteriKuyrukYoneticisi.Sistem != null)
            MusteriKuyrukYoneticisi.Sistem.dukkanAcikMi = true;
       
    }

    public void MusteriKacti()
    {
        kacanMusteri++;
        MusteriGitti();
    }
}


