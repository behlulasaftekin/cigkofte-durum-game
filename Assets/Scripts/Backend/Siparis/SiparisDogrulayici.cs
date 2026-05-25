using System.Collections.Generic;
using UnityEngine;

public static class SiparisDogrulayici
{
  public enum DogrulamaSonucu
    {
        Kusursuz,
        FazlaMalzeme,
        EksikMalzeme,
        YanlisPorsiyon,
        TamamenYanlis   
    }

    public struct DogrulamaRaporu
    {
        public DogrulamaSonucu sonuc;
        public List<MalzemeSO> eksikMalzemeler;
        public List<MalzemeSO> fazlaMalzemeler;
        public bool dubleDogruMu;
        public bool ciftLavasDogruMu;
        public float memnuniyetSkoru;
    }


    public static DogrulamaRaporu Dogrula(SiparisVerisi istenen, SiparisVerisi hazirlanan)
    {
        DogrulamaRaporu rapor = new DogrulamaRaporu
        {
            eksikMalzemeler = new List<MalzemeSO>(),
            fazlaMalzemeler = new List<MalzemeSO>()

        };

        rapor.dubleDogruMu = (istenen.dubleMi == hazirlanan.dubleMi);
        rapor.ciftLavasDogruMu = (istenen.ciftLavasMi == hazirlanan.ciftLavasMi);

        List<MalzemeSO> istenenListe = new List<MalzemeSO>(istenen.istenenMalzemeler);
        List<MalzemeSO> hazirlananListe = new List<MalzemeSO>(hazirlanan.istenenMalzemeler);

        foreach(var malzeme in istenenListe)
        {
            if (!hazirlananListe.Remove(malzeme))
            {
                rapor.eksikMalzemeler.Add(malzeme);
            }
        }

        rapor.fazlaMalzemeler.AddRange(hazirlananListe);

        bool eksikYok = rapor.eksikMalzemeler.Count == 0;
        bool fazlaYok = rapor.fazlaMalzemeler.Count == 0;

        if (eksikYok && fazlaYok && rapor.dubleDogruMu && rapor.ciftLavasDogruMu)
            rapor.sonuc = DogrulamaSonucu.Kusursuz;
        else if ((!eksikYok || !fazlaYok) && (!rapor.dubleDogruMu || !rapor.ciftLavasDogruMu))
            rapor.sonuc = DogrulamaSonucu.TamamenYanlis;
        else if (!rapor.dubleDogruMu || !rapor.ciftLavasDogruMu)
            rapor.sonuc = DogrulamaSonucu.YanlisPorsiyon; 
        else if (!eksikYok)
            rapor.sonuc = DogrulamaSonucu.EksikMalzeme;
        else
            rapor.sonuc = DogrulamaSonucu.FazlaMalzeme;

        float skor = 1f;
        skor -= rapor.eksikMalzemeler.Count * 0.30f; 
        skor -= rapor.fazlaMalzemeler.Count * 0.20f; 
        if (!rapor.dubleDogruMu) skor -= 0.40f;      
        if (!rapor.ciftLavasDogruMu) skor -= 0.30f;  

        rapor.memnuniyetSkoru = Mathf.Clamp01(skor);
        if (rapor.memnuniyetSkoru < 0.4f)
        {
            rapor.sonuc = DogrulamaSonucu.TamamenYanlis;
        }   
        return rapor;
    }



}
