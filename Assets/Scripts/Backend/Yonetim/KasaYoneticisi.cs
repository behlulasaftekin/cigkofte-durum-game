    using UnityEngine;
    using TMPro;

    public class KasaYoneticisi : MonoBehaviour
    {
        public static KasaYoneticisi Sistem;

        [Header("Ekonomi Durumu")]
        public float kasaBakiyesi = 100f;
        public TextMeshProUGUI kasaText;
        
        [Header("Animasyonlar")]
        public Animator kasaAnimator;
        public TextMeshProUGUI gelirText;
            
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
            if (OyunYoneticisi.Sistem != null)
                OyunYoneticisi.Sistem.gunlukGider += eklenenMalzeme.maliyet;

            ArayuzuGuncelle();
            Debug.Log($"[-] Kasadan {eklenenMalzeme.maliyet} TL çıktı. Kullanılan: {eklenenMalzeme.ekrandaGozukenAd}. Güncel Kasa:{kasaBakiyesi}");

        }

        public void SiparisGeliriEkle(float kazanilanPara)
        {
            kasaBakiyesi += kazanilanPara;
            if (OyunYoneticisi.Sistem != null)
            {
                OyunYoneticisi.Sistem.gunlukGelir += kazanilanPara;
                OyunYoneticisi.Sistem.basariliSiparis++;
            }
          
            if (gelirText != null)
            {
            gelirText.text = "+" + kazanilanPara.ToString("F2") + " TL";
            }

            ArayuzuGuncelle();

            if (kasaAnimator != null)
            {
                kasaAnimator.SetTrigger("ParaGeldi");
            }

            Debug.Log($"[+] Kasaya {kazanilanPara} TL girdi. Güncel Kasa: {kasaBakiyesi}");
        }
        private void ArayuzuGuncelle()
        {
            if (kasaText != null)
            {
                kasaText.text = kasaBakiyesi.ToString("F2") + " TL";
            }
        }

}
