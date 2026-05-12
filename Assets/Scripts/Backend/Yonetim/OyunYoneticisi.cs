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

    private void Awake()
    {
        if (Sistem != null && Sistem != this) { Destroy(gameObject); return; }
        Sistem = this;
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
        string rapor = $"\n--- {mevcutGun}. GÜN SONU RAPORU ---\n" +
                       $"💰 Toplam Kazanç: {gunlukGelir:F2} TL\n" +
                       $"💸 Malzeme Gideri: {gunlukGider:F2} TL\n" +
                       $"✅ Mutlu Müşteri: {basariliSiparis}\n" +
                       $"❌ Kaçan Müşteri: {kacanMusteri}\n" +
                       $"📈 Net Kar: {(gunlukGelir - gunlukGider):F2} TL\n" +
                       $"---------------------------";

        Debug.Log(rapor);

        if (mevcutGun >= maxGun)
        {
            Debug.Log("<color=gold>🏆 TEBRİKLER! TÜM GÜNLERİ BAŞARIYLA ATLATIP ZENGİN BİR ESNAF OLDUN!</color>");
            Time.timeScale = 0;
        }
        else
        {
            mevcutGun++;
       
            bugunHizmetEdilen = 0;
            gunlukGelir = 0;
            gunlukGider = 0;
            basariliSiparis = 0;
            kacanMusteri = 0;
            Debug.Log($"<color=green>--- {mevcutGun}. GÜN BAŞLIYOR! ---</color>");
        }
    }
}


