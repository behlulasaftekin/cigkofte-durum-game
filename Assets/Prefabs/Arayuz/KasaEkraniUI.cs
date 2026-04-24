using UnityEngine;
using TMPro;
public class KasaEkraniUI : MonoBehaviour
{
    [Header("UI Elemanları")]
    public TextMeshProUGUI kasaYazisi;

    [Header("Ayarlar")]
    public string onEk = "Kasa: ";
    public string birim = " TL";

    void update()
    {
        if (KasaYoneticisi.Sistem != null && kasaYazisi != null)
        {
            float bakiye = KasaYoneticisi.Sistem.kasaBakiyesi;

            kasaYazisi.text = onEk + bakiye.ToString("F2") + birim;

        }
    }
}
