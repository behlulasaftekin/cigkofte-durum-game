using UnityEngine;

[CreateAssetMenu(fileName = "Yeni Malzeme", menuName = "Cigkofte Oyunu/Malzeme")]
public class MalzemeSO : ScriptableObject
{
    [Header("Kimlik Bilgileri")]
    public string sistemAdi;
    public string ekrandaGozukenAd;
    public Sprite ikon;

    [Header("Ekonomi")]
    public float fiyat = 0f;
    public float maliyet = 0f; 

    [Header("Görünüm")]
    public Sprite tabaktakiGorseli;
    public Color renkTonu = Color.white;

    [Header("Sesler")]
    public AudioClip eklemeSesi;
}
