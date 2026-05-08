using UnityEngine;
using TMPro;
public class AdisyonUI : MonoBehaviour
{
    public static AdisyonUI Sistem {  get; private set; }

    [Header("UI Elemanları")]
    public TextMeshProUGUI adisyonYazisi;

    private void Awake()
    {
        if (Sistem != null && Sistem != this)
        {
            Destroy(gameObject);
            return;
        }
        Sistem = this;
    }

    private void Start()
    {
        FisiTemizle();
    }

    public void FiseYaz(string musteriAdi, string siparisDetayi)
    {
        if(adisyonYazisi != null)
        {
            adisyonYazisi.text = $"MÜŞTERİ:\n{musteriAdi}\n\nİSTEK:\n{siparisDetayi}";
        }
    }

    public void FisiTemizle()
    {
        if(adisyonYazisi != null)
        {
            adisyonYazisi.text = "Sıradaki müşteri bekleniyor...";
        }
    }
}
