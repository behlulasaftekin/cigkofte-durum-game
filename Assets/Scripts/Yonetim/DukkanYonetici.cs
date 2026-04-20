using UnityEngine;
using TMPro;
public class DukkanYonetici : MonoBehaviour
{
    public GameObject[] musteriPrefablari;

    public Transform kapiNoktasi;

    public float musteriGelmeSuresi = 3.0f;

    private float zamanSayaci;
    private GameObject mevcutMusteri;
    void Start()
    {
        zamanSayaci = musteriGelmeSuresi;
    }

    void Update()
    {
        if(mevcutMusteri == null)
        {
            zamanSayaci -= Time.deltaTime;
            if (zamanSayaci <= 0)
            {
                YeniMusteriCagir();
                zamanSayaci = musteriGelmeSuresi;
            }
        }
        
        
    }
    public void YeniMusteriCagir()
    {
        int rastgeleIndex = Random.Range(0, musteriPrefablari.Length);
        mevcutMusteri = Instantiate(musteriPrefablari[rastgeleIndex], kapiNoktasi.position, Quaternion.identity);
    }

    [Header("Kasa Ayarları")]
    public TMP_Text kasaEkrani;
    public int toplamPara = 0;

    public void KasayaParaEkle(int miktar)
    {
        toplamPara += miktar;

        kasaEkrani.text = "Kasa: " + toplamPara + "TL";
        Debug.Log("Kasaya para girdi/çıktı: " + miktar + "TL. Güncel Kasa: "+  toplamPara + "TL");
    }
}
