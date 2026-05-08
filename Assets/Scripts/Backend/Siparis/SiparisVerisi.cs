using System.Collections.Generic;
public class SiparisVerisi 
{
    public bool dubleMi { get; private set; }
    public bool ciftLavasMi {  get; private set; }
    public List<MalzemeSO> istenenMalzemeler { get; private set; }
    public float toplamFiyat { get; private set; }

    public SiparisVerisi(bool dubleMi,bool ciftLavasMi, List<MalzemeSO> malzemeler)
    {
        this.dubleMi = dubleMi;
        this.ciftLavasMi = ciftLavasMi;
        this.istenenMalzemeler = new List<MalzemeSO>(malzemeler);
        this.toplamFiyat = FiyatHesapla();
    }

    private float FiyatHesapla()
    {
        float toplam = 60f;

        if (dubleMi)
            toplam += 30f;
        if (ciftLavasMi)
            toplam += 10;

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
        string lavasDurumu = ciftLavasMi ? "ÇİFT LAVAŞ" : "TEK LAVAŞ";
        return $"[{porsiyon}] [{lavasDurumu}] {malzemeIsimleri} - Toplam: {toplamFiyat} TL";
    }
}
