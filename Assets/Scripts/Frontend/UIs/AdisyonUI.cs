using UnityEngine;
using TMPro;

public class AdisyonUI : MonoBehaviour
{
    public static AdisyonUI Sistem { get; private set; }

    [Header("UI Elemanları")]
    public TextMeshProUGUI adisyonYazisi;

    [Header("Görsel Ayarlar")]
    [Tooltip("Parşömen kağıdına yakışacak koyu kahve tonu (Hex Kodu)")]
    public string yaziRenkKodu = "#3E2723";

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
        if (adisyonYazisi != null)
        {
            //
            adisyonYazisi.text =
                $"<color={yaziRenkKodu}>" +
                $"<b>MÜŞTERİ:</b>\n" +
                $"{musteriAdi}\n" +
                $"-----------------------------------\n" +
                $"<b>SİPARİŞ:</b>\n" +
                $"{siparisDetayi}" +
                $"</color>";
        }
    }

    public void FisiTemizle()
    {
        if (adisyonYazisi != null)
        {
            adisyonYazisi.text =
                $"<color={yaziRenkKodu}>" +
                $"<align=center>" +
                $"<alpha=#77><i>Sıradaki müşteri\nbekleniyor...</i></align>" +
                $"</color>";
        }
    }
}
