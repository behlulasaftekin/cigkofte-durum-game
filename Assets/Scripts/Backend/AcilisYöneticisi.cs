using UnityEngine;
using System.Collections;

public class AcilisYoneticisi : MonoBehaviour
{
    [Header("UI Panelleri")]
    public GameObject girisPaneli;
    public GameObject gunBasladiPaneli;

    void Start()
    {
        if (girisPaneli != null) girisPaneli.SetActive(true);
        if (gunBasladiPaneli != null) gunBasladiPaneli.SetActive(false);

        Time.timeScale = 0f;
    }

    public void OyunuBaslat()
    {
        if (girisPaneli != null) girisPaneli.SetActive(false);

        StartCoroutine(GunYazisiSureci());
    }

    private IEnumerator GunYazisiSureci()
    {
        if (gunBasladiPaneli != null) gunBasladiPaneli.SetActive(true);

        if (SesYoneticisi.Sistem != null) SesYoneticisi.Sistem.ButonSesiniCal();

        
        yield return new WaitForSecondsRealtime(2f);

        if (gunBasladiPaneli != null) gunBasladiPaneli.SetActive(false);

        Time.timeScale = 1f;

        Debug.Log("[Oyun] 1. Gün Resmi Olarak Başladı, zaman akıyor!");
    }
}