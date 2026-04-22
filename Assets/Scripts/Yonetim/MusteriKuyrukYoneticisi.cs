using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class MusteriKuyrukYoneticisi : MonoBehaviour
{
    public static MusteriKuyrukYoneticisi Sistem { get; private set; }

    [Header("Kuyruk Ayarları")]
    public GameObject musteriPrefab;
    public Transform[] siraNoktalari;
    public float musteriGelmeSuresi = 5f;

    [Header("Müşteri Çeşitleri")]
    public List<MusteriProfiliSO> profilHavuzu;

    private List<Musteri> kuyruk = new List<Musteri>();
    private float zamanSayaci;

    private void Awake()
    {
        if (Sistem != null && Sistem != this)
        {
            Destroy(gameObject);
            return;
        }
        Sistem = this;

    }   

    private void Update()
    {
        if (kuyruk.Count < siraNoktalari.Length)
        {
            zamanSayaci += Time.deltaTime;

            if (zamanSayaci >= musteriGelmeSuresi)
            {
                MusteriCagir();
                zamanSayaci = 0f;
            }
        }
    }

    private void OnEnable()
    {
        HazirlikYoneticisi.OnSiparisTeslimEdildi += OndenSiparisTeslimEt;
    }

    private void OnDisable()
    {
        HazirlikYoneticisi.OnSiparisTeslimEdildi -= OndenSiparisTeslimEt;
    }

    private void OndenSiparisTeslimEt(SiparisVerisi mutfaktanGelenDurum)
    {
        if (kuyruk.Count > 0)
        {
            kuyruk[0].SiparisTeslimAl(mutfaktanGelenDurum);
        }

        else
            Debug.LogWarning("Dükkanda müşteri yok mutfaktan dürüm çıktı, Çöpe gitti.");
    }

    
    private void MusteriCagir()
    {
        if (profilHavuzu.Count == 0) return;
      

        int siraIndex = kuyruk.Count;
        Transform hedefNokta = siraNoktalari[siraIndex];

        GameObject yeniObj = Instantiate(musteriPrefab, hedefNokta.position, Quaternion.identity);
        Musteri yeniMusteri = yeniObj.GetComponent<Musteri>();

        int rasgteleProfilIndex = Random.Range(0, profilHavuzu.Count);
        yeniMusteri.Kurulum(profilHavuzu[rasgteleProfilIndex]);

        kuyruk.Add(yeniMusteri);
        Debug.Log($"Kapıdan {siraIndex + 1}. müşteri girdi. Tipi: {yeniMusteri.profil.profilAdi}");

        if(kuyruk.Count == 1)
        {
            yeniMusteri.KasayaSiraGeldi();
        }
    }
        
    public void KuyruguIlerlet()
    {
        if (kuyruk.Count > 0)
        {
            kuyruk.RemoveAt(0);

            for (int i = 0; i < kuyruk.Count; i++)
            {
                kuyruk[i].transform.position = siraNoktalari[i].position;

            }
            Debug.Log("Kuyruk bir kişi ilerledi.");
        }

        if(kuyruk.Count > 0)
        {
            kuyruk[0].KasayaSiraGeldi();
        }


    }
}
