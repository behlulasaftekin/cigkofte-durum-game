using UnityEngine;
using TMPro;
public class KasaEkraniUI : MonoBehaviour
{
    [Header("UI Elemanları")]
    public TextMeshProUGUI kasaYazisi;

    [Header("Ayarlar")]
    public string birim = " TL";

    void Update()
    {
        if (KasaYoneticisi.Sistem != null && kasaYazisi != null)
        {
            float bakiye = KasaYoneticisi.Sistem.kasaBakiyesi;

            kasaYazisi.text = bakiye.ToString("F2") + birim;

        }
    }
}
