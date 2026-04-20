
using UnityEngine;

public class Musteri : MonoBehaviour
{
    [Header("Müşteri Bİlgileri")]
    public SiparisVerisi siparisim;
    public MusteriProfiliSO profil;

    private bool siparisOnaylandiMi = false;
    private float kalanSabir;

    public void Kurulum(MusteriProfiliSO atananProfil)
    {
        profil = atananProfil;
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

                if(MusteriKuyrukYoneticisi.Sistem != null)
                {
                    MusteriKuyrukYoneticisi.Sistem.KuyruguIlerlet();
                }

                Destroy(gameObject);
            }
        }
    }

    public void SiparisKabulEdildi()
    {
        siparisOnaylandiMi = true;
        Debug.Log($"Sıra {profil.profilAdi}'ne geldi. Siparişi: {siparisim.ToString()}");

    }

    public void SiparisReddedildi()
    {
        Debug.Log($"{profil.profilAdi} adlı elemanı kovduk, dükkandan ayrılıyor.");

        if(MusteriKuyrukYoneticisi.Sistem != null)
        {
            MusteriKuyrukYoneticisi.Sistem.KuyruguIlerlet();
        }
        Destroy (gameObject);
    }
}
