using UnityEngine;

public class SesYoneticisi : MonoBehaviour
{
    public static SesYoneticisi Sistem { get; private set; }

    [Header("Hoparlör")]
    public AudioSource hoparlor;

    [Header("Ses Kasetleri (Klipler)")]
    public AudioClip kasaSesi;
    public AudioClip malzemeKoymaSesi;
    public AudioClip butonTiklamaSesi;

    [Header("Özel Buton Sesleri")]
    public AudioClip teslimEtSesi;
    public AudioClip temizleSesi;

    private void Awake()
    {
        if (Sistem != null && Sistem != this) { Destroy(gameObject); return; }
        Sistem = this;
    }

    public void KasaSesiniCal()
    {
        if (hoparlor != null && kasaSesi != null) hoparlor.PlayOneShot(kasaSesi);
    }

    public void MalzemeSesiniCal()
    {
        if (hoparlor != null && malzemeKoymaSesi != null) hoparlor.PlayOneShot(malzemeKoymaSesi);
    }

    public void ButonSesiniCal()
    {
        if (hoparlor != null && butonTiklamaSesi != null) hoparlor.PlayOneShot(butonTiklamaSesi);
    }

    public void TeslimEtSesiniCal()
    {
        if (hoparlor != null && teslimEtSesi != null) hoparlor.PlayOneShot(teslimEtSesi);
    }

    public void TemizleSesiniCal()
    {
        if (hoparlor != null && temizleSesi != null) hoparlor.PlayOneShot(temizleSesi);
    }
}