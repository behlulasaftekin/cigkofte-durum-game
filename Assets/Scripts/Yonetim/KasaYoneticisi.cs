    using UnityEngine;

    public class KasaYoneticisi : MonoBehaviour
    {
        public static KasaYoneticisi Sistem;

        [Header("Ekonomi Durumu")]
        public float kasaBakiyesi = 100f;
    
        private void Awake()
        {
            if(Sistem != null && Sistem != this) { Destroy(gameObject); return; }
            Sistem = this;
        }
    
        private void OnEnable()
        {
            HazirlikYoneticisi.OnMalzemeEklendi += MalzemeMaliyetiDus;
        }

        private void OnDisable()
        {
            HazirlikYoneticisi.OnMalzemeEklendi -= MalzemeMaliyetiDus;
        }

        private void MalzemeMaliyetiDus(MalzemeSO eklenenMalzeme)
        {
            kasaBakiyesi -= eklenenMalzeme.maliyet;
            Debug.Log($"[-] Kasadan {eklenenMalzeme.maliyet} TL çıktı. Kullanılan: {eklenenMalzeme.ekrandaGozukenAd}. Güncel Kasa:{kasaBakiyesi}");

        }

        public void SiparisGeliriEkle(float kazanilanPara)
        {
            kasaBakiyesi += kazanilanPara;
            Debug.Log($"[+] Kasaya {kazanilanPara} TL girdi. Güncel Kasa: {kasaBakiyesi}");
        }



    }
