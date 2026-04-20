using System;
using System.Collections.Generic;
using UnityEngine;

public class HazirlikYoneticisi : MonoBehaviour
{
    public static HazirlikYoneticisi Sistem { get; private set; }
    //[Header("Yayın Yapılacak Olaylar (Events)")]
    public static event Action<MalzemeSO> OnMalzemeEklendi;
    public static event Action OnTezgahTemizlendi;
    public static event Action<SiparisVerisi> OnSiparisTeslimEdildi;

    [Header("Tezgahın Anlık Durumu")]
    private bool dubleMi = false;
    private List<MalzemeSO> tezgahtakiMalzeler = new List<MalzemeSO>();

    private void Awake()
    {
        if(Sistem != null && Sistem != this)
        {
            Destroy(gameObject);
            return;
        }

        Sistem = this;
    }

    public void MalzemeEkle(MalzemeSO malzeme)
    {
        tezgahtakiMalzeler.Add(malzeme);
        OnMalzemeEklendi?.Invoke(malzeme);
        Debug.Log($"Tezgaha Eklendi: {malzeme.ekrandaGozukenAd}");

    }

    public void DubleSecimi(bool secildiMi)
    {
        dubleMi = secildiMi;
        Debug.Log(dubleMi ? "Dürüm artık DUBLE!" : "Dürüm NORMAL porsiyona döndü.");

    }
    private void TezgahiSifirla()
    {
        tezgahtakiMalzeler.Clear();
        dubleMi = false;
        OnTezgahTemizlendi?.Invoke();
    }

    public void CopeAt()
    {
        TezgahiSifirla();
        Debug.Log("Tezgah temizlendi, Zarardayız.");

    }

    public void SiparisiTeslimEt()
    {
        SiparisVerisi hazirlananDurum = new SiparisVerisi(dubleMi, tezgahtakiMalzeler);
        OnSiparisTeslimEdildi?.Invoke(hazirlananDurum);
        Debug.Log("Dürüm sarıldı ve teslim edildi: "+ hazirlananDurum.ToString());
        TezgahiSifirla();
    }

}
