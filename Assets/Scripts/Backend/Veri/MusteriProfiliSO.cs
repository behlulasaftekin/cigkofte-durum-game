using UnityEngine;

[CreateAssetMenu(fileName = "Yeni Profil", menuName = "Cigkofte Oyunu/Musteri Profili")]
public class MusteriProfiliSO : ScriptableObject
{
    [Header("Kimlik")]
    public string profilAdi;
    public Sprite karakterGorseli;

    [Header("Davranış Ayarları")]
    public float beklemeSuresi = 30f;
    public float bahsisCarpani = 1f;
    public float bahsisBirakmaIhtimali = 0.5f;

    [Header("Sipariş Zorluğu")]
    public int minMalzeme = 1;
    public int maxMalzeme = 5;
    [Range(0f, 1f)]
    public float dubleIstemeIhtimali = 0.3f;
}
   

