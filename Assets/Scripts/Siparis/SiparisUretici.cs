using System.Collections.Generic;
using UnityEngine;

public class SiparisUretici : MonoBehaviour
{
  
    public static SiparisUretici Sistem {  get; private set; }


    [Header("Zorunlu Temel Malzemeler")]
    [SerializeField] private MalzemeSO zorunluLavas;
    [SerializeField] private MalzemeSO zorunluCigkofte;

    [Header("Dükkandaki Ekstra Malzemeler (Havuz)")]
    [SerializeField] private List<MalzemeSO> mevcutEkstraMalzemeler;

    private void Awake()
    {
        if(Sistem != null  && Sistem != this)
        {
            Destroy(gameObject);
            return;
        }
        Sistem = this;
    }


    public SiparisVerisi YeniSiparisUret(MusteriProfiliSO profil)
    {
        
        int eklenecekEkstraSayisi = Random.Range(profil.minMalzeme, profil.maxMalzeme + 1);

        bool dubleOlsunMu = Random.value < profil.dubleIstemeIhtimali;
        bool ciftLavasIstiyorMu = Random.value <= 0.3f;

        
        List<MalzemeSO> secilenMalzemeler = new List<MalzemeSO>();
        if (zorunluLavas != null) secilenMalzemeler.Add(zorunluLavas);
        if (zorunluCigkofte != null) secilenMalzemeler.Add(zorunluCigkofte);

        
        List<MalzemeSO> ekstralar = RastgeleMalzemeSec(eklenecekEkstraSayisi);
        secilenMalzemeler.AddRange(ekstralar);

        return new SiparisVerisi(dubleOlsunMu, ciftLavasIstiyorMu, secilenMalzemeler);
    }

    private List<MalzemeSO> RastgeleMalzemeSec(int miktar)
    {
        List<MalzemeSO> secilenler = new List<MalzemeSO>();
        List<MalzemeSO> kuraHavuzu = new List<MalzemeSO>(mevcutEkstraMalzemeler);

        for (int i = 0; i < miktar; i++)
        {
            if (kuraHavuzu.Count == 0) break;

            int rastgeleIndex = Random.Range(0, kuraHavuzu.Count);
            secilenler.Add(kuraHavuzu[rastgeleIndex]);
            kuraHavuzu.RemoveAt(rastgeleIndex);
        }

        return secilenler;
    }
}
