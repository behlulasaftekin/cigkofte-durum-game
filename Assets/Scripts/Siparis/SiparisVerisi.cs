using System.Collections.Generic;
public class SiparisVerisi 
{
    public bool dubleMi { get; private set; }
    public List<MalzemeSO> istenenMalzemeler { get; private set; }
    public float toplamFiyat { get; private set; }

    public SiparisVerisi(bool dubleMi, List<MalzemeSO> malzemeler)
    {
        this.dubleMi = dubleMi;
        this.istenenMalzemeler = new List<MalzemeSO>(malzemeler);
        this.toplamFiyat = FiyatHesapla();
    }

    private float FiyatHesapla()
    {
        float toplam = 60f;

        if (dubleMi)
        {
            toplam += 30f;
        }

        foreach(var malzeme in istenenMalzemeler)
        {
            toplam += malzeme.fiyat;
        }
        return toplam;
    }

    public override string ToString()
    {
        List<string> isimler = new List<string>();
        foreach(var m in istenenMalzemeler)
        {
            isimler.Add(m.ekrandaGozukenAd);
        }

        string malzemeIsimleri = string.Join(", ", isimler);
        string porsiyon = dubleMi ? "DUBLE" : "NORMAL";
        return $"[{porsiyon}] {malzemeIsimleri} - Toplam: {toplamFiyat} TL";
    }
}
