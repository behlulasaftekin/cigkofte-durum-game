using UnityEngine;

public class OyunYoneticisi : MonoBehaviour
{
    public static OyunYoneticisi Sistem { get; private set; }
    [Header("Gün Ayarları")]
    public int mevcutGun = 1;
    public int maxGun = 3;
    public int gunlukMusteriHedefi = 5;
    private int bugunHizmetEdilen = 0;

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
        Debug.Log($"--- {mevcutGun}. GÜN BİTTİ! Kasa: {KasaYoneticisi.Sistem.kasaBakiyesi} TL ---");
        if(mevcutGun >= maxGun)
        {
            Debug.Log("🏆 TEBRİKLER! 3 GÜNÜ BAŞARIYLA ATLATIP ZENGİN BİR ESNAF OLDUN!");
            Time.timeScale = 0;
        }

        else
        {
            mevcutGun++;
            bugunHizmetEdilen = 0;
            Debug.Log($"--- {mevcutGun}. GÜN BAŞLIYOR! ---");
        }
    }
}


