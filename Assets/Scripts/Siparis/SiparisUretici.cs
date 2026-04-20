using System.Collections.Generic;
using UnityEngine;

public class SiparisUretici : MonoBehaviour
{
  
    public static SiparisUretici Sistem {  get; private set; }


    [Header("Dükkandaki Ekstra Malzemeler")]

    [SerializeField] private List<MalzemeSO> mevcutMalzemeler;

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
        int eklenecekMalzemeSayisi = Random.Range(profil.minMalzeme, profil.maxMalzeme+1);

        bool dubleOlsunMu = Random.value < profil.dubleIstemeIhtimali;
        List<MalzemeSO> secilenMalzemeler = RastgeleMalzemeSec(eklenecekMalzemeSayisi);

        return new SiparisVerisi(dubleOlsunMu, secilenMalzemeler);
    }

    private List<MalzemeSO> RastgeleMalzemeSec(int miktar)
    {
        List<MalzemeSO> secilenler = new List<MalzemeSO>();

        List<MalzemeSO> kuraHavuzu = new List<MalzemeSO>(mevcutMalzemeler);

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
