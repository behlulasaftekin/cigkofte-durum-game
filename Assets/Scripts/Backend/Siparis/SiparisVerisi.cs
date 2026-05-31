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

    public string AdisyonFisiFormati()
    {
        List<string> isimler = new List<string>();
        foreach (var m in istenenMalzemeler)
        {
            if (m.ekrandaGozukenAd != "Lavaş" && m.ekrandaGozukenAd != "Çiğköfte")
                isimler.Add(m.ekrandaGozukenAd);
        }
        string malzemeIsimleri = string.Join(", ", isimler);
        string porsiyon = dubleMi ? "DUBLE" : "NORMAL";
        string lavasDurumu = ciftLavasMi ? "ÇİFT LAVAŞ" : "TEK LAVAŞ";

        if (isimler.Count > 0)
            return $"<b>PORSİYON:</b> {porsiyon}\n<b>LAVAŞ:</b> {lavasDurumu}\n<b>İÇİNDEKİLER:</b>\n{malzemeIsimleri}\n-----------------\n<b>TOPLAM: {toplamFiyat} TL</b>";
        else
            return $"<b>PORSİYON:</b> {porsiyon}\n<b>LAVAŞ:</b> {lavasDurumu}\n<b>İÇİNDEKİLER:</b>\n(Sade)\n-----------------\n<b>TOPLAM: {toplamFiyat} TL</b>";
    }
    public string MusteriKonusmaFormati()
    {
        List<string> isimler = new List<string>();
        foreach (var m in istenenMalzemeler)
        {
            if (m.ekrandaGozukenAd != "Lavaş" && m.ekrandaGozukenAd != "Çiğköfte")
                isimler.Add(m.ekrandaGozukenAd);
        }
        string malzemeIsimleri = string.Join(", ", isimler);
        string porsiyon = dubleMi ? "Duble" : "Normal";
        string lavasDurumu = ciftLavasMi ? "çift lavaş" : "tek lavaş";

        if (isimler.Count > 0)
            return $"Ustam bana bi {porsiyon} dürüm sar, {lavasDurumu} olsun. İçine de {malzemeIsimleri} koy sana zahmet.";
        else
            return $"Ustam bana bi {porsiyon} dürüm sar, {lavasDurumu} olsun. Yeşillik falan istemiyorum, sade olsun.";
    }
}
